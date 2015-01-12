using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PointPrototype
{
    public class RemoveLineItemResponse : ResponseBase
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Remove Line Item Response Message.
        /// </summary>
        public RemoveLineItemResponse() : base()
        {

        }

        /// <summary>
        /// Initializes an instance of the Remove Line Item Response Message.
        /// </summary>
        /// <param name="doc">The Remove Line Item Response Message.</param>
        public RemoveLineItemResponse(XDocument doc)
            : base(doc)
        {

        }

        #endregion
    }
}
