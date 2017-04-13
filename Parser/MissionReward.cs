using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class MissionReward
    {
        # region variables
        private int m_credits = 0;
        public int credits
        {
            get
            {
                return m_credits;
            }
        }
        private List<string> m_items = new List<string>();
        public List<string> items
        {
            get
            {
                return m_items;
            }
        }
        private List<Tuple<string, int>> m_countedItems = new List<Tuple<string, int>>();
        public List<Tuple<string, int>> countedItems
        {
            get
            {
                return m_countedItems;
            }
        }
        // probably some other stuff
        # endregion


        public MissionReward()
        {
            // empty
        }

        public MissionReward(JObject mr)
        {
            m_credits = mr["credits"].ToObject<int>();
            if (mr["items"] != null)
            {
                foreach (var jobj in mr["items"])
                {
                    m_items.Add(jobj.ToString());
                }
            }
            if (mr["countedItems"] != null)
            {
                foreach (var jobj in mr["countedItems"])
                {
                    m_countedItems.Add(new Tuple<string, int>(jobj["ItemType"].ToString(), jobj["ItemCount"].ToObject<int>()));
                }
            }
        }
    }
}

/*
Alert: list of objects
	_id: object
		$oid: string
	Activation: object
		sec: long
		usec: long
	Expiry: object
		sec: long
		usec: long
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