using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class ResponseBase
    {

        #region Constructor

        /// <summary>
        /// Initializes an empty instance of the response request.
        /// </summary>
        public ResponseBase()
        {
            ResponseText = String.Empty;
            Result = String.Empty;
            ResultCode = -1;
            TerminationStatus = String.Empty;
        }

        /// <summary>
        /// Initializes a response message.
        /// </summary>
        /// <param name="doc">The response message.</param>
        public ResponseBase(XDocument doc)
        {
            // Read through the XDocument to initialize the the response message.
            XElement responseMessage = doc.Element("RESPONSE");

            ResponseText = responseMessage.Element("RESPONSE_TEXT").Value;

            if (responseMessage.Element("RESULT") != null)
                Result = responseMessage.Element("RESULT").Value;
            
            ResultCode = int.Parse(responseMessage.Element("RESULT_CODE").Value);
            TerminationStatus = responseMessage.Element("TERMINATION_STATUS").Value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks to see if the response is successful.
        /// </summary>
        /// <returns>The flag that the response is a success.</returns>
        public bool Success()
        {
            bool success = false;

            if (TerminationStatus == "SUCCESS")
                success = true;
            else
            {
                Console.WriteLine("ERROR: " + ResponseText);
                success = false;
            }

            return success;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Response Text
        /// </summary>
        public string ResponseText { get; set; }

        /// <summary>
        /// Gets or sets the Response Text
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets the Response Text
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// Gets or sets the Response Text
        /// </summary>
        public string TerminationStatus { get; set; }

        #endregion

    }
}
