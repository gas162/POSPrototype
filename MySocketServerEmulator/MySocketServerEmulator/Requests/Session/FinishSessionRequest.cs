using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class FinishSessionRequest : RequestBase
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Start Session request message.
        /// </summary>
        public FinishSessionRequest()
            : base()
        {
            FunctionType = "SESSION";
            Command = "FINISH";
            m_bDeviceRequired = true;
        }

        public FinishSessionRequest(XDocument doc)
            : base(doc)
        {

        }

        #endregion
    }
}
