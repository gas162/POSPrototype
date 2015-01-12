using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class RequestSettlementResponseField
        {
            public RequestSettlementResponseField()
            {

            }

            #region Properties

            /// <summary>
            /// Batch Sequence Number
            /// </summary>
            [RequestAttributes(Order = 1, Type = "L", Name = "BATCH_SEQ_NUM", Required = false)]
            public string BatchSeqNum { get; set; }

            /// <summary>
            /// Error Transaction in Batch
            /// </summary>
            [RequestAttributes(Order = 2, Type = "L", Name = "ERR_TRANS_IN_BATCH", Required = false)]
            public string ErrTransInBatch { get; set; }

            /// <summary>
            /// Settle Date
            /// </summary>
            [RequestAttributes(Order = 3, Type = "L", Name = "SETTLE_DATE", Required = false)]
            public string SettleDate { get; set; }

            /// <summary>
            /// Settle Code
            /// </summary>
            [RequestAttributes(Order = 4, Type = "L", Name = "SETTLE_CODE", Required = false)]
            public string SettleCode { get; set; }

            /// <summary>
            /// Credit Sale Amount
            /// </summary>
            [RequestAttributes(Order = 5, Type = "L", Name = "CRDT_SALE_AMT", Required = false)]
            public string CreditSaleAmount { get; set; }

            /// <summary>
            /// Credit Sale Count
            /// </summary>
            [RequestAttributes(Order = 6, Type = "L", Name = "CRDT_SALE_CNT", Required = false)]
            public string CreditSaleCount { get; set; }

            // <summary>
            /// Credit Credit Amount
            /// </summary>
            [RequestAttributes(Order = 7, Type = "L", Name = "CRDT_CRDT_AMT", Required = false)]
            public string CreditCreditAmount { get; set; }

            /// <summary>
            /// Credit Credit Count
            /// </summary>
            [RequestAttributes(Order = 8, Type = "L", Name = "CRDT_CRDT_CNT", Required = false)]
            public string CreditCreditCount { get; set; }

            // <summary>
            /// Credit Void Amount
            /// </summary>
            [RequestAttributes(Order = 9, Type = "L", Name = "CRDT_VOID_AMT", Required = false)]
            public string CreditVoidAmount { get; set; }

            /// <summary>
            /// Credit Void Count
            /// </summary>
            [RequestAttributes(Order = 10, Type = "L", Name = "CRDT_VOID_CNT", Required = false)]
            public string CreditVoidCount { get; set; }

            #endregion
        }
    
    public class SettlementSummaryRequest : RequestBase
    {


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

        public SettlementSummaryRequest(XDocument doc)
            : base(doc)
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            MaxNumRecordsReturned = int.Parse(transactionElement.Element("MAX_NUM_RECORDS_RETURNED").Value);

            if (transactionElement.Element("START_SETTLE_DATE") != null)
                StartSettleDate = DateTime.Parse(transactionElement.Element("START_SETTLE_DATE").Value);
            if (transactionElement.Element("END_SETTLE_DATE") != null)
                EndSettleDate = DateTime.Parse(transactionElement.Element("END_SETTLE_DATE").Value);

            ResponseFields = new RequestSettlementResponseField();
            XElement responseElement = transactionElement.Element("RESPONSEFIELDS");

            if (responseElement.Element("BATCH_SEQ_NUM") != null)
                ResponseFields.BatchSeqNum = responseElement.Element("BATCH_SEQ_NUM").Value;
            if (responseElement.Element("ERR_TRANS_IN_BATCH") != null)
                ResponseFields.ErrTransInBatch = responseElement.Element("ERR_TRANS_IN_BATCH").Value;
            if (responseElement.Element("SETTLE_DATE") != null)
                ResponseFields.SettleDate = responseElement.Element("SETTLE_DATE").Value;
            if (responseElement.Element("SETTLE_CODE") != null)
                ResponseFields.SettleCode = responseElement.Element("SETTLE_CODE").Value;
            if (responseElement.Element("CRDT_SALE_AMT") != null)
                ResponseFields.CreditSaleAmount = responseElement.Element("CRDT_SALE_AMT").Value;
            if (responseElement.Element("CRDT_SALE_CNT") != null)
                ResponseFields.CreditSaleCount = responseElement.Element("CRDT_SALE_CNT").Value;
            if (responseElement.Element("CRDT_CRDT_AMT") != null)
                ResponseFields.CreditCreditAmount = responseElement.Element("CRDT_CRDT_AMT").Value;
            if (responseElement.Element("CRDT_CRDT_CNT") != null)
                ResponseFields.CreditCreditCount = responseElement.Element("CRDT_CRDT_CNT").Value;
            if (responseElement.Element("CRDT_VOID_AMT") != null)
                ResponseFields.CreditVoidAmount = responseElement.Element("CRDT_VOID_AMT").Value;
            if (responseElement.Element("CRDT_VOID_CNT") != null)
                ResponseFields.CreditVoidCount = responseElement.Element("CRDT_VOID_CNT").Value;
        }

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
        /// Maximum Number of Records Returned
        /// </summary>
        [RequestAttributes(Order = 6, Type = "N", Name = "MAX_NUM_RECORDS_RETURNED", Min = 1, Max = 4, Required = true)]
        public int MaxNumRecordsReturned { get; set; }

        /// <summary>
        /// Response Fields
        /// </summary>
        [RequestAttributes(Order = 7, Type = "L", Name = "RESPONSEFIELDS", Required = true)]
        public RequestSettlementResponseField ResponseFields { get; set; }

        /// <summary>
        /// Start Settle Date
        /// </summary>
        [RequestAttributes(Order = 8, Type = "D", Name = "START_SETTLE_DATE", Required = true)]
        public DateTime StartSettleDate { get; set; }

        /// <summary>
        /// End Settle Date
        /// </summary>
        [RequestAttributes(Order = 9, Type = "D", Name = "END_SETTLE_DATE", Required = true)]
        public DateTime EndSettleDate { get; set; }

        #endregion
    }
}
