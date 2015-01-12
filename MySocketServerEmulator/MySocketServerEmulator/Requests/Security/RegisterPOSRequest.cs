using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace MySocketServerEmulator
{
    public class RegisterPOSRequest : RequestBase
    {

        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the register POS request message.
        /// </summary>
        public RegisterPOSRequest()
            : base()
        {
            FunctionType = "SECURITY";
            Command = "REGISTER";
            EntryCode = -1;
            Key = String.Empty;
            m_bDeviceRequired = true;
        }

        public RegisterPOSRequest(XDocument doc)
            : base(doc)
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            Key = transactionElement.Element("KEY").Value;
            EntryCode = int.Parse(transactionElement.Element("ENTRY_CODE").Value);

        }

        #endregion

        #region Properties

        /// <summary>
        /// The Entry Code
        /// </summary>
        [RequestAttributes(Order = 3, Type = "N", Name = "ENTRY_CODE", Min = 4, Max = 4, Required = true)]
        public int EntryCode { get; set; }

        /// <summary>
        /// The Key Address
        /// </summary>
        [RequestAttributes(Order = 4, Type = "E", Name = "KEY", Required = true)]
        public string Key { get; set; }

        #endregion
    }
}
