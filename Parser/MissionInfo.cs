using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
	public class MissionInfo
	{
		# region variables
        private string m_descText = "";
        public string r_descText
        {
            get
            {
                return m_descText;
            }
        }
        public string descText
        {
            get
            {
                return m_descText;  // no Languages mapper so placeholder
            }
        }
		private string m_missionType = "";
		public string r_missionType
		{
			get
			{
				return m_missionType;
			}
		}
		public string missionType
		{
			get
			{
				return MapMissionType.getMissionType(m_missionType);
			}
		}
		private string m_faction = "";
		public string r_faction
		{
			get
			{
				return m_faction;
			}
		}
		public string faction
		{
			get
			{
				return MapFaction.getFaction(m_faction);
			}
		}
		private string m_location = "";
		public string r_location
		{
			get
			{
				return m_location;
			}
		}
		public string location
		{
			get
			{
				return MapSolNode.getNodeName(m_location);
			}
		}
		private string m_levelOverride = "";
		public string levelOverride
		{
			get
			{
				return m_levelOverride;
			}
		}
		private string m_enemySpec = "";
		public string enemySpec
		{
			get
			{
				return m_enemySpec;
			}
		}
		private string m_extraEnemySpec = "";
		public string extraEnemySpec
		{
			get
			{
				return m_extraEnemySpec;
			}
		}
        private List<string> m_customAdvancedSpawners = new List<string>();
        public List<string> r_customAdvancedSpawners
        {
            get
            {
                return m_customAdvancedSpawners;
            }
        }
        public List<string> customAdvancedSpawners
        {
            get
            {
                return m_customAdvancedSpawners;    // placeholder
            }
        }
		private int m_minEnemyLevel = 0;
		public int minEnemyLevel
		{
			get
			{
				return m_minEnemyLevel;
			}
		}
		private int m_maxEnemyLevel = 0;
		public int maxEnemyLevel
		{
			get
			{
				return m_maxEnemyLevel;
			}
		}
		private double m_difficulty = 0.0;
		public double difficulty
		{
			get
			{
				return m_difficulty;
			}
		}
        private bool m_archwingRequired = false;
        public bool archwingRequired
        {
            get
            {
                return m_archwingRequired;
            }
        }
        private List<string> m_requiredItems = new List<string>();
        public List<string> r_requiredItems
        {
            get
            {
                return m_requiredItems;
            }
        }
        public List<string> requiredItems
        {
            get
            {
                List<string> returnMe = new List<string>();
                foreach(string s in m_requiredItems)
                {
                    returnMe.Add(MapResource.getResource(s));
                }
                return returnMe;
            }
        }
        private string m_vipAgent = "";
        public string r_vipAgent
        {
            get
            {
                return m_vipAgent;
            }
        }
        public string vipAgent
        {
            get
            {
                return MapResource.getResource(m_vipAgent);
            }
        }
        private bool m_leadersAlwaysAllowed = false;
        public bool leadersAlwaysAllowed
        {
            get
            {
                return m_leadersAlwaysAllowed;
            }
        }
        private string m_goalTag = "";
        public string goalTag
        {
            get
            {
                return m_goalTag;
            }
        }
        private string m_icon = "";
        public string icon
        {
            get
            {
                return m_icon;
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
		private int m_maxWaveNum = 0;
		public int maxWaveNum
		{
			get
			{
				return m_maxWaveNum;
			}
		}
        private string m_fxLayer = "";
        public string fxLayer
        {
            get
            {
                return m_fxLayer;
            }
        }
        private string m_eomBoss = "";
        public string r_eomBoss
        {
            get
            {
                return m_eomBoss;
            }
        }
        public string eomBoss
        {
            get
            {
                return m_eomBoss; //placeholder
            }
        }
        private string m_exclusiveWeapon = "";
        public string r_exclusiveWeapon
        {
            get
            {
                return m_exclusiveWeapon;
            }
        }
        public string exclusiveWeapon
        {
            get
            {
                return MapResource.getResource(m_exclusiveWeapon);
            }
        }
		private MissionReward m_missionReward = new MissionReward();
		public MissionReward missionReward
		{
			get
			{
				return m_missionReward;
			}
		}
		# endregion


		public MissionInfo()
		{
			// empty
		}

		public MissionInfo(JObject mi)
		{
            if (mi["missionType"] != null) { m_missionType = mi["missionType"].ToString(); }
            if (mi["faction"] != null) { m_faction = mi["faction"].ToString(); }
            if (mi["location"] != null) { m_location = mi["location"].ToString(); }
            if (mi["levelOverride"] != null) { m_levelOverride = mi["levelOverride"].ToString(); }
            if (mi["enemySpec"] != null) { m_enemySpec = mi["enemySpec"].ToString(); }
            if (mi["extraEnemySpec"] != null) { m_extraEnemySpec = mi["extraEnemySpec"].ToString(); }
            if (mi["customAdvancedSpawners"] != null)
            {
                foreach (JValue jval in mi["customAdvancedSpawners"])
                    m_customAdvancedSpawners.Add(jval.ToString());
            }
            if (mi["minEnemyLevel"] != null) { m_minEnemyLevel = mi["minEnemyLevel"].ToObject<int>(); }
            if (mi["maxEnemyLevel"] != null) { m_maxEnemyLevel = mi["maxEnemyLevel"].ToObject<int>(); }
            if (mi["difficulty"] != null) { m_difficulty = mi["difficulty"].ToObject<double>(); }
            if (mi["archwingRequired"] != null) { m_archwingRequired = mi["archwingRequired"].ToObject<bool>(); }
            if (mi["requiredItems"] != null)
            {
                foreach(JValue jval in mi["requiredItems"])
                {
                    m_requiredItems.Add(jval.ToString());
                }
            }
            if (mi["vipAgent"] != null) { m_vipAgent = mi["vipAgent"].ToString(); }
            if (mi["leadersAlwaysAllowed"] != null) { m_leadersAlwaysAllowed = mi["leadersAlwaysAllowed"].ToObject<bool>(); }
            if (mi["goalTag"] != null) { m_goalTag = mi["goalTag"].ToString(); }
            if (mi["icon"] != null) { m_icon = mi["icon"].ToString(); }
            if (mi["seed"] != null) { m_seed = mi["seed"].ToObject<int>(); }
            if (mi["maxWaveNum"] != null) { m_maxWaveNum = mi["maxWaveNum"].ToObject<int>(); }
            if (mi["fxLayer"] != null) { m_fxLayer = mi["fxLayer"].ToString(); }
            if (mi["eomBoss"] != null) { m_eomBoss = mi["eomBoss"].ToString(); }
            if (mi["exclusiveWeapon"] != null) { m_exclusiveWeapon = mi["exclusiveWeapon"].ToString(); }
            if (mi["missionReward"] != null) { m_missionReward = new MissionReward((JObject)mi["missionReward"]); }
		}
	}
}

/*
Alert: list of objects
	_id: object
		$oid: string
	Activation: object
        $date: object
            $numberLong: long
	Expiry: object
        $date: object
            $numberLong: long
	MissionInfo: object
        descText: string (optional)
		missionType: string
		faction: string
		location: string (SolNode###)
		levelOverride: string (path)
		enemySpec: string (path)
		extraEnemySpec: string (path) (optional)
        customAdvancedSpawners: list
            of strings
		minEnemyLevel: int
		maxEnemyLevel: int
		difficulty: double (or float, idk)
        archwingRequired: bool (optional)
        requiredItems: list (optional)
            of strings
        vipAgent: string (optional)
        leadersAlwaysAllowed: bool (optional)
        goalTag: string (optional)
        icon: string (optional)
		seed: int (optional)
		maxWaveNum: int (optional)
        fxLayer: string (optional)
        eomBoss: string (optional)
        exclusiveWeapon: string (optional)
		missionReward: object
			credits: int
			items: list 
				of strings
			countedItems: list of objects
				ItemType: string (path)
				ItemCount: int
            randomizedItems: string (optional)
			probablysomemorestuff: other
    ForceUnlock: bool (optional)
    Tag: string (optional)
*/