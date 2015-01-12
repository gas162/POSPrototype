using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace MySocketServerEmulator
{
    public class RegisterPOSResponse : ResponseBase
    {

        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Register POS Response Message.
        /// </summary>
        public RegisterPOSResponse() : base()
        {
            MacKey = null;
            MacLabel = String.Empty;
        }

        /// <summary>
        /// Initializes an instance of the Register POS Response Message.
        /// </summary>
        /// <param name="doc">the Register POS Response message.</param>
        public RegisterPOSResponse(XDocument doc) : base(doc)
        {
            XElement responseMessage = doc.Element("RESPONSE");

            string macKeyString = responseMessage.Element("MAC_KEY").Value;

            MacKey = Convert.FromBase64String(macKeyString);
            MacLabel = responseMessage.Element("MAC_LABEL").Value;
        }

        public RegisterPOSResponse(XDocument doc, RSACryptoServiceProvider cryptoProvider)
            : base(doc)
        {
            XElement responseMessage = doc.Element("RESPONSE");

            var encryptedMacKey = responseMessage.Element("MAC_KEY").Value;
            var macKeyBase64Decoded = Convert.FromBase64String(encryptedMacKey);
            var macKey = cryptoProvider.Decrypt(macKeyBase64Decoded, false);
             
            MacKey = macKey;
            MacLabel = responseMessage.Element("MAC_LABEL").Value;
        }

        #endregion

        #region Methods

        protected override List<XElement> GenerateMessagePortion()
        {
            List<XElement> listClassSpecificElements = new List<XElement>();

            listClassSpecificElements.Add(new XElement("MAC_KEY", Convert.ToBase64String(MacKey)));
            listClassSpecificElements.Add(new XElement("MAC_LABEL", MacLabel));

            return listClassSpecificElements;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Mac Key (MAC_KEY)
        /// </summary>
        public byte[] MacKey { get; set; }

        /// <summary>
        /// Gets or sets the Mac Label (MAC_LABEL)
        /// </summary>
        public string MacLabel { get; set; }

        #endregion
    }
}
