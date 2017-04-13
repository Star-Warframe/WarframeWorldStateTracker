using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class NodeOverride : WorldStateObject
    {
        # region variables
        private string m_node = "";
        string node
        {
            get
            {
                return m_node;
            }
        }
        private bool m_hide = false;
        bool hide
        {
            get
            {
                return m_hide;
            }
        }
        private string m_faction = "";
        string faction
        {
            get
            {
                return m_faction;
            }
        }
        private string m_enemySpec = "";
        string enemySpec
        {
            get
            {
                return m_enemySpec;
            }
        }
        private string m_extraEnemySpec = "";
        string extraEnemySpec
        {
            get
            {
                return m_extraEnemySpec;
            }
        }
        private DateTime m_expiry = new DateTime();
        DateTime expiry
        {
            get
            {
                return m_expiry;
            }
        }
        # endregion

        public NodeOverride()
        {
            // empty
        }

        public NodeOverride(JObject no)
        {
            m_id = no["_id"]["$oid"].ToString();
            m_node = no["Node"].ToString();
            try     // Hide is sometimes not present
            {
                m_hide = no["Hide"].ToObject<bool>();
            } catch (Exception e)
            {
                m_hide = false;
            }
            try     // Faction/EnemySpec/ExtraEnemySpec/Expiry are sometimes not present
            {
                m_faction = no["Faction"].ToString();
                m_enemySpec = no["EnemySpec"].ToString();
                m_extraEnemySpec = no["ExtraEnemySpec"].ToString();
                m_expiry = WorldStateHelper.unixTimeToDateTime(no["Expiry"]["$date"]["$numberLong"].ToObject<long>());
            } catch (Exception e)
            {
                // do nothing?
            }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(MapSolNode.getNodeName(m_node));
            if (m_faction != "") { str.AppendLine(MapFaction.getFaction(m_faction)); }
            if (m_enemySpec != "") { str.AppendLine("Enemy Spec: " + m_enemySpec); }
            if (m_extraEnemySpec != "") { str.AppendLine("Extra Enemy Spec: " + m_extraEnemySpec); }
            if (m_expiry.CompareTo(new DateTime()) != 0) { str.AppendLine("Expires: " + m_expiry); }
            if (m_hide) { str.AppendLine("Hidden"); }

            return str.ToString();
        }
    }
}

/*
NodeOverrides: list of objects
	_id: object
		$oid: string
	Node: string
	Hide: bool
    Faction: string
    EnemySpec: string (path)
    ExtraEnemySpec: string (path?)
    Expiry: object
        $date: object
            $numberLong: long
*/