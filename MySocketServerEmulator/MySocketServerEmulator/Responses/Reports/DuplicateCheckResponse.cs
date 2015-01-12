using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{

    public class DuplicateCheckRecord
    {
        public DuplicateCheckRecord()
        {
            TransDate = DateTime.Now;
            TransTime = DateTime.Now;
            ClientID = -1;
            IntrnSeqNum = -1;
            AcctNum = String.Empty;
            TransAmount = 0.0f;
            StatusCode = -1;
            PaymentType = String.Empty;
            Command = String.Empty;
            BusName = String.Empty;
        }

        public DuplicateCheckRecord(XElement recordElement)
        {
            TransDate = DateTime.Parse(recordElement.Element("TRANS_DATE").Value);
            TransTime = DateTime.Parse(recordElement.Element("TRANS_TIME").Value);
            ClientID = int.Parse(recordElement.Element("CLIENT_ID").Value);
            IntrnSeqNum = int.Parse(recordElement.Element("INTRN_SEQ_NUM").Value);
            AcctNum = recordElement.Element("ACCT_NUM").Value;
            TransAmount = float.Parse(recordElement.Element("TRANS_AMOUNT").Value);
            StatusCode = int.Parse(recordElement.Element("STATUS_CODE").Value);
            PaymentType = recordElement.Element("PAYMENT_TYPE").Value;
            Command = recordElement.Element("COMMAND").Value;
            BusName = recordElement.Element("BUSNAME").Value;
        }
        #region Properties


        /// <summary>
        /// Gets or sets the Transaction Date
        /// </summary>
        public DateTime TransDate { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Time
        /// </summary>
        public DateTime TransTime { get; set; }

        /// <summary>
        /// Gets or sets the Client ID
        /// </summary>
        public int ClientID { get; set; }

        /// <summary>
        /// Internal Sequence Number
        /// </summary>
        public int IntrnSeqNum { get; set; }

        /// <summary>
        /// Gets or sets Account Number
        /// </summary>
        public string AcctNum { get; set; }

        /// <summary>
        /// Transaction Amount
        /// </summary>
        public float TransAmount { get; set; }

        /// <summary>
        /// Status Code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Payment Type
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        /// Command
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Business Name
        /// </summary>
        public string BusName { get; set; }

        #endregion
    }

    public class DuplicateCheckResponse : ResponseBase
    {
        private List<DuplicateCheckRecord> m_listDuplicateCheckRecords;

        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Report Response Message.
        /// </summary>
        public DuplicateCheckResponse()
            : base()
        {
            m_listDuplicateCheckRecords = new List<DuplicateCheckRecord>();
        }

        /// <summary>
        /// Initializes an instance of the Report Response Message.
        /// </summary>
        /// <param name="doc">the Report Response message.</param>
        public DuplicateCheckResponse(XDocument doc)
            : base(doc)
        {
            XElement responseMessage = doc.Element("RESPONSE");

            m_listDuplicateCheckRecords = new List<DuplicateCheckRecord>();

            if (responseMessage.Element("RECORDS").HasElements)
            {
                foreach (XElement responseElement in responseMessage.Element("RECORDS").Elements("RECORD"))
                {
                    DuplicateCheckRecord record = new DuplicateCheckRecord(responseElement);
                    m_listDuplicateCheckRecords.Add(record);
                }
            }
        }

        #endregion

        #region Methods

        protected override List<XElement> GenerateMessagePortion()
        {
            List<XElement> listClassSpecificElements = new List<XElement>();

            XElement recordsElement = new XElement("RECORDS");

            if (m_listDuplicateCheckRecords.Count > 0)
            {
                foreach (DuplicateCheckRecord record in m_listDuplicateCheckRecords)
                {
                    XElement recordElement = new XElement("RECORD");

                    recordElement.Add(new XElement("TRANS_DATE", record.TransDate.ToString("yyyy.MM.dd")));
                    recordElement.Add(new XElement("TRANS_TIME", record.TransTime.ToString("HH:mm:ss")));
                    recordElement.Add(new XElement("CLIENT_ID", record.ClientID));
                    recordElement.Add(new XElement("INTRN_SEQ_NUM", record.IntrnSeqNum));
                    recordElement.Add(new XElement("ACCT_NUM", record.AcctNum));
                    recordElement.Add(new XElement("TRANS_AMOUNT", record.TransAmount.ToString("0.00")));
                    recordElement.Add(new XElement("STATUS_CODE", record.StatusCode));
                    recordElement.Add(new XElement("PAYMENT_TYPE", record.PaymentType));
                    recordElement.Add(new XElement("COMMAND", record.Command));
                    recordElement.Add(new XElement("BUSNAME", record.BusName));

                    recordsElement.Add(recordElement);
                }
            }

            listClassSpecificElements.Add(recordsElement);

            return listClassSpecificElements;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of Response Records.
        /// </summary>
        public List<DuplicateCheckRecord> DuplicateCheckRecords
        {
            get
            {
                return m_listDuplicateCheckRecords;
            }
        }

        #endregion
    }
}
