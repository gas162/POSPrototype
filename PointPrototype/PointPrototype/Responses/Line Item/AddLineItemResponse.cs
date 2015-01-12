using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class AddLineItemResponse : ResponseBase
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Add Line Item Response Message.
        /// </summary>
        public AddLineItemResponse() : base()
        {

        }

        /// <summary>
        /// Initializes an instance of the Add Line Item Response Message.
        /// </summary>
        /// <param name="doc">The Add Line Item Response Message.</param>
        public AddLineItemResponse(XDocument doc)
            : base(doc)
        {

        }

        #endregion
    }
}
