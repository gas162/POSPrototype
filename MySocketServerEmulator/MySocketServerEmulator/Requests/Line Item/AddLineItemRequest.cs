using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace MySocketServerEmulator
{
    public class Merchandise
    {
        public Merchandise()
        {
            LineItemID = -1;
            Description = String.Empty;
            Quantity = -1;
            UnitPrice = 0.0f;
            ExtendedPrice = 0.0f;
        }

        public Merchandise(XElement element)
        {
            LineItemID = int.Parse(element.Element("LINE_ITEM_ID").Value);

            if (element.Element("SKU") != null)
                SKU = element.Element("SKU").Value;
            if (element.Element("UPC") != null)
                UPC = element.Element("UPC").Value;

            Description = element.Element("DESCRIPTION").Value;
            Quantity = int.Parse(element.Element("QUANTITY").Value);
            UnitPrice = float.Parse(element.Element("UNIT_PRICE").Value);
            ExtendedPrice = float.Parse(element.Element("EXTENDED_PRICE").Value);
        }

        /// <summary>
        /// Line Item ID
        /// </summary>
        [RequestAttributes(Order = 1, Type = "N", Name = "LINE_ITEM_ID", Min = 1, Max = 10, Required = true)]
        public int LineItemID { get; set; }

        /// <summary>
        /// SKU
        /// </summary>
        [RequestAttributes(Order = 2, Type = "C", Name = "SKU", Min = 1, Max = 50, Required = false)]
        public string SKU { get; set; }

        /// <summary>
        /// UPC
        /// </summary>
        [RequestAttributes(Order = 3, Type = "C", Name = "UPC", Min = 1, Max = 50, Required = false)]
        public string UPC { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [RequestAttributes(Order = 4, Type = "C", Name = "DESCRIPTION", Min = 1, Max = 31,  Required = true)]
        public string Description { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        [RequestAttributes(Order = 5, Type = "N", Name = "QUANTITY", Min = 1, Max = 10, Required = true)]
        public int Quantity { get; set; }

        /// <summary>
        /// Unit Price
        /// </summary>
        [RequestAttributes(Order = 6, Type = "F", Name = "UNIT_PRICE", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float UnitPrice { get; set; }

        /// <summary>
        /// Extended Price
        /// </summary>
        [RequestAttributes(Order = 7, Type = "F", Name = "EXTENDED_PRICE", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float ExtendedPrice { get; set; }
    }

    public class Offer
    {
        public Offer()
        {
            Type = String.Empty;
            LineItemID = -1;
            Description = String.Empty;
            OfferAmount = 0.0f;
            OfferLineItem = -1;
        }

        public Offer(XElement element)
        {
            if (element.Element("TYPE") != null)
                Type = element.Element("TYPE").Value;

            LineItemID = int.Parse(element.Element("LINE_ITEM_ID").Value);

            if (element.Element("SKU") != null)
                SKU = element.Element("SKU").Value;

            Description = element.Element("DESCRIPTION").Value;
            OfferAmount = float.Parse(element.Element("OFFER_AMOUNT").Value);
            OfferLineItem = int.Parse(element.Element("OFFER_LINE_ITEM").Value);
        }

        /// <summary>
        /// Type
        /// </summary>
        [RequestAttributes(Order = 1, Type = "L", Name = "TYPE", Required = false)]
        public string Type { get; set; }

        /// <summary>
        /// Line Item ID
        /// </summary>
        [RequestAttributes(Order = 2, Type = "N", Name = "LINE_ITEM_ID", Min = 1, Max = 10, Required = true)]
        public int LineItemID { get; set; }

        /// <summary>
        /// SKU
        /// </summary>
        [RequestAttributes(Order = 3, Type = "C", Name = "SKU", Min = 1, Max = 50, DecimalMin = 2, DecimalMax = 2, Required = false)]
        public string SKU { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [RequestAttributes(Order = 4, Type = "C", Name = "DESCRIPTION", Min = 1, Max = 31,  Required = true)]
        public string Description { get; set; }

        /// <summary>
        /// Offer Amount
        /// </summary>
        [RequestAttributes(Order = 5, Type = "F", Name = "OFFER_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float OfferAmount { get; set; }

        /// <summary>
        /// Offer Line Item
        /// </summary>
        [RequestAttributes(Order = 6, Type = "N", Name = "OFFER_LINE_ITEM", Min = 1, Max = 10, Required = true)]
        public int OfferLineItem { get; set; }

    }

    public class LineItems
    {
        private List<Merchandise> m_listMerchandiseItems;
        private List<Offer> m_listOfferItems;

        public LineItems()
        {
            m_listMerchandiseItems = new List<Merchandise>();
            m_listOfferItems = new List<Offer>();
        }

        public LineItems(XElement element)
        {
            m_listMerchandiseItems = new List<Merchandise>();

            foreach (XElement merchandiseElement in element.Elements("MERCHANDISE"))
            {
                m_listMerchandiseItems.Add(new Merchandise(merchandiseElement));
            }

            m_listOfferItems = new List<Offer>();

            foreach (XElement offerElement in element.Elements("OFFER"))
            {
                m_listOfferItems.Add(new Offer(offerElement));
            }
        }

        public List<Merchandise> MerchandiseItems {
            get
            {
                return m_listMerchandiseItems;
            }
        }
        public List<Offer> OfferItems
        {
            get
            {
                return m_listOfferItems;
            }
        }
    }
    
    public class AddLineItemRequest : RequestBase
    {
        
        public AddLineItemRequest()
            : base()
        {
            FunctionType = "LINE_ITEM";
            Command = "ADD";
            RunningTaxAmount = 0.0f;
            RunningTransAmount = 0.0f;

            LineItemsObject = new LineItems();

            m_bDeviceRequired = true;
        }

        public AddLineItemRequest(XDocument doc)
            : base(doc)
        {
            XElement transactionElement = doc.Element("TRANSACTION");

            RunningTaxAmount = float.Parse(transactionElement.Element("RUNNING_TAX_AMOUNT").Value);
            RunningTransAmount = float.Parse(transactionElement.Element("RUNNING_TRANS_AMOUNT").Value);

            XElement lineItemElement = transactionElement.Element("LINE_ITEMS");
            LineItemsObject = new LineItems(lineItemElement);

            m_bDeviceRequired = true;
        }

        #region Methods

        public XElement ValidateLineItems()
        {
            XElement lineItems = new XElement("LINE_ITEMS");

            // Validate each merchandise item.
            foreach (Merchandise m in LineItemsObject.MerchandiseItems)
            {
                bool validMerchandise = true;

                // Check required fields.
                if (m.LineItemID == -1)
                {
                    validMerchandise = false;
                }
                if (String.IsNullOrEmpty(m.Description))
                {
                    validMerchandise = false;
                }
                if (m.Quantity == -1)
                {
                    validMerchandise = false;
                }
                if (m.UnitPrice == -1)
                {
                    validMerchandise = false;
                }
                if (m.ExtendedPrice == -1)
                {
                    validMerchandise = false;
                }

                if (validMerchandise)
                {
                    XElement merchandiseItem = new XElement("MERCHANDISE");
                    PropertyInfo[] properties = m.GetType().GetProperties().OrderBy(x => ((RequestAttributes)x.GetCustomAttribute(typeof(RequestAttributes))).Order).ToArray();
                    foreach (PropertyInfo property in properties)
                    {
                        object obj = property.GetValue(m, null);
                        RequestAttributes attributes = (RequestAttributes)property.GetCustomAttribute(typeof(RequestAttributes));
                        string strObj = obj != null ? obj.ToString() : String.Empty;
                        if ((property.Name == "SKU" || property.Name == "UPC") && String.IsNullOrEmpty(strObj))
                        {
                            continue;
                        }
                        if (attributes.Type == "F")
                        {
                            float num = float.Parse(obj.ToString());
                            string formatedFloat = num.ToString("0.00");
                            strObj = formatedFloat;
                        }
                        merchandiseItem.Add(new XElement(attributes.Name, strObj));
                    }
                    lineItems.Add(merchandiseItem);
                }
            }

            // Validate each offer item.
            foreach (Offer o in LineItemsObject.OfferItems)
            {
                bool validOffer = true;

                if (o.LineItemID == -1)
                {
                    validOffer = false;
                }
                if (String.IsNullOrEmpty(o.Description))
                {
                    validOffer = false;
                }
                if (o.OfferAmount == -1)
                {
                    validOffer = false;
                }
                if (o.OfferLineItem == -1)
                {
                    validOffer = false;
                }

                // Check required fields.
                if (validOffer)
                {
                    XElement offerItem = new XElement("OFFER");
                    PropertyInfo[] properties = o.GetType().GetProperties().OrderBy(x => ((RequestAttributes)x.GetCustomAttribute(typeof(RequestAttributes))).Order).ToArray();
                    foreach (PropertyInfo property in properties)
                    {
                        object obj = property.GetValue(o, null);
                        RequestAttributes attributes = (RequestAttributes)property.GetCustomAttribute(typeof(RequestAttributes));
                        string strObj = obj != null ? obj.ToString() : String.Empty;
                        if (property.Name == "SKU" && String.IsNullOrEmpty(strObj))
                        {
                            continue;
                        }
                        if (attributes.Type == "F")
                        {
                            float num = float.Parse(obj.ToString());
                            string formatedFloat = num.ToString("0.00");
                            strObj = formatedFloat;
                        }
                        offerItem.Add(new XElement(attributes.Name, strObj));
                    }
                    lineItems.Add(offerItem);
                }
            }

            return lineItems;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Running Tax Amount
        /// </summary>
        [RequestAttributes(Order = 6, Type = "F", Name = "RUNNING_TAX_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float RunningTaxAmount { get; set; }

        /// <summary>
        /// Running Transaction Amount
        /// </summary>
        [RequestAttributes(Order = 7, Type = "F", Name = "RUNNING_TRANS_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float RunningTransAmount { get; set; }


        // LINE_ITEMS
        [RequestAttributes(Order = 8, Type = "L", Name = "LINE_ITEMS", Required = true)]
        public LineItems LineItemsObject { get; private set; }

        #endregion


    }
}
