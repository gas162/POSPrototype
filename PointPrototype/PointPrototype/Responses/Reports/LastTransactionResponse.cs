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
    public class LastTransactionResponse : ResponseBase
    {

        #region Constructors

        /// <summary>
        /// Initializes an empty instance of the Last Transaction Response Message.
        /// </summary>
        public LastTransactionResponse()
            : base()
        {

        }

        /// <summary>
        /// Initializes an instance of the Last Transaction Response Message.
        /// </summary>
        /// <param name="doc">The Last Transaction Response Message.</param>
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

        #region Properties

        /// <summary>
        /// Gets or sets the Authorization Code
        /// </summary>
        public string AuthCode { get; set; }

        /// <summary>
        /// Gets or sets the Client ID
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// Gets or sets the Command
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets the Client-specific Transaction Routing ID
        /// </summary>
        public int? Ctroutd { get; set; }

        /// <summary>
        /// Gets or sets the Internal sequence number
        /// </summary>
        public int? IntrnSeqNum { get; set; }

        /// <summary>
        /// Gets or sets the Reference
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the Payment Type
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        /// Gets or sets the Payment Media
        /// </summary>
        public string PaymentMedia { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Amount
        /// </summary>
        public float? TransAmount { get; set; }

        /// <summary>
        /// Gets or sets the Approved Amount
        /// </summary>
        public float? ApprovedAmount { get; set; }

        /// <summary>
        /// Gets or sets the Processor/Batch Transaction Sequence Number
        /// </summary>
        public int? TransSeqNum { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Date
        /// </summary>
        public DateTime? TransDate { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Time
        /// </summary>
        public DateTime? TransTime { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Routing ID
        /// </summary>
        public int? Troutd { get; set; }

        #endregion
    }
}
