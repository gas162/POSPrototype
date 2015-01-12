using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace MySocketServerEmulator
{
    public class RemoveSAFResponse : SAFResponse
    {
         #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Remove SAF Response Message.
        /// </summary>
        public RemoveSAFResponse() : base()
        {

        }

        /// <summary>
        /// Initializes an instance of the Remove SAF Response Message.
        /// </summary>
        /// <param name="doc">the Remove SAF Response message.</param>
        public RemoveSAFResponse(XDocument doc)
            : base(doc)
        {

        }

        #endregion

    }
}
