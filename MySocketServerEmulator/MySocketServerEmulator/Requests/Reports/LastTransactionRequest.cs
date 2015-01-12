using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class LastTransactionRequest : RequestBase
    {
        public LastTransactionRequest() : base()
        {
            FunctionType = "REPORT";
            Command = "LAST_TRAN";
            m_bDeviceRequired = false;
        }

        public LastTransactionRequest(XDocument doc)
            : base(doc)
        {

        }
    }
}
