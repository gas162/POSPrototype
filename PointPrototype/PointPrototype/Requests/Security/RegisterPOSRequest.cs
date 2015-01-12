using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class RegisterPOSRequest : RequestBase
    {

        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Register POS Request message.
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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Entry Code
        /// </summary>
        [RequestAttributes(Order = 3, Type = "N", Name = "ENTRY_CODE", Min = 4, Max = 4, Required = true)]
        public int EntryCode { get; set; }

        /// <summary>
        /// Gets or sets the RSA Public Key
        /// </summary>
        [RequestAttributes(Order = 4, Type = "E", Name = "KEY", Required = true)]
        public string Key { get; set; }

        #endregion
    }
}
