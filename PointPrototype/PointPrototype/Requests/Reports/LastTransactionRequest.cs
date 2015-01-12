using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class LastTransactionRequest : RequestBase
    {
        /// <summary>
        /// Initializes an empty instance of the Last Transaction Request Message.
        /// </summary>
        public LastTransactionRequest() : base()
        {
            FunctionType = "REPORT";
            Command = "LAST_TRAN";
            m_bDeviceRequired = false;
        }
    }
}
