using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class SAFRecord
    {
       /// <summary>
       /// Initializes an empty instance of an SAF Record.
       /// </summary>
        public SAFRecord()
        {
            AccNum = String.Empty;
            TransAmount = 0.0f;
            Invoice = String.Empty;
            PaymentType = String.Empty;
            PaymentMedia = String.Empty;
            SAFNum = -1;
            SAFStatus = String.Empty;
        }
        
        #region Properties

        /// <summary>
        /// Account Number
        /// </summary>
        public string AccNum { get; set; }

        /// <summary>
        /// Transaction Amount
        /// </summary>
        public float TransAmount { get; set; }

        /// <summary>
        /// Invoice
        /// </summary>
        public string Invoice { get; set; }

        /// <summary>
        /// Payment Type
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        /// Payment Media
        /// </summary>
        public string PaymentMedia { get; set; }

        /// <summary>
        /// SAF Number
        /// </summary>
        public int SAFNum { get; set; }

        /// <summary>
        /// SAF Status
        /// </summary>
        public string SAFStatus { get; set; }

        #endregion
    }
    
    public class SAFResponse : ResponseBase
    {
        #region Variables

        private List<SAFRecord> m_listSAFRecords;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the SAF Response Message.
        /// </summary>
        public SAFResponse() : base()
        {
            m_listSAFRecords = new List<SAFRecord>();
        }

        /// <summary>
        /// Initializes an instance of the SAF Response Message.
        /// </summary>
        /// <param name="doc">the SAF Response message.</param>
        public SAFResponse(XDocument doc)
            : base(doc)
        {
            // Read through the XDocument to initialize the the SAF response message.
            XElement responseMessage = doc.Element("RESPONSE");

            m_listSAFRecords = new List<SAFRecord>();

            if (responseMessage.Element("RECORDS").HasElements)
            {
                foreach (XElement record in responseMessage.Element("RECORDS").Elements("RECORD"))
                {
                    SAFRecord safRecord = new SAFRecord();

                    safRecord.AccNum = record.Element("ACCT_NUM").Value;
                    safRecord.TransAmount = float.Parse(record.Element("TRANS_AMOUNT").Value);
                    safRecord.Invoice = record.Element("INVOICE").Value;
                    safRecord.PaymentType = record.Element("PAYMENT_TYPE").Value;
                    safRecord.PaymentMedia = record.Element("PAYMENT_MEDIA").Value;
                    safRecord.SAFNum = int.Parse(record.Element("SAF_NUM").Value);
                    safRecord.SAFStatus = record.Element("SAF_STATUS").Value;

                    m_listSAFRecords.Add(safRecord);
                }
            }
        }

        #endregion

        #region Methods

        protected override List<XElement> GenerateMessagePortion()
        {
            List<XElement> listClassSpecificElements = new List<XElement>();

            listClassSpecificElements.Add(new XElement("RECORD_COUNT", RecordCount));
            listClassSpecificElements.Add(new XElement("TOTAL_AMOUNT", TotalAmount.ToString("0.00")));

            XElement RecordElements = new XElement("RECORDS");

            foreach (SAFRecord record in m_listSAFRecords)
            {
                XElement recordElement = new XElement("RECORD");
                recordElement.Add(new XElement("ACCT_NUM", record.AccNum));
                recordElement.Add(new XElement("TRANS_AMOUNT", record.TransAmount));
                recordElement.Add(new XElement("INVOICE", record.Invoice));
                recordElement.Add(new XElement("PAYMENT_TYPE", record.PaymentType));
                recordElement.Add(new XElement("PAYMENT_MEDIA", record.PaymentMedia));
                recordElement.Add(new XElement("SAF_NUM", record.SAFNum));
                recordElement.Add(new XElement("SAF_STATUS", record.SAFStatus));
                RecordElements.Add(recordElement);
            }
            listClassSpecificElements.Add(RecordElements);

            return listClassSpecificElements;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Record Count
        /// </summary>
        public int RecordCount 
        {
            get 
            {
                return m_listSAFRecords.Count;
            } 
        }

        /// <summary>
        /// Total Amount
        /// </summary>
        public float TotalAmount
        {
            get
            {
                return m_listSAFRecords.Sum(x=>x.TransAmount);
            }
        }

        /// <summary>
        /// Records
        /// </summary>
        public List<SAFRecord> Records
        {
            get
            {
                return m_listSAFRecords;
            }
        }

        #endregion
    }
}
