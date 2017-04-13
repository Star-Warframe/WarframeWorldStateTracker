using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class DailyDeal
    {
        # region variables
        private string m_storeItem = "";
        public string r_storeItem
        {
            get
            {
                return m_storeItem;
            }
        }
        public string storeItem
        {
            get
            {
                return MapResource.getResource(m_storeItem);
                //return m_storeItem;
            }
        }
        private DateTime m_activation = new DateTime();
        public DateTime activation
        {
            get
            {
                return m_activation;
            }
        }
        private DateTime m_expiry = new DateTime();
        public DateTime expiry
        {
            get
            {
                return m_expiry;
            }
        }
        private int m_discount = 0;
        public int discount
        {
            get
            {
                return m_discount;
            }
        }
        private int m_originalPrice = 0;
        public int originalPrice
        {
            get
            {
                return m_originalPrice;
            }
        }
        private int m_salePrice = 0;
        public int salePrice
        {
            get
            {
                return m_salePrice;
            }
        }
        private int m_amountTotal = 0;
        public int amountTotal
        {
            get
            {
                return m_amountTotal;
            }
        }
        private int m_amountSold = 0;
        public int amountSold
        {
            get
            {
                return m_amountSold;
            }
        }
        # endregion

        public DailyDeal()
        {
            // empty
        }

        public DailyDeal(JObject dd)
        {
            m_storeItem = dd["StoreItem"].ToString();
            m_activation = WorldStateHelper.unixTimeToDateTime(dd["Activation"]["$date"]["$numberLong"].ToObject<long>());
            m_expiry = WorldStateHelper.unixTimeToDateTime(dd["Expiry"]["$date"]["$numberLong"].ToObject<long>());
            m_discount = dd["Discount"].ToObject<int>();
            m_originalPrice = dd["OriginalPrice"].ToObject<int>();
            m_salePrice = dd["SalePrice"].ToObject<int>();
            m_amountTotal = dd["AmountTotal"].ToObject<int>();
            m_amountSold = dd["AmountSold"].ToObject<int>();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(MapResource.getResource(m_storeItem));
            str.AppendLine("Original price: " + m_originalPrice + " Platinum");
            int d = 100 - (int)(Math.Floor((double)m_salePrice / (double)m_originalPrice * 100.0));
            str.AppendLine("Sale price: " + m_salePrice + " Platinum (" + d + "% off)");
            str.AppendLine((m_amountTotal - m_amountSold) + " left (of " + m_amountTotal + ")");
            str.AppendLine("Starts: " + m_activation.ToLocalTime());
            TimeSpan tte = (m_expiry - DateTime.UtcNow);
            str.AppendLine("Expires: " + m_expiry.ToLocalTime().ToString("hh:mm tt") + " (" + tte.Days + (tte.Days != 1 ? " days, " : " day, ") + tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") + tte.Minutes + (tte.Minutes != 1 ? " minutes left)" : " minute left)"));
            
            return str.ToString();
        }
    }
}

/*
DailyDeals (Darvo I assume in relay): list of objects
	StoreItem: string (path)
	Activation: object
		sec: long
		usec: long
	Expiry: object
		sec: long
		usec: long
	Discount: int
	OriginalPrice: int
	SalePrice: int
	AmountTotal: int
	AmountSold: int
*/