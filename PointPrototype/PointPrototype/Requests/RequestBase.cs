using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace PointPrototype
{
    public class RequestBase
    {
        #region Variables

        private XDocument m_xDocument = null;
        protected bool m_bDeviceRequired = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes an empty instance of the request message.
        /// </summary>
        public RequestBase()
        {
            FunctionType = String.Empty;
            Command = String.Empty;
            MacLabel = String.Empty;
            Mac = null;
            Counter = -1;
            m_xDocument = new XDocument();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validates the request message before it is sent.
        /// </summary>
        /// <returns>The flag indicated that the request message has been validated.</returns>
        public bool ValidateMessage()
        {
            XElement transaction = new XElement("TRANSACTION");
            StringBuilder sb = new StringBuilder();
            bool valid = true;
            Type messageType = this.GetType();

            try
            {

                PropertyInfo[] properties = messageType.GetProperties().OrderBy(x => ((RequestAttributes)x.GetCustomAttribute(typeof(RequestAttributes))).Order).ToArray();
                foreach (PropertyInfo property in properties)
                {
                    //Console.WriteLine(string.Format("Name: {0} | Value: {1}", property.Name, property.GetValue(this, null)));

                    RequestAttributes attributes = (RequestAttributes)property.GetCustomAttribute(typeof(RequestAttributes));

                    object obj = property.GetValue(this, null);

                    // For Register and UnRegister Request Messages, Ignore COUNTER and MAC; Ignore Mac Label for Register Request.
                    if ((FunctionType == "SECURITY" && Command == "REGISTER") && (property.Name == "Counter" || property.Name == "Mac" || property.Name == "MacLabel"))
                    {
                        continue;
                    }
                    if ((FunctionType == "SECURITY" && Command == "UNREGISTER") && (property.Name == "Counter" || property.Name == "Mac"))
                    {
                        continue;
                    }

                    string strObj = String.Empty;

                    switch (attributes.Type)
                    {
                        case "B":
                            if (obj != null)
                            {
                                bool boolObj = (bool)obj;
                                string boolValue = boolObj ? "TRUE" : "FALSE";
                                transaction.Add(new XElement(attributes.Name, boolValue));
                            }
                            else if (attributes.Required && obj == null)
                            {
                                valid = false;
                                sb.AppendLine(String.Format("Error: The required field, {0}, does not have a value.", property.Name));
                            }
                            break;
                        case "C":
                            strObj = obj != null ? obj.ToString() : String.Empty;
                            if (!string.IsNullOrEmpty(strObj))
                            {
                                if (strObj.Length < attributes.Min)
                                {
                                    Console.WriteLine(String.Format("Warning: The value for {0} is too short. Min {1}; Actual: {2}", property.Name, attributes.Min, strObj.Length));
                                }
                                if (strObj.Length > attributes.Max)
                                {
                                    Console.WriteLine(String.Format("Warning: Maximum length has exceeded limit for {0}. Max {1}; Actual: {2}", property.Name, attributes.Max, strObj.Length));
                                    strObj = strObj.Substring(0, attributes.Max);
                                }

                                transaction.Add(new XElement(attributes.Name, strObj));
                            }
                            else if (attributes.Required && strObj == String.Empty)
                            {
                                valid = false;
                                sb.AppendLine(String.Format("Error: The required field, {0}, does not have a value.", property.Name));
                            }
                            break;
                        case "D":
                            if (obj != null)
                            {
                                DateTime date = (DateTime)obj;
                                string formatedDate = date.ToString("yyyy.MM.dd");
                                transaction.Add(new XElement(attributes.Name, formatedDate));
                            }
                            else if (attributes.Required && obj == null)
                            {
                                valid = false;
                                sb.AppendLine(String.Format("Error: The required field, {0}, does not have a value.", property.Name));
                            }
                            break;
                        case "E":
                            if (obj != null)
                            {
                                string encodedString = obj.ToString();
                                transaction.Add(new XElement(attributes.Name, encodedString));
                            }
                            else if (attributes.Required && obj == null)
                            {
                                valid = false;
                                sb.AppendLine(String.Format("Error: The required field, {0}, does not have a value.", property.Name));
                            }
                            break;
                        case "F":
                            if (obj != null)
                            {
                                float num = float.Parse(obj.ToString());
                                string formatedFloat = num.ToString("0.00");
                                string strFormat = String.Empty;

                                string[] floatParts = formatedFloat.Split('.');

                                if (floatParts[0].Length < attributes.Min)
                                {
                                    Console.WriteLine(String.Format("Warning: The value for {0} is too short. Min {1}; Actual: {2}", property.Name, attributes.Min, floatParts[0].Length));
                                }
                                if (floatParts[0].Length > attributes.Max)
                                {
                                    Console.WriteLine(String.Format("Warning: Maximum length has exceeded limit for {0}. Max {1}; Actual: {2}", property.Name, attributes.Max, floatParts[0].Length));
                                    floatParts[0] = floatParts[0].Substring(0, attributes.Max);
                                }

                                transaction.Add(new XElement(attributes.Name, formatedFloat));
                            }
                            else if (attributes.Required && obj == null)
                            {
                                valid = false;
                                sb.AppendLine(String.Format("Error: The required field, {0}, does not have a value.", property.Name));
                            }
                            break;
                        case "L":
                            if (obj != null)
                            {
                                // If the L Type field is a string, then add it to the list. Otherwise iterate the list of child items.
                                if (obj.GetType() == typeof(string))
                                {
                                    transaction.Add(new XElement(attributes.Name, obj.ToString()));
                                }
                                else
                                {
                                    //XElement parent = new XElement(attributes.Name);

                                    if (FunctionType == "LINE_ITEM" & Command == "ADD")
                                    {
                                        XElement lineItemListElement = ((AddLineItemRequest)this).ValidateLineItems();
                                        transaction.Add(lineItemListElement);
                                    }
                                    else if (FunctionType == "REPORT")
                                    {
                                        if (Command == "DAYSUMMARY" || Command == "PRESETTLEMENT" || Command == "SETTLEERROR" || Command == "TRANSEARCH")
                                        {
                                            if (attributes.Name == "SEARCHFIELDS")
                                            {
                                                XElement searchFieldsElement = ((ReportRequest)this).ValidateSearchFields();
                                                transaction.Add(searchFieldsElement);
                                            }
                                            else
                                            {
                                                XElement responseFieldElement = ((ReportRequest)this).ValidateResponseFields();
                                                transaction.Add(responseFieldElement);
                                            }
                                        }
                                        else if (Command == "SETTLESUMMARY")
                                        {
                                            XElement responseFieldElement = ((SettlementSummaryRequest)this).ValidateResponseFields();
                                            transaction.Add(responseFieldElement);
                                        }
                                    }
                                    
                                }
                            }
                            else if (attributes.Required && obj == null)
                            {
                                valid = false;
                                sb.AppendLine(String.Format("Error: The required field, {0}, does not have a value.", property.Name));
                            }
                            break;
                        case "N":
                            if (obj != null)
                            {
                                int num = int.Parse(obj.ToString());
                                string strNum = num.ToString();

                                // Negative integers are not allowed
                                if (strObj != "-1")
                                {

                                    // This currently applies to EntryCode as there must be a minimum of 4 integers.
                                    if (strNum.Length < attributes.Min)
                                    {
                                        while (strNum.Length < attributes.Min)
                                        {
                                            strNum = "0" + strNum;
                                        }
                                    }
                                    if (strObj.Length > attributes.Max)
                                    {
                                        Console.WriteLine(String.Format("Warning: Maximum length has exceeded limit for {0}. Max {1}; Actual: {2}", property.Name, attributes.Max, strNum.Length));
                                        strNum = strNum.Substring(0, attributes.Max);
                                    }

                                    transaction.Add(new XElement(attributes.Name, strNum));
                                }
                                else if (strObj == "-1" && attributes.Required)
                                {
                                    valid = false;
                                    sb.AppendLine(String.Format("Error: The required field, {0}, does not have a value.", property.Name));
                                }
                            }
                            else if (attributes.Required && obj == null)
                            {
                                valid = false;
                                sb.AppendLine(String.Format("Error: The required field, {0}, does not have a value.", property.Name));
                            }
                            break;
                        case "S":
                            transaction.Add(new XElement(attributes.Name, obj.ToString()));
                            break;
                        case "T":
                            if (obj != null)
                            {
                                // Format the time portion of the date.
                                DateTime time = (DateTime)obj;
                                string formatedTime = time.ToString("HH:mm:ss");
                                transaction.Add(new XElement(attributes.Name, formatedTime));
                            }
                            else if (attributes.Required && obj == null)
                            {
                                valid = false;
                                sb.AppendLine(String.Format("Error: The required field, {0}, does not have a value.", property.Name));
                            }
                            break;
                        default:
                            break;
                    }

                }

                if (!String.IsNullOrEmpty(sb.ToString()))
                {
                    Console.WriteLine("The Request message has the following errors.");
                    Console.WriteLine(sb.ToString());
                }
                else
                {
                    m_xDocument.Add(transaction);
                }
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

        /// <summary>
        /// Gets the Xml Message to be sent.
        /// </summary>
        /// <returns>The Request Message as an XML Document.</returns>
        public XDocument GetMessage()
        {
            return m_xDocument;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Function Type
        /// </summary>
        [RequestAttributes(Order = 1, Type = "S", Name="FUNCTION_TYPE", Required = true)]
        public string FunctionType { get; protected set; }

        /// <summary>
        /// Gets or sets the Request Command
        /// </summary>
        [RequestAttributes(Order = 2, Type = "S", Name = "COMMAND", Required = true)]
        public string Command { get; protected set; }

        /// <summary>
        /// Gets or sets the Counter
        /// </summary>
        [RequestAttributes(Order = 3, Type = "N", Name = "COUNTER", Min = 1, Max = 10, Required = true)]
        public int Counter { get; set; }

        /// <summary>
        /// Gets or sets the MAC Address
        /// </summary>
        [RequestAttributes(Order = 4, Type = "E", Name = "MAC", Required = true)]
        public string Mac { get; set; }

        /// <summary>
        /// Gets or sets the MAC Label
        /// </summary>
        [RequestAttributes(Order = 5, Type = "C", Name = "MAC_LABEL", Min = 1, Max = 50, Required = true)]
        public string MacLabel { get; set; }

        #endregion
    }
}
