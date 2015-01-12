using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace MySocketServerEmulator
{
    public class VoidPaymentResponse : PaymentResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Void Payment Response Message.
        /// </summary>
        public VoidPaymentResponse() : base()
        {
        }

        /// <summary>
        /// Initializes an instance of the Void Payment Response Message.
        /// </summary>
        /// <param name="doc">the Void Payment Response message.</param>
        public VoidPaymentResponse(XDocument doc)
            : base(doc)
        {
        }

        #endregion
    }
}
