using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PointPrototype
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
            /// Gets or sets the Batch Sequence Number
            /// </summary>
            public string BatchSeqNum { get; set; }

            /// <summary>
            /// Gets or sets the Error Transaction in Batch
            /// </summary>
            public string ErrTransInBatch { get; set; }

            /// <summary>
            /// Gets or sets the Settlement Date
            /// </summary>
            public string SettleDate { get; set; }

            /// <summary>
            /// Gets or sets the Settlement Code
            /// </summary>
            public string SettleCode { get; set; }

            /// <summary>
            /// Gets or sets the Credit Sale Amount
            /// </summary>
            public string CreditSaleAmount { get; set; }

            /// <summary>
            /// Gets or sets the Credit Sale Count
            /// </summary>
            public string CreditSaleCount { get; set; }

            /// <summary>
            /// Gets or sets the Credit Credit Amount
            /// </summary>
            public string CreditCreditAmount { get; set; }

            /// <summary>
            /// Gets or sets the Credit Credit Count
            /// </summary>
            public string CreditCreditCount { get; set; }

            /// <summary>
            /// Gets or sets the Credit Void Amount
            /// </summary>
            public string CreditVoidAmount { get; set; }

            /// <summary>
            /// Gets or sets the Credit Void Count
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
        /// <param name="doc">The Report Response message.</param>
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
