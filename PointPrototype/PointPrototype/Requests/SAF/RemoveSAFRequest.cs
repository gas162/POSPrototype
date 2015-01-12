using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class RemoveSAFRequest : RequestBase
    {
        public RemoveSAFRequest()
            : base()
        {
            FunctionType = "SAF";
            Command = "REMOVE";

            m_bDeviceRequired = false;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the starting SAf Number
        /// </summary>
        [RequestAttributes(Order = 6, Type = "N", Name = "SAF_NUM_BEGIN", Min = 1, Max = 10, Required = false)]
        public int? SafNumBegin { get; set; }

        /// <summary>
        /// Gets or sets the ending SAF Number
        /// </summary>
        [RequestAttributes(Order = 7, Type = "N", Name = "SAF_NUM_END", Min = 1, Max = 10, Required = false)]
        public int? SafNumEnd { get; set; }


        #endregion
    }
}
