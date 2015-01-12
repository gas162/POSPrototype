using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace MySocketServerEmulator
{
    public class TestMACResponse : ResponseBase
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Test MAC Response Message.
        /// </summary>
        public TestMACResponse() : base()
        {

        }

        /// <summary>
        /// Initializes an instance of the Test MAC Response Message.
        /// </summary>
        /// <param name="doc">the Test MAC Response message.</param>
        public TestMACResponse(XDocument doc)
            : base(doc)
        {

        }

        #endregion

    }
}
