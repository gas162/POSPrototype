using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class AuthorizePaymentRequest : PaymentRequest
    {
        public AuthorizePaymentRequest()
            : base()
        {
            Command = "AUTH";
            TransAmount = 0.0f;
        }

        public AuthorizePaymentRequest(XDocument doc)
            : base()
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            TransAmount = float.Parse(transactionElement.Element("TRANS_AMOUNT").Value);

            if (transactionElement.Element("AUTH_CODE") != null)
                AuthCode = transactionElement.Element("AUTH_CODE").Value;
            if (transactionElement.Element("MANUAL_ENTRY") != null)
                ManualEntry = transactionElement.Element("MANUAL_ENTRY").Value == "TRUE" ? true : false;
            if (transactionElement.Element("FORCE_FLAG") != null)
                ForceFlag = transactionElement.Element("FORCE_FLAG").Value == "TRUE" ? true : false;

            // Level II
            if (transactionElement.Element("TAX_AMOUNT") != null)
                TaxAmount = float.Parse(transactionElement.Element("TAX_AMOUNT").Value);
            if (transactionElement.Element("TAX_IND") != null)
                TaxIndicator = transactionElement.Element("TAX_IND").Value;
            if (transactionElement.Element("CMRCL_FLAG") != null)
                CommecrialFlag = transactionElement.Element("CMRCL_FLAG").Value;
        }

        #region Properties

        /// <summary>
        /// Trans Amount
        /// </summary>
        [RequestAttributes(Order = 6, Type = "F", Name = "TRANS_AMOUNT", Min=1, Max=5, DecimalMin=2, DecimalMax=2, Required = true)]
        public float TransAmount { get; set; }

        /// <summary>
        /// Payment Type
        /// </summary>
        [RequestAttributes(Order = 7, Type = "L", Name = "PAYMENT_TYPE", Required = false)]
        public override string PaymentType { get; set; }

        /// <summary>
        /// Authorization Code
        /// </summary>
        [RequestAttributes(Order = 8, Type = "C", Name = "AUTH_CODE", Min = 1, Max = 16, Required = false)]
        public string AuthCode { get; set; }

        /// <summary>
        /// Payment Type
        /// </summary>
        [RequestAttributes(Order = 9, Type = "B", Name = "MANUAL_ENTRY", Required = false)]
        public bool? ManualEntry { get; set; }

        /// <summary>
        /// Force Flag
        /// </summary>
        [RequestAttributes(Order = 10, Type = "B", Name = "FORCE_FLAG", Required = false)]
        public bool? ForceFlag { get; set; }


        // Level II Properties

        /// <summary>
        /// Tax Amount
        /// </summary>
        [RequestAttributes(Order = 11, Type = "F", Name = "TAX_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = false)]
        public float? TaxAmount { get; set; }

        /// <summary>
        /// Tax Indicator
        /// </summary>
        [RequestAttributes(Order = 12, Type = "L", Name = "TAX_IND", Required = false)]
        public string TaxIndicator { get; set; }

        /// <summary>
        /// Commecrial Flag
        /// </summary>
        [RequestAttributes(Order = 13, Type = "L", Name = "CMRCL_FLAG", Required = false)]
        public string CommecrialFlag { get; set; }


        #endregion
    }
}
