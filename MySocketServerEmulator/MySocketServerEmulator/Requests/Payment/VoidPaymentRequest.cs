using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class VoidPaymentRequest : PaymentRequest
    {
        public VoidPaymentRequest()
            : base()
        {
            Command = "VOID";
            Ctroutd = -1;
        }

        public VoidPaymentRequest(XDocument doc)
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            Ctroutd = int.Parse(transactionElement.Element("CTROUTD").Value);
        }

        #region Properties

        /// <summary>
        /// Payment Type
        /// </summary>
        [RequestAttributes(Order = 6, Type = "L", Name = "PAYMENT_TYPE", Required = true)]
        public override string PaymentType { get; set; }

        /// <summary>
        /// Ctroutd
        /// </summary>
        [RequestAttributes(Order = 7, Type = "N", Name = "CTROUTD", Min = 1, Max = 10, Required = true)]
        public int Ctroutd { get; set; }

        #endregion
    }
}
