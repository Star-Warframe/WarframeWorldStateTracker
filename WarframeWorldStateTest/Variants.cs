using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class Variants  // sortie variants
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
        private string m_modifierType = "";
        public string modifierType
        {
            get
            {
                return m_modifierType;
            }
        }
        private string m_node = ""; // SolNode###
        public string node
        {
            get
            {
                return m_node;
            }
        }
        private string m_tileset = "";
        public string tileset
        {
            get
            {
                return m_tileset;
            }
        }
        # endregion

        public Variants()
        {
            // empty
        }

        public Variants(JObject va)
        {
            m_missionType = va["missionType"].ToString();
            m_modifierType = va["modifierType"].ToString();
            m_node = va["node"].ToString();
            m_tileset = va["tileset"].ToString();
        }
    }
}

/*
Sorties: list of objects
	_id: object
		$oid: string
	Activation: object
		sec: long
		usec: long
	Expiry: object
		sec: long
		usec: long
	Reward: string (path)
	ExtraDrops: list (empty)
	Seed: int
	Variants: list of objects
		missionType: int
		modifierType: string
		nodeName: string (SolNode###)
		tileset: string
*/