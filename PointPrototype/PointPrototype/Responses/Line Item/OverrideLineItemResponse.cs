using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PointPrototype
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
        /// <param name="doc">The Override Line Item Response Message.</param>
        public OverrideLineItemResponse(XDocument doc)
            : base(doc)
        {

        }

        #endregion
    }
}
