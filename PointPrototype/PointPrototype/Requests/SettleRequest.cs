using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class SettleRequest : RequestBase
    {
        /// <summary>
        /// Initializes an empty instance of the Schedule Settlement Request Message.
        /// </summary>
        public SettleRequest() :base()
        {
            FunctionType = "BATCH";
            Command = "SETTLE";

            m_bDeviceRequired = false;
        }
    }
}
