using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class Sorties : WorldStateObject
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

        private string m_boss;
        public string boss
        {
            get
            {
                return m_boss;
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
        private string m_reward = "";
        public string reward
        {
            get
            {
                return m_reward;
            }
        }

        private List<string> m_extraDrops = new List<string>(); // I don't actually know what type of thing ExtraDrops is atm
        public List<string> extraDrops
        {
            get
            {
                return m_extraDrops;
            }
        }

        private List<Variants> m_variants = new List<Variants>();
        public List<Variants> variants
        {
            get
            {
                return m_variants;
            }
        }
        # endregion

        public Sorties()
        {
            // empty?
        }
        
        public Sorties(JObject so)
        {
            m_id = so["_id"]["$oid"].ToString();
            m_activation = WorldStateHelper.unixTimeToDateTime(so["Activation"]["$date"]["$numberLong"].ToObject<long>());
            m_expiry = WorldStateHelper.unixTimeToDateTime(so["Expiry"]["$date"]["$numberLong"].ToObject<long>());
            m_boss = so["Boss"].ToString();
            m_reward = so["Reward"].ToString();
            foreach(JObject jobj in so["ExtraDrops"])   // just use a list of ExtraDrop strings for now
            {
                m_extraDrops.Add(jobj.ToString());
            }
            m_seed = so["Seed"].ToObject<int>();

            foreach (JObject jobj in so["Variants"])
            {
                m_variants.Add(new Variants(jobj));     // should only be three of these
            }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(MapSortie.getBoss(m_boss));
            str.AppendLine("Starts: " + m_activation.ToLocalTime());
            str.AppendLine("Expires: " + m_expiry.ToLocalTime());
            str.AppendLine("Missions:");
            for (int i = 0; i < m_variants.Count; i++)
            {
                str.AppendLine("  " + (i+1) + ":");
                str.AppendLine("    " + MapSolNode.getNodeName(m_variants[i].node));
                str.AppendLine("    Tileset: " + MapTileset.getTileset(m_variants[i].tileset));
                str.AppendLine("    Mission: " + MapMissionType.getMissionType(variants[i].missionType));
                str.AppendLine("    Modifier: " + MapSortie.getModifier(variants[i].modifierType));
            }

            return str.ToString();
        }
    }
}

/*
Sorties: list of objects
	_id: object
		$oid: string
	Activation: object
		$date: object
            $numberLong: long
	Expiry: object
		$date: object
            $numberLong: long
    Boss: string
	Reward: string (path)
	ExtraDrops: list (empty)
	Seed: int
	Variants: list of objects
		bossIndex: int
		regionIndex: int
		missionIndex: int
		modifierIndex: 5
		nodeName: string (SolNode###)
		tileset: string
    Twitter: boolean
*/