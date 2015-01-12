using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class OverrideLineItemRequest : RequestBase
    {
        /// <summary>
        /// Initializes an empty instance of the Override Line Item Request message.
        /// </summary>
        public OverrideLineItemRequest()
            : base()
        {
            FunctionType = "LINE_ITEM";
            Command = "OVERRIDE";
            LineItemID = -1;
            ExtendedPrice = 0.0f;
            RunningTaxAmount = 0.0f;
            RunningTransAmount = 0.0f;

            m_bDeviceRequired = true;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the Line Item ID
        /// </summary>
        [RequestAttributes(Order = 6, Type = "N", Name = "LINE_ITEM_ID", Min = 1, Max = 10, Required = true)]
        public int LineItemID { get; set; }

        /// <summary>
        /// Gets or sets the Quantity
        /// </summary>
        [RequestAttributes(Order = 7, Type = "N", Name = "QUANTITY", Min = 1, Max = 10, Required = false)]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the Unit Price
        /// </summary>
        [RequestAttributes(Order = 8, Type = "F", Name = "UNIT_PRICE", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = false)]
        public float UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the Extended Price
        /// </summary>
        [RequestAttributes(Order = 9, Type = "F", Name = "EXTENDED_PRICE", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float ExtendedPrice { get; set; }

        /// <summary>
        /// Gets or sets the Running Tax Amount
        /// </summary>
        [RequestAttributes(Order = 10, Type = "F", Name = "RUNNING_TAX_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float RunningTaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the Running Transaction Amount
        /// </summary>
        [RequestAttributes(Order = 11, Type = "F", Name = "RUNNING_TRANS_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float RunningTransAmount { get; set; }

        #endregion
    }
}
