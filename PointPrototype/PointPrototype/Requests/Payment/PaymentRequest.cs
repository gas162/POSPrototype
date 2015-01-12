using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class PaymentRequest : RequestBase
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Payment Request message.
        /// </summary>
        public PaymentRequest()
            : base()
        {
            FunctionType = "PAYMENT";
            PaymentType = String.Empty;
            m_bDeviceRequired = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Payment Type
        /// </summary>
        [RequestAttributes(Order = 6, Type = "L", Name = "PAYMENT_TYPE",  Required = true)]
        public virtual string PaymentType { get; set; }

        #endregion
    }
}
