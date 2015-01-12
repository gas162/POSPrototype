using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class TestMACRequest : RequestBase
    {
        /// <summary>
        /// Initializes an empty instance of the Test MAC Request message.
        /// </summary>
        public TestMACRequest() : base()
        {
            FunctionType = "SECURITY";
            Command = "TEST_MAC";
            m_bDeviceRequired = false;
        }
    }
}
