using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class CapturePaymentRequest : PaymentRequest
    {
        /// <summary>
        /// Initializes an empty instance of the Capture Payment Request message.
        /// </summary>
        public CapturePaymentRequest()
            : base()
        {
            Command = "CAPTURE";
            TransAmount = 0.0f;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the Transaction Amount
        /// </summary>
        [RequestAttributes(Order = 6, Type = "F", Name = "TRANS_AMOUNT", Min=1, Max=5, DecimalMin=2, DecimalMax=2, Required = true)]
        public float TransAmount { get; set; }

        /// <summary>
        /// Gets or sets the Payment Type
        /// </summary>
        [RequestAttributes(Order = 7, Type = "L", Name = "PAYMENT_TYPE", Required = false)]
        public override string PaymentType { get; set; }

        /// <summary>
        /// Gets or sets the Authorization Code
        /// </summary>
        [RequestAttributes(Order = 8, Type = "C", Name = "AUTH_CODE", Min = 1, Max = 16, Required = false)]
        public string AuthCode { get; set; }

        /// <summary>
        /// Gets or sets the Manual Entry flag
        /// </summary>
        [RequestAttributes(Order = 9, Type = "B", Name = "MANUAL_ENTRY", Required = false)]
        public bool? ManualEntry { get; set; }

        /// <summary>
        /// Gets or sets the Client-specific Transaction Routing ID
        /// </summary>
        [RequestAttributes(Order = 10, Type = "N", Name = "CTROUTD", Min = 1, Max = 10, Required = false)]
        public int? Ctroutd { get; set; }

        /// <summary>
        /// Gets or sets the Force Flag
        /// </summary>
        [RequestAttributes(Order = 11, Type = "B", Name = "FORCE_FLAG", Required = false)]
        public bool? ForceFlag { get; set; }


        #region Level II Properties

        /// <summary>
        /// Gets or sets the Tax Amount
        /// </summary>
        [RequestAttributes(Order = 12, Type = "F", Name = "TAX_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = false)]
        public float? TaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the Tax Indicator
        /// </summary>
        [RequestAttributes(Order = 13, Type = "L", Name = "TAX_IND", Required = false)]
        public string TaxIndicator { get; set; }

        /// <summary>
        /// Gets or sets the Commecrial Flag
        /// </summary>
        [RequestAttributes(Order = 14, Type = "L", Name = "CMRCL_FLAG", Required = false)]
        public string CommecrialFlag { get; set; }

        #endregion

        #endregion
    }
}
