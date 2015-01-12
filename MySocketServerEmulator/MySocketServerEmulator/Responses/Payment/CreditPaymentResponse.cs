using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace MySocketServerEmulator
{
    public class CreditPaymentResponse : PaymentResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Credit Payment Response Message.
        /// </summary>
        public CreditPaymentResponse() : base()
        {
            TransSeqNum = -1;
            IntrnSeqNum = -1;
            Troutd = -1;
            Ctroutd = -1;
        }

        /// <summary>
        /// Initializes an instance of the Credit Payment Response Message.
        /// </summary>
        /// <param name="doc">the Credit Payment Response message.</param>
        public CreditPaymentResponse(XDocument doc)
            : base(doc)
        {
            XElement responseMessage = doc.Element("RESPONSE");

            if (responseMessage.Element("SAF_NUM") != null)
            {
                SafNum = int.Parse(responseMessage.Element("SAF_NUM").Value);
            }

            if (responseMessage.Element("SIGNATUREDATA") != null)
            {
                string signature = responseMessage.Element("SIGNATUREDATA").Value;
                SignatureData = Convert.FromBase64String(signature);

                MimeType = responseMessage.Element("MIME_TYPE").Value;
            }
        }

        #endregion

        #region Methods

        public override List<XElement> GeneratePaymentortion()
        {
            List<XElement> listPaymentElements = new List<XElement>();

            if (SafNum != null)
            {
                listPaymentElements.Add(new XElement("SAF_NUM", SafNum));
            }
            if (SignatureData != null)
            {
                listPaymentElements.Add(new XElement("SIGNATUREDATA", Convert.ToBase64String(SignatureData)));
                listPaymentElements.Add(new XElement("MIME_TYPE", MimeType));
            }

            return listPaymentElements;
        }

        #endregion

        #region Properties

        /// <summary>
        /// SAF Number
        /// </summary>
        public int? SafNum { get; set; }

        // Signature Properties

        /// <summary>
        /// Signature Data
        /// </summary>
        public byte[] SignatureData { get; set; }

        /// <summary>
        /// Mime Type
        /// </summary>
        public string MimeType { get; set; }


        #endregion
    }
}
