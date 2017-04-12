using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class FlashSale
    {
        # region variables
        private string m_typeName = "";
        public string r_typeName
        {
            get
            {
                return m_typeName;
            }
        }
        public string typeName
        {
            get
            {
                return MapResource.getResource(m_typeName);
                //return m_typeName;
            }
        }
        private DateTime m_startDate = new DateTime();
        public DateTime startDate
        {
            get
            {
                return m_startDate;
            }
        }
        private DateTime m_endDate = new DateTime();
        public DateTime endDate
        {
            get
            {
                return m_endDate;
            }
        }
        private bool m_featured = false;
        public bool featured
        {
            get
            {
                return m_featured;
            }
        }
        private bool m_popular = false;
        public bool popular
        {
            get
            {
                return m_popular;
            }
        }
        private int m_bannerIndex = 0;
        public int bannerIndex
        {
            get
            {
                return m_bannerIndex;
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
        private int m_regularOverride = 0;
        public int regularOverride
        {
            get
            {
                return m_regularOverride;
            }
        }
        private int m_premiumOverride = 0;
        public int premiumOverride
        {
            get
            {
                return m_premiumOverride;
            }
        }
        private int m_bogoBuy = 0;
        public int bogoBuy
        {
            get
            {
                return m_bogoBuy;
            }
        }
        private int m_bogoGet = 0;
        public int bogoGet
        {
            get
            {
                return m_bogoGet;
            }
        }
        # endregion

        public FlashSale()
        {
            // empty
        }

        public FlashSale(JObject fs)
        {
            m_typeName = fs["TypeName"].ToString();
            m_startDate = WorldStateHelper.unixTimeToDateTime(fs["StartDate"]["$date"]["$numberLong"].ToObject<long>());
            m_endDate = WorldStateHelper.unixTimeToDateTime(fs["EndDate"]["$date"]["$numberLong"].ToObject<long>());
            m_featured = fs["Featured"].ToObject<bool>();
            m_popular = fs["Popular"].ToObject<bool>();
            m_bannerIndex = fs["BannerIndex"].ToObject<int>();
            m_discount = fs["Discount"].ToObject<int>();
            m_regularOverride = fs["RegularOverride"].ToObject<int>();
            m_premiumOverride = fs["PremiumOverride"].ToObject<int>();
            m_bogoBuy = fs["BogoBuy"].ToObject<int>();
            m_bogoGet = fs["BogoGet"].ToObject<int>();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(MapResource.getResource(m_typeName));
            str.AppendLine("Start date: " + m_startDate);
            str.AppendLine("End date: " + m_endDate);
            if (m_featured) { str.AppendLine("Featured"); }
            if (m_popular) { str.AppendLine("Popular"); }
            if (m_discount > 0) { str.AppendLine("Discount: " + m_discount); }
            str.AppendLine("Price: " + m_premiumOverride + " Platinum");

            return str.ToString();
        }
    }
}

/*
FlashSales: list of ojbects
	TypeName: string (path)
	StartDate: object
		$date: object
            $numberLong: long
	EndDate: object
        $date: object
            $numberLong: long
	Featured: bool
	Popular: bool
	BannerIndex: int
	Discount: int
	RegularOverride: int
	PremiumOverride: int
	BogoBuy: int
	BogoGet: int
*/