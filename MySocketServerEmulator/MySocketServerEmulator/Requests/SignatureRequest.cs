using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class SignatureRequest : RequestBase
    {
        public SignatureRequest()
        {
            FunctionType = "DEVICE";
            Command = "SIGNATURE";

            m_bDeviceRequired = true;
        }

        public SignatureRequest(XDocument doc)
            : base(doc)
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            DisplayText = transactionElement.Element("DISPLAY_TEXT").Value;
        }

        #region Properties

        /// <summary>
        /// Display Text
        /// </summary>
        [RequestAttributes(Order = 6, Type = "C", Name = "DISPLAY_TEXT", Min = 1, Max = 50, Required = false)]
        public string DisplayText { get; set; }

        #endregion
    }
}
