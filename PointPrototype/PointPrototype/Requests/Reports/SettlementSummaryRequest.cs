using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PointPrototype
{
    public class RequestSettlementResponseField
    {
        /// <summary>
        /// Initializes an empty instance of the Request Settlement Response Field object.
        /// </summary>
        public RequestSettlementResponseField()
        {

        }

        #region Properties

        /// <summary>
        /// Gets or sets the Batch Sequence Number
        /// </summary>
        [RequestAttributes(Order = 1, Type = "L", Name = "BATCH_SEQ_NUM", Required = false)]
        public string BatchSeqNum { get; set; }

        /// <summary>
        /// Gets or sets the Error Transaction in Batch
        /// </summary>
        [RequestAttributes(Order = 2, Type = "L", Name = "ERR_TRANS_IN_BATCH", Required = false)]
        public string ErrTransInBatch { get; set; }

        /// <summary>
        /// Gets or sets the Settlement Date
        /// </summary>
        [RequestAttributes(Order = 3, Type = "L", Name = "SETTLE_DATE", Required = false)]
        public string SettleDate { get; set; }

        /// <summary>
        /// Gets or sets the Settlement Code
        /// </summary>
        [RequestAttributes(Order = 4, Type = "L", Name = "SETTLE_CODE", Required = false)]
        public string SettleCode { get; set; }

        /// <summary>
        /// Gets or sets the Credit Sale Amount
        /// </summary>
        [RequestAttributes(Order = 5, Type = "L", Name = "CRDT_SALE_AMT", Required = false)]
        public string CreditSaleAmount { get; set; }

        /// <summary>
        /// Gets or sets the Credit Sale Count
        /// </summary>
        [RequestAttributes(Order = 6, Type = "L", Name = "CRDT_SALE_CNT", Required = false)]
        public string CreditSaleCount { get; set; }

        /// <summary>
        /// Gets or sets the Credit Credit Amount
        /// </summary>
        [RequestAttributes(Order = 7, Type = "L", Name = "CRDT_CRDT_AMT", Required = false)]
        public string CreditCreditAmount { get; set; }

        /// <summary>
        /// Gets or sets the Credit Credit Count
        /// </summary>
        [RequestAttributes(Order = 8, Type = "L", Name = "CRDT_CRDT_CNT", Required = false)]
        public string CreditCreditCount { get; set; }

        /// <summary>
        /// Gets or sets the Credit Void Amount
        /// </summary>
        [RequestAttributes(Order = 9, Type = "L", Name = "CRDT_VOID_AMT", Required = false)]
        public string CreditVoidAmount { get; set; }

        /// <summary>
        /// Gets or sets the Credit Void Count
        /// </summary>
        [RequestAttributes(Order = 10, Type = "L", Name = "CRDT_VOID_CNT", Required = false)]
        public string CreditVoidCount { get; set; }

        #endregion
    }
    
    public class SettlementSummaryRequest : RequestBase
    {

        /// <summary>
        /// Initializes an empty instance of the Settlement Summary Request Message.
        /// </summary>
        public SettlementSummaryRequest()
        {
            FunctionType = "REPORT";
            Command = "SETTLESUMMARY";
            MaxNumRecordsReturned = -1;

            ResponseFields = new RequestSettlementResponseField();

            StartSettleDate = DateTime.Now;
            EndSettleDate = DateTime.Now;

            m_bDeviceRequired = false;
        }

        /// <summary>
        /// Validates all fields within the Response Fields object.
        /// </summary>
        /// <returns>The RESPONSEFIELDS XML Element.</returns>
        public XElement ValidateResponseFields()
        {
            XElement responseElement = new XElement("RESPONSEFIELDS");
            Type fieldType = this.ResponseFields.GetType();
            int count = 0;

            PropertyInfo[] properties = fieldType.GetProperties().OrderBy(x => ((RequestAttributes)x.GetCustomAttribute(typeof(RequestAttributes))).Order).ToArray();
            foreach (PropertyInfo property in properties)
            {
                RequestAttributes attributes = (RequestAttributes)property.GetCustomAttribute(typeof(RequestAttributes));
                object obj = property.GetValue(this.ResponseFields, null);
                string strObj = obj != null ? obj.ToString() : String.Empty;

                if (strObj == "INCLUDE")
                {
                    responseElement.Add(new XElement(attributes.Name));
                    count++;
                }
            }

            // The RESPONSEFIELDS Element must have at least one value.
            if (count == 0)
            {
                responseElement = null;
            }

            return responseElement;
        }

        //TODO: Add code to see if MaxNumRecords is no greater than 8000.

        #region Properties

        /// <summary>
        /// Gets or sets the Maximum Number of Records Returned
        /// </summary>
        [RequestAttributes(Order = 6, Type = "N", Name = "MAX_NUM_RECORDS_RETURNED", Min = 1, Max = 4, Required = true)]
        public int MaxNumRecordsReturned { get; set; }

        /// <summary>
        /// Gets or sets the Response Fields
        /// </summary>
        [RequestAttributes(Order = 7, Type = "L", Name = "RESPONSEFIELDS", Required = true)]
        public RequestSettlementResponseField ResponseFields { get; set; }

        /// <summary>
        /// Gets or sets the Start Settle Date
        /// </summary>
        [RequestAttributes(Order = 8, Type = "D", Name = "START_SETTLE_DATE", Required = true)]
        public DateTime StartSettleDate { get; set; }

        /// <summary>
        /// Gets or sets the End Settle Date
        /// </summary>
        [RequestAttributes(Order = 9, Type = "D", Name = "END_SETTLE_DATE", Required = true)]
        public DateTime EndSettleDate { get; set; }

        #endregion
    }
}
