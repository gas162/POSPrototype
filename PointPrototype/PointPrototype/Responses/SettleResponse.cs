using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PointPrototype
{
    public class SettleResponse : ResponseBase
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Settle Response Message.
        /// </summary>
        public SettleResponse() : base()
        {

        }

        /// <summary>
        /// Initializes an instance of the Settle Response Message.
        /// </summary>
        /// <param name="doc">the Settle Response message.</param>
        public SettleResponse(XDocument doc)
            : base(doc)
        {

        }

        #endregion
    }
}
