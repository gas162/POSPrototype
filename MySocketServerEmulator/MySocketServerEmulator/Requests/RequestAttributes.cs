using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocketServerEmulator
{
    public class RequestAttributes : Attribute
    {
        /// <summary>
        /// Initializes the attributes for a request message.
        /// </summary>
        public RequestAttributes()
        {
            Order = 1;
            Type = "S";
            Name = String.Empty;
            Required = false;
            Min = -1;
            Max = -1;
            DecimalMin = -1;
            DecimalMax = -1;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the Order that the request field appears in an XML section.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the Type of field.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the Name of the field to be the name of the XML Element.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets whether the field is required.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets the minimum field length (only applies to Types C, F or N)
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Gets or sets the maximum field length (only applies to Types C, F or N)
        /// </summary>
        public int Max { get; set; }

        // Only applies for Type F
        /// <summary>
        /// The minimum number of digits right of the decimal.
        /// </summary>
        public int DecimalMin { get; set; }

        /// <summary>
        /// The maximum number of digits right of the decimal.
        /// </summary>
        public int DecimalMax { get; set; }

        #endregion
    }
}
