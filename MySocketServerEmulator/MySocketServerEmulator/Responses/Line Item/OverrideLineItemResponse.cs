using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class OverrideLineItemResponse : ResponseBase
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Override Line Item Response Message.
        /// </summary>
        public OverrideLineItemResponse() : base()
        {

        }

        /// <summary>
        /// Initializes an instance of the Override Line Item Response Message.
        /// </summary>
        /// <param name="doc">the Override Line Item Response message.</param>
        public OverrideLineItemResponse(XDocument doc)
            : base(doc)
        {

        }

        #endregion
    }
}
