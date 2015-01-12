using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class QuerySAFRequest : RequestBase
    {
        public QuerySAFRequest()
            : base()
        {
            FunctionType = "SAF";
            Command = "QUERY";

            m_bDeviceRequired = false;
        }

        public QuerySAFRequest(XDocument doc)
            : base()
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            if (transactionElement.Element("SAF_STATUS") != null)
                SafStatus = transactionElement.Element("SAF_STATUS").Value;
            if (transactionElement.Element("SAF_NUM_BEGIN") != null)
                SafNumBegin = int.Parse(transactionElement.Element("SAF_NUM_BEGIN").Value);
            if (transactionElement.Element("SAF_NUM_END") != null)
                SafNumEnd = int.Parse(transactionElement.Element("SAF_NUM_END").Value);
        }

        #region Properties

        // <summary>
        /// Saf Status
        /// </summary>
        [RequestAttributes(Order = 6, Type = "L", Name = "SAF_STATUS", Required = false)]
        public string SafStatus{ get; set; }

        /// <summary>
        /// Saf Number Begin
        /// </summary>
        [RequestAttributes(Order = 7, Type = "N", Name = "SAF_NUM_BEGIN", Min = 1, Max = 10, Required = false)]
        public int? SafNumBegin { get; set; }

        /// <summary>
        /// Saf Number End
        /// </summary>
        [RequestAttributes(Order = 8, Type = "N", Name = "SAF_NUM_END", Min = 1, Max = 10, Required = false)]
        public int? SafNumEnd { get; set; }


        #endregion
    }
}
