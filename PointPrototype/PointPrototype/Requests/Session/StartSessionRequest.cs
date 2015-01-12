using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace PointPrototype
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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Merchant Invoice Number
        /// </summary>
        [RequestAttributes(Order = 6, Type = "C", Name = "INVOICE", Min = 1, Max = 6, Required = true)]
        public string Invoice { get; set; }

        /// <summary>
        /// Gets or sets the Store Number
        /// </summary>
        [RequestAttributes(Order = 7, Type = "C", Name = "STORE_NUM", Min = 1, Max = 10, Required = false)]
        public string StoreNum { get; set; }

        /// <summary>
        /// Gets or sets the Lane
        /// </summary>
        [RequestAttributes(Order = 8, Type = "C", Name = "LANE", Min = 1, Max = 20, Required = false)]
        public string Lane { get; set; }

        /// <summary>
        /// Gets or sets the Cashier ID
        /// </summary>
        [RequestAttributes(Order = 9, Type = "N", Name = "CASHIER_ID", Min = 1, Max = 3, Required = false)]
        public int? CashierID { get; set; }

        /// <summary>
        /// Gets or sets the Server ID
        /// </summary>
        [RequestAttributes(Order = 10, Type = "N", Name = "SERVER_ID", Min = 1, Max = 3, Required = false)]
        public int? ServerID { get; set; }

        /// <summary>
        /// Gets or sets the Shift ID
        /// </summary>
        [RequestAttributes(Order = 11, Type = "C", Name = "SHIFT_ID", Min = 1, Max = 1, Required = false)]
        public string ShiftID { get; set; }

        /// <summary>
        /// Gets or sets the Table Number
        /// </summary>
        [RequestAttributes(Order = 12, Type = "N", Name = "TABLE_NUM", Min = 1, Max = 5, Required = false)]
        public int? TableNum { get; set; }

        /// <summary>
        /// Gets or sets the Purchase ID (Needed for Level II Processing)
        /// </summary>
        [RequestAttributes(Order = 13, Type = "N", Name = "PURCHASE_ID", Min = 1, Max = 17, Required = false)]
        public int? PurchaseID { get; set; }

        #endregion
    }
}
