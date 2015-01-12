using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace MySocketServerEmulator
{
    public class UnRegisterPOSResponse: ResponseBase
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Unregister POS Response Message.
        /// </summary>
        public UnRegisterPOSResponse() : base()
        {

        }

        /// <summary>
        /// Initializes an instance of the Unregister POS Response Message.
        /// </summary>
        /// <param name="doc">the Unregister POS Response message.</param>
        public UnRegisterPOSResponse(XDocument doc) : base(doc)
        {

        }

        #endregion
    }
}
