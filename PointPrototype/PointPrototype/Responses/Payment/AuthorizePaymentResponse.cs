using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class AuthorizePaymentResponse : PaymentResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Payment Response Message.
        /// </summary>
        public AuthorizePaymentResponse() : base()
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
        /// <param name="doc">The Payment Response Message.</param>
        public AuthorizePaymentResponse(XDocument doc)
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

        #region Properties

        /// <summary>
        /// Gets or sets the Payment Media
        /// </summary>
        public string PaymentMedia { get; set; }

        /// <summary>
        /// Gets or sets the Account Number
        /// </summary>
        public string AccNum { get; set; }

        /// <summary>
        /// Gets or sets the Authorization Code
        /// </summary>
        public string AuthCode { get; set; }

        /// <summary>
        /// Gets or sets the Available Balance
        /// </summary>
        public float AvailableBalance { get; set; }

        /// <summary>
        /// Gets or sets the Approved Amount
        /// </summary>
        public float ApprovedAmount { get; set; }

        /// <summary>
        /// Gets or sets the Cardholder
        /// </summary>
        public string Cardholder { get; set; }

        /// <summary>
        /// Gets or sets the CVV2 Code
        /// </summary>
        public string CVV2Code { get; set; }


        #region Loyalty Properties

        /// <summary>
        /// Gets or sets the LoyaltyAccount Number
        /// </summary>
        public string LoyaltyAccNum { get; set; }

        /// <summary>
        /// Gets or sets the Loyalty Phone Number
        /// </summary>
        public int? LoyaltyPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the Loyalty Email
        /// </summary>
        public string LoyaltyEmail { get; set; }

        /// <summary>
        /// Gets or sets the Loyalty Token ID
        /// </summary>
        public string LoyaltyTokenID { get; set; }

        /// <summary>
        /// Gets or sets the Reward Receipt Text
        /// </summary>
        public string RewardReceiptText { get; set; }

        /// <summary>
        /// Gets or sets the Reward ID
        /// </summary>
        public string RewardID { get; set; }

        /// <summary>
        /// Gets or sets the Reward Amount
        /// </summary>
        public float? RewardAmount { get; set; }

        #endregion

        #region Signature Properties

        /// <summary>
        /// Gets or sets the Signature Data
        /// </summary>
        public byte[] SignatureData { get; set; }

        /// <summary>
        /// Gets or sets the Mime Type
        /// </summary>
        public string MimeType { get; set; }

        #endregion

        #endregion
    }
}
