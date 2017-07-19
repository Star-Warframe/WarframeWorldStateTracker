using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class Invasion : WorldStateObject
    {
        # region variables
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
        private string m_locTag = "";
        public string locTag
        {
            get
            {
                return m_locTag;
            }
        }
        private bool m_completed = false;
        public bool completed
        {
            get
            {
                return m_completed;
            }
        }
        private List<Tuple<string, int>> m_attackerReward = new List<Tuple<string, int>>();
        public List<Tuple<string, int>> r_attackerReward
        {
            get
            {
                return m_attackerReward;
            }
        }
        public List<Tuple<string, int>> attackerReward
        {
            get
            {
                List<Tuple<string, int>> returnMe = new List<Tuple<string, int>>();
                foreach (Tuple<string, int> t in m_attackerReward)
                {
                    returnMe.Add(new Tuple<string, int>(MapResource.getResource(t.Item1), t.Item2));
                }
                return returnMe;
            }
        }
        private Tuple<int, string> m_attackerMissionInfo = new Tuple<int, string>(0, "");
        public Tuple<int, string> r_attackerMissionInfo
        {
            get
            {
                return m_attackerMissionInfo;
            }
        }
        public Tuple<int, string> attackerMissionInfo
        {
            get
            {
                Tuple<int, string> returnMe = new Tuple<int, string>(m_attackerMissionInfo.Item1, MapFaction.getFaction(m_attackerMissionInfo.Item2));
                return returnMe;
            }
        }
        private List<Tuple<string, int>> m_defenderReward = new List<Tuple<string, int>>();
        public List<Tuple<string, int>> r_defenderReward
        {
            get
            {
                return m_defenderReward;
            }
        }
        public List<Tuple<string, int>> defenderReward
        {
            get
            {
                List<Tuple<string, int>> returnMe = new List<Tuple<string, int>>();
                foreach(Tuple<string, int> t in m_defenderReward)
                {
                    returnMe.Add(new Tuple<string, int>(MapResource.getResource(t.Item1), t.Item2));
                }
                return returnMe;
            }
        }
        private Tuple<int, string> m_defenderMissionInfo = new Tuple<int, string>(0, "");
        public Tuple<int, string> r_defenderMissionInfo
        {
            get
            {
                return m_defenderMissionInfo;
            }
        }
        public Tuple<int, string> defenderMissionInfo
        {
            get
            {
                Tuple<int, string> returnMe = new Tuple<int, string>(m_defenderMissionInfo.Item1, MapFaction.getFaction(m_defenderMissionInfo.Item2));
                return returnMe;
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
        # endregion

        public Invasion()
        {
            // empty
        }

        public Invasion(JObject iv)
        {
            m_id = iv["_id"]["$oid"].ToString();
            m_faction = iv["Faction"].ToString();
            m_node = iv["Node"].ToString();
            m_count = iv["Count"].ToObject<int>();
            m_goal = iv["Goal"].ToObject<int>();
            m_locTag = iv["LocTag"].ToString();
            m_completed = iv["Completed"].ToObject<bool>();
            if (iv["AttackerReward"] != null && iv["AttackerReward"].HasValues)
                if (iv["AttackerReward"]["countedItems"] != null)
                    foreach (JObject jobj in iv["AttackerReward"]["countedItems"])
                        m_attackerReward.Add(new Tuple<string, int>(jobj["ItemType"].ToString(), jobj["ItemCount"].ToObject<int>()));
            if (iv["AttackerMissionInfo"] != null)
                m_attackerMissionInfo = new Tuple<int, string>(iv["AttackerMissionInfo"]["seed"].ToObject<int>(), iv["AttackerMissionInfo"]["faction"].ToString());
            if (iv["DefenderReward"] != null)
                if (iv["DefenderReward"]["countedItems"] != null)
                    foreach (JObject jobj in iv["DefenderReward"]["countedItems"])
                        m_defenderReward.Add(new Tuple<string, int>(jobj["ItemType"].ToString(), jobj["ItemCount"].ToObject<int>()));
            if (iv["DefenderMissionInfo"] != null)
                m_defenderMissionInfo = new Tuple<int, string>(iv["DefenderMissionInfo"]["seed"].ToObject<int>(), iv["DefenderMissionInfo"]["faction"].ToString());
            m_activation = WorldStateHelper.unixTimeToDateTime(iv["Activation"]["$date"]["$numberLong"].ToObject<long>());
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            // AttackerMissionInfo faction is defender faction and vice versa
            string attacker = MapFaction.getFaction(m_defenderMissionInfo.Item2);
            string defender = MapFaction.getFaction(m_attackerMissionInfo.Item2);

            str.AppendLine(MapSolNode.getNodeName(m_node));
            str.AppendLine(MapFaction.getFaction(m_faction) + " invasion" + (m_completed ? " (completed)" : ""));
            str.AppendLine("Activation: " + m_activation.ToLocalTime());

            if (!m_completed)                           // no point printing this out if the invasion has been completed
            {
                double percentComplete = ((double)m_count / (double)m_goal);
                str.Append("Completion: " + Math.Floor(Math.Abs(percentComplete * 100.0)) + "%");
                if (percentComplete > 0)
                {
                    str.AppendLine(" (Advantage: " + attacker + ")");
                }
                else if (percentComplete < 0)
                {
                    str.AppendLine(" (Advantage: " + defender + ")");
                }
                else
                {
                    str.AppendLine(" (Tie)");
                }
            }

            if (m_faction != "FC_INFESTATION")          // no attacker rewards if Infested invasion
            {
                str.AppendLine("Attacker rewards:");
                foreach (Tuple<string, int> t in m_attackerReward)
                {
                    str.AppendLine("  " + t.Item2 + " " + MapResource.getResource(t.Item1));
                }
            }
            str.AppendLine("Defender rewards:");
            foreach(Tuple<string, int> t in m_defenderReward)
            {
                str.AppendLine("  " + t.Item2 + " " + MapResource.getResource(t.Item1));
            }

            return str.ToString();
        }
    }
}

/*
Invasions: list of objects
	_id: object
		$oid: string
	Faction: string
	Node: string (SolNode###)
	Count: int
	Goal: int
	LocTag: string (path)
	Completed: bool
	AttackerReward: object
		countedItems: list of objects
			ItemType: string (path)
			ItemCount: int
	AttackerMissionInfo: object
		seed: int
		faction: string
	DefenderReward: object
		countedItems: list of items
			ItemType: string (path)
			ItemCount: int
	DefenderMissionInfo: object
		seed: int
		faction: string
	Activation: object
		$date: object
            $numberLong: long
*/