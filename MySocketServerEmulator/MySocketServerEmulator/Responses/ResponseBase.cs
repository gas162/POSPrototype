using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace MySocketServerEmulator
{
    public class ResponseBase
    {

        #region Constructor

        /// <summary>
        /// Initializes an empty instance of the Response Request
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
        /// <param name="doc">the response message</param>
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


        public XDocument GenerateResponseMessage()
        {
            XDocument doc = new XDocument();

            XElement responseElement = new XElement("RESPONSE");

            Type messageType = this.GetType();

            try
            {
                responseElement.Add(new XElement("RESPONSE_TEXT", ResponseText));
                if (!String.IsNullOrEmpty(Result))
                    responseElement.Add(new XElement("RESULT", Result));
                responseElement.Add(new XElement("RESULT_CODE", ResultCode));
                responseElement.Add(new XElement("TERMINATION_STATUS", TerminationStatus));

                List<XElement> moreElements = GenerateMessagePortion();
                if (moreElements.Count > 0)
                {
                    foreach (XElement element in moreElements)
                    {
                        responseElement.Add(element);
                    }
                }
            }
            catch
            {

            }

            doc.Add(responseElement);

            return doc;
        }

        protected virtual List<XElement> GenerateMessagePortion()
        {
            List<XElement> listClassSpecificElements = new List<XElement>();

            return listClassSpecificElements;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Response Text (RESPONSE_TEXT)
        /// </summary>
        public string ResponseText { get; set; }

        /// <summary>
        /// Gets or sets the Response Text (RESULT)
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets the Response Text (RESULT_CODE)
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// Gets or sets the Response Text (TERMINATION_STATUS)
        /// </summary>
        public string TerminationStatus { get; set; }

        #endregion

    }
}
