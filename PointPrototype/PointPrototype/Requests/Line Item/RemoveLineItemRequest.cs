using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class RemoveLineItemRequest : RequestBase
    {
        /// <summary>
        /// Initializes an empty instance of the Remove Line Item Request message.
        /// </summary>
        public RemoveLineItemRequest()
            : base()
        {
            FunctionType = "LINE_ITEM";
            Command = "REMOVE";
            LineItemID = -1;
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
        /// Gets or sets the Running Tax Amount
        /// </summary>
        [RequestAttributes(Order = 7, Type = "F", Name = "RUNNING_TAX_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float RunningTaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the Running Transaction Amount
        /// </summary>
        [RequestAttributes(Order = 8, Type = "F", Name = "RUNNING_TRANS_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float RunningTransAmount { get; set; }

        #endregion
    }
}
