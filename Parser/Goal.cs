﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class Goal : WorldStateObject
    {
        #region variables
        private bool m_fomorian = false;
        public bool fomorian
        {
            get
            {
                return m_fomorian;
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
        private string m_tag = "";
        public string tag
        {
            get
            {
                return m_tag;
            }
        }
        private List<string> m_prereqGoalTags = new List<string>();
        public List<string> prereqGoalTags
        {
            get
            {
                return m_prereqGoalTags;
            }
        }
        private int m_count = 0;
        public int count
        {
            get
            {
                return m_count;
            }
        }
        private int m_goal = 0;
        public int goal
        {
            get
            {
                return m_goal;
            }
        }
        private double m_healthPct = 0.0;
        public double healthPct
        {
            get
            {
                return m_healthPct;
            }
        }
        private string m_victimNode = "";
        public string r_victimNode
        {
            get
            {
                return m_victimNode;
            }
        }
        public string victimNode
        {
            get
            {
                return MapSolNode.getNodeValue(m_victimNode);
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
                return MapSolNode.getNodeValue(node);
            }
        }
        private List<int> m_clanGoal = new List<int>();
        public List<int> clanGoal
        {
            get
            {
                return m_clanGoal;
            }
        }
        private int m_success = 0;
        public int success
        {
            get
            {
                return m_success;
            }
        }
        private bool m_personal = false;
        public bool personal
        {
            get
            {
                return m_personal;
            }
        }
        private bool m_best = false;
        public bool best
        {
            get
            {
                return m_best;
            }
        }
        private int m_regionIdx = 0;
        public int regionIdx
        {
            get
            {
                return m_regionIdx;
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
        private string m_itemType = "";
        public string itemType
        {
            get
            {
                return m_itemType;
            }
        }
        private string m_scoreVar = "";
        public string scoreVar
        {
            get
            {
                return m_scoreVar;
            }
        }
        private string m_scoreMaxTag = "";
        public string scoreMaxTag
        {
            get
            {
                return m_scoreMaxTag;
            }
        }
        private string m_scoreLocTag = "";
        public string scoreLocTag
        {
            get
            {
                return m_scoreLocTag;
            }
        }
        private string m_desc = "";
        public string desc
        {
            get
            {
                return m_desc;
            }
        }
        private string m_toolTip = "";
        public string toolTip
        {
            get
            {
                return m_toolTip;
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
        private List<string> m_regionDrops = new List<string>();
        public List<string> r_regionDrops
        {
            get
            {
                return m_regionDrops;
            }
        }
        public List<string> regionDrops
        {
            get
            {
                List<string> returnMe = new List<string>();
                foreach (string s in m_regionDrops)
                {
                    returnMe.Add(MapResource.getResource(s));
                }
                return returnMe;
            }
        }
        private List<string> m_archwingDrops = new List<string>();
        public List<string> r_archwingDrops
        {
            get
            {
                return m_archwingDrops;
            }
        }
        public List<string> archwingDrops
        {
            get
            {
                List<string> returnMe = new List<string>();
                foreach (string s in m_archwingDrops)
                {
                    returnMe.Add(MapResource.getResource(s));
                }
                return returnMe;
            }
        }
        private List<string> m_rewardItems = new List<string>();
        public List<string> r_rewardItems
        {
            get
            {
                return m_rewardItems;
            }
        }
        public List<string> rewardItems
        {
            get
            {
                List<string> returnMe = new List<string>();
                foreach(string s in m_rewardItems)
                {
                    returnMe.Add(MapResource.getResource(s));
                }
                return returnMe;
            }
        }
        private bool m_roaming = false;
        public bool roaming
        {
            get
            {
                return m_roaming;
            }
        }
        private string m_roamingVip = "";
        public string roamingVip
        {
            get
            {
                return m_roamingVip;
            }
        }
        private string m_rewardNode = "";
        public string r_rewardNode
        {
            get
            {
                return m_rewardNode;
            }
        }
        public string rewardNode
        {
            get
            {
                return MapSolNode.getNodeValue(m_rewardNode);
            }
        }
        MissionInfo m_missionInfo = new MissionInfo();
        #endregion

        public Goal()
        {
            // empty
        }

        public Goal(JObject go)
        {
            m_id = go["_id"]["$oid"].ToString();
            if (go["Fomorian"] != null) { m_fomorian = go["Fomorian"].ToObject<bool>(); }
            m_activation = WorldStateHelper.unixTimeToDateTime(go["Activation"]["$date"]["$numberLong"].ToObject<long>());
            m_expiry = WorldStateHelper.unixTimeToDateTime(go["Expiry"]["$date"]["$numberLong"].ToObject<long>());
            m_tag = go["Tag"].ToString();
            if (go["PrereqGoalTags"] != null)
            {
                foreach (JValue jval in go["PrereqGoalTags"])
                {
                    m_prereqGoalTags.Add(jval.ToString());
                }
            }
            m_count = go["Count"].ToObject<int>();
            m_goal = go["Goal"].ToObject<int>();
            if (go["HealthPct"] != null) { m_healthPct = go["HealthPct"].ToObject<double>(); }
            if (go["VictimNode"] != null) { m_victimNode = go["VictimNode"].ToString(); }
            if (go["Node"] != null) { m_node = go["Node"].ToString(); }
            if (go["ClanGoal"] != null)
            {
                foreach(JValue jval in go["ClanGoal"])
                {
                    m_clanGoal.Add(jval.ToObject<int>());
                }
            }
            m_success = go["Success"].ToObject<int>();
            m_personal = go["Personal"].ToObject<bool>();
            if (go["Best"] != null) { m_best = go["Best"].ToObject<bool>(); }
            if (go["RegionIdx"] != null) { m_regionIdx = go["RegionIdx"].ToObject<int>(); }
            m_faction = go["Faction"].ToString();
            if (go["ItemType"] != null) { m_itemType = go["ItemType"].ToString(); }
            if (go["ScoreVar"] != null) { m_scoreVar = go["ScoreVar"].ToString(); }
            if (go["ScoreMaxTag"] != null) { m_scoreMaxTag = go["ScoreMaxTag"].ToString(); }
            if (go["ScoreLocTag"] != null) { m_scoreLocTag = go["ScoreLocTag"].ToString(); }
            m_desc = go["Desc"].ToString();
            if (go["ToolTip"] != null) { m_toolTip = go["ToolTip"].ToString(); }
            m_icon = go["Icon"].ToString();
            if (go["RegionDrops"] != null)
            {
                foreach(JValue jval in go["RegionDrops"])
                {
                    m_regionDrops.Add(jval.ToString());
                }
            }
            if (go["ArchwingDrops"] != null)
            {
                foreach(JValue jval in go["ArchwingDrops"])
                {
                    m_archwingDrops.Add(jval.ToString());
                }
            }
            if (go["Reward"] != null)
            {
                foreach (JValue jval in go["Reward"]["items"])
                {
                    m_rewardItems.Add(jval.ToString());
                }
            }
            if (go["Roaming"] != null) { m_roaming = go["Roaming"].ToObject<bool>(); }
            if (go["RoamingVIP"] != null) { m_roamingVip = go["RoamingVIP"].ToString(); }
            if (go["MissionInfo"] != null) { m_missionInfo = new MissionInfo((JObject)go["MissionInfo"]); }
            if (go["RewardNode"] != null) { m_rewardNode = go["RewardNode"].ToString(); }

        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(m_tag);
            str.AppendLine("Starts: " + m_activation.ToLocalTime().ToString());
            TimeSpan tte = m_expiry - DateTime.UtcNow;
            str.AppendLine("Expires: " + m_expiry.ToLocalTime().ToString() + " (" + (tte.Days > 0 ? tte.Days + (tte.Days != 1 ? " days, " : " day, ") : "") + (tte.Hours > 0 ? tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") : "") + tte.Minutes + (tte.Minutes != 1 ? " minutes left)" : " minute left)"));
            str.AppendLine("Faction: " + faction);
            if (m_rewardItems.Count > 0)
            {
                str.AppendLine("Rewards:");
                foreach(string s in rewardItems)
                {
                    str.AppendLine("  " + s);
                }
            }

            return str.ToString();
        }
    }
}

/*
This will probably have different stuff in it for different events

Goal: list of objects
	_id: object
		$oid: string
    Fomorian: bool (optional)
	Activation: object
		$date: object
			$numberLong: long
	Expiry: object
		$date: object
			$numberLong
	Tag: string
	PrereqGoalTags: list (optional)
		of strings
	Count: int
	Goal: int
    HealthPct: double/float (optional)
    VictimNode: string (optional)
	Node: string (solNode) (optional)
	ClanGoal: list (optional)
		of ints (No affiliation, Ghost, Shadow, Storm, Mountain, Moon)
	Success: int
	Personal: bool
    Best: bool
	RegionIdx: int (optional)
	Faction: string
	ItemType: string (path) (optional)
	ScoreVar: string (optional)
	ScoreMaxTag: string (optional)
	ScoreLocTag: string (path) (optional)
	Desc: string (path)
	ToolTip: string (path)
	Icon: string (path)
    RegionDrops: list (optional)
        of strings
    ArchwingDrops: list (optional)
        of strings
	Reward: object (optional)
		items: list
			of strings (paths)
	Roaming: bool (optional)
	RoamingVIP: string (path) (optional)
	MissionInfo: object
	RewardNode: string (solNode) (optional)
*/