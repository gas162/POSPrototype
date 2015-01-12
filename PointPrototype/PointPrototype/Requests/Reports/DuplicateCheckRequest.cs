using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class DuplicateCheckRequest : RequestBase
    {
        /// <summary>
        /// Initializes an empty instance of the Duplicate Check Request Message.
        /// </summary>
        public DuplicateCheckRequest()
            : base()
        {
            FunctionType = "REPORT";
            Command = "DUPCHECK";
            DupCheckDate = DateTime.Now;
            DupCheckToTime = DateTime.Now;
            DupCheckToTime = DateTime.Now;

            m_bDeviceRequired = false;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the Duplicate Check Date
        /// </summary>
        [RequestAttributes(Order = 6, Type = "D", Name = "DUPCHECK_DATE", Required = true)]
        public DateTime DupCheckDate { get; set; }

        /// <summary>
        /// Gets or sets the Duplicate Check From Time
        /// </summary>
        [RequestAttributes(Order = 7, Type = "T", Name = "DUPCHECK_FROMTIME", Required = true)]
        public DateTime DupCheckFromTime { get; set; }

        /// <summary>
        /// Gets or sets the Duplicate Check To Time
        /// </summary>
        [RequestAttributes(Order = 8, Type = "T", Name = "DUPCHECK_TOTIME", Required = true)]
        public DateTime DupCheckToTime { get; set; }

        #endregion
    }
}
