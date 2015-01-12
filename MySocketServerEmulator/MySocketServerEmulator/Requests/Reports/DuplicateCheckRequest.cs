using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MySocketServerEmulator
{
    public class DuplicateCheckRequest : RequestBase
    {
        public DuplicateCheckRequest()
            : base()
        {
            FunctionType = "REPORT";
            Command = "DUPCHECK";
            DupCheckDate = DateTime.Now;
            DupCheckToTime = DateTime.Now;
            DupCheckToTime = DateTime.Now;

            m_bDeviceRequired = false;
        }

        public DuplicateCheckRequest(XDocument doc)
            : base(doc)
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            DupCheckDate = DateTime.Parse(transactionElement.Element("DUPCHECK_DATE").Value);
            DupCheckFromTime = DateTime.Parse(transactionElement.Element("DUPCHECK_FROMTIME").Value);
            DupCheckToTime = DateTime.Parse(transactionElement.Element("DUPCHECK_TOTIME").Value);
        }

        #region Properties

        /// <summary>
        /// Duplicate Check Date
        /// </summary>
        [RequestAttributes(Order = 6, Type = "D", Name = "DUPCHECK_DATE", Required = true)]
        public DateTime DupCheckDate { get; set; }

        /// <summary>
        /// Duplicate Check From Time
        /// </summary>
        [RequestAttributes(Order = 7, Type = "T", Name = "DUPCHECK_FROMTIME", Required = true)]
        public DateTime DupCheckFromTime { get; set; }

        /// <summary>
        /// Duplicate Check To Time
        /// </summary>
        [RequestAttributes(Order = 8, Type = "T", Name = "DUPCHECK_TOTIME", Required = true)]
        public DateTime DupCheckToTime { get; set; }

        #endregion
    }
}
