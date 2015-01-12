using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class SettleRequest : RequestBase
    {
        public SettleRequest()
            : base()
        {
            FunctionType = "BATCH";
            Command = "SETTLE";

            m_bDeviceRequired = false;
        }

        public SettleRequest(XDocument doc)
            : base(doc)
        {
        }
    }
}
