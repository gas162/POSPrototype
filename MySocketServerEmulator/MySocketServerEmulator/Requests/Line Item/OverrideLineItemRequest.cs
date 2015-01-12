using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class OverrideLineItemRequest : RequestBase
    {

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

        public OverrideLineItemRequest(XDocument doc)
            : base()
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            LineItemID = int.Parse(transactionElement.Element("LINE_ITEM_ID").Value);

            if (transactionElement.Element("QUANTITY") != null)
                Quantity = int.Parse(transactionElement.Element("QUANTITY").Value);
            if (transactionElement.Element("UNIT_PRICE") != null)
                UnitPrice = float.Parse(transactionElement.Element("UNIT_PRICE").Value);

            ExtendedPrice = float.Parse(transactionElement.Element("EXTENDED_PRICE").Value);
            RunningTaxAmount = float.Parse(transactionElement.Element("RUNNING_TAX_AMOUNT").Value);
            RunningTransAmount = float.Parse(transactionElement.Element("RUNNING_TRANS_AMOUNT").Value);
        }

        #region Properties

        /// <summary>
        /// Line Item ID
        /// </summary>
        [RequestAttributes(Order = 6, Type = "N", Name = "LINE_ITEM_ID", Min = 1, Max = 10, Required = true)]
        public int LineItemID { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        [RequestAttributes(Order = 7, Type = "N", Name = "QUANTITY", Min = 1, Max = 10, Required = false)]
        public int? Quantity { get; set; }

        /// <summary>
        /// Unit Price
        /// </summary>
        [RequestAttributes(Order = 8, Type = "F", Name = "UNIT_PRICE", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = false)]
        public float? UnitPrice { get; set; }

        /// <summary>
        /// Extended Price
        /// </summary>
        [RequestAttributes(Order = 9, Type = "F", Name = "EXTENDED_PRICE", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float ExtendedPrice { get; set; }

        /// <summary>
        /// Running Tax Amount
        /// </summary>
        [RequestAttributes(Order = 10, Type = "F", Name = "RUNNING_TAX_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float RunningTaxAmount { get; set; }

        /// <summary>
        /// Running Transaction Amount
        /// </summary>
        [RequestAttributes(Order = 11, Type = "F", Name = "RUNNING_TRANS_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float RunningTransAmount { get; set; }

        #endregion
    }
}
