using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class StartSessionResponse : ResponseBase
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Start Session Response Message.
        /// </summary>
        public StartSessionResponse() : base()
        {

        }

        /// <summary>
        /// Initializes an instance of the Start Session Response Message.
        /// </summary>
        /// <param name="doc">the Start Session Response message.</param>
        public StartSessionResponse(XDocument doc)
            : base(doc)
        {

        }

        #endregion
    }
}
