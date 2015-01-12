using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class UnregisterPOSRequest : RequestBase
    {
        
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Unregister POS Request message.
        /// </summary>
        public UnregisterPOSRequest()
        {
            FunctionType = "SECURITY";
            Command = "UNREGISTER";
            EntryCode = -1;
            m_bDeviceRequired = true;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Entry Code
        /// </summary>
        [RequestAttributes(Order = 4, Type = "N", Name = "ENTRY_CODE", Min = 4, Max = 4, Required = true)]
        public int EntryCode { get; set; }

        #endregion

    }
}
