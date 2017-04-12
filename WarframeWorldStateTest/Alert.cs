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
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(MapSolNode.getNodeName(missionInfo.location));
            str.AppendLine("Starts: " + m_activationDate.ToLocalTime().ToString("hh:mm tt"));
            TimeSpan tte = m_expiryDate - DateTime.UtcNow;
            str.AppendLine("Expires: " + m_expiryDate.ToLocalTime().ToString("hh:mm tt") + " (" + (tte.Hours > 0 ? tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") : "") + tte.Minutes + (tte.Minutes != 1 ? " minutes left)" : " minute left)"));   // I bet I'm gonna need to debug this (YUP)
            //Console.WriteLine("Current time: " + DateTime.Now);
            str.AppendLine("Mission type: " + MapMissionType.getMissionType(m_missionInfo.missionType));
            str.AppendLine("Faction: " + MapFaction.getFaction(m_missionInfo.faction));
            str.AppendLine("Level: " + m_missionInfo.minEnemyLevel + "-" + m_missionInfo.maxEnemyLevel);
            str.AppendLine("Rewards: ");
            str.AppendLine("  " + m_missionInfo.missionReward.credits + " credits");
            foreach (string s in m_missionInfo.missionReward.items)
            {
                string rName = MapResource.getResource(s);
                str.AppendLine("  " + (rName != "" ? rName : s));
            }
            foreach (Tuple<string, int> t in m_missionInfo.missionReward.countedItems)
            {
                string rName = MapResource.getResource(t.Item1);
                str.AppendLine("  " + t.Item2 + " " + (rName != "" ? rName : t.Item1));
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
		missionType: string
		faction: string
		location: string (SolNode###)
		levelOverride: string (path)
		enemySpec: string (path)
		extraEnemySpec: string (path)
		minEnemyLevel: int
		maxEnemyLevel: int
		difficulty: double (or float, idk)
		seed: int
		maxWaveNum: int
		missionReward: object
			credits: int
			items: list 
				of strings
			countedItems: list of objects
				ItemType: string (path)
				ItemCount: int
			probablysomemorestuff: other
*/