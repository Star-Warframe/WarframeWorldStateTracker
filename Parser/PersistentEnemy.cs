using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class PersistentEnemy : WorldStateObject
    {
        # region variables
        private string m_agentType = "";
        string agentType
        {
            get
            {
                return m_agentType;
            }
        }
        private string m_locTag = "";
        string locTag
        {
            get
            {
                return m_locTag;
            }
        }
        private string m_icon = "";
        string icon
        {
            get
            {
                return m_icon;
            }
        }
        private int m_rank = 0;
        int rank
        {
            get
            {
                return m_rank;
            }
        }
        private double m_healthPercent = 0.0;
        double healthPercent
        {
            get
            {
                return m_healthPercent;
            }
        }
        private int m_fleeDamage = 0;
        int fleeDamage
        {
            get
            {
                return m_fleeDamage;
            }
        }
        private int m_region = 0;
        int region
        {
            get
            {
                return m_region;
            }
        }
        private string m_lastDiscoveredLocation = "";
        string lastDiscoveredLocation
        {
            get
            {
                return m_lastDiscoveredLocation;
            }
        }
        private DateTime m_lastDiscoveredTime = new DateTime();
        DateTime lastDiscoveredTime
        {
            get
            {
                return m_lastDiscoveredTime;
            }
        }
        private bool m_discovered = false;
        bool discovered
        {
            get
            {
                return m_discovered;
            }
        }
        private bool m_useTicketing = false;
        bool useTicketing
        {
            get
            {
                return m_useTicketing;
            }
        }
        # endregion


        public PersistentEnemy()
        {
            // empty
        }

        public PersistentEnemy(JObject pe)
        {
            m_id = pe["_id"]["$oid"].ToString();
            m_agentType = pe["AgentType"].ToString();
            m_locTag = pe["LocTag"].ToString();
            m_icon = pe["Icon"].ToString();
            m_rank = pe["Rank"].ToObject<int>();
            m_healthPercent = pe["HealthPercent"].ToObject<double>();
            m_fleeDamage = pe["FleeDamage"].ToObject<int>();
            m_region = pe["Region"].ToObject<int>();
            m_lastDiscoveredLocation = pe["LastDiscoveredLocation"].ToString();
            m_lastDiscoveredTime = WorldStateHelper.unixTimeToDateTime(pe["LastDiscoveredTime"]["$date"]["$numberLong"].ToObject<long>());
            m_discovered = pe["Discovered"].ToObject<bool>();
            m_useTicketing = pe["UseTicketing"].ToObject<bool>();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.Append("todo");

            return str.ToString();
        }
    }
}

/*
PersistentEnemies (assumed Stalkerlytes): list of objects
	_id: object
		$oid: string
	AgentType: string (path)
	LocTag: string (path)
	Icon: image (path)
	Rank: int
	HealthPercent: double/float
	FleeDamage: int
	Region: int
	LastDiscoveredLocation: string (SolNode###)
	LastDiscoveredTime: object
		$date: object
			$numberLong: long (string form)
	Discovered: bool
	UseTicketing: bool
*/