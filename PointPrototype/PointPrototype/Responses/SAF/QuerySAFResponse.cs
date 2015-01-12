using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class QuerySAFResponse : SAFResponse
    {
         #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Query SAF Response Message.
        /// </summary>
        public QuerySAFResponse() : base()
        {

        }

        /// <summary>
        /// Initializes an instance of the Query SAF Response Message.
        /// </summary>
        /// <param name="doc">The Query SAF Response message.</param>
        public QuerySAFResponse(XDocument doc)
            : base(doc)
        {

        }

        #endregion
    }
}
