using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class BadlandDefenderInfo
    {
        #region variables
        private int m_creditsTaxRate = 0;
        public int creditsTaxRate
        {
            get
            {
                return m_creditsTaxRate;
            }
        }
        private int m_memberCreditsTaxRate = 0;
        public int memberCreditsTaxRate
        {
            get
            {
                return m_memberCreditsTaxRate;
            }
        }
        private int m_itemsTaxRate = 0;
        public int itemsTaxRate
        {
            get
            {
                return m_itemsTaxRate;
            }
        }
        private int m_memberItemsTaxRate = 0;
        public int memberItemsTaxRate
        {
            get
            {
                return m_memberItemsTaxRate;
            }
        }
        private DateTime m_taxChangeAllowedTime = new DateTime();
        public DateTime taxChangeAllowedTime
        {
            get
            {
                return m_taxChangeAllowedTime;
            }
        }
        private bool m_isAlliance = false;
        public bool isAlliance
        {
            get
            {
                return m_isAlliance;
            }
        }
        private string m_defenderId = "";
        public string defenderId
        {
            get
            {
                return m_defenderId;
            }
        }
        private string m_name = "";
        public string name
        {
            get
            {
                return m_name;
            }
        }
        private int m_strengthRemaining = 0;
        public int strengthRemaining
        {
            get
            {
                return m_strengthRemaining;
            }
        }
        private int m_maxStrength = 0;
        public int maxStrength
        {
            get
            {
                return m_maxStrength;
            }
        }
        private DateTime m_deploymentActivationTime = new DateTime();
        public DateTime deploymentActivationTime
        {
            get
            {
                return m_deploymentActivationTime;
            }
        }
        private string m_railType = ""; // is there supposed to be more than one rail type?
        public string railType
        {
            get
            {
                return m_railType;
            }
        }
        private string m_motd = "";
        public string motd
        {
            get
            {
                return m_motd;
            }
        }
        private string m_deployerName = "";
        public string deployerName
        {
            get
            {
                return m_deployerName;
            }
        }
        private string m_deployerClan = "";
        public string deployerClan
        {
            get
            {
                return m_deployerClan;
            }
        }
        private int m_railHealReserve = 0;
        public int railHealReserve
        {
            get
            {
                return m_railHealReserve;
            }
        }
        private double m_healRate = 0.0;
        public double healRate
        {
            get
            {
                return m_healRate;
            }
        }
        private int m_damagePerMission = 0;
        public int damagePerMission
        {
            get
            {
                return m_damagePerMission;
            }
        }
        private int m_battlePayReserve = 0;
        public int battlePayReserve
        {
            get
            {
                return m_battlePayReserve;
            }
        }
        private int m_missionBattlePay = 0;
        public int missionBattlePay
        {
            get
            {
                return m_missionBattlePay;
            }
        }
        private string m_battlePaySetBy = "";
        public string battlePaySetBy
        {
            get
            {
                return m_battlePaySetBy;
            }
        }
        private string m_battlePaySetByClan = "";
        public string battlePaySetByClan
        {
            get
            {
                return m_battlePaySetByClan;
            }
        }
        private string m_taxLastChangedBy = "";
        public string taxLastChangedBy
        {
            get
            {
                return m_taxLastChangedBy;
            }
        }
        private string m_taxLastChangedByClan = "";
        public string taxLastChangedByClan
        {
            get
            {
                return m_taxLastChangedByClan;
            }
        }
        #endregion

        public BadlandDefenderInfo()
        {
            // empty
        }

        public BadlandDefenderInfo(JObject bd)
        {
            // might as well just check everything for null...
            if (bd["CreditsTaxRate"] != null) { m_creditsTaxRate = bd["CreditsTaxRate"].ToObject<int>(); }
            if (bd["MemberCreditsTaxRate"] != null) { m_memberCreditsTaxRate = bd["MemberCreditsTaxRate"].ToObject<int>(); }
            if (bd["ItemsTaxRate"] != null) { m_itemsTaxRate = bd["ItemsTaxRate"].ToObject<int>(); }
            if (bd["MemberItemsTaxRate"] != null) { m_memberItemsTaxRate = bd["MemberItemsTaxRate"].ToObject<int>(); }
            if (bd["TaxChangeAllowedTime"] != null) { m_taxChangeAllowedTime = WorldStateHelper.unixTimeToDateTime(bd["TaxChangeAllowedTime"]["$date"]["$numberLong"].ToObject<long>()); }
            if (bd["IsAlliance"] != null) { m_isAlliance = bd["IsAlliance"].ToObject<bool>(); }
            if (bd["Id"] != null) { m_defenderId = bd["Id"]["$oid"].ToString(); }
            if (bd["Name"] != null) { m_name = bd["Name"].ToString(); }
            if (bd["StrengthRemaining"] != null) { m_strengthRemaining = bd["StrengthRemaining"].ToObject<int>(); }
            if (bd["MaxStrength"] != null) { m_maxStrength = bd["MaxStrength"].ToObject<int>(); }
            if (bd["DeploymentActivationTime"] != null) { m_deploymentActivationTime = WorldStateHelper.unixTimeToDateTime(bd["DeploymentActivationTime"]["$date"]["$numberLong"].ToObject<long>()); }
            if (bd["RailType"] != null) { m_railType = bd["RailType"].ToString(); }
            if (bd["MOTD"] != null) { m_motd = bd["MOTD"].ToString(); }
            if (bd["DeployerName"] != null) { m_deployerName = bd["DeployerName"].ToString(); }
            if (bd["DeployerClan"] != null) { m_deployerClan = bd["DeployerClan"].ToString(); }
            if (bd["RailHealReserve"] != null) { m_railHealReserve = bd["RailHealReserve"].ToObject<int>(); }
            if (bd["HealRate"] != null) { m_healRate = bd["HealRate"].ToObject<double>(); }
            if (bd["DamagePerMission"] != null) { m_damagePerMission = bd["DamagePerMission"].ToObject<int>(); }
            if (bd["BattlePayReserve"] != null) { m_battlePayReserve = bd["BattlePayReserve"].ToObject<int>(); }
            if (bd["MissionBattlePay"] != null) { m_missionBattlePay = bd["MissionBattlePay"].ToObject<int>();}
            if (bd["BattlePaySetBy"] != null) { m_battlePaySetBy = bd["BattlePaySetBy"].ToString(); }
            if (bd["BattlePaySetByClan"] != null) { m_battlePaySetByClan = bd["BattlePaySetByClan"].ToString(); }
            if (bd["TaxLastChangedBy"] != null) { m_taxLastChangedBy = bd["TaxLastChangedBy"].ToString(); }
            if (bd["TaxLastChangedByClan"] != null) { m_taxLastChangedByClan = bd["TaxLastChangedByClan"].ToString(); }
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