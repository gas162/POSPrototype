using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class UnRegisterPOSRequest : RequestBase
    {
        
        #region Constructors

        public UnRegisterPOSRequest()
        {
            FunctionType = "SECURITY";
            Command = "UNREGISTER";
            EntryCode = -1;
            m_bDeviceRequired = true;
        }

        public UnRegisterPOSRequest(XDocument doc)
            : base(doc)
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            EntryCode = int.Parse(transactionElement.Element("ENTRY_CODE").Value);

        }

        #endregion

        #region Properties

        /// <summary>
        /// The Entry Code
        /// </summary>
        [RequestAttributes(Order = 4, Type = "N", Name = "ENTRY_CODE", Min = 4, Max = 4, Required = true)]
        public int EntryCode { get; set; }

        #endregion

    }
}
