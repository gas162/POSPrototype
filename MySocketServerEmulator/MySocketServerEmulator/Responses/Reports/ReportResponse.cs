using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class ReportResponse : ResponseBase
    {
        public class ResponseFieldRecord
        {
            public ResponseFieldRecord()
            {

            }

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
            /// Internal Sequence Number
            public string IntrnSeqNum { get; set; }

            /// <summary>
            /// Processor ID
            /// </summary>
            public string ProcessorID { get; set; }

            /// <summary>
            /// Batch Sequence Number
            /// </summary>
            public string BatchSeqNum { get; set; }

            /// <summary>
            /// Invoice
            /// </summary>
            public string Invoice { get; set; }

            /// <summary>
            /// Command
            /// </summary>
            public string Command { get; set; }

            /// <summary>
            /// Account Number
            /// </summary>
            public string AccNum { get; set; }

            /// <summary>
            /// Expiration Month
            /// </summary>
            public string ExpMonth { get; set; }

            /// <summary>
            /// Expiration Year
            /// </summary>
            public string ExpYear { get; set; }

            /// <summary>
            /// Cardholder
            /// </summary>
            public string Cardholder { get; set; }

            /// <summary>
            /// Transaction Amount
            /// </summary>
            public string TransAmount { get; set; }

            /// <summary>
            /// Reference
            /// </summary>
            public string Reference { get; set; }

            /// <summary>
            /// Transaction Date
            /// </summary>
            public string TransDate { get; set; }

            /// <summary>
            /// Transaction Time
            /// </summary>
            public string TransTime { get; set; }

            /// <summary>
            /// Orginal Sequence Number
            /// </summary>
            public string OrigSeqNum { get; set; }

            /// <summary>
            /// Status Code
            /// </summary>
            public string StatusCode { get; set; }

            /// <summary>
            /// CtrOutd
            /// </summary>
            public string CtrOutd { get; set; }

            /// <summary>
            /// Payment Type
            /// </summary>
            public string PaymentType { get; set; }

            /// <summary>
            /// Payment Media
            /// </summary>
            public string PaymentMedia { get; set; }

            /// <summary>
            /// Result Code
            /// </summary>
            public string ResultCode { get; set; }

            /// <summary>
            /// Auth Code
            /// </summary>
            public string AuthCode { get; set; }

            /// <summary>
            /// Trace Code
            /// </summary>
            public string TraceCode { get; set; }

            /// <summary>
            /// AVS Code
            /// </summary>
            public string AVSCode { get; set; }

            /// <summary>
            /// CVV2 Code
            /// </summary>
            public string CVV2Code { get; set; }

            /// <summary>
            /// User ID
            /// </summary>
            public string UserID { get; set; }

            /// <summary>
            /// Cashback Amount
            /// </summary>
            public string CashbackAmnt { get; set; }

            /// <summary>
            /// Tip Amount 
            /// </summary>
            public string TipAmount { get; set; }

            /// <summary>
            /// Response Reference
            /// </summary>
            public string ResponseReference { get; set; }

            /// <summary>
            /// R Auth Code
            /// </summary>
            public string RAuthCode { get; set; }

            /// <summary>
            /// Column X
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
        /// <param name="doc">the Report Response message.</param>
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

        #region Methods

        protected override List<XElement> GenerateMessagePortion()
        {
            List<XElement> listClassSpecificElements = new List<XElement>();

            listClassSpecificElements.Add(new XElement("CLIENT_ID", ClientID));
            listClassSpecificElements.Add(new XElement("NUM_RECORDS_FOUND", m_listResponseRecords.Count));
            XElement recordsElement = new XElement("RECORDS");

            if (m_listResponseRecords.Count > 0)
            {
                foreach (ResponseFieldRecord record in m_listResponseRecords)
                {
                    XElement recordElement = new XElement("RECORD");

                    if (!String.IsNullOrEmpty(record.IntrnSeqNum))
                        recordElement.Add(new XElement("INTRN_SEQ_NUM", record.IntrnSeqNum));

                    if (!String.IsNullOrEmpty(record.ProcessorID))
                        recordElement.Add(new XElement("PROCESSOR_ID", record.ProcessorID));

                    if (!String.IsNullOrEmpty(record.BatchSeqNum))
                        recordElement.Add(new XElement("BATCH_SEQ_NUM", record.BatchSeqNum));

                    if (!String.IsNullOrEmpty(record.Invoice))
                        recordElement.Add(new XElement("INVOICE", record.Invoice));

                    if (!String.IsNullOrEmpty(record.Command))
                        recordElement.Add(new XElement("COMMAND", record.Command));

                    if (!String.IsNullOrEmpty(record.AccNum))
                        recordElement.Add(new XElement("ACCT_NUM", record.AccNum));

                    if (!String.IsNullOrEmpty(record.ExpMonth))
                        recordElement.Add(new XElement("EXP_MONTH", record.ExpMonth));

                    if (!String.IsNullOrEmpty(record.ExpYear))
                        recordElement.Add(new XElement("EXP_YEAR", record.ExpYear));

                    if (!String.IsNullOrEmpty(record.Cardholder))
                        recordElement.Add(new XElement("CARDHOLDER", record.Cardholder));

                    if (!String.IsNullOrEmpty(record.TransAmount))
                        recordElement.Add(new XElement("TRANS_AMOUNT", record.TransAmount));

                    if (!String.IsNullOrEmpty(record.Reference))
                        recordElement.Add(new XElement("REFERENCE", record.Reference));

                    if (!String.IsNullOrEmpty(record.TransDate))
                        recordElement.Add(new XElement("TRANS_DATE", record.TransDate));

                    if (!String.IsNullOrEmpty(record.TransTime))
                        recordElement.Add(new XElement("TRANS_TIME", record.TransTime));

                    if (!String.IsNullOrEmpty(record.OrigSeqNum))
                        recordElement.Add(new XElement("ORIG_SEQ_NUM", record.OrigSeqNum));

                    if (!String.IsNullOrEmpty(record.StatusCode))
                        recordElement.Add(new XElement("STATUS_CODE", record.StatusCode));

                    if (!String.IsNullOrEmpty(record.CtrOutd))
                        recordElement.Add(new XElement("CTROUTD", record.CtrOutd));

                    if (!String.IsNullOrEmpty(record.PaymentType))
                        recordElement.Add(new XElement("PAYMENT_TYPE", record.PaymentType));

                    if (!String.IsNullOrEmpty(record.PaymentMedia))
                        recordElement.Add(new XElement("PAYMENT_MEDIA", record.PaymentMedia));

                    if (!String.IsNullOrEmpty(record.ResultCode))
                        recordElement.Add(new XElement("RESULT_CODE", record.ResultCode));

                    if (!String.IsNullOrEmpty(record.AuthCode))
                        recordElement.Add(new XElement("AUTH_CODE", record.AuthCode));

                    if (!String.IsNullOrEmpty(record.TraceCode))
                        recordElement.Add(new XElement("TRACE_CODE", record.TraceCode));

                    if (!String.IsNullOrEmpty(record.AVSCode))
                        recordElement.Add(new XElement("AVS_CODE", record.AVSCode));

                    if (!String.IsNullOrEmpty(record.CVV2Code))
                        recordElement.Add(new XElement("CVV2_CODE", record.CVV2Code));

                    if (!String.IsNullOrEmpty(record.UserID))
                        recordElement.Add(new XElement("USERID", record.UserID));

                    if (!String.IsNullOrEmpty(record.CashbackAmnt))
                        recordElement.Add(new XElement("CASHBACK_AMNT", record.CashbackAmnt));

                    if (!String.IsNullOrEmpty(record.TipAmount))
                        recordElement.Add(new XElement("TIP_AMOUNT", record.TipAmount));

                    if (!String.IsNullOrEmpty(record.ResponseReference))
                        recordElement.Add(new XElement("RESPONSE_REFERENCE", record.ResponseReference));

                    if (!String.IsNullOrEmpty(record.RAuthCode))
                        recordElement.Add(new XElement("R_AUTH_CODE", record.RAuthCode));

                    if (!String.IsNullOrEmpty(record.ColX))
                        recordElement.Add(new XElement("COL_X", record.ColX));

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
