using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class CreditPaymentRequest : PaymentRequest
    {
        /// <summary>
        /// Initializes an empty instance of the Credit Payment Request message.
        /// </summary>
        public CreditPaymentRequest()
            : base()
        {
            Command = "CREDIT";
            TransAmount = 0.0f;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the Payment Type
        /// </summary>
        [RequestAttributes(Order = 6, Type = "L", Name = "PAYMENT_TYPE", Required = true)]
        public override string PaymentType { get; set; }
        
        /// <summary>
        /// Gets or sets the Transaction Amount
        /// </summary>
        [RequestAttributes(Order = 7, Type = "F", Name = "TRANS_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float TransAmount { get; set; }

        /// <summary>
        /// Gets or sets the Manual Entry flag
        /// </summary>
        [RequestAttributes(Order = 8, Type = "B", Name = "MANUAL_ENTRY", Required = false)]
        public bool? ManualEntry { get; set; }

        /// <summary>
        /// Gets or sets the Client-specific Transaction Routing ID
        /// </summary>
        [RequestAttributes(Order = 9, Type = "N", Name = "CTROUTD", Min = 1, Max = 10, Required = false)]
        public int? Ctroutd { get; set; }

        #endregion
    }
}
