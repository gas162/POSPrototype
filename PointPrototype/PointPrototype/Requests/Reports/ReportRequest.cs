using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace PointPrototype
{
    public class SearchField
    {
        /// <summary>
        /// Initializes an empty instace of the Search Field object.
        /// </summary>
        public SearchField()
        {

        }

        #region Properties

        /// <summary>
        /// Gets or sets the Starting Transaction Date
        /// </summary>
        [RequestAttributes(Order = 1, Type = "D", Name = "START_TRANS_DATE", Required = false)]
        public DateTime? StartTransDate { get; set; }

        /// <summary>
        /// Gets or sets the Starting Transaction Time
        /// </summary>
        [RequestAttributes(Order = 2, Type = "T", Name = "START_TRANS_TIME", Required = false)]
        public DateTime? StartTransTime { get; set; }

        /// <summary>
        /// Gets or sets the Ending Transaction Date
        /// </summary>
        [RequestAttributes(Order = 3, Type = "D", Name = "END_TRANS_DATE", Required = false)]
        public DateTime? EndTransDate { get; set; }

        /// <summary>
        /// Gets or sets the Ending Transaction Time
        /// </summary>
        [RequestAttributes(Order = 4, Type = "T", Name = "END_TRANS_TIME", Required = false)]
        public DateTime? EndTransTime { get; set; }

        /// <summary>
        /// Gets or sets the Payment Type
        /// </summary>
        [RequestAttributes(Order = 5, Type = "L", Name = "PAYMENT_TYPE", Required = false)]
        public string PaymentType { get; set; }

        /// <summary>
        /// Gets or sets the Request Command
        /// </summary>
        [RequestAttributes(Order = 6, Type = "L", Name = "REQUEST_COMMAND", Required = false)]
        public string RequestCommand { get; set; }

        /// <summary>
        /// Gets or sets the Account Number
        /// </summary>
        [RequestAttributes(Order = 7, Type = "N", Name = "ACCT_NUM", Min=1, Max=16, Required = false)]
        public string AccNum { get; set; }

        /// <summary>
        /// Gets or sets the Invoice
        /// </summary>
        [RequestAttributes(Order = 8, Type = "C", Name = "INVOICE", Min = 1, Max = 6, Required = false)]
        public string Invoice { get; set; }

        /// <summary>
        /// Gets or sets the Batch Sequence Number
        /// </summary>
        [RequestAttributes(Order = 9, Type = "N", Name = "BATCH_SEQ_NUM", Min = 1, Max = 10, Required = false)]
        public int? BatchSeqNum { get; set; }

        /// <summary>
        /// Gets or sets the Starting Transaction Amount
        /// </summary>
        [RequestAttributes(Order = 10, Type = "F", Name = "START_TRANS_AMOUNT", Min=1, Max=1, DecimalMin=2, DecimalMax=2, Required = false)]
        public float? StartTransAmount { get; set; }

        /// <summary>
        /// Gets or sets the Ending Transaction Amount
        /// </summary>
        [RequestAttributes(Order = 11, Type = "F", Name = "END_TRANS_AMOUNT", Min = 1, Max = 1, DecimalMin = 2, DecimalMax = 2, Required = false)]
        public float? EndTransAmount { get; set; }

        /// <summary>
        /// Gets or sets the Starting Transaction Routing ID
        /// </summary>
        [RequestAttributes(Order = 12, Type = "N", Name = "START_TROUTD", Min = 1, Max = 10, Required = false)]
        public int? StartTroutD { get; set; }

        /// <summary>
        /// Gets or sets the Ending Transaction Routing ID
        /// </summary>
        [RequestAttributes(Order = 13, Type = "N", Name = "END_TROUTD", Min = 1, Max = 10, Required = false)]
        public int? EndTroutD { get; set; }

        #endregion

    }

    public class RequestReportResponseField
    {
        /// <summary>
        /// Initiaizes an empty instance of the Respone Field object.
        /// </summary>
        public RequestReportResponseField()
        {

        }

        #region Properties

        /// <summary>
        /// Gets or sets the Internal Sequence Number
        /// </summary>
        [RequestAttributes(Order = 1, Type = "L", Name = "INTRN_SEQ_NUM", Required = false)]
        public string IntrnSeqNum { get; set; }

        /// <summary>
        /// Gets or sets the Processor ID
        /// </summary>
        [RequestAttributes(Order = 2, Type = "L", Name = "PROCESSOR_ID", Required = false)]
        public string ProcessorID { get; set; }

        /// <summary>
        /// Gets or sets the Batch Sequence Number
        /// </summary>
        [RequestAttributes(Order = 3, Type = "L", Name = "BATCH_SEQ_NUM", Required = false)]
        public string BatchSeqNum { get; set; }

        /// <summary>
        /// Gets or sets the Invoice
        /// </summary>
        [RequestAttributes(Order = 4, Type = "L", Name = "INVOICE", Required = false)]
        public string Invoice { get; set; }

        /// <summary>
        /// Gets or sets the Command
        /// </summary>
        [RequestAttributes(Order = 5, Type = "L", Name = "COMMAND", Required = false)]
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets the Account Number
        /// </summary>
        [RequestAttributes(Order = 6, Type = "L", Name = "ACCT_NUM", Required = false)]
        public string AccNum { get; set; }

        /// <summary>
        /// Gets or sets the Expiration Month
        /// </summary>
        [RequestAttributes(Order = 7, Type = "L", Name = "EXP_MONTH", Required = false)]
        public string ExpMonth { get; set; }

        /// <summary>
        /// Gets or sets the Expiration Year
        /// </summary>
        [RequestAttributes(Order = 8, Type = "L", Name = "EXP_YEAR", Required = false)]
        public string ExpYear { get; set; }

        /// <summary>
        /// Gets or sets the Cardholder
        /// </summary>
        [RequestAttributes(Order = 9, Type = "L", Name = "CARDHOLDER", Required = false)]
        public string Cardholder { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Amount
        /// </summary>
        [RequestAttributes(Order = 10, Type = "L", Name = "TRANS_AMOUNT", Required = false)]
        public string TransAmount { get; set; }

        /// <summary>
        /// Gets or sets the Reference
        /// </summary>
        [RequestAttributes(Order = 11, Type = "L", Name = "REFERENCE", Required = false)]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Date
        /// </summary>
        [RequestAttributes(Order = 12, Type = "L", Name = "TRANS_DATE", Required = false)]
        public string TransDate { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Time
        /// </summary>
        [RequestAttributes(Order = 13, Type = "L", Name = "TRANS_TIME", Required = false)]
        public string TransTime { get; set; }

        /// <summary>
        /// Gets or sets the Orginal Sequence Number
        /// </summary>
        [RequestAttributes(Order = 14, Type = "L", Name = "ORIG_SEQ_NUM", Required = false)]
        public string OrigSeqNum { get; set; }

        /// <summary>
        /// Gets or sets the Status Code
        /// </summary>
        [RequestAttributes(Order = 15, Type = "L", Name = "STATUS_CODE", Required = false)]
        public string StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the Client-specific Transaction Routing ID
        /// </summary>
        [RequestAttributes(Order = 16, Type = "L", Name = "CTROUTD", Required = true)]
        public string CtrOutd { get; set; }

        /// <summary>
        /// Gets or sets the Payment Type
        /// </summary>
        [RequestAttributes(Order = 17, Type = "L", Name = "PAYMENT_TYPE", Required = false)]
        public string PaymentType { get; set; }

        /// <summary>
        /// Gets or sets the Payment Media
        /// </summary>
        [RequestAttributes(Order = 18, Type = "L", Name = "PAYMENT_MEDIA", Required = false)]
        public string PaymentMedia { get; set; }

        /// <summary>
        /// Gets or sets the Result Code
        /// </summary>
        [RequestAttributes(Order = 19, Type = "L", Name = "RESULT_CODE", Required = false)]
        public string ResultCode { get; set; }

        /// <summary>
        /// Gets or sets the Authorization Code
        /// </summary>
        [RequestAttributes(Order = 20, Type = "L", Name = "AUTH_CODE", Required = false)]
        public string AuthCode { get; set; }

        /// <summary>
        /// Gets or sets the Trace Code
        /// </summary>
        [RequestAttributes(Order = 21, Type = "L", Name = "TRACE_CODE", Required = false)]
        public string TraceCode { get; set; }

        /// <summary>
        /// Gets or sets the AVS Code
        /// </summary>
        [RequestAttributes(Order = 22, Type = "L", Name = "AVS_CODE", Required = false)]
        public string AVSCode { get; set; }

        /// <summary>
        /// Gets or sets the CVV2 Code
        /// </summary>
        [RequestAttributes(Order = 23, Type = "L", Name = "CVV2_CODE", Required = false)]
        public string CVV2Code { get; set; }

        /// <summary>
        /// Gets or sets the User ID
        /// </summary>
        [RequestAttributes(Order = 24, Type = "L", Name = "USERID", Required = false)]
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the Cashback Amount
        /// </summary>
        [RequestAttributes(Order = 25, Type = "L", Name = "CASHBACK_AMNT", Required = false)]
        public string CashbackAmnt { get; set; }

        /// <summary>
        /// Gets or sets the Tip Amount 
        /// </summary>
        [RequestAttributes(Order = 26, Type = "L", Name = "TIP_AMOUNT", Required = false)]
        public string TipAmount { get; set; }

        /// <summary>
        /// Gets or sets the Response Reference
        /// </summary>
        [RequestAttributes(Order = 27, Type = "L", Name = "RESPONSE_REFERENCE", Required = false)]
        public string ResponseReference { get; set; }

        /// <summary>
        /// Gets or sets the Response Authorization Code
        /// </summary>
        [RequestAttributes(Order = 28, Type = "L", Name = "R_AUTH_CODE", Required = false)]
        public string RAuthCode { get; set; }

        /// <summary>
        /// Gets or sets the Column X
        /// </summary>
        [RequestAttributes(Order = 29, Type = "L", Name = "COL_X", Required = false)]
        public string ColX { get; set; }

        #endregion
    }
    
    public class ReportRequest : RequestBase
    {
        /// <summary>
        /// Initializes the Report Request message.
        /// </summary>
        /// <param name="command">The Type of Report (Either DAYSUMMARY, PRESETTLEMENT, or TRANSEARCH)</param>
        public ReportRequest(string command)
        {
            FunctionType = "REPORT";
            Command = command;
            MaxNumRecordsReturned = -1;
            SearchFields = new SearchField();
            ResponseFields = new RequestReportResponseField();

            m_bDeviceRequired = false;
        }

        /// <summary>
        /// Validates the SearchFields object.
        /// </summary>
        /// <returns>The SEARCHFIELDS XML Element</returns>
        public XElement ValidateSearchFields()
        {
            XElement searchElement = new XElement("SEARCHFIELDS");
            string dateFormat = "yyyy.MM.dd";
            string timeFormat = "HH:mm:ss";
            string floatFormat = "0.00";

            // Validate Start Date and Time
            if (SearchFields.StartTransDate != null)
            {
                searchElement.Add(new XElement("START_TRANS_DATE", ((DateTime)SearchFields.StartTransDate).ToString(dateFormat)));
            }

            if (SearchFields.StartTransTime != null)
            {
                searchElement.Add(new XElement("START_TRANS_TIME", ((DateTime)SearchFields.StartTransTime).ToString(timeFormat)));
            }


            // Validate End Date and Time
            if (SearchFields.EndTransDate != null)
            {
                searchElement.Add(new XElement("END_TRANS_DATE", ((DateTime)SearchFields.EndTransDate).ToString(dateFormat)));
            }
            // Default to START_TRANS_DATE if END_TRANS_DATE is not provided.
            else if (SearchFields.EndTransDate == null && SearchFields.StartTransDate != null)
            {
                searchElement.Add(new XElement("END_TRANS_DATE", ((DateTime)SearchFields.StartTransDate).ToString(dateFormat)));
            }

            if (SearchFields.EndTransTime != null)
            {
                searchElement.Add(new XElement("END_TRANS_TIME", ((DateTime)SearchFields.EndTransTime).ToString(timeFormat)));
            }
            // Default to START_TRANS_TIME if END_TRANS_TIME is not provided.
            else if (SearchFields.EndTransTime == null && SearchFields.StartTransTime != null)
            {
                searchElement.Add(new XElement("END_TRANS_TIME", ((DateTime)SearchFields.StartTransTime).ToString(timeFormat)));
            }


            // Validate Payment Type and Request Command
            if (!String.IsNullOrEmpty(SearchFields.PaymentType))
            {
                searchElement.Add(new XElement("PAYMENT_TYPE", SearchFields.PaymentType));
            }
            if (!String.IsNullOrEmpty(SearchFields.RequestCommand))
            {
                searchElement.Add(new XElement("REQUEST_COMMAND", SearchFields.RequestCommand));
            }

            // Validate Account Number
            if (!String.IsNullOrEmpty(SearchFields.AccNum))
            {
                string aNum = SearchFields.AccNum;
                string formattedAccNum = String.Empty;

                // Mask all the account number except for the first 6 and last 4 digits.
                if (aNum.Length < 16)
                {
                    string accNumPartOne = aNum.Substring(0, 6);
                    string accNumPartTwo = aNum.Substring((aNum.Length - 4), 4);

                    formattedAccNum = accNumPartOne;

                    while (formattedAccNum.Length < 12)
                    {
                        formattedAccNum = String.Concat(formattedAccNum, "*");
                    }
                    formattedAccNum = formattedAccNum + accNumPartTwo;
                }
                else
                    formattedAccNum = aNum;

                searchElement.Add(new XElement("ACCT_NUM", formattedAccNum));
            }

            if (!String.IsNullOrEmpty(SearchFields.Invoice))
            {
                searchElement.Add(new XElement("INVOICE", SearchFields.Invoice));
            }
            if (SearchFields.BatchSeqNum != null)
            {
                searchElement.Add(new XElement("BATCH_SEQ_NUM", SearchFields.BatchSeqNum.ToString()));
            }

            if (SearchFields.StartTransAmount != null)
            {
                searchElement.Add(new XElement("START_TRANS_AMOUNT", ((float)SearchFields.StartTransAmount).ToString(floatFormat)));
            }
            if (SearchFields.EndTransAmount != null)
            {
                searchElement.Add(new XElement("END_TRANS_AMOUNT", ((float)SearchFields.EndTransAmount).ToString(floatFormat)));
            }

            if (SearchFields.StartTroutD != null)
            {
                searchElement.Add(new XElement("START_TROUTD", SearchFields.StartTroutD.ToString()));
            }
            // Default to START_TROUTD if END_TROUTD is not provided.
            if (SearchFields.EndTroutD != null)
            {
                searchElement.Add(new XElement("END_TROUTD", SearchFields.EndTroutD.ToString()));
            }
            else if (SearchFields.EndTroutD == null && SearchFields.StartTroutD != null)
            {
                searchElement.Add(new XElement("END_TROUTD", SearchFields.StartTroutD.ToString()));
            }

            return searchElement;
        }

        /// <summary>
        /// Validates the ResponseFields object.
        /// </summary>
        /// <returns>The RESPONSEFIELDS XML Element</returns>
        public XElement ValidateResponseFields()
        {
            XElement responseElement = new XElement("RESPONSEFIELDS");
            Type fieldType = this.ResponseFields.GetType();
            int count = 0;

            PropertyInfo[] properties = fieldType.GetProperties().OrderBy(x => ((RequestAttributes)x.GetCustomAttribute(typeof(RequestAttributes))).Order).ToArray();
            foreach (PropertyInfo property in properties)
            {
                RequestAttributes attributes = (RequestAttributes)property.GetCustomAttribute(typeof(RequestAttributes));
                object obj = property.GetValue(this.ResponseFields, null);
                string strObj = obj != null ? obj.ToString() : String.Empty;

                // If a Response field is flagged, then include it in the request message.
                if (strObj == "INCLUDE")
                {
                    responseElement.Add(new XElement(attributes.Name));
                    count++;
                }
            }

            // The RESPONSEFIELDS Element must have at least one value.
            if (count == 0)
            {
                responseElement = null;
            }

            return responseElement;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the Maximum Number of Records Returned
        /// </summary>
        [RequestAttributes(Order = 6, Type = "N", Name = "MAX_NUM_RECORDS_RETURNED", Min = 1, Max = 4, Required = true)]
        public int MaxNumRecordsReturned { get; set; }

        /// <summary>
        /// Gets or sets the Search Fields object
        /// </summary>
        [RequestAttributes(Order = 7, Type = "L", Name = "SEARCHFIELDS", Required = true)]
        public SearchField SearchFields { get; set; }

        /// <summary>
        /// Gets or sets the Response Fields object
        /// </summary>
        [RequestAttributes(Order = 8, Type = "L", Name = "RESPONSEFIELDS", Required = true)]
        public RequestReportResponseField ResponseFields { get; set; }

        #endregion
    }
}
