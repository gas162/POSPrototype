using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class SignatureRequest : RequestBase
    {
        /// <summary>
        /// Initializes an empty instance of the Signature Request Message.
        /// </summary>
        public SignatureRequest()
        {
            FunctionType = "DEVICE";
            Command = "SIGNATURE";

            m_bDeviceRequired = true;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the Display Text
        /// </summary>
        [RequestAttributes(Order = 6, Type = "C", Name = "DISPLAY_TEXT", Min = 1, Max = 50, Required = false)]
        public string DisplayText { get; set; }

        #endregion
    }
}
