using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class TestMACRequest : RequestBase
    {
        public TestMACRequest() : base()
        {
            FunctionType = "SECURITY";
            Command = "TEST_MAC";
            m_bDeviceRequired = false;
        }

        public TestMACRequest(XDocument doc)
            : base(doc)
        {

        }
    }
}
