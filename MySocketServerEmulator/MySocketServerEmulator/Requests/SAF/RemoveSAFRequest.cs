using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class RemoveSAFRequest : RequestBase
    {
        public RemoveSAFRequest()
            : base()
        {
            FunctionType = "SAF";
            Command = "REMOVE";

            m_bDeviceRequired = false;
        }

        public RemoveSAFRequest(XDocument doc)
            : base()
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            if (transactionElement.Element("SAF_NUM_BEGIN") != null)
                SafNumBegin = int.Parse(transactionElement.Element("SAF_NUM_BEGIN").Value);
            if (transactionElement.Element("SAF_NUM_END") != null)
                SafNumEnd = int.Parse(transactionElement.Element("SAF_NUM_END").Value);
        }

        #region Properties

        /// <summary>
        /// Saf Number Begin
        /// </summary>
        [RequestAttributes(Order = 6, Type = "N", Name = "SAF_NUM_BEGIN", Min = 1, Max = 10, Required = false)]
        public int? SafNumBegin { get; set; }

        /// <summary>
        /// Saf Number End
        /// </summary>
        [RequestAttributes(Order = 7, Type = "N", Name = "SAF_NUM_END", Min = 1, Max = 10, Required = false)]
        public int? SafNumEnd { get; set; }


        #endregion
    }
}
