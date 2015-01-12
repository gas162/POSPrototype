using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Security;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace PointPrototype
{
    class Program
    {
        #region Variables

        // For all request messages
        private static int m_intCounter = 0;
        private static byte[] m_arrByteMacKey;
        private static string m_strMacLabel;

        // Entry code or register and unregister request message.
        private static int m_intEntryCode;

        // Public key to send to Point Device
        private static string m_strPublicKey;

        // For Store and Forward (SAF)
        private static bool m_bsafCaptured = false;
        private static int m_intSAFNumber = 0;

        // Report
        private static bool m_bShowReport = false;

        // Settings
        private static bool m_bManualEntry = false;
        private static bool m_bForceFlag = false;

        // Line Item Transaction values
        private static int m_intLineItemID = 0;
        private static int m_intOfferLineItemID = 0;
        private static float m_fLineItemTotal = 0.0f;
        private static float m_fLineItemTax = 0.0f;
        private static float m_fOfferLineItemTotal = 0.0f;
        private static float m_fTransTotal = 0.0f;

        // Payment Transaction values
        private static int m_intTroutD = 0;
        private static int m_intCTroutD = 0;
        private static string m_strTransactionStatus = String.Empty;

        #endregion

        static void Main(string[] args)
        {
            string address = "199.5.83.138";
            int port = 5015;

            try
            {
                Console.WriteLine("Connecting to the Point Device...");
                Console.WriteLine();
                using (var socket = new TcpClient(address, port))
                {
                    Console.WriteLine("Socket has opened. Now Writing the request to Point Device...");
                    Console.WriteLine();
                    NetworkStream stream = socket.GetStream();

                    if (Register(stream))
                    {
                        if (StartSession(stream))
                        {

                            //// Either start the retail or gift card scenario.
                            //Console.WriteLine("Options (Default Choice: 1)");
                            //Console.WriteLine("1 - Retail");
                            //Console.WriteLine("2 - Gift Card");
                            //string choice = Console.ReadLine();
                            //Console.WriteLine();
                            //switch (choice)
                            //{
                            //    case "2":
                            //        GiftCardSimulation(stream);
                            //        break;
                            //    case "1":
                            //    default:
                            RetailSimulation(stream);
                            m_bShowReport = true;
                            //        break;
                            //}

                            if (EndSession(stream))
                            {
                                // Run the SAF simulation if a transaction is stored and forwarded.
                                if (m_bsafCaptured)
                                {
                                    StoreAndForwardSimulation(stream);
                                }

                                // If a retail simulation has been done, then the user can view a report.
                                if (m_bShowReport)
                                {
                                    Console.WriteLine("Do you want to view the daily report?");
                                    string reportChoice = Console.ReadLine();
                                    Console.WriteLine();
                                    if (reportChoice.ToUpper() == "Y" || reportChoice.ToUpper() == "YES")
                                    {
                                        RunReport(stream, "DAYSUMMARY");
                                        //ReportSimulation(stream);
                                    }
                                }

                                // Test any other message that has not been used.
                                OtherMessageSimulation(stream);

                                if (UnRegister(stream))
                                {

                                    Console.WriteLine("Press any key to end the POS application.");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        #region Menus

        public static void MainMenu(NetworkStream stream)
        {
            string choice = String.Empty;

            while (choice != "4")
            {
                Console.Clear();
                Console.WriteLine("Main Menu");
                Console.WriteLine("1 - Register");
                Console.WriteLine("2 - Unregister");
                //Console.WriteLine("3 - Start Session");
                //Console.WriteLine("4 - SAF");
                //Console.WriteLine("5 - Schedule Settlement");
                //Console.WriteLine("6 - Capture Signature");
                //Console.WriteLine("7 - Report");
                Console.WriteLine("4 - Exit");
                Console.Write("Choice: ");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Register(stream);
                        break;
                    case "2":
                        UnRegister(stream);
                        break;
                    //case "3":
                    //    StartSession(stream);
                    //    SessionMenu(stream);
                    //    break;
                    //case "4":
                    //    SAFMenu(stream);
                    //    break;
                    //case "5":
                    //    ScheduleSettlement(stream);
                    //    break;
                    //case "6":
                    //    RequestSignature(stream);
                    //    break;
                    //case "7":
                    //    ReportMenu(stream);
                    //    break;
                    default:
                        break;
                }
            }
        }

        public static void SessionMenu(NetworkStream stream)
        {
            string choice = String.Empty;

            while (choice != "9")
            {
                Console.Clear();
                Console.WriteLine("Payment Transaction Menu");
                Console.WriteLine("1 - Add Line Item Item");
                Console.WriteLine("2 - Override Line Item");
                Console.WriteLine("3 - Remove Line Item");
                Console.WriteLine("4 - Authorize Payment");
                Console.WriteLine("5 - Capture Payment");
                Console.WriteLine("6 - Credit Payment");
                Console.WriteLine("7 - Void Payment");
                Console.WriteLine("8 - Last Transaction");
                Console.WriteLine("9 - Finish Session");
                Console.Write("Choice: ");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Merchandise (M) or (O)ffer? ");
                        string itemChoice = Console.ReadLine();
                        bool addItem = itemChoice == "M";
                        bool addOffer = itemChoice == "O";
                        AddLineItem(stream, addItem, addOffer, 1);
                        break;
                    case "2":
                        Console.Write("Enter in a line number ");
                        int itemChoice2 = 1;
                        int.TryParse(Console.ReadLine(), out itemChoice2);
                        OverrideLineItem(stream, itemChoice2);
                        break;
                    case "3":
                        Console.Write("Enter in a line number ");
                        int itemChoice3 = 1;
                        int.TryParse(Console.ReadLine(), out itemChoice3);
                        RemoveLineItem(stream, 0.0f, itemChoice3);
                        break;
                    case "4":
                        AuthorizePayment(stream, m_fTransTotal, false);
                        break;
                    case "5":
                        CapturePayment(stream, m_fTransTotal, false, false);
                        break;
                    case "6":
                        CreditPayment(stream, m_fTransTotal, false);
                        break;
                    case "7":
                        VoidPayment(stream);
                        break;
                    case "8":
                        RunLastTransaction(stream);
                        break;
                    case "9":
                        EndSession(stream);
                        break;
                    default:
                        break;
                }
            }
        }

        public static void SAFMenu(NetworkStream stream)
        {
            string choice = String.Empty;

            while (choice != "3")
            {
                Console.Clear();
                Console.WriteLine("Store and Forward Menu");
                Console.WriteLine("1 - Query");
                Console.WriteLine("2 - Remove");
                Console.WriteLine("3 - Exit");
                Console.Write("Choice: ");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        QuerySAF(stream);
                        break;
                    case "2":
                        RemoveSAF(stream);
                        break;
                    default:
                        break;
                }
            }
        }

        public static void ReportMenu(NetworkStream stream)
        {
            string choice = String.Empty;

            while (choice != "6")
            {
                Console.Clear();
                Console.WriteLine("Report Menu");
                Console.WriteLine("1 - Daily Summary");
                Console.WriteLine("2 - Pre-Settlement");
                Console.WriteLine("3 - Transaction Search");
                Console.WriteLine("4 - Duplicate Checking");
                Console.WriteLine("5 - Last Transaction");
                Console.WriteLine("6 - Exit");
                Console.Write("Choice: ");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        RunReport(stream, "DAYSUMMARY");
                        break;
                    case "2":
                        RunReport(stream, "PRESETTLEMENT");
                        break;
                    case "3":
                        RunReport(stream, "TRANSEARCH");
                        break;
                    case "4":
                        DuplicateChecking(stream);
                        break;
                    case "5":
                        RunLastTransaction(stream);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Security/Session Request Message Methods

        /// <summary>
        /// Sends a request to register the Point Device.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        public static bool Register(NetworkStream stream)
        {
            bool success = false;
            
            // Create Register POS Message.
            var random = new Random();
            RegisterPOSRequest rPOSRequest = new RegisterPOSRequest();

            // Generate random entry code.
            m_intEntryCode = random.Next(9999);
            rPOSRequest.EntryCode = m_intEntryCode;

            var cryptoProvider = new RSACryptoServiceProvider(2048);
            var pubkeyInfo = cryptoProvider.ExportParameters(false);
            var modulusBytes = pubkeyInfo.Modulus;
            var exponentBytes = pubkeyInfo.Exponent;
            List<byte> list1 = new List<byte>(modulusBytes);
            List<byte> list2 = new List<byte>(exponentBytes);
            list1.AddRange(list2);
            var pubKeyBytes = list1.ToArray();
            m_strPublicKey = Convert.ToBase64String(pubKeyBytes);

            m_strPublicKey = EncodePublicKey(pubkeyInfo);

            rPOSRequest.Key = m_strPublicKey;
            rPOSRequest.ValidateMessage();
            XDocument request = rPOSRequest.GetMessage();

            string strEntryCode = m_intEntryCode.ToString("0000");

            Console.WriteLine("Entry Code for Point Device: " + strEntryCode);
            Console.WriteLine();

            WriteToStream(rPOSRequest.GetMessage(), stream);
            XDocument response = ReadFromStream(stream);

            // Get the Response message. If successful then proceed to create a Test MAC request message.
            RegisterPOSResponse rPOSResponse = new RegisterPOSResponse(response, cryptoProvider);

            if (rPOSResponse.Success())
            {
                // Test MAC
                m_arrByteMacKey = rPOSResponse.MacKey;
                m_strMacLabel = rPOSResponse.MacLabel;

                // Create a Test MAC request message
                TestMACRequest tmRequest = new TestMACRequest();
                tmRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
                tmRequest.MacLabel = m_strMacLabel;
                tmRequest.Counter = m_intCounter;

                Console.WriteLine("Verifying MAC...");
                Console.WriteLine();

                tmRequest.ValidateMessage();
                XDocument request2 = tmRequest.GetMessage();

                WriteToStream(request2, stream);
                XDocument response2 = ReadFromStream(stream);

                TestMACResponse tmResponse = new TestMACResponse(response2);
                success = tmResponse.Success();
            }

            return success;
        }

        /// <summary>
        /// Sends a request to unregister the Point Device.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        public static bool UnRegister(NetworkStream stream)
        {
            // UnRegister Request
            UnregisterPOSRequest urPOSrequest = new UnregisterPOSRequest();
            urPOSrequest.EntryCode = m_intEntryCode;
            urPOSrequest.MacLabel = m_strMacLabel;

            urPOSrequest.ValidateMessage();
            XDocument request = urPOSrequest.GetMessage();

            string strEntryCode = m_intEntryCode.ToString("0000");

            Console.WriteLine("Entry Code for Point Device: " + strEntryCode);
            Console.WriteLine();

            WriteToStream(request, stream);
            XDocument response = ReadFromStream(stream);

            UnRegisterPOSResponse urPOSResponse = new UnRegisterPOSResponse(response);

            return urPOSResponse.Success();
        }

        /// <summary>
        /// Sends a request to start a session with a Point Device.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        public static bool StartSession(NetworkStream stream)
        {
            StartSessionRequest startRequest = new StartSessionRequest()
            {
                Invoice = "L10123",
                StoreNum = "123ABC",
                Lane = "12",
                MacLabel = m_strMacLabel
            };

            startRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            startRequest.Counter = m_intCounter;

            Console.WriteLine("Starting Session...");
            Console.WriteLine();

            startRequest.ValidateMessage();
            XDocument request = startRequest.GetMessage();
            
            WriteToStream(request, stream);
            XDocument response = ReadFromStream(stream);

            StartSessionResponse ssResponse = new StartSessionResponse(response);

            return ssResponse.Success();
        }

        /// <summary>
        /// Sends a request to End a session with a Point Device.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        public static bool EndSession(NetworkStream stream)
        {
            FinishSessionRequest finishRequest = new FinishSessionRequest();
            finishRequest.MacLabel = m_strMacLabel;

            finishRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            finishRequest.Counter = m_intCounter;

            Console.WriteLine("Ending Session...");
            Console.WriteLine();

            finishRequest.ValidateMessage();
            XDocument request = finishRequest.GetMessage();

            WriteToStream(request, stream);
            XDocument response = ReadFromStream(stream);

            FinishSessionResponse fsResponse = new FinishSessionResponse(response);

            return fsResponse.Success();
        }

        #endregion

        #region Line Item Transaction Methods

        /// <summary>
        /// Sends a request to add a line item to the transaction. It can either be merchandise or offer.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        /// <param name="addItem">The add item flag.</param>
        /// <param name="addOffer">The add offer flag.</param>
        /// <param name="lineItem">The line item ID.</param>
        public static void AddLineItem(NetworkStream stream, bool addItem, bool addOffer, int lineItem)
        {
            double tax = 0.0;
            float total = 0.0f;
            float transTotal = 0.0f;

            // 1. Add Merchandise

            AddLineItemRequest aliRequest = new AddLineItemRequest();
            Merchandise anItem = new Merchandise();

            if (addItem)
            {
                // Enter the Description, Quantity and Price for the Item.

                Console.WriteLine("Enter in the information for the item:");
                Console.WriteLine();
                anItem.LineItemID = lineItem;

                Console.Write("Description: ");
                anItem.Description = Console.ReadLine();
                while (String.IsNullOrEmpty(anItem.Description))
                {
                    Console.WriteLine("You must enter in a value for the description!");
                    Console.Write("Description: ");
                    anItem.Description = Console.ReadLine();
                }
                Console.Write("Quantity: ");
                string quant = Console.ReadLine();

                while (!Regex.IsMatch(quant, "^\\d{1,10}$"))
                {
                    Console.WriteLine("You must enter a number for quantity!");
                    Console.Write("Quantity: ");
                    quant = Console.ReadLine();
                }
                anItem.Quantity = int.Parse(quant);

                Console.Write("Unit Price: ");
                string price = Console.ReadLine();

                while (!Regex.IsMatch(price, "^[0-9]{1,5}\\.[0-9]{2}$"))
                {
                    Console.WriteLine("You must enter in a valid value for unit price!");
                    Console.Write("Unit Price: ");
                    price = Console.ReadLine();
                }
                anItem.UnitPrice = float.Parse(price);
                total = anItem.UnitPrice * anItem.Quantity;
                tax = Math.Round(Convert.ToDouble(total * 0.06), 2);
                anItem.ExtendedPrice = (float)(total + tax);

                Console.WriteLine();

                m_fLineItemTotal = anItem.ExtendedPrice;
                m_fLineItemTax = (float)tax;
                transTotal = m_fLineItemTotal;

                aliRequest.LineItemsObject.MerchandiseItems.Add(anItem);
            }
            if (addOffer)
            {
                // Create an offer object to go with the merchandise item.
                Offer coupon = new Offer()
                {
                    LineItemID = lineItem,
                    OfferLineItem = m_intLineItemID,
                    Description = "$1 Off",
                    OfferAmount = 1.00f,
                    Type = "MERCHANT_COUPON"
                };

                m_fOfferLineItemTotal = coupon.OfferAmount;

                transTotal = m_fOfferLineItemTotal;
                
                aliRequest.LineItemsObject.OfferItems.Add(coupon);
            }
            if (addItem && addOffer)
            {
                transTotal = m_fLineItemTotal - m_fOfferLineItemTotal;
            }

            aliRequest.RunningTaxAmount = (float)tax;
            aliRequest.RunningTransAmount = transTotal;

            aliRequest.MacLabel = m_strMacLabel;
            aliRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            aliRequest.Counter = m_intCounter;
            aliRequest.ValidateMessage();
            XDocument retailRequest = aliRequest.GetMessage();

            Console.WriteLine("Adding Line Item...");
            Console.WriteLine();

            WriteToStream(retailRequest, stream);
            XDocument retailResponse = ReadFromStream(stream);

            AddLineItemResponse aliResponse = new AddLineItemResponse(retailResponse);
        }

        /// <summary>
        /// Sends a request to override a merchandise line item.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        /// <param name="lineItem">The line item ID.</param>
        public static void OverrideLineItem(NetworkStream stream, int lineItem)
        {
            double tax = 0.00;
            float total = 0.0f;
            float transTotal = 0.0f;
            
            OverrideLineItemRequest overrideRequest = new OverrideLineItemRequest()
            {
                LineItemID = lineItem
            };

            // Enter the new quantity and price.
            Console.Write("Quantity: ");
            string quant2 = Console.ReadLine();

            while (!Regex.IsMatch(quant2, "^\\d{1,10}$"))
            {
                Console.WriteLine("You must enter a number for quantity!");
                Console.Write("Quantity: ");
                quant2 = Console.ReadLine();
            }
            overrideRequest.Quantity = int.Parse(quant2);

            Console.Write("Unit Price: ");
            string price2 = Console.ReadLine();

            while (!Regex.IsMatch(price2, "^[0-9]{1,5}\\.[0-9]{2}$"))
            {
                Console.WriteLine("You must enter in a valid value for unit price!");
                Console.Write("Unit Price: ");
                price2 = Console.ReadLine();
            }
            Console.WriteLine();

            overrideRequest.UnitPrice = float.Parse(price2);

            total = overrideRequest.UnitPrice * overrideRequest.Quantity;
            tax = Math.Round(Convert.ToDouble(total * 0.06), 2);
            transTotal = (float)(tax + total);

            m_fLineItemTax = (float)tax;
            m_fLineItemTotal = transTotal;

            overrideRequest.RunningTaxAmount = (float)tax;
            overrideRequest.RunningTransAmount = transTotal;

            overrideRequest.MacLabel = m_strMacLabel;
            overrideRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            overrideRequest.Counter = m_intCounter;
            overrideRequest.ValidateMessage();
            XDocument retailRequest = overrideRequest.GetMessage();

            m_fTransTotal = transTotal;

            Console.WriteLine("Overriding Line Item...");
            Console.WriteLine();

            WriteToStream(retailRequest, stream);
            XDocument retailResponse = ReadFromStream(stream);

            OverrideLineItemResponse overrideResponse = new OverrideLineItemResponse(retailResponse);
        }

        /// <summary>
        /// Sends a request to remove a line item from the transaction.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        /// <param name="lineItem">The line item ID.</param>
        public static void RemoveLineItem(NetworkStream stream, float amount, int lineItemID)
        {
            RemoveLineItemRequest removeItemRequest = new RemoveLineItemRequest()
            {
                RunningTransAmount = amount,
                LineItemID = lineItemID,
                RunningTaxAmount = 0.0f,
            };

            removeItemRequest.MacLabel = m_strMacLabel;
            removeItemRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            removeItemRequest.Counter = m_intCounter;
            removeItemRequest.ValidateMessage();
            XDocument retailRequest = removeItemRequest.GetMessage();

            Console.WriteLine("Removing Line Item...");
            Console.WriteLine();

            WriteToStream(retailRequest, stream);
            XDocument retailResponse = ReadFromStream(stream);

            RemoveLineItemResponse removeItemResponse = new RemoveLineItemResponse(retailResponse);
                
        }

        #endregion

        #region Payment Transaction Methods

        /// <summary>
        /// Sends a request to authorize a payment. It can be done offline or online.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        /// <param name="transTotal">The transaction total to be authorized.</param>
        /// <param name="authorize">The authorization flag. If true, then the authorization is performed offline.</param>
        public static void AuthorizePayment(NetworkStream stream, float transTotal, bool authorize)
        {
            AuthorizePaymentRequest authPayRequest = new AuthorizePaymentRequest()
            {
                PaymentType = "CREDIT",
                MacLabel = m_strMacLabel,
                AuthCode = authorize ? "L101001" : String.Empty,
                ManualEntry = m_bManualEntry,
                ForceFlag = m_bForceFlag
            };

            authPayRequest.TransAmount = transTotal;
            authPayRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            authPayRequest.Counter = m_intCounter;
            authPayRequest.ValidateMessage();

            XDocument retailRequest2 = authPayRequest.GetMessage();

            Console.WriteLine("Authorizing payment...");
            Console.WriteLine();

            WriteToStream(retailRequest2, stream);
            XDocument retailResponse2 = ReadFromStream(stream);

            AuthorizePaymentResponse authPayResponse = new AuthorizePaymentResponse(retailResponse2);

            // Store the client-specific transaction ID and transaction ID for later use.
            m_intCTroutD = authPayResponse.Ctroutd;
            m_intTroutD = authPayResponse.Troutd;
        }

        /// <summary>
        /// Sends a request to Capture a payment.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        /// <param name="transTotal">The transaction total to be captured.</param>
        /// <param name="authorize">The authorization flag. If true, then the capture request is performed offline.</param>
        /// <param name="settle">The settle flag. If true, then the capture payment request is a follow-on transaction.</param>
        public static void CapturePayment(NetworkStream stream, float transTotal, bool authorize, bool settle)
        {
            CapturePaymentRequest capturePayRequest = new CapturePaymentRequest()
            {
                PaymentType = "CREDIT",
                MacLabel = m_strMacLabel,
                ManualEntry = m_bManualEntry,
                ForceFlag = m_bForceFlag
            };

            // Offline only. 
            if (authorize && !settle)
            {
                capturePayRequest.AuthCode = "L101001";
            }

            // Add the Ctroud to the message for a follow-on transaction.
            else if (settle)
            {
                capturePayRequest.Ctroutd = m_intCTroutD;
            }

            capturePayRequest.TransAmount = transTotal;
            capturePayRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            capturePayRequest.Counter = m_intCounter;
            capturePayRequest.ValidateMessage();

            XDocument retailRequest = capturePayRequest.GetMessage();

            Console.WriteLine("Capturing Payment...");
            Console.WriteLine();

            WriteToStream(retailRequest, stream);
            XDocument retailResponse = ReadFromStream(stream);

            CapturePaymentResponse capturePayResponse = new CapturePaymentResponse(retailResponse);

            if (capturePayResponse.SafNum != null)
            {
                m_bsafCaptured = true;
                m_intSAFNumber = capturePayResponse.SafNum.Value;
            }

            // Store the client-specific transaction ID and transaction ID for later use.
            m_intCTroutD = capturePayResponse.Ctroutd;
            m_intTroutD = capturePayResponse.Troutd;

            if (settle)
            {
                m_strTransactionStatus = "Settled";
            }
        }

        /// <summary>
        /// Sends a request to return funds to an account.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        /// <param name="transTotal">The total amount to refund.</param>
        /// <param name="useCtroutd">The flag to indicate if the client-specific transaction routing ID should be included.</param>
        public static void CreditPayment(NetworkStream stream, float transTotal, bool useCtroutd)
        {
            CreditPaymentRequest creditPayRequest = new CreditPaymentRequest()
            {
                PaymentType = "CREDIT",
                MacLabel = m_strMacLabel,
                ManualEntry = m_bManualEntry
            };

            if (useCtroutd)
                creditPayRequest.Ctroutd = m_intCTroutD;

            creditPayRequest.TransAmount = transTotal;
            creditPayRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            creditPayRequest.Counter = m_intCounter;
            creditPayRequest.ValidateMessage();

            XDocument retailRequest = creditPayRequest.GetMessage();

            Console.WriteLine("Refunding Payment...");
            Console.WriteLine();

            WriteToStream(retailRequest, stream);
            XDocument retailResponse = ReadFromStream(stream);

            CreditPaymentResponse creditPayResponse = new CreditPaymentResponse(retailResponse);
        }

        /// <summary>
        /// Sends a request to void a transaction.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        public static void VoidPayment(NetworkStream stream)
        {
            VoidPaymentRequest voidPayRequest = new VoidPaymentRequest()
            {
                PaymentType = "CREDIT",
                MacLabel = m_strMacLabel,
                Ctroutd = m_intCTroutD
            };

            voidPayRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            voidPayRequest.Counter = m_intCounter;
            voidPayRequest.ValidateMessage();

            XDocument retailRequest = voidPayRequest.GetMessage();

            Console.WriteLine("Voiding Payment...");
            Console.WriteLine();

            WriteToStream(retailRequest, stream);
            XDocument retailResponse = ReadFromStream(stream);

            VoidPaymentResponse voidPayResponse = new VoidPaymentResponse(retailResponse);

            m_strTransactionStatus = "Voided";
        }

        #endregion

        #region SAF Request Message Methods

        /// <summary>
        /// Sends a Query SAF request to retrieve database records.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        public static void QuerySAF(NetworkStream stream)
        {
            QuerySAFRequest queryRequest = new QuerySAFRequest()
            {
                SafNumBegin = m_intSAFNumber,
                SafStatus = "ELIGIBLE",
                MacLabel = m_strMacLabel
            };

            queryRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            queryRequest.Counter = m_intCounter;
            queryRequest.ValidateMessage();

            XDocument safRequest = queryRequest.GetMessage();

            Console.WriteLine("Looking up the SAF message...");
            Console.WriteLine();

            WriteToStream(safRequest, stream);
            XDocument safResponse = ReadFromStream(stream);

            QuerySAFResponse queryResponse = new QuerySAFResponse(safResponse);
        }

        /// <summary>
        /// Sends a Remove SAF request to remove database records.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        public static void RemoveSAF(NetworkStream stream)
        {
            RemoveSAFRequest removeRequest = new RemoveSAFRequest()
            {
                SafNumBegin = m_intSAFNumber,
                MacLabel = m_strMacLabel
            };

            removeRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            removeRequest.Counter = m_intCounter;
            removeRequest.ValidateMessage();

            XDocument safRequest = removeRequest.GetMessage();

            Console.WriteLine("Removing SAF message...");
            Console.WriteLine();

            WriteToStream(safRequest, stream);
            XDocument safResponse = ReadFromStream(stream);

            RemoveSAFResponse removeResponse = new RemoveSAFResponse(safResponse);
        }

        #endregion

        #region Report Request Message Methods

        /// <summary>
        /// Sends a report request to retrieve a report.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        /// <param name="reportType">The Type of Report (Either DAYSUMMARY, PRESETTLEMENT, or TRANSEARCH)</param>
        public static void RunReport(NetworkStream stream, string reportType)
        {
            ReportRequest dailyRequest = new ReportRequest(reportType)
            {
                MacLabel = m_strMacLabel,
                MaxNumRecordsReturned = 1
            };

            DateTime current = DateTime.Now;

            // Set up the search values.
            dailyRequest.SearchFields.RequestCommand = "AUTH";
            dailyRequest.SearchFields.StartTransDate = current;
            dailyRequest.SearchFields.StartTransTime = new DateTime(current.Year, current.Month, current.Day, 0, 0, 0);
            dailyRequest.SearchFields.EndTransDate = current;
            dailyRequest.SearchFields.EndTransTime = new DateTime(current.Year, current.Month, current.Day, 23, 59, 59);

            // Selecte the response fields for the report.
            dailyRequest.ResponseFields.CtrOutd = "INCLUDE";
            dailyRequest.ResponseFields.Command = "INCLUDE";
            dailyRequest.ResponseFields.TransAmount = "INCLUDE";
            dailyRequest.ResponseFields.PaymentType = "INCLUDE";
            dailyRequest.ResponseFields.PaymentMedia = "INCLUDE";
            dailyRequest.ResponseFields.IntrnSeqNum = "INCLUDE";

            dailyRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            dailyRequest.Counter = m_intCounter;
            dailyRequest.ValidateMessage();

            XDocument reportRequest = dailyRequest.GetMessage();

            Console.WriteLine("Retrieving the report...");
            Console.WriteLine();

            WriteToStream(reportRequest, stream);
            XDocument reportResponse = ReadFromStream(stream);

            ReportResponse dailyResponse = new ReportResponse(reportResponse);
        }

        public static void RunSettlementSummary(NetworkStream stream)
        {
            SettlementSummaryRequest settleSummaryRequest = new SettlementSummaryRequest()
            {
                MacLabel = m_strMacLabel,
                MaxNumRecordsReturned = 1
            };

            // Set up the seach values for the settle summary request.
            settleSummaryRequest.StartSettleDate = DateTime.Now;
            settleSummaryRequest.EndSettleDate = DateTime.Now.AddDays(1);

            // Select the response fields for the summary.
            settleSummaryRequest.ResponseFields.CreditSaleCount = "INCLUDE";
            settleSummaryRequest.ResponseFields.SettleDate = "INCLUDE";
            settleSummaryRequest.ResponseFields.SettleCode = "INCLUDE";

            settleSummaryRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            settleSummaryRequest.Counter = m_intCounter;
            settleSummaryRequest.ValidateMessage();

            XDocument otherRequest3 = settleSummaryRequest.GetMessage();

            Console.WriteLine("Retrieving settlement summary...");
            Console.WriteLine();

            WriteToStream(otherRequest3, stream);
            XDocument otherResponse3 = ReadFromStream(stream);

            SettlementSummaryResponse settleSymmaryResponse = new SettlementSummaryResponse(otherResponse3);
        }

        /// <summary>
        /// Sends a Last Transaction request to get last known transaction.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        public static void RunLastTransaction(NetworkStream stream)
        {
            LastTransactionRequest lastTransReport = new LastTransactionRequest()
            {
                MacLabel = m_strMacLabel
            };

            lastTransReport.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            lastTransReport.Counter = m_intCounter;
            lastTransReport.ValidateMessage();

            XDocument otherRequest = lastTransReport.GetMessage();

            Console.WriteLine("Retrieving the last known transaction...");
            Console.WriteLine();

            WriteToStream(otherRequest, stream);
            XDocument otherResponse = ReadFromStream(stream);

            LastTransactionResponse lastTransResponse = new LastTransactionResponse(otherResponse);

            Console.WriteLine("Last Transaction: ");
            Console.WriteLine(otherResponse.ToString());
        }

        /// <summary>
        /// Sends a Duplicate Checking request to retrieve possible duplicate transactions.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        public static void DuplicateChecking(NetworkStream stream)
        {
            DuplicateCheckRequest dupCheckReport = new DuplicateCheckRequest()
            {
                MacLabel = m_strMacLabel
            };

            DateTime current = DateTime.Now;

            dupCheckReport.DupCheckDate = current;
            dupCheckReport.DupCheckFromTime = new DateTime(current.Year, current.Month, current.Day, 0, 0, 0);
            dupCheckReport.DupCheckToTime = new DateTime(current.Year, current.Month, current.Day, 23, 59, 59);

            dupCheckReport.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            dupCheckReport.Counter = m_intCounter;
            dupCheckReport.ValidateMessage();

            XDocument otherRequest = dupCheckReport.GetMessage();

            Console.WriteLine("Looking up duplicate checks...");
            Console.WriteLine();

            WriteToStream(otherRequest, stream);
            XDocument otherResponse = ReadFromStream(stream);

            DuplicateCheckResponse dupCheckResponse = new DuplicateCheckResponse(otherResponse);
        }

        #endregion

        #region Other Request Message Methods

        /// <summary>
        /// Sends a Duplicate Checking request to retrieve possible duplicate transactions.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        public static void ScheduleSettlement(NetworkStream stream)
        {
            SettleRequest settleRequest = new SettleRequest()
            {
                MacLabel = m_strMacLabel
            };

            settleRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            settleRequest.Counter = m_intCounter;
            settleRequest.ValidateMessage();

            XDocument otherRequest = settleRequest.GetMessage();

            Console.WriteLine("Scheduling a settlement...");
            Console.WriteLine();

            WriteToStream(otherRequest, stream);
            XDocument otherResponse = ReadFromStream(stream);

            SettleResponse settleResponse = new SettleResponse(otherResponse);
        }

        /// <summary>
        /// Sends a request to retrieve possible duplicate transactions.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        public static void RequestSignature(NetworkStream stream)
        {
            SignatureRequest signatureRequest = new SignatureRequest()
            {
                MacLabel = m_strMacLabel,
                DisplayText = "Example Text"
            };

            signatureRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            signatureRequest.Counter = m_intCounter;
            signatureRequest.ValidateMessage();

            XDocument otherRequest = signatureRequest.GetMessage();

            Console.WriteLine("Requesting digital signature...");
            Console.WriteLine();

            WriteToStream(otherRequest, stream);
            XDocument otherResponse = ReadFromStream(stream);

            SignatureResponse signatureResponse = new SignatureResponse(otherResponse);
        }

        #endregion

        #region Message Simulation Methods

        /// <summary>
        /// Iterates through a retail simulation.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        public static void RetailSimulation(NetworkStream stream)
        {
            
            // 1. Add Merchandise

            //Set Line Item IDs
            m_intLineItemID = 1;
            m_intOfferLineItemID = 2;

            AddLineItem(stream, true, false, m_intLineItemID);

            Console.WriteLine("Do you want to override the request?");
            string overrideChoice = Console.ReadLine();
            Console.WriteLine();
            if (overrideChoice.ToUpper() == "Y" || overrideChoice.ToUpper() == "YES")
            {
                // Override Item
                OverrideLineItem(stream, m_intLineItemID);
            }

            Console.WriteLine("Do you want to redeem the offer?");
            string redeemChoice = Console.ReadLine();
            Console.WriteLine();
            if (redeemChoice.ToUpper() == "Y" || redeemChoice.ToUpper() == "YES")
            {
                AddLineItem(stream, false, true, m_intOfferLineItemID);

                Console.WriteLine("Do you want to remove the offer?");
                string removeChoice = Console.ReadLine();
                Console.WriteLine();
                if (removeChoice.ToUpper() == "Y" || removeChoice.ToUpper() == "YES")
                {
                    RemoveLineItem(stream, m_fOfferLineItemTotal, m_intOfferLineItemID);
                    m_fTransTotal = m_fLineItemTotal;
                }
                else
                {
                    m_fTransTotal = m_fLineItemTotal - m_fOfferLineItemTotal;
                }
            }
            else
            {
                m_fTransTotal = m_fLineItemTotal;
            }


            //Console.WriteLine("Do you want to authorize a payment?");
            //string authCode = Console.ReadLine();
            //string authorize = String.Empty;
            //Console.WriteLine();

            //if(authCode.ToUpper() == "Y" || authCode.ToUpper() == "YES")
            //    authorize = true;

            // 2. Authorize a payment.
            AuthorizePayment(stream, m_fTransTotal, false);

            // 3. Capture a sales transaction

            CapturePayment(stream, m_fTransTotal, false, false);

            // 4. Refund or Void

            Console.WriteLine("Do you want to refund or void the transaction? (Default: 2)");
            Console.WriteLine("1 - Refund");
            Console.WriteLine("2 - Void");
            string choice2 = Console.ReadLine();
            Console.WriteLine();
            switch (choice2)
            {
                case "1":
                    CreditPayment(stream, m_fTransTotal, false);
                    break;
                case "2":
                default:
                    VoidPayment(stream);    
                    break;
            }
        }

        /// <summary>
        /// Iterates through gift card messages.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        public static void GiftCardSimulation(NetworkStream stream)
        {
            // 1. Activate Gift Card

            ActivateGiftCardPaymentRequest activateGiftRequest = new ActivateGiftCardPaymentRequest();

            // Enter in a gift card amount.
            Console.Write("Enter in the initial amount for gift card: ");
            string price = Console.ReadLine();

            while (!Regex.IsMatch(price, "^[0-9]{1,5}\\.[0-9]{2}$"))
            {
                Console.WriteLine("You must enter in a valid value for gift card amount!");
                Console.Write("Enter in the initial amount for gift card: ");
                price = Console.ReadLine();
            }
            Console.WriteLine();
            activateGiftRequest.TransAmount = float.Parse(price);

            activateGiftRequest.MacLabel = m_strMacLabel;
            activateGiftRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            activateGiftRequest.Counter = m_intCounter;
            activateGiftRequest.ValidateMessage();
            XDocument giftRequest1 = activateGiftRequest.GetMessage();

            Console.WriteLine("Activating Gift Card...");
            Console.WriteLine();

            WriteToStream(giftRequest1, stream);
            XDocument giftResponse1 = ReadFromStream(stream);

            ActivateGiftCardPaymentResponse activateGiftResponse = new ActivateGiftCardPaymentResponse(giftResponse1);

            // 2. Add Value to the gift card

            AddValueGiftCardPaymentRequest addValueGiftRequest = new AddValueGiftCardPaymentRequest();

            // Enter in a gift card amount.
            Console.Write("Enter in the amount to add to the gift card: ");
            string value = Console.ReadLine();

            while (!Regex.IsMatch(value, "^[0-9]{1,5}\\.[0-9]{2}$"))
            {
                Console.WriteLine("You must enter in a valid value for gift card amount!");
                Console.Write("Enter in the amount to add to the gift card: ");
                value = Console.ReadLine();
            }
            Console.WriteLine();
            addValueGiftRequest.TransAmount = float.Parse(value);

            addValueGiftRequest.MacLabel = m_strMacLabel;
            addValueGiftRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            addValueGiftRequest.Counter = m_intCounter;
            addValueGiftRequest.ValidateMessage();
            XDocument giftRequest2 = addValueGiftRequest.GetMessage();

            Console.WriteLine("Adding additional money to the gift card...");
            Console.WriteLine();

            WriteToStream(giftRequest2, stream);
            XDocument giftResponse2 = ReadFromStream(stream);

            AddValueGiftCardPaymentResponse addValueGiftResponse = new AddValueGiftCardPaymentResponse(giftResponse2);

            // 3. Balance Gift Card

            BalanceGiftCardPaymentRequest balanceGiftRequest = new BalanceGiftCardPaymentRequest();

            balanceGiftRequest.MacLabel = m_strMacLabel;
            balanceGiftRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            balanceGiftRequest.Counter = m_intCounter;
            balanceGiftRequest.ValidateMessage();
            XDocument giftRequest3 = balanceGiftRequest.GetMessage();

            Console.WriteLine("Retrieving gift card balance...");
            Console.WriteLine();

            WriteToStream(giftRequest3, stream);
            XDocument giftResponse3 = ReadFromStream(stream);

            BalanceGiftCardPaymentResponse balanceGiftResponse = new BalanceGiftCardPaymentResponse(giftResponse3);

            // 4. Cash Out Gift card

            CashOutPaymentRequest cashOutRequest = new CashOutPaymentRequest();

            cashOutRequest.MacLabel = m_strMacLabel;
            cashOutRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            cashOutRequest.Counter = m_intCounter;
            cashOutRequest.ValidateMessage();
            XDocument giftRequest4 = cashOutRequest.GetMessage();

            Console.WriteLine("Cashing out the gift card...");
            Console.WriteLine();

            WriteToStream(giftRequest4, stream);
            XDocument giftResponse4 = ReadFromStream(stream);

            CashOutPaymentResponse cashOutResponse = new CashOutPaymentResponse(giftResponse4);

            // 4. Deactivate Gift card

            DeactivateGiftCardPaymentRequest deactivateGiftCardRequest = new DeactivateGiftCardPaymentRequest();

            deactivateGiftCardRequest.MacLabel = m_strMacLabel;
            deactivateGiftCardRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            deactivateGiftCardRequest.Counter = m_intCounter;
            deactivateGiftCardRequest.ValidateMessage();
            XDocument giftRequest5 = deactivateGiftCardRequest.GetMessage();

            Console.WriteLine("Deactivating the gift card...");
            Console.WriteLine();

            WriteToStream(giftRequest5, stream);
            XDocument giftResponse5 = ReadFromStream(stream);

            DeactivateGiftCardPaymenResponse deactivateResponse = new DeactivateGiftCardPaymenResponse(giftResponse5);
        }

        /// <summary>
        /// Iterate through a Store and Forward Simulation
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        public static void StoreAndForwardSimulation(NetworkStream stream)
        {
            // 1. Query SAF
            QuerySAF(stream);

            // 2 Remove SAF
            RemoveSAF(stream);
        }

        /// <summary>
        /// Create a daily Summary report for the simulation.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        public static void ReportSimulation(NetworkStream stream)
        {

            // Create a Daily Report Request message.
            ReportRequest dailyRequest = new ReportRequest("DAYSUMMARY")
            {
                MacLabel = m_strMacLabel,
                MaxNumRecordsReturned = 1
            };

            DateTime current = DateTime.Now;

            // Set up the search values.
            dailyRequest.SearchFields.RequestCommand = "AUTH";
            dailyRequest.SearchFields.StartTransDate = current;
            dailyRequest.SearchFields.StartTransTime = new DateTime(current.Year, current.Month, current.Day, 0, 0, 0);
            dailyRequest.SearchFields.EndTransDate = current;
            dailyRequest.SearchFields.EndTransTime = new DateTime(current.Year, current.Month, current.Day, 23, 59, 59);

            // Selecte the response fields for the report.
            dailyRequest.ResponseFields.CtrOutd = "INCLUDE";
            dailyRequest.ResponseFields.Command = "INCLUDE";
            dailyRequest.ResponseFields.TransAmount = "INCLUDE";
            dailyRequest.ResponseFields.PaymentType = "INCLUDE";
            dailyRequest.ResponseFields.PaymentMedia = "INCLUDE";
            dailyRequest.ResponseFields.IntrnSeqNum = "INCLUDE";

            dailyRequest.Mac = UpdateCounter(ref m_intCounter, m_arrByteMacKey);
            dailyRequest.Counter = m_intCounter;
            dailyRequest.ValidateMessage();

            XDocument reportRequest = dailyRequest.GetMessage();

            Console.WriteLine("Retrieving the daily report...");
            Console.WriteLine();

            WriteToStream(reportRequest, stream);
            XDocument reportResponse = ReadFromStream(stream);

            ReportResponse dailyResponse = new ReportResponse(reportResponse);
        }

        /// <summary>
        /// Test other request messages that needs to be tested.
        /// </summary>
        /// <param name="stream">The socket stream.</param>
        public static void OtherMessageSimulation(NetworkStream stream)
        {
            // 1. Signature Request
            RequestSignature(stream);

            // 2. Settlement Request
            ScheduleSettlement(stream);

            // 3. Settlement Summary Request
            RunSettlementSummary(stream);

            // 4. Last Transaction Report
            RunLastTransaction(stream);

            // 5. Duplicate Check 
            DuplicateChecking(stream);
        }


        #endregion

        #region Helper Methods

        /// <summary>
        /// Updates the counter for each request message
        /// </summary>
        /// <param name="counter">the counter value</param>
        /// <param name="macKey">the key create a MAC based on the counter value</param>
        /// <returns>the MAC value for the next request message</returns>
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

        /// <summary>
        /// Encodes the RSA Public key after converting it into an ASN1 object.
        /// </summary>
        /// <param name="param">The Public Key parameters.</param>
        /// <returns>The Encoded Public Key</returns>
        public static string EncodePublicKey(RSAParameters param)
        {
            byte[] RSAID = { 0x30, 0xD, 0x6, 0x9, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0xD, 0x1, 0x1, 0x1, 0x5, 0x0 };
            List<byte> arrBinaryPublicKey = new List<byte>();

            arrBinaryPublicKey.InsertRange(0, param.Exponent); // EXPONENT
            arrBinaryPublicKey.Insert(0, (byte)arrBinaryPublicKey.Count);
            arrBinaryPublicKey.Insert(0, 0x2); // INTEGER

            arrBinaryPublicKey.InsertRange(0, param.Modulus); // MODULUS
            arrBinaryPublicKey.Insert(0, 0x0); // Leading Zero
            AppendLength(ref arrBinaryPublicKey, param.Modulus.Length+1);
            arrBinaryPublicKey.Insert(0, 0x2); // INTEGER

            AppendLength(ref arrBinaryPublicKey, arrBinaryPublicKey.Count);
            arrBinaryPublicKey.Insert(0, 0x30); // SEQUENCE

            arrBinaryPublicKey.Insert(0, 0x0); // Optional NULL value

            AppendLength(ref arrBinaryPublicKey, arrBinaryPublicKey.Count);

            arrBinaryPublicKey.Insert(0, 0x3); // BIT_STRNG
            arrBinaryPublicKey.InsertRange(0, RSAID); // RSA Algorithm ID

            AppendLength(ref arrBinaryPublicKey, arrBinaryPublicKey.Count);

            arrBinaryPublicKey.Insert(0, 0x30); // SEQUENCE

            return System.Convert.ToBase64String(arrBinaryPublicKey.ToArray());
        }

        /// <summary>
        /// Adds the length of the array into an array.
        /// </summary>
        /// <param name="arrBinaryData">The byte array</param>
        /// <param name="nLen">array Length</param>
        private static void AppendLength(ref List<byte> arrBinaryData, int nLen)
        {
            if (nLen <= byte.MaxValue)
            {
                arrBinaryData.Insert(0, Convert.ToByte(nLen));
                arrBinaryData.Insert(0, 0x81); //This byte means that the length fits in one byte
            }
            else
            {
                arrBinaryData.Insert(0, Convert.ToByte(nLen % (byte.MaxValue + 1)));
                arrBinaryData.Insert(0, Convert.ToByte(nLen / (byte.MaxValue + 1)));
                arrBinaryData.Insert(0, 0x82); //This byte means that the length fits in two byte
            }

        }

        #endregion

        #region XML Stream Methods

        /// <summary>
        /// Writes the request message to the socket's stream.
        /// </summary>
        /// <param name="request">the request message to be sent to the stream.</param>
        /// <param name="stream">The socket stream.</param>
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

        #endregion
    }
}
