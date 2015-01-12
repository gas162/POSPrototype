using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class CashOutPaymentRequest : PaymentRequest
    {
        /// <summary>
        /// Initializes an empty element of the Cash Out Payment Request message.
        /// </summary>
        public CashOutPaymentRequest()
            : base()
        {
            Command = "GIFT_CLOSE";
            PaymentType = "GIFT";
        }

        #region Properties

        /// <summary>
        /// Gets or sets the Payment Type
        /// </summary>
        [RequestAttributes(Order = 6, Type = "S", Name = "PAYMENT_TYPE", Required = true)]
        public override string PaymentType { get; set; }

        /// <summary>
        /// Gets or sets the Manual Entry flag
        /// </summary>
        [RequestAttributes(Order = 7, Type = "B", Name = "MANUAL_ENTRY", Required = false)]
        public bool? ManualEntry { get; set; }


        #endregion
    }
}
