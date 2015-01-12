using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class CashOutPaymentRequest : PaymentRequest
    {
        public CashOutPaymentRequest()
            : base()
        {
            Command = "GIFT_CLOSE";
            PaymentType = "GIFT";
        }

        public CashOutPaymentRequest(XDocument doc)
            : base(doc)
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            if (transactionElement.Element("MANUAL_ENTRY") != null)
                ManualEntry = transactionElement.Element("MANUAL_ENTRY").Value == "TRUE" ? true : false;
        }

        #region Properties

        /// <summary>
        /// Payment Type
        /// </summary>
        [RequestAttributes(Order = 6, Type = "S", Name = "PAYMENT_TYPE", Required = true)]
        public override string PaymentType { get; set; }

        /// <summary>
        /// Payment Type
        /// </summary>
        [RequestAttributes(Order = 7, Type = "B", Name = "MANUAL_ENTRY", Required = false)]
        public bool? ManualEntry { get; set; }


        #endregion
    }
}
