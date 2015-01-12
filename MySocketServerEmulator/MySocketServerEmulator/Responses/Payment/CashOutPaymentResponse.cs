using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace MySocketServerEmulator
{
    public class CashOutPaymentResponse : PaymentResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Payment Response Message.
        /// </summary>
        public CashOutPaymentResponse() : base()
        {
            PaymentMedia = String.Empty;
            AccNum = String.Empty;
            AvailableBalance = 0.0f;
        }

        /// <summary>
        /// Initializes an instance of the Payment Response Message.
        /// </summary>
        /// <param name="doc">the Payment Response message.</param>
        public CashOutPaymentResponse(XDocument doc)
            : base(doc)
        {
            XElement responseMessage = doc.Element("RESPONSE");

            PaymentMedia = responseMessage.Element("PAYMENT_MEDIA").Value;
            AccNum = responseMessage.Element("ACCT_NUM").Value;
            AvailableBalance = float.Parse(responseMessage.Element("AVAILABLE_BALANCE").Value);
            
        }

        #endregion

        #region Methods

        public override List<XElement> GeneratePaymentortion()
        {
            List<XElement> listPaymentElements = new List<XElement>();

            listPaymentElements.Add(new XElement("PAYMENT_MEDIA", PaymentMedia));
            listPaymentElements.Add(new XElement("ACCT_NUM", AccNum));
            listPaymentElements.Add(new XElement("AVAILABLE_BALANCE", AvailableBalance.ToString("0.00")));

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
        /// Available Balance
        /// </summary>
        public float AvailableBalance { get; set; }


        #endregion
    }
}
