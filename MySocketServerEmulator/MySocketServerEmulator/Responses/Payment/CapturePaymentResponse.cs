using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace MySocketServerEmulator
{
    public class CapturePaymentResponse : PaymentResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Payment Response Message.
        /// </summary>
        public CapturePaymentResponse() : base()
        {
            PaymentMedia = String.Empty;
            AccNum = String.Empty;
            AuthCode = String.Empty;
            AvailableBalance = 0.0f;
            ApprovedAmount = 0.0f;
            Cardholder = String.Empty;
            CVV2Code = String.Empty;
        }

        /// <summary>
        /// Initializes an instance of the Payment Response Message.
        /// </summary>
        /// <param name="doc">the Payment Response message.</param>
        public CapturePaymentResponse(XDocument doc)
            : base(doc)
        {
            XElement responseMessage = doc.Element("RESPONSE");

            PaymentMedia = responseMessage.Element("PAYMENT_MEDIA").Value;
            AccNum = responseMessage.Element("ACCT_NUM").Value;
            AuthCode = responseMessage.Element("AUTH_CODE").Value;
            AvailableBalance = float.Parse(responseMessage.Element("AVAILABLE_BALANCE").Value);
            ApprovedAmount = float.Parse(responseMessage.Element("APPROVED_AMOUNT").Value);
            Cardholder = responseMessage.Element("CARDHOLDER").Value;
            CVV2Code = responseMessage.Element("CVV2_CODE").Value;

            if (responseMessage.Element("SAF_NUM") != null)
            {
                SafNum = int.Parse(responseMessage.Element("SAF_NUM").Value);
            }

            if (responseMessage.Element("LTY_ACCT_NUM") != null)
            {
                LoyaltyAccNum = responseMessage.Element("LTY_ACCT_NUM").Value;
                if (responseMessage.Element("LTY_PHONE") != null)
                    LoyaltyPhoneNumber = int.Parse(responseMessage.Element("LTY_PHONE").Value);
                if (responseMessage.Element("LTY_EMAIL") != null)
                    LoyaltyEmail = responseMessage.Element("LTY_EMAIL").Value;
                if (responseMessage.Element("LTY_TOKEN_ID") != null)
                    LoyaltyTokenID = responseMessage.Element("LTY_TOKEN_ID").Value;
                if (responseMessage.Element("REWARD_RECEIPT_TEXT") != null)
                    RewardReceiptText = responseMessage.Element("REWARD_RECEIPT_TEXT").Value;
                if (responseMessage.Element("REWARD_ID") != null)
                    RewardID = responseMessage.Element("REWARD_ID").Value;
                if (responseMessage.Element("REWARD_AMOUNT") != null)
                    RewardAmount = float.Parse(responseMessage.Element("REWARD_AMOUNT").Value);
            }

            if (responseMessage.Element("SIGNATUREDATA") != null)
            {
                string signature = responseMessage.Element("SIGNATUREDATA").Value;
                SignatureData = Convert.FromBase64String(signature);

                MimeType = responseMessage.Element("MIME_TYPE").Value;
            }
        }

        #endregion

        #region Methods

        public override List<XElement> GeneratePaymentortion()
        {
            List<XElement> listPaymentElements = new List<XElement>();

            listPaymentElements.Add(new XElement("PAYMENT_MEDIA", PaymentMedia));
            listPaymentElements.Add(new XElement("ACCT_NUM", AccNum));
            listPaymentElements.Add(new XElement("AUTH_CODE", AuthCode));
            listPaymentElements.Add(new XElement("AVAILABLE_BALANCE", AvailableBalance.ToString("0.00")));
            listPaymentElements.Add(new XElement("APPROVED_AMOUNT", ApprovedAmount.ToString("0.00")));
            listPaymentElements.Add(new XElement("CARDHOLDER", Cardholder));
            listPaymentElements.Add(new XElement("CVV2_CODE", CVV2Code));

            if (SafNum != null)
            {
                listPaymentElements.Add(new XElement("SAF_NUM", SafNum));
            }

            if (!String.IsNullOrEmpty(LoyaltyAccNum))
            {
                listPaymentElements.Add(new XElement("LTY_ACCT_NUM", LoyaltyAccNum));
                if (LoyaltyPhoneNumber == null)
                    listPaymentElements.Add(new XElement("LTY_PHONE", LoyaltyPhoneNumber));
                if (!String.IsNullOrEmpty(LoyaltyEmail))
                    listPaymentElements.Add(new XElement("LTY_EMAIL", LoyaltyEmail));
                if (!String.IsNullOrEmpty(LoyaltyTokenID))
                    listPaymentElements.Add(new XElement("LTY_TOKEN_ID", LoyaltyTokenID));
                if (!String.IsNullOrEmpty(RewardReceiptText))
                    listPaymentElements.Add(new XElement("REWARD_RECEIPT_TEXT", RewardReceiptText));
                if (!String.IsNullOrEmpty(RewardID))
                    listPaymentElements.Add(new XElement("REWARD_ID", RewardID));
                if (RewardAmount == null)
                    listPaymentElements.Add(new XElement("REWARD_AMOUNT", RewardAmount.Value.ToString("0.00")));
            }
            if (SignatureData != null)
            {
                listPaymentElements.Add(new XElement("SIGNATUREDATA", Convert.ToBase64String(SignatureData)));
                listPaymentElements.Add(new XElement("MIME_TYPE", MimeType));
            }


            return listPaymentElements;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Payment Media
        /// </summary>
        public string PaymentMedia { get; set; }

        /// <summary>
        /// Account Number
        /// </summary>
        public string AccNum { get; set; }

        /// <summary>
        /// Authorization Code
        /// </summary>
        public string AuthCode { get; set; }

        /// <summary>
        /// Available Balance
        /// </summary>
        public float AvailableBalance { get; set; }

        /// <summary>
        /// Approved Amount
        /// </summary>
        public float ApprovedAmount { get; set; }

        /// <summary>
        /// Cardholder
        /// </summary>
        public string Cardholder { get; set; }

        /// <summary>
        /// CVV2 Code
        /// </summary>
        public string CVV2Code { get; set; }

        /// <summary>
        /// SAF Number
        /// </summary>
        public int? SafNum { get; set; }


        // Loyalty Properties

        /// <summary>
        /// LoyaltyAccount Number
        /// </summary>
        public string LoyaltyAccNum { get; set; }

        /// <summary>
        /// Loyalty Phone Number
        /// </summary>
        public int? LoyaltyPhoneNumber { get; set; }

        /// <summary>
        /// Loyalty Email
        /// </summary>
        public string LoyaltyEmail { get; set; }

        /// <summary>
        /// Loyalty Token ID
        /// </summary>
        public string LoyaltyTokenID { get; set; }

        /// <summary>
        /// Reward Receipt Text
        /// </summary>
        public string RewardReceiptText { get; set; }

        /// <summary>
        /// Reward ID
        /// </summary>
        public string RewardID { get; set; }

        /// <summary>
        /// Reward Amount
        /// </summary>
        public float? RewardAmount { get; set; }


        // Signature Properties

        /// <summary>
        /// Signature Data
        /// </summary>
        public byte[] SignatureData { get; set; }

        /// <summary>
        /// Mime Type
        /// </summary>
        public string MimeType { get; set; }

        #endregion
    }
}
