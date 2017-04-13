using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class Event : WorldStateObject
    {
        # region variables
        private string m_prop = "";
        public string prop
        {
            get
            {
                return m_prop;
            }
        }
        private string m_imageUrl = "";
        public string imageUrl
        {
            get
            {
                return m_imageUrl;
            }
        }

        private bool m_priority = false;
        public bool priority
        {
            get
            {
                return m_priority;
            }
        }
        private bool m_mobileOnly = false;
        public bool mobileOnly
        {
            get
            {
                return m_mobileOnly;
            }
        }

        private DateTime m_date = new DateTime();
        public DateTime date
        {
            get
            {
                return m_date;
            }
        }
        private DateTime m_eventStartDate = new DateTime();
        public DateTime eventStartDate
        {
            get
            {
                return m_eventStartDate;
            }
        }
        
        private Dictionary<string, string> m_messages = new Dictionary<string, string>();
        public Dictionary<string, string> messages
        {
            get
            {
                return m_messages;
            }
        }
        # endregion


        public Event(JObject ev)
        {
            m_id = ev["_id"]["$oid"].ToString();
            foreach (var jobj in ev["Messages"])
            {
                if (!m_messages.ContainsKey(jobj["LanguageCode"].ToString()))
                    m_messages.Add(jobj["LanguageCode"].ToString(), jobj["Message"].ToString());
            }
            m_prop = ev["Prop"].ToString();
            m_date = WorldStateHelper.unixTimeToDateTime(ev["Date"]["$date"]["$numberLong"].ToObject<long>());
            // try parse eventStartDate
            if (ev["EventStartDate"] != null)
            {
                try
                {
                    m_eventStartDate = WorldStateHelper.unixTimeToDateTime(ev["EventStartDate"]["$date"]["$numberLong"].ToObject<long>());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception while parsing eventStartDate for id: " + m_id);
                }
            }
            // try imageUrl
            if (ev["ImageUrl"] != null)
            {
                try
                {
                    m_imageUrl = ev["ImageUrl"].ToString();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception while parsing imageUrl for id: " + m_id);
                }
            }
            m_priority = ev["Priority"].ToObject<bool>();
            m_mobileOnly = ev["MobileOnly"].ToObject<bool>();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            foreach (var m in m_messages)
            {
                if (m.Key == "en")
                {
                    str.AppendLine(m.Value);
                    break;
                }
            }
            TimeSpan timeUp = DateTime.UtcNow - m_date;
            str.AppendLine("(" + timeUp.Days + (timeUp.Days != 1 ? " days ago) " : " day ago) ") + m_date.ToString());
            str.AppendLine(m_prop);

            return str.ToString();
        }
    }
}

/*
Event: list of objects
	_id: object
		$oid: string
	Messages: list of objects
		LanguageCode: string
		Message: string
	Prop: string (url)
	Date: object
		$date: object
            $numberLong: long
    EventStartDate: object?
        
	ImageUrl: string (url)
	Priority: bool
	MobileOnly: bool
*/