using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class Receipt
    {
        private List<string> m_listTextLines;

        /// <summary>
        /// Initializes an empty instance of a receipt.
        /// </summary>
        public Receipt()
        {
            m_listTextLines = new List<string>();
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

        /// <summary>
        /// Gets the list of text lines for a receipt.
        /// </summary>
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
        /// <param name="doc">The Payment Response Message.</param>
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

        #region Properties

        /// <summary>
        /// Gets or sets the Processor/Batch Transaction Sequence Number
        /// </summary>
        public int TransSeqNum { get; set; }

        /// <summary>
        /// Gets or sets the Internal Sequence Number
        /// </summary>
        public int IntrnSeqNum { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Routing ID
        /// </summary>
        public int Troutd { get; set; }

        /// <summary>
        /// Gets or sets the Client-specific Transaction Routing ID
        /// </summary>
        public int Ctroutd { get; set; }

        /// <summary>
        /// Gets or sets the list of Receipts.
        /// </summary>
        public List<Receipt> Receipts { get; set; }

        #endregion
    }
}
