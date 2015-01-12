using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PointPrototype
{
    public class ReportResponse : ResponseBase
    {
        public class ResponseFieldRecord
        {
            /// <summary>
            /// Initializes an empty instance of a Response Field Record.
            /// </summary>
            public ResponseFieldRecord()
            {

            }

            /// <summary>
            /// Initializes an instance of a Response Field Record.
            /// </summary>
            /// <param name="recordElement">An XML element reporesenting a Response Field Record.</param>
            public ResponseFieldRecord(XElement recordElement)
            {
                if (recordElement.Element("INTRN_SEQ_NUM") != null)
                    IntrnSeqNum = recordElement.Element("INTRN_SEQ_NUM").Value;

                if (recordElement.Element("PROCESSOR_ID") != null)
                    ProcessorID = recordElement.Element("PROCESSOR_ID").Value;

                if (recordElement.Element("BATCH_SEQ_NUM") != null)
                    BatchSeqNum = recordElement.Element("BATCH_SEQ_NUM").Value;

                if (recordElement.Element("INVOICE") != null)
                    Invoice = recordElement.Element("INVOICE").Value;

                if (recordElement.Element("COMMAND") != null)
                    Command = recordElement.Element("COMMAND").Value;

                if (recordElement.Element("ACCT_NUM") != null)
                    AccNum = recordElement.Element("ACCT_NUM").Value;

                if (recordElement.Element("EXP_MONTH") != null)
                    ExpMonth = recordElement.Element("EXP_MONTH").Value;

                if (recordElement.Element("EXP_YEAR") != null)
                    ExpYear = recordElement.Element("EXP_YEAR").Value;

                if (recordElement.Element("CARDHOLDER") != null)
                    Cardholder = recordElement.Element("CARDHOLDER").Value;

                if (recordElement.Element("TRANS_AMOUNT") != null)
                    TransAmount = recordElement.Element("TRANS_AMOUNT").Value;

                if (recordElement.Element("REFERENCE") != null)
                    Reference = recordElement.Element("REFERENCE").Value;

                if (recordElement.Element("TRANS_DATE") != null)
                    TransDate = recordElement.Element("TRANS_DATE").Value;

                if (recordElement.Element("TRANS_TIME") != null)
                    TransTime = recordElement.Element("TRANS_TIME").Value;

                if (recordElement.Element("ORIG_SEQ_NUM") != null)
                    OrigSeqNum = recordElement.Element("ORIG_SEQ_NUM").Value;

                if (recordElement.Element("STATUS_CODE") != null)
                    StatusCode = recordElement.Element("STATUS_CODE").Value;

                if (recordElement.Element("CTROUTD") != null)
                    CtrOutd = recordElement.Element("CTROUTD").Value;

                if (recordElement.Element("PAYMENT_TYPE") != null)
                    PaymentType = recordElement.Element("PAYMENT_TYPE").Value;

                if (recordElement.Element("PAYMENT_MEDIA") != null)
                    PaymentMedia = recordElement.Element("PAYMENT_MEDIA").Value;

                if (recordElement.Element("RESULT_CODE") != null)
                    ResultCode = recordElement.Element("RESULT_CODE").Value;

                if (recordElement.Element("AUTH_CODE") != null)
                    AuthCode = recordElement.Element("AUTH_CODE").Value;

                if (recordElement.Element("TRACE_CODE") != null)
                    TraceCode = recordElement.Element("TRACE_CODE").Value;

                if (recordElement.Element("AVS_CODE") != null)
                    AVSCode = recordElement.Element("AVS_CODE").Value;

                if (recordElement.Element("CVV2_CODE") != null)
                    CVV2Code = recordElement.Element("CVV2_CODE").Value;

                if (recordElement.Element("USERID") != null)
                    UserID = recordElement.Element("USERID").Value;

                if (recordElement.Element("CASHBACK_AMNT") != null)
                    CashbackAmnt = recordElement.Element("CASHBACK_AMNT").Value;

                if (recordElement.Element("TIP_AMOUNT") != null)
                    TipAmount = recordElement.Element("TIP_AMOUNT").Value;

                if (recordElement.Element("RESPONSE_REFERENCE") != null)
                    ResponseReference = recordElement.Element("RESPONSE_REFERENCE").Value;

                if (recordElement.Element("R_AUTH_CODE") != null)
                    RAuthCode = recordElement.Element("R_AUTH_CODE").Value;

                if (recordElement.Element("COL_X") != null)
                    ColX = recordElement.Element("COL_X").Value;

            }

            #region Properties

            /// <summary>
            /// Gets or sets the Internal Sequence Number
            /// </summary>
            public string IntrnSeqNum { get; set; }

            /// <summary>
            /// Gets or sets the Processor ID
            /// </summary>
            public string ProcessorID { get; set; }

            /// <summary>
            /// Gets or sets the Batch Sequence Number
            /// </summary>
            public string BatchSeqNum { get; set; }

            /// <summary>
            /// Gets or sets the Invoice
            /// </summary>
            public string Invoice { get; set; }

            /// <summary>
            /// Gets or sets the Command
            /// </summary>
            public string Command { get; set; }

            /// <summary>
            /// Gets or sets the Account Number
            /// </summary>
            public string AccNum { get; set; }

            /// <summary>
            /// Gets or sets the Expiration Month
            /// </summary>
            public string ExpMonth { get; set; }

            /// <summary>
            /// Gets or sets the Expiration Year
            /// </summary>
            public string ExpYear { get; set; }

            /// <summary>
            /// Gets or sets the Cardholder
            /// </summary>
            public string Cardholder { get; set; }

            /// <summary>
            /// Gets or sets the Transaction Amount
            /// </summary>
            public string TransAmount { get; set; }

            /// <summary>
            /// Gets or sets the Reference
            /// </summary>
            public string Reference { get; set; }

            /// <summary>
            /// Gets or sets the Transaction Date
            /// </summary>
            public string TransDate { get; set; }

            /// <summary>
            /// Gets or sets the Transaction Time
            /// </summary>
            public string TransTime { get; set; }

            /// <summary>
            /// Gets or sets the Orginal Sequence Number
            /// </summary>
            public string OrigSeqNum { get; set; }

            /// <summary>
            /// Gets or sets the Status Code
            /// </summary>
            public string StatusCode { get; set; }

            /// <summary>
            /// Gets or sets the Client-specific Transaction Routing ID
            /// </summary>
            public string CtrOutd { get; set; }

            /// <summary>
            /// Gets or sets the Payment Type
            /// </summary>
            public string PaymentType { get; set; }

            /// <summary>
            /// Gets or sets the Payment Media
            /// </summary>
            public string PaymentMedia { get; set; }

            /// <summary>
            /// Gets or sets the Result Code
            /// </summary>
            public string ResultCode { get; set; }

            /// <summary>
            /// Gets or sets the Authorization Code
            /// </summary>
            public string AuthCode { get; set; }

            /// <summary>
            /// Gets or sets the Trace Code
            /// </summary>
            public string TraceCode { get; set; }

            /// <summary>
            /// Gets or sets the AVS Code
            /// </summary>
            public string AVSCode { get; set; }

            /// <summary>
            /// Gets or sets the CVV2 Code
            /// </summary>
            public string CVV2Code { get; set; }

            /// <summary>
            /// Gets or sets the User ID
            /// </summary>
            public string UserID { get; set; }

            /// <summary>
            /// Gets or sets the Cashback Amount
            /// </summary>
            public string CashbackAmnt { get; set; }

            /// <summary>
            /// Gets or sets the Tip Amount 
            /// </summary>
            public string TipAmount { get; set; }

            /// <summary>
            /// Gets or sets the Response Reference
            /// </summary>
            public string ResponseReference { get; set; }

            /// <summary>
            /// Gets or sets the Respone Authorization Code
            /// </summary>
            public string RAuthCode { get; set; }

            /// <summary>
            /// Gets or sets the Column X
            /// </summary>
            public string ColX { get; set; }

            #endregion
        }

        private List<ResponseFieldRecord> m_listResponseRecords;
        
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Report Response Message.
        /// </summary>
        public ReportResponse() : base()
        {
            ClientID = String.Empty;
            m_listResponseRecords = new List<ResponseFieldRecord>();
        }

        /// <summary>
        /// Initializes an instance of the Report Response Message.
        /// </summary>
        /// <param name="doc">The Report Response message.</param>
        public ReportResponse(XDocument doc)
            : base(doc)
        {
            XElement responseMessage = doc.Element("RESPONSE");

            ClientID = responseMessage.Element("CLIENT_ID").Value;

            m_listResponseRecords = new List<ResponseFieldRecord>();

            int recordCount = int.Parse(responseMessage.Element("NUM_RECORDS_FOUND").Value);

            if (recordCount > 0)
            {
                foreach (XElement responseElement in responseMessage.Element("RECORDS").Elements("RECORD"))
                {
                    ResponseFieldRecord record = new ResponseFieldRecord(responseElement);
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
        public List<ResponseFieldRecord> ResponseRecords
        {
            get
            {
                return m_listResponseRecords;
            }
        }

        #endregion
    }
}
