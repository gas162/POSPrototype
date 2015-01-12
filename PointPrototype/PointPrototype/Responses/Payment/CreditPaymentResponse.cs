using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace PointPrototype
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
        /// <param name="doc">The Credit Payment Response Message.</param>
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

        #region Properties

        /// <summary>
        /// Gets or sets the SAF Number
        /// </summary>
        public int? SafNum { get; set; }

        #region Signature Properties

        /// <summary>
        /// Gets or sets the Signature Data
        /// </summary>
        public byte[] SignatureData { get; set; }

        /// <summary>
        /// Gets or sets the Mime Type
        /// </summary>
        public string MimeType { get; set; }

        #endregion

        #endregion
    }
}
