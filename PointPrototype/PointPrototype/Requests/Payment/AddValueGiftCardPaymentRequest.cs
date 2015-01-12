using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class AddValueGiftCardPaymentRequest : PaymentRequest
    {
        /// <summary>
        /// Initializes an empty instance of the Add Value Gift Card Payment Request message.
        /// </summary>
        public AddValueGiftCardPaymentRequest()
            : base()
        {
            Command = "ADD_VALUE";
            PaymentType = "GIFT";
            TransAmount = 0.0f;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the Payment Type
        /// </summary>
        [RequestAttributes(Order = 6, Type = "S", Name = "PAYMENT_TYPE", Required = true)]
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


        #endregion
    }
}
