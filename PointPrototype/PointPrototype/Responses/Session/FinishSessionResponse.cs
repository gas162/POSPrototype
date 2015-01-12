using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class FinishSessionResponse : ResponseBase
    {
        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Finish Session Response Message.
        /// </summary>
        public FinishSessionResponse() : base()
        {

        }

        /// <summary>
        /// Initializes an instance of the Finish Session Response Message.
        /// </summary>
        /// <param name="doc">the Finish Session Response message.</param>
        public FinishSessionResponse(XDocument doc)
            : base(doc)
        {

        }

        #endregion
    }
}
