using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class ActivateGiftCardPaymentResponse : PaymentResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Payment Response Message.
        /// </summary>
        public ActivateGiftCardPaymentResponse() : base()
        {
            PaymentMedia = String.Empty;
            AccNum = String.Empty;
            AvailableBalance = 0.0f;
        }

        /// <summary>
        /// Initializes an instance of the Payment Response Message.
        /// </summary>
        /// <param name="doc">The Payment Response Message.</param>
        public ActivateGiftCardPaymentResponse(XDocument doc)
            : base(doc)
        {
            XElement responseMessage = doc.Element("RESPONSE");

            PaymentMedia = responseMessage.Element("PAYMENT_MEDIA").Value;
            AccNum = responseMessage.Element("ACCT_NUM").Value;
            AvailableBalance = float.Parse(responseMessage.Element("AVAILABLE_BALANCE").Value);
            
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
        /// Gets or sets the Available Balance
        /// </summary>
        public float AvailableBalance { get; set; }


        #endregion
    }
}
