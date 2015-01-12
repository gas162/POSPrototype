using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class RemoveLineItemRequest : RequestBase
    {

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

        public RemoveLineItemRequest(XDocument doc)
            : base()
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            LineItemID = int.Parse(transactionElement.Element("LINE_ITEM_ID").Value);
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
        /// Running Tax Amount
        /// </summary>
        [RequestAttributes(Order = 7, Type = "F", Name = "RUNNING_TAX_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float RunningTaxAmount { get; set; }

        /// <summary>
        /// Running Transaction Amount
        /// </summary>
        [RequestAttributes(Order = 8, Type = "F", Name = "RUNNING_TRANS_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float RunningTransAmount { get; set; }

        #endregion
    }
}
