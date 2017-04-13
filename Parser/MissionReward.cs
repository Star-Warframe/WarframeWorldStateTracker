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
        public List<string> r_items
        {
            get
            {
                return m_items;
            }
        }
        public List<string> items
        {
            get
            {
                List<string> returnMe = new List<string>();
                foreach (string s in m_items)
                    returnMe.Add(MapResource.getResource(s));
                return returnMe;
            }
        }
        private List<Tuple<string, int>> m_countedItems = new List<Tuple<string, int>>();
        public List<Tuple<string, int>> r_countedItems
        {
            get
            {
                return m_countedItems;
            }
        }
        public List<Tuple<string, int>> countedItems
        {
            get
            {
                List<Tuple<string, int>> returnMe = new List<Tuple<string, int>>();
                foreach(Tuple<string, int> t in m_countedItems)
                    returnMe.Add(new Tuple<string, int>(MapResource.getResource(t.Item1), t.Item2));
                return returnMe;
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