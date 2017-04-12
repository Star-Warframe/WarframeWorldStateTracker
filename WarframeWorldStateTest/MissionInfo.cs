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
        private string m_missionType = "";
        public string missionType
        {
            get
            {
                return m_missionType;
            }
        }
        private string m_faction = "";
        public string faction
        {
            get
            {
                return m_faction;
            }
        }
        private string m_location = "";
        public string location
        {
            get
            {
                return m_location;
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
            m_missionType = mi["missionType"].ToString();
            m_faction = mi["faction"].ToString();
            m_location = mi["location"].ToString();
            m_levelOverride = mi["levelOverride"].ToString();
            m_enemySpec = mi["enemySpec"].ToString();
            if (mi["extraEnemySpec"] != null)
                m_extraEnemySpec = mi["extraEnemySpec"].ToString();
            m_minEnemyLevel = mi["minEnemyLevel"].ToObject<int>();
            m_maxEnemyLevel = mi["maxEnemyLevel"].ToObject<int>();
            m_difficulty = mi["difficulty"].ToObject<double>();
            m_seed = mi["seed"].ToObject<int>();
            if (mi["maxWaveNum"] != null)
                m_maxWaveNum = mi["maxWaveNum"].ToObject<int>();
            m_missionReward = new MissionReward((JObject)mi["missionReward"]);
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