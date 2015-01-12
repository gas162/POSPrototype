using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace MySocketServerEmulator
{
    public class StartSessionRequest : RequestBase
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Start Session request message.
        /// </summary>
        public StartSessionRequest()
            : base()
        {
            FunctionType = "SESSION";
            Command = "START";
            Invoice = String.Empty;
            StoreNum = String.Empty;
            Lane = String.Empty;
            CashierID = null;
            ServerID = null;
            ShiftID = String.Empty;
            TableNum = null;
            PurchaseID = null;
            m_bDeviceRequired = true;
        }

        public StartSessionRequest(XDocument doc)
            : base(doc)
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            Invoice = transactionElement.Element("INVOICE").Value;

            if (transactionElement.Element("STORE_NUM") != null)
                StoreNum = transactionElement.Element("STORE_NUM").Value;
            if (transactionElement.Element("LANE") != null)
                Lane = transactionElement.Element("LANE").Value;
            if (transactionElement.Element("CASHIER_ID") != null)
                CashierID = int.Parse(transactionElement.Element("CASHIER_ID").Value);
            if (transactionElement.Element("SERVER_ID") != null)
                ServerID = int.Parse(transactionElement.Element("SERVER_ID").Value);
            if (transactionElement.Element("SHIFT_ID") != null)
                ShiftID = transactionElement.Element("SHIFT_ID").Value;
            if (transactionElement.Element("TABLE_NUM") != null)
                TableNum = int.Parse(transactionElement.Element("TABLE_NUM").Value);
            if (transactionElement.Element("PURCHASE_ID") != null)
                PurchaseID = int.Parse(transactionElement.Element("PURCHASE_ID").Value);

            m_bDeviceRequired = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Merchant Invoice Number
        /// </summary>
        [RequestAttributes(Order = 6, Type = "C", Name = "INVOICE", Min = 1, Max = 6, Required = true)]
        public string Invoice { get; set; }

        /// <summary>
        /// Store Number
        /// </summary>
        [RequestAttributes(Order = 7, Type = "C", Name = "STORE_NUM", Min = 1, Max = 10, Required = false)]
        public string StoreNum { get; set; }

        /// <summary>
        /// Lane
        /// </summary>
        [RequestAttributes(Order = 8, Type = "C", Name = "LANE", Min = 1, Max = 20, Required = false)]
        public string Lane { get; set; }

        /// <summary>
        /// Cashier ID
        /// </summary>
        [RequestAttributes(Order = 9, Type = "N", Name = "CASHIER_ID", Min = 1, Max = 3, Required = false)]
        public int? CashierID { get; set; }

        /// <summary>
        /// Server ID
        /// </summary>
        [RequestAttributes(Order = 10, Type = "N", Name = "SERVER_ID", Min = 1, Max = 3, Required = false)]
        public int? ServerID { get; set; }

        /// <summary>
        /// Shift ID
        /// </summary>
        [RequestAttributes(Order = 11, Type = "C", Name = "SHIFT_ID", Min = 1, Max = 1, Required = false)]
        public string ShiftID { get; set; }

        /// <summary>
        /// Table Number
        /// </summary>
        [RequestAttributes(Order = 12, Type = "N", Name = "TABLE_NUM", Min = 1, Max = 5, Required = false)]
        public int? TableNum { get; set; }

        /// <summary>
        /// Purchase ID - Needed for Level II Processing
        /// </summary>
        [RequestAttributes(Order = 13, Type = "N", Name = "PURCHASE_ID", Min = 1, Max = 17, Required = false)]
        public int? PurchaseID { get; set; }

        #endregion
    }
}
