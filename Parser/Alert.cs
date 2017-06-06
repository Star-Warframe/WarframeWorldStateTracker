using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class Alert : WorldStateObject
    {
        # region variables
        private DateTime m_activationDate = new DateTime();
        public DateTime activationDate
        {
            get
            {
                return m_activationDate;
            }
        }
        private DateTime m_expiryDate = new DateTime();
        public DateTime expiryDate
        {
            get
            {
                return m_expiryDate;
            }
        }
        private MissionInfo m_missionInfo = new MissionInfo();
        public MissionInfo missionInfo
        {
            get
            {
                return m_missionInfo;
            }
        }
        private bool m_forceUnlock = false;
        public bool forceUnlock
        {
            get
            {
                return m_forceUnlock;
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
        # endregion

        public Alert()
        {
        }

        public Alert(JObject al)
        {
            m_id = al["_id"]["$oid"].ToString();
            m_activationDate = WorldStateHelper.unixTimeToDateTime(al["Activation"]["$date"]["$numberLong"].ToObject<long>());
            m_expiryDate = WorldStateHelper.unixTimeToDateTime(al["Expiry"]["$date"]["$numberLong"].ToObject<long>());
            m_missionInfo = new MissionInfo((JObject)al["MissionInfo"]);
            if (al["ForceUnlock"] != null) { m_forceUnlock = al["ForceUnlock"].ToObject<bool>(); }
            if (al["Tag"] != null) { m_tag = al["Tag"].ToString(); }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(missionInfo.location);
            if (m_tag != "") { str.AppendLine("Tag: " + m_tag); }
            str.AppendLine("Starts: " + m_activationDate.ToLocalTime().ToString("hh:mm tt"));
            TimeSpan tte = m_expiryDate - DateTime.UtcNow;
            str.AppendLine("Expires: " + m_expiryDate.ToLocalTime().ToString("hh:mm tt") + " (" + (tte.Hours > 0 ? tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") : "") + tte.Minutes + (tte.Minutes != 1 ? " minutes left)" : " minute left)"));
            //Console.WriteLine("Current time: " + DateTime.Now);
            str.AppendLine("Mission type: " + m_missionInfo.missionType);
            str.AppendLine("Faction: " + m_missionInfo.faction);
            str.AppendLine("Level: " + m_missionInfo.minEnemyLevel + "-" + m_missionInfo.maxEnemyLevel);
            str.AppendLine("Rewards: ");
            str.AppendLine("  " + m_missionInfo.missionReward.credits + " credits");
            foreach (string s in m_missionInfo.missionReward.items)
            {
                str.AppendLine("  " + s);
            }
            foreach (Tuple<string, int> t in m_missionInfo.missionReward.countedItems)
            {
                str.AppendLine("  " + t.Item2 + " " +  t.Item1);
            }

            return str.ToString();
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