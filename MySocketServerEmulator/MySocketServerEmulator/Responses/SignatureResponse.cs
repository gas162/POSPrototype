using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class SignatureResponse : ResponseBase
    {
         #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Signature Response Message.
        /// </summary>
        public SignatureResponse() : base()
        {

        }

        /// <summary>
        /// Initializes an instance of the Signature Response Message.
        /// </summary>
        /// <param name="doc">the Signature Response message.</param>
        public SignatureResponse(XDocument doc)
            : base(doc)
        {
            XElement responseMessage = doc.Element("RESPONSE");

            string signature = responseMessage.Element("SIGNATUREDATA").Value;
            SignatureData = Convert.FromBase64String(signature);

            MimeType = responseMessage.Element("MIME_TYPE").Value;
        }

        #endregion

        #region Methods

        protected override List<XElement> GenerateMessagePortion()
        {
            List<XElement> listClassSpecificElements = new List<XElement>();

            listClassSpecificElements.Add(new XElement("SIGNATUREDATA", Convert.ToBase64String(SignatureData)));
            listClassSpecificElements.Add(new XElement("MIME_TYPE", MimeType));

            return listClassSpecificElements;
        }

        #endregion
        
        #region Properties

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
