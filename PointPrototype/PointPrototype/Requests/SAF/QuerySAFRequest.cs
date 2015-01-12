using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class QuerySAFRequest : RequestBase
    {
        public QuerySAFRequest()
            : base()
        {
            FunctionType = "SAF";
            Command = "QUERY";

            m_bDeviceRequired = false;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the SAF Status
        /// </summary>
        [RequestAttributes(Order = 6, Type = "S", Name = "SAF_STATUS", Required = false)]
        public string SafStatus{ get; set; }

        /// <summary>
        /// Gets or sets the SAF Number to begin with
        /// </summary>
        [RequestAttributes(Order = 7, Type = "N", Name = "SAF_NUM_BEGIN", Min = 1, Max = 10, Required = false)]
        public int? SafNumBegin { get; set; }

        /// <summary>
        /// Gets or sets the SAF Number to end with
        /// </summary>
        [RequestAttributes(Order = 8, Type = "N", Name = "SAF_NUM_END", Min = 1, Max = 10, Required = false)]
        public int? SafNumEnd { get; set; }


        #endregion
    }
}
