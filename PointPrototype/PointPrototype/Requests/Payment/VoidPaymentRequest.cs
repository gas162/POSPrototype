using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class VoidPaymentRequest : PaymentRequest
    {
        /// <summary>
        /// Initializes an empty instance of the Void Payment Request Message.
        /// </summary>
        public VoidPaymentRequest()
            : base()
        {
            Command = "VOID";
            Ctroutd = -1;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the Payment Type
        /// </summary>
        [RequestAttributes(Order = 6, Type = "L", Name = "PAYMENT_TYPE", Required = true)]
        public override string PaymentType { get; set; }

        /// <summary>
        /// Gets or sets the Client-specific Transaction Routing ID
        /// </summary>
        [RequestAttributes(Order = 7, Type = "N", Name = "CTROUTD", Min = 1, Max = 10, Required = true)]
        public int Ctroutd { get; set; }

        #endregion
    }
}
