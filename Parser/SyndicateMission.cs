using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class SyndicateMission : WorldStateObject
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
        private string m_tag = "";
        public string r_tag
        {
            get
            {
                return m_tag;
            }
        }
        public string tag
        {
            get
            {
                return MapSyndicate.getSyndicate(m_tag);
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
        private List<string> m_nodes = new List<string>();
        public List<string> r_nodes
        {
            get
            {
                return m_nodes;
            }
        }
        public List<string> nodes
        {
            get
            {
                List<string> returnMe = new List<string>();
                foreach (string s in m_nodes)
                    returnMe.Add(MapSolNode.getNodeValue(s));
                return returnMe;
            }
        }
        # endregion

        public SyndicateMission()
        {
            // empty
        }

        public SyndicateMission(JObject sm)
        {
            m_id = sm["_id"]["$oid"].ToString();
            m_activation = WorldStateHelper.unixTimeToDateTime(sm["Activation"]["$date"]["$numberLong"].ToObject<long>());
            m_expiry = WorldStateHelper.unixTimeToDateTime(sm["Expiry"]["$date"]["$numberLong"].ToObject<long>());
            m_tag = sm["Tag"].ToString();
            m_seed = sm["Seed"].ToObject<int>();
            if (sm["Nodes"] != null)
                foreach (JValue jval in sm["Nodes"])
                    m_nodes.Add(jval.ToString());
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(tag + ":");
            str.AppendLine("Starts: " + m_activation.ToLocalTime());
            str.AppendLine("Expires: " + m_expiry.ToLocalTime());
            str.AppendLine("Missions: ");
            foreach (string n in m_nodes)
            {
                str.AppendLine("  " + MapSolNode.getNodeValue(n) + " (" + MapSolNode.getNodeEnemy(n) + ")");
            }

            return str.ToString();
        }
    }
}

/*
SyndicateMissions: list of objects
	_id: object
		$oid: string
	Activation: object
		$date: object
            $numberLong: long
	Expiry: object
		$date: object
            $numberLong: long
	Tag: string (syndicate name)
	Seed: int
	Nodes: list 
		of strings
*/