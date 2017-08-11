using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class FissureMission : WorldStateObject
    {
        # region variables
        private int m_region = 0;
        public int region
        {
            get
            {
                return m_region;
            }
        }
        private int m_seed = 0;
        public int seed
        {
            get
            {
                return m_seed;
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
        private string m_node = "";
        public string r_node
        {
            get
            {
                return m_node;
            }
        }
        public string nodeName
        {
            get
            {
                return MapSolNode.getNodeValue(m_node);
                //return m_node;
            }
        }
        public string nodeType
        {
            get
            {
                return MapSolNode.getNodeType(m_node);
            }
        }
        public string nodeEnemy
        {
            get
            {
                return MapSolNode.getNodeEnemy(m_node);
            }
        }
        private string m_modifier = "";
        public string r_modifier
        {
            get
            {
                return m_modifier;
            }
        }
        public string modifier
        {
            get
            {
                return MapFissure.getModifier(m_modifier);
                //return m_modifier;
            }
        }
        private int m_num = 0;
        public int num
        {
            get
            {
                return m_num;
            }
        }
        # endregion

        public FissureMission()
        {
            // empty
        }
        
        public FissureMission(JObject fm)
        {
            m_id = fm["_id"]["$oid"].ToString();
            m_region = fm["Region"].ToObject<int>();
            m_seed = fm["Seed"].ToObject<int>();
            m_activation = WorldStateHelper.unixTimeToDateTime(fm["Activation"]["$date"]["$numberLong"].ToObject<long>());
            m_expiry = WorldStateHelper.unixTimeToDateTime(fm["Expiry"]["$date"]["$numberLong"].ToObject<long>());
            m_node = fm["Node"].ToString();
            m_modifier = fm["Modifier"].ToString();
            m_num = MapFissure.getNumber(m_modifier);
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(MapFissure.getModifier(m_modifier));
            str.AppendLine(MapSolNode.getNodeValue(m_node));
            str.AppendLine(MapSolNode.getNodeType(m_node) + " (" + MapSolNode.getNodeEnemy(m_node) + ")");
            str.AppendLine("Starts: " + m_activation.ToLocalTime().ToString("hh:mm tt"));
            TimeSpan tte = m_expiry - DateTime.UtcNow;
            int totalMinsLeft = (m_expiry - DateTime.UtcNow).Minutes;
            str.AppendLine("Expires: " + m_expiry.ToLocalTime().ToString("hh:mm tt") + " (" + (tte.Hours > 0 ? tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") : "") + tte.Minutes + (tte.Minutes != 1 ? " minutes left)" : " minute left)"));

            return str.ToString();
        }
    }
}

/*
ActiveMissions (Fissures): list of objects
	_id: object
		$oid: string
	Region: int
	Seed: int
	Activation: object
		sec: long
		usec: long
	Expiry: object
		sec: long
		usec: long
	Node: string (SolNode###)
	Modifier: string (VoidT[1-4])
*/