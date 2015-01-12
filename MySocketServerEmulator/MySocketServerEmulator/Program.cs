using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MySocketServerEmulator
{
    public class Program
    {
        #region Variables
        // Mac Key Variables
        private static byte[] macKey = new byte[64];
        private static string macKeyString = String.Empty;

        // SAF Variables
        private static bool m_bSAF = false;
        private static SAFRecord m_safRecord;

        // Report Vairables
        private static float m_fTransAmount;

        //Last Transaction
        private static string m_strCommand;

        #endregion

        static void Main(string[] args)
        {
            
            int counter = 0;
            IPAddress ipAddress = IPAddress.Parse("199.5.83.138");

            string strResponse = String.Empty;

            TcpListener serverSocket = new TcpListener(ipAddress, 5015);
            TcpClient clientSocket = default(TcpClient);

            try
            {
                serverSocket.Start();
                Console.WriteLine(" >> Server Started");
                clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine(" >> Accept connection from client");

                NetworkStream networkStream = clientSocket.GetStream();
                XDocument response = ReadFromStream(networkStream);
                Console.WriteLine(" >> Request Message from client - ");
                Console.WriteLine(response.ToString());
                Console.WriteLine();

                RegisterPOSRequest request1 = new RegisterPOSRequest(response);
                RegisterPOSResponse response1 = new RegisterPOSResponse();

                string expectedCode = request1.EntryCode.ToString();

                while (expectedCode.Length < 4)
                {
                    expectedCode = "0" + expectedCode;
                }

                EnterEntryCode(response1, expectedCode);

                if (response1.ResultCode == -1)
                {
                    response1.MacLabel = "12345";
                    response1.ResponseText = "Registered MAC_LABEL";

                    // Generate Mac Key and encrypt it.
                    try
                    {
                        Aes myAes = Aes.Create();
                        myAes.KeySize = 128;
                        myAes.GenerateKey();
                        myAes.GenerateIV();
                        using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                        {

                            RSAParameters RSAKeyInfo = new RSAParameters();

                            byte[] publicKey = Convert.FromBase64String(request1.Key);

                            RSAKeyInfo.Modulus = publicKey.Skip(33).Take(256).ToArray();
                            RSAKeyInfo.Exponent = publicKey.Skip(291).Take(3).ToArray();

                            //Import the RSA Key information. This only needs 
                            //toinclude the public key information.
                            RSA.ImportParameters(RSAKeyInfo);

                            //Encrypt the passed byte array and specify OAEP padding.   
                            //OAEP padding is only available on Microsoft Windows XP or 
                            //later.  
                            byte[] encryptedData = RSA.Encrypt(myAes.Key, false);
                            response1.MacKey = encryptedData;
                            macKey = myAes.Key;
                            macKeyString = Convert.ToBase64String(myAes.Key);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }

                XDocument responseToSend = response1.GenerateResponseMessage();

                Console.WriteLine(" >> Response Message to send - ");
                Console.WriteLine(responseToSend.ToString());
                Console.WriteLine();

                string mac = UpdateCounter(ref counter, macKey);

                WriteToStream(responseToSend, networkStream);
                response = ReadFromStream(networkStream);
                strResponse = response.ToString();
                if (!String.IsNullOrEmpty(strResponse))
                {
                    Console.WriteLine(" >> Request Message from client - ");
                    Console.WriteLine(strResponse);
                    Console.WriteLine();

                    // Validate the MAC from the Point device.
                    TestMACRequest request2 = new TestMACRequest(response);
                    TestMACResponse response2 = new TestMACResponse();

                    if (request2.Mac != mac)
                    {
                        response2.Result = "FIELD_ERROR";
                        response2.ResultCode = 59040;
                        response2.ResponseText = "MAC mismatch (59040)";
                        response2.TerminationStatus = "FAILURE";
                    }
                    else
                    {
                        response2.Result = "OK";
                        response2.ResultCode = -1;
                        response2.ResponseText = "Match";
                        response2.TerminationStatus = "SUCCESS";
                    }

                    responseToSend = response2.GenerateResponseMessage();

                    Console.WriteLine(" >> Response Message to send - ");
                    Console.WriteLine(responseToSend.ToString());
                    Console.WriteLine();

                    WriteToStream(responseToSend, networkStream);
                    response = ReadFromStream(networkStream);
                    strResponse = response.ToString();
                    if (!String.IsNullOrEmpty(strResponse))
                    {
                        Console.WriteLine(" >> Request Message from client - ");
                        Console.WriteLine(strResponse);
                        Console.WriteLine();

                        StartSessionRequest request3 = new StartSessionRequest(response);
                        StartSessionResponse response3 = new StartSessionResponse();

                        Console.WriteLine("Press Enter to Start the session.");
                        Console.ReadLine();

                        response3.Result = "OK";
                        response3.ResultCode = -1;
                        response3.ResponseText = "Session Started";
                        response3.TerminationStatus = "SUCCESS";

                        responseToSend = response3.GenerateResponseMessage();

                        Console.WriteLine(" >> Response Message to send - ");
                        Console.WriteLine(responseToSend.ToString());
                        Console.WriteLine();

                        WriteToStream(responseToSend, networkStream);

                        response = ReadFromStream(networkStream);
                        RequestBase tempRequest = new RequestBase(response);
                        if (tempRequest.Command == "ADD")
                        {
                            RetailSimulation(networkStream, response);
                            response = ReadFromStream(networkStream);
                            strResponse = response.ToString();
                        }
                        else if (tempRequest.Command == "ACTIVATE")
                        {
                            GiftCardSimulaton(networkStream, response);
                            response = ReadFromStream(networkStream);
                            strResponse = response.ToString();
                        }
                        else
                        {
                            strResponse = response.ToString();
                        }

                        Console.WriteLine(" >> Request Message from client - ");
                        Console.WriteLine(strResponse);
                        Console.WriteLine();

                        // End a session before doing other simulations
                        FinishSessionRequest request4 = new FinishSessionRequest(response);
                        FinishSessionResponse response4 = new FinishSessionResponse();

                        Console.WriteLine("Press Enter to End the session.");
                        Console.ReadLine();

                        response4.Result = "OK";
                        response4.ResultCode = -1;
                        response4.ResponseText = "Session finished";
                        response4.TerminationStatus = "SUCCESS";

                        responseToSend = response4.GenerateResponseMessage();

                        Console.WriteLine(" >> Response Message to send - ");
                        Console.WriteLine(responseToSend.ToString());
                        Console.WriteLine();

                        WriteToStream(responseToSend, networkStream);
                        response = ReadFromStream(networkStream);

                        // Run the Store and Forward Simulation if a transaction has been stored.
                        if (m_bSAF)
                        {
                            StoreAndForwardSimulation(networkStream, response);
                            response = ReadFromStream(networkStream);
                            strResponse = response.ToString();
                        }
                        else
                        {
                            strResponse = response.ToString();
                        }

                        RequestBase tempRequest2 = new RequestBase(response);
                        if (tempRequest2.FunctionType == "REPORT")
                        {
                            ReportSimulation(networkStream, response);
                            response = ReadFromStream(networkStream);
                            strResponse = response.ToString();
                        }
                        else
                        {
                            strResponse = response.ToString();
                        }

                        OtherMessageSimulation(networkStream, response);
                        response = ReadFromStream(networkStream);
                        strResponse = response.ToString();

                        Console.WriteLine(" >> Request Message from client - ");
                        Console.WriteLine(strResponse);
                        Console.WriteLine();

                        UnRegisterPOSRequest request5 = new UnRegisterPOSRequest(response);
                        UnRegisterPOSResponse response5 = new UnRegisterPOSResponse();

                        // An entry code must be entred again in order to unregister.
                        EnterEntryCode(response5, expectedCode);

                        if (response5.ResultCode == -1)
                        {
                            response5.ResponseText = "Unregistered MAC_LABEL";
                        }
                        responseToSend = response5.GenerateResponseMessage();

                        Console.WriteLine(" >> Response Message to send - ");
                        Console.WriteLine(responseToSend.ToString());
                        Console.WriteLine();

                        WriteToStream(responseToSend, networkStream);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                clientSocket.Close();
                serverSocket.Stop();
            }
            Console.WriteLine(" >> exit");
            Console.ReadLine();
        }

        #region Message Simulation Methods

        public static void RetailSimulation(NetworkStream networkStream, XDocument doc)
        {
            // Add items

            XDocument response = doc;
            string strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            AddLineItemRequest retailRequest1 = new AddLineItemRequest(response);
            AddLineItemResponse retailResponse = new AddLineItemResponse();

            Console.WriteLine("Press Enter to confirm items.");
            Console.ReadLine();

            retailResponse.Result = "OK";
            retailResponse.ResultCode = -1;
            retailResponse.ResponseText = "Added merchandise";
            retailResponse.TerminationStatus = "SUCCESS";

            XDocument responseToSend = retailResponse.GenerateResponseMessage();

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);

            response = ReadFromStream(networkStream);
            strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            RequestBase request = new RequestBase(response);
            if (request.Command == "OVERRIDE")
            {
                // Override
                OverrideLineItemRequest retailRequest1a = new OverrideLineItemRequest(response);
                OverrideLineItemResponse retailResponse1a = new OverrideLineItemResponse();

                Console.WriteLine("Press Enter to override items.");
                Console.ReadLine();

                retailResponse1a.Result = "OK";
                retailResponse1a.ResultCode = -1;
                retailResponse1a.ResponseText = "Overwrote line item";
                retailResponse1a.TerminationStatus = "SUCCESS";

                responseToSend = retailResponse1a.GenerateResponseMessage();

                Console.WriteLine(" >> Response Message to send - ");
                Console.WriteLine(responseToSend.ToString());
                Console.WriteLine();

                WriteToStream(responseToSend, networkStream);

                response = ReadFromStream(networkStream);
                strResponse = response.ToString();
                Console.WriteLine(" >> Request Message from client - ");
                Console.WriteLine(strResponse);
                Console.WriteLine();
            }
            request = new RequestBase(response);
            if (request.Command == "ADD")
            {
                AddLineItemRequest retailRequest1c = new AddLineItemRequest(response);
                AddLineItemResponse retailResponse1c = new AddLineItemResponse();

                Console.WriteLine("Press Enter to confirm items.");
                Console.ReadLine();

                retailResponse1c.Result = "OK";
                retailResponse1c.ResultCode = -1;
                retailResponse1c.ResponseText = "Added merchandise";
                retailResponse1c.TerminationStatus = "SUCCESS";

                responseToSend = retailResponse1c.GenerateResponseMessage();

                Console.WriteLine(" >> Response Message to send - ");
                Console.WriteLine(responseToSend.ToString());
                Console.WriteLine();

                WriteToStream(responseToSend, networkStream);

                response = ReadFromStream(networkStream);
                strResponse = response.ToString();
                Console.WriteLine(" >> Request Message from client - ");
                Console.WriteLine(strResponse);
                Console.WriteLine();
                request = new RequestBase(response);
                if (request.Command == "REMOVE")
                {
                    // Remove Item

                    RemoveLineItemRequest retailRequest1b = new RemoveLineItemRequest(response);
                    RemoveLineItemResponse retailResponse1b = new RemoveLineItemResponse();

                    Console.WriteLine("Press Enter to remove item.");
                    Console.ReadLine();

                    retailResponse1b.Result = "OK";
                    retailResponse1b.ResultCode = -1;
                    retailResponse1b.ResponseText = "Removed line item";
                    retailResponse1b.TerminationStatus = "SUCCESS";

                    responseToSend = retailResponse1b.GenerateResponseMessage();

                    Console.WriteLine(" >> Response Message to send - ");
                    Console.WriteLine(responseToSend.ToString());
                    Console.WriteLine();

                    WriteToStream(responseToSend, networkStream);

                    response = ReadFromStream(networkStream);
                    strResponse = response.ToString();
                    Console.WriteLine(" >> Request Message from client - ");
                    Console.WriteLine(strResponse);
                    Console.WriteLine();
                }
            }



            // Authorize

            AuthorizePaymentRequest retailRequest2 = new AuthorizePaymentRequest(response);
            
            Console.WriteLine("Press Enter to confirm payment.");
            Console.ReadLine();

            m_fTransAmount = retailRequest2.TransAmount;

            AuthorizePaymentResponse retailResponse2 = new AuthorizePaymentResponse()
            {
                Result = "APPROVED",
                ResultCode = 5,
                ResponseText = "APPROVED",
                TerminationStatus = "SUCCESS",
                TransSeqNum = 5,
                IntrnSeqNum = 100000001,
                Troutd = 10001,
                Ctroutd = 22,
                PaymentMedia = "VISA",
                AccNum = "456789******0123",
                AuthCode = "L101001",
                AvailableBalance = 1500.00f,
                ApprovedAmount = m_fTransAmount,
                Cardholder = "Doe/John",
                CVV2Code = "M",
            };

            Receipt receipt = new Receipt();
            Receipt receipt2 = new Receipt();
            receipt.SetReceiptText(GenerateCustomerReceipt(retailResponse2.AccNum, retailResponse2.AuthCode));
            receipt2.SetReceiptText(GenerateMerchantReceipt(retailResponse2.AccNum, retailResponse2.AuthCode));

            retailResponse2.Receipts.Add(receipt);
            retailResponse2.Receipts.Add(receipt2);

            responseToSend = retailResponse2.GenerateResponseMessage();

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);

            response = ReadFromStream(networkStream);
            strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            // Capture

            CapturePaymentRequest retailRequest3 = new CapturePaymentRequest(response);

            Console.WriteLine("Press Enter to confirm payment.");
            Console.ReadLine();

            CapturePaymentResponse retailResponse3 = new CapturePaymentResponse()
            {
                Result = "CAPTURED",
                ResultCode = 4,
                ResponseText = "CAPTURED",
                TerminationStatus = "SUCCESS",
                TransSeqNum = 5,
                IntrnSeqNum = 100000002,
                Troutd = 10001,
                Ctroutd = 22,
                PaymentMedia = "VISA",
                AccNum = "456789******0123",
                AuthCode = "L101001",
                AvailableBalance = 1500.00f,
                ApprovedAmount = m_fTransAmount,
                Cardholder = "Doe/John",
                CVV2Code = "M",
            };

            if (String.IsNullOrEmpty(retailRequest3.AuthCode) && retailRequest3.Ctroutd == null)
            {
                Console.WriteLine("Do you want to put this transaction into Store and Forward?");
                string safChoice = Console.ReadLine();
                if (safChoice.ToUpper() == "Y" || safChoice.ToUpper() == "YES")
                {
                    retailResponse3.SafNum = 12345;
                    m_safRecord = new SAFRecord()
                    {
                        AccNum = retailResponse3.AccNum,
                        TransAmount = retailRequest2.TransAmount,
                        Invoice = "L101234",
                        SAFNum = 12345,
                        PaymentMedia = retailResponse3.PaymentMedia,
                        PaymentType = retailRequest3.PaymentType,
                        SAFStatus = "ELIGIBLE"
                    };
                    m_bSAF = true;
                }
            }

            Receipt receipt3 = new Receipt();
            Receipt receipt4 = new Receipt();
            receipt3.SetReceiptText(GenerateCustomerReceipt(retailResponse3.AccNum, retailResponse3.AuthCode));
            receipt4.SetReceiptText(GenerateMerchantReceipt(retailResponse3.AccNum, retailResponse3.AuthCode));

            retailResponse3.Receipts.Add(receipt3);
            retailResponse3.Receipts.Add(receipt4);

            responseToSend = retailResponse3.GenerateResponseMessage();

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);

            response = ReadFromStream(networkStream);
            strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            // Refund or Void

            PaymentRequest payRequest = new PaymentRequest(response);
            m_strCommand = payRequest.Command;
            if (m_strCommand == "VOID")
            {
                VoidPaymentRequest retailRequest4 = new VoidPaymentRequest(response);

                Console.WriteLine("Press Enter to void payment.");
                Console.ReadLine();

                VoidPaymentResponse retailResponse4 = new VoidPaymentResponse()
                {
                    Result = "VOIDED",
                    ResultCode = 7,
                    ResponseText = "VOIDED",
                    TerminationStatus = "SUCCESS",
                    TransSeqNum = 5,
                    IntrnSeqNum = 100000003,
                    Troutd = 10001,
                    Ctroutd = 22
                };

                retailResponse4.Receipts.Add(receipt3);
                retailResponse4.Receipts.Add(receipt4);

                responseToSend = retailResponse4.GenerateResponseMessage();
            }
            else
            {
                CreditPaymentRequest retailRequest4a = new CreditPaymentRequest(response);

                Console.WriteLine("Press Enter to refund payment.");
                Console.ReadLine();

                CreditPaymentResponse retailResponse4a = new CreditPaymentResponse()
                {
                    Result = "CAPTURED",
                    ResultCode = 4,
                    ResponseText = "RETURNED",
                    TerminationStatus = "SUCCESS",
                    TransSeqNum = 5,
                    IntrnSeqNum = 100000003,
                    Troutd = 10001,
                    Ctroutd = 22
                };

                retailResponse4a.Receipts.Add(receipt3);
                retailResponse4a.Receipts.Add(receipt4);

                responseToSend = retailResponse4a.GenerateResponseMessage();
            }

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);
        }

        public static void GiftCardSimulaton(NetworkStream networkStream, XDocument doc)
        {
            float giftCardValue = 0.0f;
            
            // Activate Gift Card
            
            XDocument response = doc;
            string strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            ActivateGiftCardPaymentRequest giftCardRequest1 = new ActivateGiftCardPaymentRequest(response);
 
            Console.WriteLine("Press Enter to activate gift card.");
            Console.ReadLine();

            giftCardValue = giftCardRequest1.TransAmount;

            ActivateGiftCardPaymentResponse giftCardResponse1 = new ActivateGiftCardPaymentResponse()
            {
                Result = "CAPTURED",
                ResultCode = 4,
                ResponseText = "ACTIVATED",
                TerminationStatus = "SUCCESS",
                TransSeqNum = 5,
                IntrnSeqNum = 123456789,
                Troutd = 123456789,
                Ctroutd = 22,
                PaymentMedia = "GIFT",
                AccNum = "678901******2345",
                AvailableBalance = giftCardValue,
            };

            Receipt receipt = new Receipt();
            Receipt receipt2 = new Receipt();
            receipt.SetReceiptText(GenerateCustomerReceipt(giftCardResponse1.AccNum, String.Empty));
            receipt2.SetReceiptText(GenerateMerchantReceipt(giftCardResponse1.AccNum, String.Empty));

            giftCardResponse1.Receipts.Add(receipt);
            giftCardResponse1.Receipts.Add(receipt2);

            XDocument responseToSend = giftCardResponse1.GenerateResponseMessage();

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);

            // Add Value Gift Card

            response = ReadFromStream(networkStream);
            strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            AddValueGiftCardPaymentRequest giftCardRequest2 = new AddValueGiftCardPaymentRequest(response);

            Console.WriteLine("Press Enter to add value to gift card.");
            Console.ReadLine();

            giftCardValue += giftCardRequest2.TransAmount;

            AddValueGiftCardPaymentResponse giftCardResponse2 = new AddValueGiftCardPaymentResponse()
            {
                Result = "CAPTURED",
                ResultCode = 4,
                ResponseText = "ADDED VALUE",
                TerminationStatus = "SUCCESS",
                TransSeqNum = 5,
                IntrnSeqNum = 123456789,
                Troutd = 123456789,
                Ctroutd = 22,
                PaymentMedia = "GIFT",
                AccNum = "678901******2345",
                AvailableBalance = giftCardValue,
            };

            giftCardResponse2.Receipts.Add(receipt);
            giftCardResponse2.Receipts.Add(receipt2);

            responseToSend = giftCardResponse2.GenerateResponseMessage();

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);

            // Balance

            response = ReadFromStream(networkStream);
            strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            BalanceGiftCardPaymentRequest giftCardRequest3 = new BalanceGiftCardPaymentRequest(response);

            Console.WriteLine("Press Enter to review gift card balance.");
            Console.ReadLine();

            BalanceGiftCardPaymentResponse giftCardResponse3 = new BalanceGiftCardPaymentResponse()
            {
                Result = "CAPTURED",
                ResultCode = 4,
                ResponseText = "TRANSACTION APPROVED ",
                TerminationStatus = "SUCCESS",
                TransSeqNum = 5,
                IntrnSeqNum = 123456789,
                Troutd = 123456789,
                Ctroutd = 22,
                PaymentMedia = "GIFT",
                AccNum = "678901******2345",
                AvailableBalance = giftCardValue,
            };

            giftCardResponse3.Receipts.Add(receipt);
            giftCardResponse3.Receipts.Add(receipt2);

            responseToSend = giftCardResponse3.GenerateResponseMessage();

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);

            // Cash Out

            response = ReadFromStream(networkStream);
            strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            CashOutPaymentRequest giftCardRequest4 = new CashOutPaymentRequest(response);

            Console.WriteLine("Press Enter to empty gift card.");
            Console.ReadLine();

            CashOutPaymentResponse giftCardResponse4 = new CashOutPaymentResponse()
            {
                Result = "CAPTURED",
                ResultCode = 4,
                ResponseText = "APPROVED",
                TerminationStatus = "SUCCESS",
                TransSeqNum = 5,
                IntrnSeqNum = 123456789,
                Troutd = 123456789,
                Ctroutd = 22,
                PaymentMedia = "GIFT",
                AccNum = "678901******2345",
            };

            giftCardResponse4.Receipts.Add(receipt);
            giftCardResponse4.Receipts.Add(receipt2);

            responseToSend = giftCardResponse4.GenerateResponseMessage();

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);

            // Deactivate

            response = ReadFromStream(networkStream);
            strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            DeactivateGiftCardPaymentRequest giftCardRequest5 = new DeactivateGiftCardPaymentRequest(response);

            Console.WriteLine("Press Enter to deactivate gift card.");
            Console.ReadLine();

            DeactivateGiftCardPaymenResponse giftCardResponse5 = new DeactivateGiftCardPaymenResponse()
            {
                Result = "VOIDED",
                ResultCode = 7,
                ResponseText = "DEACTIVATED",
                TerminationStatus = "SUCCESS",
                TransSeqNum = 5,
                IntrnSeqNum = 123456789,
                Troutd = 123456789,
                Ctroutd = 22,
                PaymentMedia = "GIFT",
                AccNum = "678901******2345",
            };

            m_strCommand = giftCardRequest5.Command;

            giftCardResponse5.Receipts.Add(receipt);
            giftCardResponse5.Receipts.Add(receipt2);

            responseToSend = giftCardResponse5.GenerateResponseMessage();

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);
        }

        public static void StoreAndForwardSimulation(NetworkStream networkStream, XDocument doc)
        {
            XDocument response = doc;
            string strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            // Query SAF

            QuerySAFRequest safRequest1 = new QuerySAFRequest(response);

            Console.WriteLine("Press Enter to Query SAF Record.");
            Console.ReadLine();

            QuerySAFResponse safResponse1 = new QuerySAFResponse()
            {
                Result = "OK",
                ResultCode = -1,
                ResponseText = "SAF records found",
                TerminationStatus = "SUCCESS"
            };

            safResponse1.Records.Add(m_safRecord);

            XDocument responseToSend = safResponse1.GenerateResponseMessage();

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);

            // Remove SAF

            response = ReadFromStream(networkStream);
            strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            RemoveSAFRequest safRequest2 = new RemoveSAFRequest(response);

            Console.WriteLine("Press Enter to remove SAF Record.");
            Console.ReadLine();

            RemoveSAFResponse safResponse2 = new RemoveSAFResponse()
            {
                Result = "OK",
                ResultCode = -1,
                ResponseText = "Remove SAF transaction(s)",
                TerminationStatus = "SUCCESS"
            };

            safResponse2.Records.Add(m_safRecord);

            responseToSend = safResponse2.GenerateResponseMessage();

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);
        }

        public static void ReportSimulation(NetworkStream networkStream, XDocument doc)
        {
            XDocument response = doc;
            string strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            // Daily report

            ReportRequest reportRequest = new ReportRequest(response);

            Console.WriteLine("Press Enter to view the Daily Report.");
            Console.ReadLine();

            ReportResponse reportResponse = new ReportResponse()
            {
                Result = "REPORT_OK",
                ResultCode = 14,
                ResponseText = "Report executed successfully",
                TerminationStatus = "SUCCESS",
                ClientID = "100010001"
            };

            ReportResponse.ResponseFieldRecord record = new ReportResponse.ResponseFieldRecord()
            {
                CtrOutd = "22",
                Command = "AUTH",
                TransAmount = m_fTransAmount.ToString("0.00"),
                PaymentType = "CREDIT",
                PaymentMedia = "VISA",
                IntrnSeqNum = "100000001"
            };

            reportResponse.ResponseRecords.Add(record);

            XDocument responseToSend = reportResponse.GenerateResponseMessage();

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);
        }

        public static void OtherMessageSimulation(NetworkStream networkStream, XDocument doc)
        {
            XDocument response = doc;
            string strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            // Signature Request

            SignatureRequest otherRequest1 = new SignatureRequest(response);

            Console.WriteLine("Press Enter to generate a signature.");
            Console.ReadLine();

            SignatureResponse otherResponse1 = new SignatureResponse()
            {
                Result = "OK",
                ResultCode = -1,
                ResponseText = "Signature captured",
                TerminationStatus = "SUCCESS",
                MimeType = "image / tiff"
            };

            otherResponse1.SignatureData = System.Text.ASCIIEncoding.ASCII.GetBytes("John Doe");

            XDocument responseToSend = otherResponse1.GenerateResponseMessage();

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);

            response = ReadFromStream(networkStream);
            strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            // Settlement Request

            SettleRequest otherRequest2 = new SettleRequest(response);

            Console.WriteLine("Press enter to schedule a settlement.");
            Console.ReadLine();

            SettleResponse otherResponse2 = new SettleResponse()
            {
                ResultCode = 21,
                ResponseText = "Settlement Scheduled",
                TerminationStatus = "SUCCESS",
            };

            responseToSend = otherResponse2.GenerateResponseMessage();

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);

            response = ReadFromStream(networkStream);
            strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            // Settlement Summary Request

            SettlementSummaryRequest otherRequest3 = new SettlementSummaryRequest(response);

            Console.WriteLine("Press enter to view Settlement Summary.");
            Console.ReadLine();

            SettlementSummaryResponse otherResponse3 = new SettlementSummaryResponse()
            {
                ResultCode = 14,
                Result = "REPORT_OK",
                ResponseText = "Report executed successfully",
                TerminationStatus = "SUCCESS",
                ClientID = "100010001"
            };

            SettlementResponseFieldRecord record = new SettlementResponseFieldRecord()
            {
                SettleCode = "22",
                SettleDate = DateTime.Now.ToString("yyyy.MM.dd"),
                CreditSaleCount = "1"
            };

            otherResponse3.ResponseRecords.Add(record);

            responseToSend = otherResponse3.GenerateResponseMessage();

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);

            response = ReadFromStream(networkStream);
            strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            // Last Transaction Request

            LastTransactionRequest otherRequest4 = new LastTransactionRequest(response);

            Console.WriteLine("Press enter to see Last Transaction.");
            Console.ReadLine();

            LastTransactionResponse otherResponse4 = new LastTransactionResponse()
            {
                ClientID = "100010001",
                Command = m_strCommand,
                TransSeqNum = 5,
                IntrnSeqNum = 100000003,
                Troutd = 10001,
                Ctroutd = 22
            };

            if (m_strCommand == "VOID")
            {
                otherResponse4.Result = "VOIDED";
                otherResponse4.ResultCode = 7;
                otherResponse4.ResponseText = "VOIDED";
                otherResponse4.TerminationStatus = "SUCCESS";
            }
            else if (m_strCommand == "GIFT_CLOSE")
            {
                otherResponse4.IntrnSeqNum = 123456789;
                otherResponse4.Troutd = 123456789;
                otherResponse4.Result = "VOIDED";
                otherResponse4.ResultCode = 7;
                otherResponse4.ResponseText = "DEACTIVATED";
                otherResponse4.TerminationStatus = "SUCCESS";
            }
            else
            {
                otherResponse4.Result = "CAPTURED";
                otherResponse4.ResultCode = 4;
                otherResponse4.ResponseText = "RETURNED";
                otherResponse4.TerminationStatus = "SUCCESS";
            }

            responseToSend = otherResponse4.GenerateResponseMessage();

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);

            response = ReadFromStream(networkStream);
            strResponse = response.ToString();
            Console.WriteLine(" >> Request Message from client - ");
            Console.WriteLine(strResponse);
            Console.WriteLine();

            // Duplicate Check Request

            DuplicateCheckRequest otherRequest5 = new DuplicateCheckRequest(response);

            Console.WriteLine("Press enter to see duplicate checks.");
            Console.ReadLine();

            DuplicateCheckResponse otherResponse5 = new DuplicateCheckResponse()
            {
                ResultCode = 14,
                Result = "REPORT_OK",
                ResponseText = "Report executed successfully",
                TerminationStatus = "SUCCESS"
            };

            DuplicateCheckRecord dupRecord = new DuplicateCheckRecord()
            {
                TransDate = DateTime.Now,
                TransTime = DateTime.Now,
                ClientID = 100010001,
                IntrnSeqNum = 123450001,
                AcctNum = "400123******4567",
                TransAmount = 27.65f,
                StatusCode = 2,
                PaymentType = "CREDIT",
                Command = "SALE",
                BusName = "TEST Store"
            };

            DuplicateCheckRecord dupRecord2 = new DuplicateCheckRecord()
            {
                TransDate = DateTime.Now,
                TransTime = DateTime.Now,
                ClientID = 100010001,
                IntrnSeqNum = 123450002,
                AcctNum = "400123******4567",
                TransAmount = 27.65f,
                StatusCode = 2,
                PaymentType = "CREDIT",
                Command = "SALE",
                BusName = "TEST Store"
            };

            otherResponse5.DuplicateCheckRecords.Add(dupRecord);
            otherResponse5.DuplicateCheckRecords.Add(dupRecord2);

            responseToSend = otherResponse5.GenerateResponseMessage();

            Console.WriteLine(" >> Response Message to send - ");
            Console.WriteLine(responseToSend.ToString());
            Console.WriteLine();

            WriteToStream(responseToSend, networkStream);
        }

        #endregion

        public static string GenerateCustomerReceipt(string accNum, string authCode)
        {
            string receipt = String.Empty;
            string lastNum = accNum.Substring(12, 4);

            receipt = String.Concat(receipt, "|");
            receipt = String.Concat(receipt, "BUSINESS RECEIPT                                        ", "|");
            receipt = String.Concat(receipt, String.Format("Acct/Card # : XXXXXXXXXXXX{0}          ", lastNum), "|");
            receipt = String.Concat(receipt, String.Format("Auth # : {0}                         ", authCode), "|");
            receipt = String.Concat(receipt, "RESP CODE 000 REF 559                                   ", "|");
            receipt = String.Concat(receipt, "|");
            receipt = String.Concat(receipt, "CUSTOMER COPY                                           ", "|");
            receipt = String.Concat(receipt, "|");
            receipt = String.Concat(receipt, "THANKS, COME AGAIN                                      ", "|");

            return receipt;
        }

        public static string GenerateMerchantReceipt(string accNum, string authCode)
        {
            string receipt = String.Empty;
            string lastNum = accNum.Substring(12, 4);

            receipt = String.Concat(receipt, "|");
            receipt = String.Concat(receipt, "BUSINESS RECEIPT                                        ", "|");
            receipt = String.Concat(receipt, String.Format("Acct/Card # : XXXXXXXXXXXX{0}          ", lastNum), "|");
            receipt = String.Concat(receipt, String.Format("Auth # : {0}                         ", authCode), "|");
            receipt = String.Concat(receipt, "RESP CODE 000 REF 559                                   ", "|");
            receipt = String.Concat(receipt, "|");
            receipt = String.Concat(receipt, "MERCHANT COPY                                           ", "|");
            receipt = String.Concat(receipt, "|");
            receipt = String.Concat(receipt, "x _______________________________________", "|");
            receipt = String.Concat(receipt, "SIGNATURE", "|");
            receipt = String.Concat(receipt, "I agree to pay the amount stated                        ", "|");
            receipt = String.Concat(receipt, "on this receipt.                                        ", "|");
            receipt = String.Concat(receipt, "|");
            receipt = String.Concat(receipt, "THANKS, COME AGAIN                                      ", "|");

            return receipt;
        }

        public static string UpdateCounter(ref int counter, byte[] macKey)
        {
            counter++;
            // convert counter to bytes
            var counterBytes = Encoding.UTF8.GetBytes(counter.ToString());

            // import AES 128 MAC_KEY
            var hmac = new HMACSHA256(macKey);
            var macBytes = hmac.ComputeHash(counterBytes);
            var mac = Convert.ToBase64String(macBytes);

            return mac;
        }

        public static void EnterEntryCode(ResponseBase response, string expectedCode)
        {
            Console.WriteLine("Enter in the entry code.");
            string entryCode = Console.ReadLine();

            try
            {
                if (String.IsNullOrEmpty(entryCode))
                {
                    response.Result = "CANCELED";
                    response.ResultCode = 59001;
                    response.ResponseText = "CANCELED by CUSTOMER (59001)";
                    response.TerminationStatus = "FAILURE";
                }
                else if (entryCode != expectedCode)
                {
                    response.Result = "FIELD_ERROR";
                    response.ResultCode = 59040;
                    response.ResponseText = "ENTRY_CODE mismatch (59040)";
                    response.TerminationStatus = "FAILURE";
                }
                else if (entryCode == expectedCode)
                {
                    response.Result = "OK";
                    response.ResultCode = -1;
                    response.TerminationStatus = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                response.Result = "INTERNAL_ERROR";
                response.ResultCode = 59020;
                response.ResponseText = ex.Message + " (59020)";
                response.TerminationStatus = "FAILURE";
            }
        }


        /// <summary>
        /// Writes the request message to the socket's stream.
        /// </summary>
        /// <param name="request">the request message to be sent to the stream.</param>
        /// <param name="stream">The socket stream</param>
        public static void WriteToStream(XDocument request, NetworkStream stream)
        {
            var writerSettings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Encoding = Encoding.UTF8
            };

            try
            {
                using (var writer = XmlWriter.Create(stream, writerSettings))
                {
                    request.WriteTo(writer);
                }
                stream.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to write to stream: " + ex.Message);
            }
        }

        /// <summary>
        /// Reads the data from the stream
        /// </summary>
        /// <param name="stream">the socket stream</param>
        /// <returns>the response message as an XDocument</returns>
        public static XDocument ReadFromStream(NetworkStream stream)
        {
            var response = new XDocument();
            var readerSettings = new XmlReaderSettings()
            {
                CloseInput = false,
                IgnoreWhitespace = true,
                ConformanceLevel = ConformanceLevel.Fragment
            };

            try
            {
                var reader = XmlReader.Create(stream, readerSettings);

                using (var writer = response.CreateWriter())
                {
                    int depth = 0;
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                depth++;
                                writer.WriteStartElement(reader.Name);
                                writer.WriteAttributes(reader, false);
                                if (reader.IsEmptyElement)
                                {
                                    depth--;
                                    writer.WriteEndElement();
                                }
                                break;
                            case XmlNodeType.Text:
                                writer.WriteString(reader.Value);
                                break;
                            case XmlNodeType.EndElement:
                                writer.WriteEndElement();
                                if (--depth == 0)
                                {
                                    reader.Close();
                                    return response;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to read from stream: " + ex.Message);
                return null;
            }

            return response;
        }
    }
}
