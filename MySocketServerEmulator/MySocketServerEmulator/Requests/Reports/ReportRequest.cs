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
    public class SearchField
    {
        public SearchField()
        {

        }

        #region Properties

        /// <summary>
        /// Start Trans Date
        /// </summary>
        [RequestAttributes(Order = 1, Type = "D", Name = "START_TRANS_DATE", Required = false)]
        public DateTime? StartTransDate { get; set; }

        /// <summary>
        /// Start Trans Time
        /// </summary>
        [RequestAttributes(Order = 2, Type = "T", Name = "START_TRANS_TIME", Required = false)]
        public DateTime? StartTransTime { get; set; }

        /// <summary>
        /// End Trans Date
        /// </summary>
        [RequestAttributes(Order = 3, Type = "D", Name = "END_TRANS_DATE", Required = false)]
        public DateTime? EndTransDate { get; set; }

        /// <summary>
        /// End Trans Time
        /// </summary>
        [RequestAttributes(Order = 4, Type = "T", Name = "END_TRANS_TIME", Required = false)]
        public DateTime? EndTransTime { get; set; }

        /// <summary>
        /// Payment Type
        /// </summary>
        [RequestAttributes(Order = 5, Type = "L", Name = "PAYMENT_TYPE", Required = false)]
        public string PaymentType { get; set; }

        /// <summary>
        /// Request Command
        /// </summary>
        [RequestAttributes(Order = 6, Type = "L", Name = "REQUEST_COMMAND", Required = false)]
        public string RequestCommand { get; set; }

        /// <summary>
        /// Account Number
        /// </summary>
        [RequestAttributes(Order = 7, Type = "N", Name = "ACCT_NUM", Min=1, Max=16, Required = false)]
        public string AccNum { get; set; }

        /// <summary>
        /// Invoice
        /// </summary>
        [RequestAttributes(Order = 8, Type = "C", Name = "INVOICE", Min = 1, Max = 6, Required = false)]
        public string Invoice { get; set; }

        /// <summary>
        /// Batch Sequence Number
        /// </summary>
        [RequestAttributes(Order = 9, Type = "N", Name = "BATCH_SEQ_NUM", Min = 1, Max = 10, Required = false)]
        public int? BatchSeqNum { get; set; }

        /// <summary>
        /// Start Transaction Amount
        /// </summary>
        [RequestAttributes(Order = 10, Type = "F", Name = "START_TRANS_AMOUNT", Min=1, Max=1, DecimalMin=2, DecimalMax=2, Required = false)]
        public float? StartTransAmount { get; set; }

        /// <summary>
        /// End Transaction Amount
        /// </summary>
        [RequestAttributes(Order = 11, Type = "F", Name = "END_TRANS_AMOUNT", Min = 1, Max = 1, DecimalMin = 2, DecimalMax = 2, Required = false)]
        public float? EndTransAmount { get; set; }

        /// <summary>
        /// Start TroutD
        /// </summary>
        [RequestAttributes(Order = 12, Type = "N", Name = "START_TROUTD", Min = 1, Max = 10, Required = false)]
        public int? StartTroutD { get; set; }

        /// <summary>
        /// End TroutD
        /// </summary>
        [RequestAttributes(Order = 13, Type = "N", Name = "END_TROUTD", Min = 1, Max = 10, Required = false)]
        public int? EndTroutD { get; set; }

        #endregion

    }

    public class RequestReportResponseField
    {
        public RequestReportResponseField()
        {
            CtrOutd = String.Empty;
        }

        #region Properties

        /// <summary>
        /// Internal Sequence Number
        /// </summary>
        [RequestAttributes(Order = 1, Type = "L", Name = "INTRN_SEQ_NUM", Required = false)]
        public string IntrnSeqNum { get; set; }

        /// <summary>
        /// Processor ID
        /// </summary>
        [RequestAttributes(Order = 2, Type = "L", Name = "PROCESSOR_ID", Required = false)]
        public string ProcessorID { get; set; }

        /// <summary>
        /// Batch Sequence Number
        /// </summary>
        [RequestAttributes(Order = 3, Type = "L", Name = "BATCH_SEQ_NUM", Required = false)]
        public string BatchSeqNum { get; set; }

        /// <summary>
        /// Invoice
        /// </summary>
        [RequestAttributes(Order = 4, Type = "L", Name = "INVOICE", Required = false)]
        public string Invoice { get; set; }

        /// <summary>
        /// Command
        /// </summary>
        [RequestAttributes(Order = 5, Type = "L", Name = "COMMAND", Required = false)]
        public string Command { get; set; }

        /// <summary>
        /// Account Number
        /// </summary>
        [RequestAttributes(Order = 6, Type = "L", Name = "ACCT_NUM", Required = false)]
        public string AccNum { get; set; }

        /// <summary>
        /// Expiration Month
        /// </summary>
        [RequestAttributes(Order = 7, Type = "L", Name = "EXP_MONTH", Required = false)]
        public string ExpMonth { get; set; }

        /// <summary>
        /// Expiration Year
        /// </summary>
        [RequestAttributes(Order = 8, Type = "L", Name = "EXP_YEAR", Required = false)]
        public string ExpYear { get; set; }

        /// <summary>
        /// Cardholder
        /// </summary>
        [RequestAttributes(Order = 9, Type = "L", Name = "CARDHOLDER", Required = false)]
        public string Cardholder { get; set; }

        /// <summary>
        /// Transaction Amount
        /// </summary>
        [RequestAttributes(Order = 10, Type = "L", Name = "TRANS_AMOUNT", Required = false)]
        public string TransAmount { get; set; }

        /// <summary>
        /// Reference
        /// </summary>
        [RequestAttributes(Order = 11, Type = "L", Name = "REFERENCE", Required = false)]
        public string Reference { get; set; }

        /// <summary>
        /// Transaction Date
        /// </summary>
        [RequestAttributes(Order = 12, Type = "L", Name = "TRANS_DATE", Required = false)]
        public string TransDate { get; set; }

        /// <summary>
        /// Transaction Time
        /// </summary>
        [RequestAttributes(Order = 13, Type = "L", Name = "TRANS_TIME", Required = false)]
        public string TransTime { get; set; }

        /// <summary>
        /// Orginal Sequence Number
        /// </summary>
        [RequestAttributes(Order = 14, Type = "L", Name = "ORIG_SEQ_NUM", Required = false)]
        public string OrigSeqNum { get; set; }

        /// <summary>
        /// Status Code
        /// </summary>
        [RequestAttributes(Order = 15, Type = "L", Name = "STATUS_CODE", Required = false)]
        public string StatusCode { get; set; }

        /// <summary>
        /// CtrOutd
        /// </summary>
        [RequestAttributes(Order = 16, Type = "L", Name = "CTROUTD", Required = true)]
        public string CtrOutd { get; set; }

        /// <summary>
        /// Payment Type
        /// </summary>
        [RequestAttributes(Order = 17, Type = "L", Name = "PAYMENT_TYPE", Required = false)]
        public string PaymentType{ get; set; }

        /// <summary>
        /// Payment Media
        /// </summary>
        [RequestAttributes(Order = 18, Type = "L", Name = "PAYMENT_MEDIA", Required = false)]
        public string PaymentMedia { get; set; }

        /// <summary>
        /// Result Code
        /// </summary>
        [RequestAttributes(Order = 19, Type = "L", Name = "RESULT_CODE", Required = false)]
        public string ResultCode { get; set; }

        /// <summary>
        /// Auth Code
        /// </summary>
        [RequestAttributes(Order = 20, Type = "L", Name = "AUTH_CODE", Required = false)]
        public string AuthCode { get; set; }

        /// <summary>
        /// Trace Code
        /// </summary>
        [RequestAttributes(Order = 21, Type = "L", Name = "TRACE_CODE", Required = false)]
        public string TraceCode { get; set; }

        /// <summary>
        /// AVS Code
        /// </summary>
        [RequestAttributes(Order = 22, Type = "L", Name = "AVS_CODE", Required = false)]
        public string AVSCode { get; set; }

        /// <summary>
        /// CVV2 Code
        /// </summary>
        [RequestAttributes(Order = 23, Type = "L", Name = "CVV2_CODE", Required = false)]
        public string CVV2Code { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        [RequestAttributes(Order = 24, Type = "L", Name = "USERID", Required = false)]
        public string UserID { get; set; }

        /// <summary>
        /// Cashback Amount
        /// </summary>
        [RequestAttributes(Order = 25, Type = "L", Name = "CASHBACK_AMNT", Required = false)]
        public string CashbackAmnt { get; set; }

        /// <summary>
        /// Tip Amount 
        /// </summary>
        [RequestAttributes(Order = 26, Type = "L", Name = "TIP_AMOUNT", Required = false)]
        public string TipAmount { get; set; }

        /// <summary>
        /// Response Reference
        /// </summary>
        [RequestAttributes(Order = 27, Type = "L", Name = "RESPONSE_REFERENCE", Required = false)]
        public string ResponseReference { get; set; }

        /// <summary>
        /// R Auth Code
        /// </summary>
        [RequestAttributes(Order = 28, Type = "L", Name = "R_AUTH_CODE", Required = false)]
        public string RAuthCode { get; set; }

        /// <summary>
        /// Column X
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
        /// <param name="command">The report type</param>
        public ReportRequest(string command)
        {
            FunctionType = "REPORT";
            Command = command;
            MaxNumRecordsReturned = -1;
            SearchFields = new SearchField();
            ResponseFields = new RequestReportResponseField();

            m_bDeviceRequired = false;
        }

        public ReportRequest(XDocument doc)
            : base(doc)
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            MaxNumRecordsReturned = int.Parse(transactionElement.Element("MAX_NUM_RECORDS_RETURNED").Value);

            SearchFields = new SearchField();
            XElement searchElement = transactionElement.Element("SEARCHFIELDS");

            if (searchElement.Element("START_TRANS_DATE") != null)
                SearchFields.StartTransDate = DateTime.Parse(searchElement.Element("START_TRANS_DATE").Value);
            if (searchElement.Element("START_TRANS_TIME") != null)
                SearchFields.StartTransTime = DateTime.Parse(searchElement.Element("START_TRANS_TIME").Value);
            if (searchElement.Element("END_TRANS_DATE") != null)
                SearchFields.EndTransDate = DateTime.Parse(searchElement.Element("END_TRANS_DATE").Value);
            if (searchElement.Element("END_TRANS_TIME") != null)
                SearchFields.EndTransTime = DateTime.Parse(searchElement.Element("END_TRANS_TIME").Value);
            if (searchElement.Element("PAYMENT_TYPE") != null)
                SearchFields.PaymentType = searchElement.Element("PAYMENT_TYPE").Value;
            if (searchElement.Element("REQUEST_COMMAND") != null)
                SearchFields.RequestCommand = searchElement.Element("REQUEST_COMMAND").Value;
            if (searchElement.Element("ACCT_NUM") != null)
                SearchFields.AccNum = searchElement.Element("ACCT_NUM").Value;
            if (searchElement.Element("INVOICE") != null)
                SearchFields.Invoice = searchElement.Element("INVOICE").Value;
            if (searchElement.Element("BATCH_SEQ_NUM") != null)
                SearchFields.BatchSeqNum = int.Parse(searchElement.Element("BATCH_SEQ_NUM").Value);
            if (searchElement.Element("START_TRANS_AMOUNT") != null)
                SearchFields.StartTransAmount = int.Parse(searchElement.Element("START_TRANS_AMOUNT").Value);
            if (searchElement.Element("END_TRANS_AMOUNT") != null)
                SearchFields.EndTransAmount = int.Parse(searchElement.Element("END_TRANS_AMOUNT").Value);
            if (searchElement.Element("START_TROUTD") != null)
                SearchFields.StartTroutD = int.Parse(searchElement.Element("START_TROUTD").Value);
            if (searchElement.Element("END_TROUTD") != null)
                SearchFields.EndTroutD = int.Parse(searchElement.Element("END_TROUTD").Value);

            ResponseFields = new RequestReportResponseField();
            XElement responseElement = transactionElement.Element("RESPONSEFIELDS");

            if (responseElement.Element("INTRN_SEQ_NUM") != null)
                ResponseFields.IntrnSeqNum = responseElement.Element("INTRN_SEQ_NUM").Value;
            if (responseElement.Element("PROCESSOR_ID") != null)
                ResponseFields.ProcessorID = responseElement.Element("PROCESSOR_ID").Value;
            if (responseElement.Element("BATCH_SEQ_NUM") != null)
                ResponseFields.BatchSeqNum = responseElement.Element("BATCH_SEQ_NUM").Value;
            if (responseElement.Element("INVOICE") != null)
                ResponseFields.Invoice = responseElement.Element("INVOICE").Value;
            if (responseElement.Element("COMMAND") != null)
                ResponseFields.Command = responseElement.Element("COMMAND").Value;
            if (responseElement.Element("ACCT_NUM") != null)
                ResponseFields.AccNum = responseElement.Element("ACCT_NUM").Value;
            if (responseElement.Element("EXP_MONTH") != null)
                ResponseFields.ExpMonth = responseElement.Element("EXP_MONTH").Value;
            if (responseElement.Element("EXP_YEAR") != null)
                ResponseFields.ExpYear = responseElement.Element("EXP_YEAR").Value;
            if (responseElement.Element("CARDHOLDER") != null)
                ResponseFields.Cardholder = responseElement.Element("CARDHOLDER").Value;
            if (responseElement.Element("TRANS_AMOUNT") != null)
                ResponseFields.TransAmount = responseElement.Element("TRANS_AMOUNT").Value;
            if (responseElement.Element("REFERENCE") != null)
                ResponseFields.Reference = responseElement.Element("REFERENCE").Value;
            if (responseElement.Element("TRANS_DATE") != null)
                ResponseFields.TransDate = responseElement.Element("TRANS_DATE").Value;
            if (responseElement.Element("TRANS_TIME") != null)
                ResponseFields.TransTime = responseElement.Element("TRANS_TIME").Value;
            if (responseElement.Element("ORIG_SEQ_NUM") != null)
                ResponseFields.OrigSeqNum = responseElement.Element("ORIG_SEQ_NUM").Value;
            if (responseElement.Element("STATUS_CODE") != null)
                ResponseFields.StatusCode = responseElement.Element("STATUS_CODE").Value;
            if (responseElement.Element("CTROUTD") != null)
                ResponseFields.CtrOutd = responseElement.Element("CTROUTD").Value;
            if (responseElement.Element("PAYMENT_TYPE") != null)
                ResponseFields.PaymentType = responseElement.Element("PAYMENT_TYPE").Value;
            if (responseElement.Element("PAYMENT_MEDIA") != null)
                ResponseFields.PaymentMedia = responseElement.Element("PAYMENT_MEDIA").Value;
            if (responseElement.Element("RESULT_CODE") != null)
                ResponseFields.ResultCode = responseElement.Element("RESULT_CODE").Value;
            if (responseElement.Element("AUTH_CODE") != null)
                ResponseFields.AuthCode = responseElement.Element("AUTH_CODE").Value;
            if (responseElement.Element("TRANS_AMOUNT") != null)
                ResponseFields.TransAmount = responseElement.Element("TRANS_AMOUNT").Value;
            if (responseElement.Element("TRACE_CODE") != null)
                ResponseFields.TraceCode = responseElement.Element("TRACE_CODE").Value;
            if (responseElement.Element("AVS_CODE") != null)
                ResponseFields.AVSCode = responseElement.Element("AVS_CODE").Value;
            if (responseElement.Element("CVV2_CODE") != null)
                ResponseFields.CVV2Code = responseElement.Element("CVV2_CODE").Value;
            if (responseElement.Element("USERID") != null)
                ResponseFields.UserID = responseElement.Element("USERID").Value;
            if (responseElement.Element("CASHBACK_AMNT") != null)
                ResponseFields.CashbackAmnt = responseElement.Element("CASHBACK_AMNT").Value;
            if (responseElement.Element("TIP_AMOUNT") != null)
                ResponseFields.TipAmount = responseElement.Element("TIP_AMOUNT").Value;
            if (responseElement.Element("RESPONSE_REFERENCE") != null)
                ResponseFields.ResponseReference = responseElement.Element("RESPONSE_REFERENCE").Value;
            if (responseElement.Element("R_AUTH_CODE") != null)
                ResponseFields.RAuthCode = responseElement.Element("R_AUTH_CODE").Value;
            if (responseElement.Element("COL_X") != null)
                ResponseFields.ColX = responseElement.Element("COL_X").Value;
        }

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
            else if (SearchFields.EndTransDate == null && SearchFields.StartTransDate != null)
            {
                searchElement.Add(new XElement("END_TRANS_DATE", ((DateTime)SearchFields.StartTransDate).ToString(dateFormat)));
            }

            if (SearchFields.EndTransTime != null)
            {
                searchElement.Add(new XElement("END_TRANS_TIME", ((DateTime)SearchFields.EndTransTime).ToString(timeFormat)));
            }
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

        //TODO: Add code to see if MaxNumRecords is no greater than 8000.

        #region Properties

        /// <summary>
        /// Maximum Number of Records Returned
        /// </summary>
        [RequestAttributes(Order = 6, Type = "N", Name = "MAX_NUM_RECORDS_RETURNED", Min = 1, Max = 4, Required = true)]
        public int MaxNumRecordsReturned { get; set; }

        /// <summary>
        /// Search Fields
        /// </summary>
        [RequestAttributes(Order = 7, Type = "L", Name = "SEARCHFIELDS", Required = true)]
        public SearchField SearchFields { get; set; }

        /// <summary>
        /// Response Fields
        /// </summary>
        [RequestAttributes(Order = 8, Type = "L", Name = "RESPONSEFIELDS", Required = true)]
        public RequestReportResponseField ResponseFields { get; set; }

        #endregion
    }
}
