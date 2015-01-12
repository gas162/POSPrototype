using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace MySocketServerEmulator
{
    public class Receipt
    {
        private List<string> m_listTextLines;

        public Receipt()
        {
            m_listTextLines = new List<string>();
        }

        public void SetReceiptText(string text)
        {
            string[] parts = text.Split('|');

            foreach (string p in parts)
            {
                if(!String.IsNullOrEmpty(p) && p.Length > 40)
                    m_listTextLines.Add(p.Substring(0, 40));
                else
                    m_listTextLines.Add(p);
            }
        }

        /// <summary>
        /// Gets the receipt text.
        /// </summary>
        /// <returns>the text on a receipt</returns>
        public string GetReceiptText()
        {
            string data = String.Empty;

            foreach (string s in m_listTextLines)
            {
                if(String.IsNullOrEmpty(s))
                    data = String.Concat(data, Environment.NewLine);
                else
                    data = String.Concat(data, s, Environment.NewLine);
            }

            return data;
        }

        public List<string> TextLines
        {
            get
            {
                return m_listTextLines;
            }
        }
    }
    
    public class PaymentResponse : ResponseBase
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Payment Response Message.
        /// </summary>
        public PaymentResponse() : base()
        {
            TransSeqNum = -1;
            IntrnSeqNum = -1;
            Troutd = -1;
            Ctroutd = -1;
            Receipts = new List<Receipt>();
        }

        /// <summary>
        /// Initializes an instance of the Payment Response Message.
        /// </summary>
        /// <param name="doc">the Payment Response message.</param>
        public PaymentResponse(XDocument doc)
            : base(doc)
        {
            XElement responseMessage = doc.Element("RESPONSE");

            TransSeqNum = int.Parse(responseMessage.Element("TRANS_SEQ_NUM").Value);
            IntrnSeqNum = int.Parse(responseMessage.Element("INTRN_SEQ_NUM").Value);
            Troutd = int.Parse(responseMessage.Element("TROUTD").Value);
            Ctroutd = int.Parse(responseMessage.Element("CTROUTD").Value);

            // Add receipt 
            Receipts = new List<Receipt>();

            if (responseMessage.Element("RECEIPT_DATA") != null)
            {
                foreach (XElement receiptElement in responseMessage.Element("RECEIPT_DATA").Elements("RECEIPT"))
                {
                    Receipt receipt = new Receipt();
                    foreach (XElement textElement in receiptElement.Elements("TEXTLINE"))
                    {
                        receipt.TextLines.Add(textElement.Value);
                    }
                    Receipts.Add(receipt);
                }
            }
        }

        #endregion

        #region Methods

        protected override List<XElement> GenerateMessagePortion()
        {
            List<XElement> listClassSpecificElements = new List<XElement>();

            listClassSpecificElements.Add(new XElement("TRANS_SEQ_NUM", TransSeqNum));
            listClassSpecificElements.Add(new XElement("INTRN_SEQ_NUM", IntrnSeqNum));
            listClassSpecificElements.Add(new XElement("TROUTD", Troutd));
            listClassSpecificElements.Add(new XElement("CTROUTD", Ctroutd));

            List<XElement> listPaymentElements = GeneratePaymentortion();
            if(listPaymentElements.Count > 0)
            {
                foreach(XElement pElem in listPaymentElements)
                {
                    listClassSpecificElements.Add(pElem);
                }
            }

            XElement receiptDataElement = new XElement("RECEIPT_DATA");
            foreach(Receipt r in Receipts)
            {
                XElement receiptElement = new XElement("RECEIPT");

                foreach (String text in r.TextLines)
                {
                    XElement textElement = new XElement("TEXTLINE");

                    if(!String.IsNullOrEmpty(text))
                        textElement.Value = text;
                    receiptElement.Add(textElement);
                }

                receiptDataElement.Add(receiptElement);
            }
            listClassSpecificElements.Add(receiptDataElement);

            return listClassSpecificElements;
        }

        public virtual List<XElement> GeneratePaymentortion()
        {
            List<XElement> listPaymentElements = new List<XElement>();
            return listPaymentElements;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Processor/Batch trans sequence number
        /// </summary>
        public int TransSeqNum { get; set; }

        /// <summary>
        /// Internal sequence number
        /// </summary>
        public int IntrnSeqNum { get; set; }

        /// <summary>
        /// Transaction Routing ID
        /// </summary>
        public int Troutd { get; set; }

        /// <summary>
        /// Client-specific Transaction Routing ID
        /// </summary>
        public int Ctroutd { get; set; }

        /// <summary>
        /// Gets or sets the Receipt data.
        /// </summary>
        public List<Receipt> Receipts { get; set; }

        #endregion
    }
}
