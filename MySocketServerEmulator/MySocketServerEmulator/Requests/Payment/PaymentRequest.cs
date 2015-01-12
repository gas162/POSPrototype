using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class PaymentRequest : RequestBase
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Start Session request message.
        /// </summary>
        public PaymentRequest()
            : base()
        {
            FunctionType = "PAYMENT";
            PaymentType = String.Empty;
            m_bDeviceRequired = true;
        }

        public PaymentRequest(XDocument doc)
            : base(doc)
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            if(transactionElement.Element("PAYMENT_TYPE") != null)
                PaymentType = transactionElement.Element("PAYMENT_TYPE").Value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Payment Type
        /// </summary>
        [RequestAttributes(Order = 6, Type = "L", Name = "PAYMENT_TYPE",  Required = true)]
        public virtual string PaymentType { get; set; }

        #endregion
    }
}
