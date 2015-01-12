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
    public class LastTransactionResponse : ResponseBase
    {

        #region Constructors

        public LastTransactionResponse() : base()
        {

        }


        public LastTransactionResponse(XDocument doc)
            : base(doc)
        {
            XElement responseMessage = doc.Element("RESPONSE");

            if (responseMessage.Element("AUTH_CODE") != null)
                AuthCode = responseMessage.Element("AUTH_CODE").Value;

            if (responseMessage.Element("CLIENT_ID") != null)
                ClientID = responseMessage.Element("CLIENT_ID").Value;

            if (responseMessage.Element("COMMAND") != null)
                Command = responseMessage.Element("COMMAND").Value;

            if (responseMessage.Element("CTROUTD") != null)
                Ctroutd = int.Parse(responseMessage.Element("CTROUTD").Value);

            if (responseMessage.Element("INTRN_SEQ_NUM") != null)
                IntrnSeqNum = int.Parse(responseMessage.Element("INTRN_SEQ_NUM").Value);

            if (responseMessage.Element("PAYMENT_TYPE") != null)
                PaymentType = responseMessage.Element("PAYMENT_TYPE").Value;

            if (responseMessage.Element("PAYMENT_MEDIA") != null)
                PaymentMedia = responseMessage.Element("PAYMENT_MEDIA").Value;

            if (responseMessage.Element("REFERENCE") != null)
                Reference = responseMessage.Element("REFERENCE").Value;

            if (responseMessage.Element("TRANS_AMOUNT") != null)
                TransAmount = float.Parse(responseMessage.Element("TRANS_AMOUNT").Value);

            if (responseMessage.Element("APPROVED_AMOUNT") != null)
                ApprovedAmount = float.Parse(responseMessage.Element("APPROVED_AMOUNT").Value);

            if (responseMessage.Element("TRANS_DATE") != null)
                TransDate = DateTime.Parse(responseMessage.Element("TRANS_DATE").Value);

            if (responseMessage.Element("TRANS_TIME") != null)
                TransTime = DateTime.Parse(responseMessage.Element("TRANS_TIME").Value);

            if (responseMessage.Element("TRANS_SEQ_NUM") != null)
                TransSeqNum = int.Parse(responseMessage.Element("TRANS_SEQ_NUM").Value);

            if (responseMessage.Element("TROUTD") != null)
                Troutd = int.Parse(responseMessage.Element("TROUTD").Value);
    
        }

        #endregion

        #region Methods

        protected override List<XElement> GenerateMessagePortion()
        {
            List<XElement> listClassSpecificElements = new List<XElement>();

            if (!String.IsNullOrEmpty(AuthCode))
                listClassSpecificElements.Add(new XElement("AUTH_CODE", AuthCode));

            if (!String.IsNullOrEmpty(ClientID))
                listClassSpecificElements.Add(new XElement("CLIENT_ID", ClientID));

            if (!String.IsNullOrEmpty(Command))
                listClassSpecificElements.Add(new XElement("COMMAND", Command));

            if (Ctroutd != null)
                listClassSpecificElements.Add(new XElement("CTROUTD", Ctroutd));

            if (IntrnSeqNum != null)
                listClassSpecificElements.Add(new XElement("INTRN_SEQ_NUM", IntrnSeqNum));

            if (!String.IsNullOrEmpty(PaymentType))
                listClassSpecificElements.Add(new XElement("PAYMENT_TYPE", PaymentType));

            if (!String.IsNullOrEmpty(PaymentMedia))
                listClassSpecificElements.Add(new XElement("PAYMENT_MEDIA", PaymentMedia));

            if (!String.IsNullOrEmpty(Reference))
                listClassSpecificElements.Add(new XElement("REFERENCE", Reference));

            if (TransAmount != null)
                listClassSpecificElements.Add(new XElement("TRANS_AMOUNT", TransAmount.Value.ToString("0.00")));

            if (ApprovedAmount != null)
                listClassSpecificElements.Add(new XElement("APPROVED_AMOUNT", ApprovedAmount.Value.ToString("0.00")));

            if (TransSeqNum != null)
                listClassSpecificElements.Add(new XElement("TRANS_SEQ_NUM", TransSeqNum));

            if (TransDate != null)
                listClassSpecificElements.Add(new XElement("TRANS_DATE", TransDate.Value.ToString("yyyy.MM.dd")));

            if (TransTime != null)
                listClassSpecificElements.Add(new XElement("TRANS_TIME", TransTime.Value.ToString("HH:mm:ss")));

            if (Troutd != null)
                listClassSpecificElements.Add(new XElement("TROUTD", Troutd));

            

            return listClassSpecificElements;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Authorization Code
        /// </summary>
        public string AuthCode { get; set; }

        /// <summary>
        /// Client ID
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// Command
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Client-specific Transaction Routing ID
        /// </summary>
        public int? Ctroutd { get; set; }

        /// <summary>
        /// Internal sequence number
        /// </summary>
        public int? IntrnSeqNum { get; set; }

        /// <summary>
        /// Reference
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Payment Type
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        /// Payment Media
        /// </summary>
        public string PaymentMedia { get; set; }

        /// <summary>
        /// Transaction Amount
        /// </summary>
        public float? TransAmount { get; set; }

        /// <summary>
        /// Approved Amount
        /// </summary>
        public float? ApprovedAmount { get; set; }

        /// <summary>
        /// Processor/Batch trans sequence number
        /// </summary>
        public int? TransSeqNum { get; set; }

        /// <summary>
        /// Transaction Date
        /// </summary>
        public DateTime? TransDate { get; set; }

        /// <summary>
        /// Transaction Time
        /// </summary>
        public DateTime? TransTime { get; set; }

        /// <summary>
        /// Transaction Routing ID
        /// </summary>
        public int? Troutd { get; set; }

        #endregion
    }
}
