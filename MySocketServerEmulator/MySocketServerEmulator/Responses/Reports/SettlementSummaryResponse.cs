using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{

    public class SettlementResponseFieldRecord
    {
        public SettlementResponseFieldRecord()
        {

        }

        public SettlementResponseFieldRecord(XElement recordElement)
        {

            if (recordElement.Element("BATCH_SEQ_NUM") != null)
                BatchSeqNum = recordElement.Element("BATCH_SEQ_NUM").Value;

            if (recordElement.Element("ERR_TRANS_IN_BATCH") != null)
                ErrTransInBatch = recordElement.Element("ERR_TRANS_IN_BATCH").Value;

            if (recordElement.Element("SETTLE_DATE") != null)
                SettleDate = recordElement.Element("SETTLE_DATE").Value;

            if (recordElement.Element("SETTLE_CODE") != null)
                SettleCode = recordElement.Element("SETTLE_CODE").Value;

            if (recordElement.Element("CRDT_SALE_AMT") != null)
                CreditSaleAmount = recordElement.Element("CRDT_SALE_AMT").Value;

            if (recordElement.Element("CRDT_SALE_CNT") != null)
                CreditSaleCount = recordElement.Element("CRDT_SALE_CNT").Value;

            if (recordElement.Element("CRDT_CRDT_AMT") != null)
                CreditCreditAmount = recordElement.Element("CRDT_CRDT_AMT").Value;

            if (recordElement.Element("CRDT_CRDT_CNT") != null)
                CreditCreditCount = recordElement.Element("CRDT_CRDT_CNT").Value;

            if (recordElement.Element("CRDT_VOID_AMT") != null)
                CreditVoidAmount = recordElement.Element("CRDT_VOID_AMT").Value;

            if (recordElement.Element("CRDT_VOID_CNT") != null)
                CreditVoidCount = recordElement.Element("CRDT_VOID_CNT").Value;

        }
        #region Properties

            /// <summary>
            /// Batch Sequence Number
            /// </summary>
            public string BatchSeqNum { get; set; }

            /// <summary>
            /// Error Transaction in Batch
            /// </summary>
            public string ErrTransInBatch { get; set; }

            /// <summary>
            /// Settle Date
            /// </summary>
            public string SettleDate { get; set; }

            /// <summary>
            /// Settle Code
            /// </summary>
            public string SettleCode { get; set; }

            /// <summary>
            /// Credit Sale Amount
            /// </summary>
            public string CreditSaleAmount { get; set; }

            /// <summary>
            /// Credit Sale Count
            /// </summary>
            public string CreditSaleCount { get; set; }

            // <summary>
            /// Credit Credit Amount
            /// </summary>
            public string CreditCreditAmount { get; set; }

            /// <summary>
            /// Credit Credit Count
            /// </summary>
            public string CreditCreditCount { get; set; }

            // <summary>
            /// Credit Void Amount
            /// </summary>
            public string CreditVoidAmount { get; set; }

            /// <summary>
            /// Credit Void Count
            /// </summary>
            public string CreditVoidCount { get; set; }

            #endregion
    }

    public class SettlementSummaryResponse : ResponseBase
    {
        private List<SettlementResponseFieldRecord> m_listResponseRecords;

        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Report Response Message.
        /// </summary>
        public SettlementSummaryResponse()
            : base()
        {
            ClientID = String.Empty;
            m_listResponseRecords = new List<SettlementResponseFieldRecord>();
        }

        /// <summary>
        /// Initializes an instance of the Report Response Message.
        /// </summary>
        /// <param name="doc">the Report Response message.</param>
        public SettlementSummaryResponse(XDocument doc)
            : base(doc)
        {
            XElement responseMessage = doc.Element("RESPONSE");

            ClientID = responseMessage.Element("CLIENT_ID").Value;

            m_listResponseRecords = new List<SettlementResponseFieldRecord>();

            if (responseMessage.Element("RECORDS").HasElements)
            {
                foreach (XElement responseElement in responseMessage.Element("RECORDS").Elements("RECORD"))
                {
                    SettlementResponseFieldRecord record = new SettlementResponseFieldRecord(responseElement);
                    m_listResponseRecords.Add(record);
                }
            }
        }

        #endregion

        #region Methods

        protected override List<XElement> GenerateMessagePortion()
        {
            List<XElement> listClassSpecificElements = new List<XElement>();

            listClassSpecificElements.Add(new XElement("CLIENT_ID", ClientID));
            listClassSpecificElements.Add(new XElement("NUM_RECORDS_FOUND", m_listResponseRecords.Count));
            XElement recordsElement = new XElement("RECORDS");

            if (m_listResponseRecords.Count > 0)
            {
                foreach (SettlementResponseFieldRecord record in m_listResponseRecords)
                {
                    XElement recordElement = new XElement("RECORD");

                    if (!String.IsNullOrEmpty(record.BatchSeqNum))
                        recordElement.Add(new XElement("BATCH_SEQ_NUM", record.BatchSeqNum));

                    if (!String.IsNullOrEmpty(record.ErrTransInBatch))
                        recordElement.Add(new XElement("ERR_TRANS_IN_BATCH", record.ErrTransInBatch));

                    if (!String.IsNullOrEmpty(record.SettleDate))
                        recordElement.Add(new XElement("SETTLE_DATE", record.SettleDate));

                    if (!String.IsNullOrEmpty(record.SettleCode))
                        recordElement.Add(new XElement("SETTLE_CODE", record.SettleCode));

                    if (!String.IsNullOrEmpty(record.CreditSaleAmount))
                        recordElement.Add(new XElement("CRDT_SALE_AMT", record.CreditSaleAmount));

                    if (!String.IsNullOrEmpty(record.CreditSaleCount))
                        recordElement.Add(new XElement("CRDT_SALE_CNT", record.CreditSaleCount));

                    if (!String.IsNullOrEmpty(record.CreditCreditAmount))
                        recordElement.Add(new XElement("CRDT_CRDT_AMT", record.CreditCreditAmount));

                    if (!String.IsNullOrEmpty(record.CreditCreditCount))
                        recordElement.Add(new XElement("CRDT_CRDT_CNT", record.CreditCreditCount));

                    if (!String.IsNullOrEmpty(record.CreditVoidAmount))
                        recordElement.Add(new XElement("CRDT_VOID_AMT", record.CreditVoidAmount));

                    if (!String.IsNullOrEmpty(record.CreditVoidCount))
                        recordElement.Add(new XElement("CRDT_VOID_CNT", record.CreditVoidCount));

                    recordsElement.Add(recordElement);
                }
            }

            listClassSpecificElements.Add(recordsElement);

            return listClassSpecificElements;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Client ID
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// Gets the list of Response Records.
        /// </summary>
        public List<SettlementResponseFieldRecord> ResponseRecords
        {
            get
            {
                return m_listResponseRecords;
            }
        }

        #endregion
    }
}
