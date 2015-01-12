using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace PointPrototype
{
    public class Merchandise
    {
        
        #region Constructor

        /// <summary>
        /// Initializes an empty instance of a Merchandise object.
        /// </summary>
        public Merchandise()
        {
            LineItemID = -1;
            Description = String.Empty;
            Quantity = -1;
            UnitPrice = 0.0f;
            ExtendedPrice = 0.0f;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Merchandise Line Item ID
        /// </summary>
        [RequestAttributes(Order = 1, Type = "N", Name = "LINE_ITEM_ID", Min = 1, Max = 10, Required = true)]
        public int LineItemID { get; set; }

        /// <summary>
        /// Gets or sets the SKU
        /// </summary>
        [RequestAttributes(Order = 2, Type = "C", Name = "SKU", Min = 1, Max = 50, Required = false)]
        public string SKU { get; set; }

        /// <summary>
        /// Gets or sets the UPC
        /// </summary>
        [RequestAttributes(Order = 3, Type = "C", Name = "UPC", Min = 1, Max = 50, Required = false)]
        public string UPC { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        [RequestAttributes(Order = 4, Type = "C", Name = "DESCRIPTION", Min = 1, Max = 31,  Required = true)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Quantity
        /// </summary>
        [RequestAttributes(Order = 5, Type = "N", Name = "QUANTITY", Min = 1, Max = 10, Required = true)]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the Unit Price
        /// </summary>
        [RequestAttributes(Order = 6, Type = "F", Name = "UNIT_PRICE", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the Extended Price
        /// </summary>
        [RequestAttributes(Order = 7, Type = "F", Name = "EXTENDED_PRICE", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float ExtendedPrice { get; set; }

        #endregion
    }

    public class Offer
    {

        #region Constructor

        /// <summary>
        /// Initializes an empty instance of the Offer object.
        /// </summary>
        public Offer()
        {
            Type = String.Empty;
            LineItemID = -1;
            Description = String.Empty;
            OfferAmount = 0.0f;
            OfferLineItem = -1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Type of offer
        /// </summary>
        [RequestAttributes(Order = 1, Type = "L", Name = "TYPE", Required = false)]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the Offer Line Item ID
        /// </summary>
        [RequestAttributes(Order = 2, Type = "N", Name = "LINE_ITEM_ID", Min = 1, Max = 10, Required = true)]
        public int LineItemID { get; set; }

        /// <summary>
        /// Gets or sets the SKU
        /// </summary>
        [RequestAttributes(Order = 3, Type = "C", Name = "SKU", Min = 1, Max = 50, DecimalMin = 2, DecimalMax = 2, Required = false)]
        public string SKU { get; set; }

        /// <summary>
        ///Gets or sets the Description
        /// </summary>
        [RequestAttributes(Order = 4, Type = "C", Name = "DESCRIPTION", Min = 1, Max = 31,  Required = true)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Offer Amount
        /// </summary>
        [RequestAttributes(Order = 5, Type = "F", Name = "OFFER_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float OfferAmount { get; set; }

        /// <summary>
        /// Gets or sets the Merchandise Line Item ID which the Offer will apply
        /// </summary>
        [RequestAttributes(Order = 6, Type = "N", Name = "OFFER_LINE_ITEM", Min = 1, Max = 10, Required = true)]
        public int OfferLineItem { get; set; }

        #endregion

    }

    public class LineItems
    {
        private List<Merchandise> m_listMerchandiseItems;
        private List<Offer> m_listOfferItems;

        #region Constructor

        /// <summary>
        /// Initializes an empty instance of a Line Items object.
        /// </summary>
        public LineItems()
        {
            m_listMerchandiseItems = new List<Merchandise>();
            m_listOfferItems = new List<Offer>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of Merchandise Items
        /// </summary>
        public List<Merchandise> MerchandiseItems
        {
            get
            {
                return m_listMerchandiseItems;
            }
        }

        /// <summary>
        /// Gets the list of Offer Items
        /// </summary>
        public List<Offer> OfferItems
        {
            get
            {
                return m_listOfferItems;
            }
        }

        #endregion
    }

    public class AddLineItemRequest : RequestBase
    {
        /// <summary>
        /// Initializes an empty instance of the Add Line Item Request message.
        /// </summary>
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

        #region Methods

        /// <summary>
        /// Validates entered merchandise or offers in the Line Items object.
        /// </summary>
        /// <returns>The LINEITEMS XML Element.</returns>
        public XElement ValidateLineItems()
        {
            XElement lineItems = new XElement("LINE_ITEMS");

            // values to track how many valid items.
            int validMerchandiseCount = 0;
            int validOfferCount = 0;

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
                    validMerchandiseCount++;
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
                    validOfferCount++;
                }
            }

            // There must be at least one valid merchandise or one valid offer.
            if (validMerchandiseCount > 0 || validOfferCount > 0)
            {
                return lineItems;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Running Tax Amount
        /// </summary>
        [RequestAttributes(Order = 6, Type = "F", Name = "RUNNING_TAX_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float RunningTaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the Running Transaction Amount
        /// </summary>
        [RequestAttributes(Order = 7, Type = "F", Name = "RUNNING_TRANS_AMOUNT", Min = 1, Max = 5, DecimalMin = 2, DecimalMax = 2, Required = true)]
        public float RunningTransAmount { get; set; }

        /// <summary>
        /// Gets or sets the Line Items Object
        /// </summary>
        [RequestAttributes(Order = 8, Type = "L", Name = "LINE_ITEMS", Required = true)]
        public LineItems LineItemsObject { get; private set; }

        #endregion


    }
}
