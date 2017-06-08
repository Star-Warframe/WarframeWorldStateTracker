using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class BadlandNode : WorldStateObject
    {
        # region variables
        private BadlandDefenderInfo m_defenderInfo = new BadlandDefenderInfo();
        public BadlandDefenderInfo defenderInfo
        {
            get
            {
                return m_defenderInfo;
            }
        }
        private List<BadlandHistory> m_history = new List<BadlandHistory>();
        public List<BadlandHistory> history
        {
            get
            {
                return m_history;
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
                //return m_node;
            }
        }
        private DateTime m_postConflictCooldown = new DateTime();
        public DateTime postConflictCooldown
        {
            get
            {
                return m_postConflictCooldown;
            }
        }
        # endregion

        public BadlandNode()
        {
            // empty
        }

        public BadlandNode(JObject bn)
        {
            m_id = bn["_id"]["$oid"].ToString();
            m_defenderInfo = new BadlandDefenderInfo((JObject)bn["DefenderInfo"]);
            if (bn["History"] != null)
                foreach (JObject jobj in bn["History"])
                    m_history.Add(new BadlandHistory(jobj));
            m_node = bn["Node"].ToString();
            if (bn["PostConflictCooldown"] != null) { m_postConflictCooldown = WorldStateHelper.unixTimeToDateTime(bn["PostConflictCooldown"]["$date"]["$numberLong"].ToObject<long>()); }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(MapSolNode.getNodeName(m_node));
            str.AppendLine("Current owner: " + m_defenderInfo.name);
            str.AppendLine("MOTD: " + m_defenderInfo.motd);

            return str.ToString();
        }
    }
}

/*
BadlandNodes (dark sectors): list of objects
	_id: object
		$oid: string
	DefenderInfo: object
		CreditsTaxRate: int
		MemberCreditsTaxRate: int
		ItemsTaxRate: int
		MemberItemsTaxRate: int
		TaxChangeAllowedTime: object
			sec: long
			usec: long
		IsAlliance: bool
		Id: object
			$oid: string
		Name: string
		StrengthRemaining: int
		MaxStrength: int
		DeploymentActivationTime: object
			sec: long
			usec: long
		RailType: string (path)
		MOTD: string
		DeployerName: string
		DeployerClan: string
		RailHealReserve: int
		HealRate: double (or float)
		DamagePerMission: int
		BattlePayReserve: int
		MissionBattlePay: int
		BattlePaySetBy: string
		BattlePaySetByClan: string
		TaxLastChangedBy: string
		TaxLastChangedByClan: string
	History: list of objects
		Def: string
		DefId: object
			$oid: string
		DefAli: bool
		Att: string
		AttId: object
			$oid: string
		AttAli: bool
		WinId: object
			$oid: string
		Start: object
			sec: long
			usec: long
		End: object
			sec: long
			usec: long
	Node: string (ClanNode##)
	PostConflictCooldown: object
		sec: long
		usec: long
*/