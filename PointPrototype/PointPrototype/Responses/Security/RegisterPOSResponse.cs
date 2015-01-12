using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace PointPrototype
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
        /// <param name="doc">The Register POS Response Message.</param>
        public RegisterPOSResponse(XDocument doc)
            : base(doc)
        {
            XElement responseMessage = doc.Element("RESPONSE");

            if (ResultCode == -1)
            {
                string macKeyString = responseMessage.Element("MAC_KEY").Value;

                MacKey = Convert.FromBase64String(macKeyString);
                MacLabel = responseMessage.Element("MAC_LABEL").Value;
            }
        }

        /// <summary>
        /// Initializes an instance of the Register POS Response Message.
        /// </summary>
        /// <param name="doc">The Register POS Response Message.</param>
        /// <param name="doc">The RSA Cryptographic Service provider used to decrypt the MAC Key.</param>
        public RegisterPOSResponse(XDocument doc, RSACryptoServiceProvider cryptoProvider)
            : base(doc)
        {
            XElement responseMessage = doc.Element("RESPONSE");

            if (ResultCode == -1)
            {
                var encryptedMacKey = responseMessage.Element("MAC_KEY").Value;
                var macKeyBase64Decoded = Convert.FromBase64String(encryptedMacKey);
                var macKey = cryptoProvider.Decrypt(macKeyBase64Decoded, false);

                MacKey = macKey;
                MacLabel = responseMessage.Element("MAC_LABEL").Value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Mac Key
        /// </summary>
        public byte[] MacKey { get; set; }

        /// <summary>
        /// Gets or sets the Mac Label
        /// </summary>
        public string MacLabel { get; set; }

        #endregion
    }
}
