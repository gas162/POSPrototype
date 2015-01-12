using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class CreditPaymentRequest : PaymentRequest
    {

        public CreditPaymentRequest()
            : base()
        {
            Command = "CREDIT";
            TransAmount = 0.0f;
        }

        public CreditPaymentRequest(XDocument doc)
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            TransAmount = float.Parse(transactionElement.Element("TRANS_AMOUNT").Value);

            if (transactionElement.Element("MANUAL_ENTRY") != null)
                ManualEntry = transactionElement.Element("MANUAL_ENTRY").Value == "TRUE" ? true : false;
            if (transactionElement.Element("CTROUTD") != null)
                Ctroutd = int.Parse(transactionElement.Element("CTROUTD").Value);
        }

        #region Properties

        /// <summary>
        /// Payment Type
        /// </summary>
        [RequestAttributes(Order = 6, Type = "L", Name = "PAYMENT_TYPE", Required = true)]
        public override string PaymentType { get; set; }
        
        /// <summary>
        /// Trans Amount
        /// </summary>
        [RequestAttributes(Order = 7, Type = "F", Name = "TRANS_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float TransAmount { get; set; }

        /// <summary>
        /// Payment Type
        /// </summary>
        [RequestAttributes(Order = 8, Type = "B", Name = "MANUAL_ENTRY", Required = false)]
        public bool? ManualEntry { get; set; }

        /// <summary>
        /// Ctroutd
        /// </summary>
        [RequestAttributes(Order = 9, Type = "N", Name = "CTROUTD", Min = 1, Max = 10, Required = false)]
        public int? Ctroutd { get; set; }

        #endregion
    }
}
