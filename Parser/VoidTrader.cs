using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class VoidTrader : WorldStateObject
    {
        # region variables
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
        private string m_character = "";    // I should just set this to Baro Ki'Teer for everything but whatever
        public string character
        {
            get
            {
                if (m_character.Equals("Baro'Ki Teel"))
                    return "Baro Ki'Teer";
                else 
                    return m_character;
            }
        }
        private string m_node = "";
        public string r_node
        {
            get
            {
                return m_node;
            }
        }
        public string node
        {
            get
            {
                return MapSolNode.getNodeName(m_node);
            }
        }
        private List<Tuple<string, int, int>> m_manifest = new List<Tuple<string, int, int>>();
        public List<Tuple<string, int, int>> r_manifest
        {
            get
            {
                return m_manifest;
            }
        }
        public List<Tuple<string, int, int>> manifest
        {
            get
            {
                List<Tuple<string, int, int>> returnMe = new List<Tuple<string, int, int>>();
                foreach (Tuple<string, int, int> t in m_manifest)
                    returnMe.Add(new Tuple<string, int, int>(MapResource.getResource(t.Item1), t.Item2, t.Item3));
                return returnMe;
            }
        }
        # endregion

        public VoidTrader()
        {
            // empty
        }

        public VoidTrader(JObject vt)
        {
            m_id = vt["_id"]["$oid"].ToString();
            m_activation = WorldStateHelper.unixTimeToDateTime(vt["Activation"]["$date"]["$numberLong"].ToObject<long>());
            m_expiry = WorldStateHelper.unixTimeToDateTime(vt["Expiry"]["$date"]["$numberLong"].ToObject<long>());
            m_character = vt["Character"].ToString();
            m_node = vt["Node"].ToString();
            if (vt["Manifest"] != null)
            {
                foreach (JObject jobj in vt["Manifest"])
                {
                    m_manifest.Add(new Tuple<string, int, int>(jobj["ItemType"].ToString(), jobj["PrimePrice"].ToObject<int>(), jobj["RegularPrice"].ToObject<int>()));
                }
            }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(m_character == "Baro'Ki Teel" ? "Baro Ki'Teer" : m_character);
            str.Append("Status: ");
            if (m_activation <= DateTime.UtcNow && m_expiry >= DateTime.UtcNow)
            {
                TimeSpan tte = m_expiry - DateTime.UtcNow;
                str.AppendLine("Active (" + tte.Days + (tte.Days != 1 ? " days, " : " day, ") + tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") + tte.Minutes + (tte.Minutes != 1 ? " minutes left)" : " minute left)"));
            }
            else
            {
                str.AppendLine("Not active");
            }
            str.AppendLine(node);
            str.AppendLine("Starts: " + m_activation.ToLocalTime());
            str.AppendLine("Expires: " + m_expiry.ToLocalTime());
            if (m_manifest.Count > 0)
            {
                str.AppendLine("Items:");
                foreach (Tuple<string, int, int> t in manifest)
                {
                    str.AppendLine(t.Item1);
                    str.AppendLine("Ducats: " + t.Item2);
                    str.AppendLine("Credits: " + t.Item3);
                    str.AppendLine();
                }
            }

            return str.ToString();
        }
    }
}

/*
VoidTraders (i.e. the one void trader....): list of objects
	_id: object
		$oid: string
	Activation: object
		$date: object
            $numberLong: long
	Expiry: object
		$date: object
            $numberLong: long
	Character: string
	Node: string (<Planet>HUB)
	Manifest: list of objects
		ItemType: string (path)
		PrimePrice: int
		RegularPrice: int
*/