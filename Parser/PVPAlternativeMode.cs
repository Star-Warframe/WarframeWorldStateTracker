using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class PVPAlternativeMode
    {
        # region variables
        private string m_targetMode = "";
        string targetMode
        {
            get
            {
                return m_targetMode;
            }
        }
        private string m_titleLoc = "";
        string titleLoc
        {
            get
            {
                return m_titleLoc;
            }
        }
        private string m_descriptionLoc = "";
        string descriptionLoc
        {
            get
            {
                return m_descriptionLoc;
            }
        }
        private bool m_disableEnergyPickups = false;
        bool disableEnergyPickups
        {
            get
            {
                return m_disableEnergyPickups;
            }
        }
        private bool m_disableEnergySurge = false;
        bool disableEnergySurge
        {
            get
            {
                return m_disableEnergySurge;
            }
        }
        private bool m_disableAmmoPickups = false;
        bool disableAmmoPickups
        {
            get
            {
                return m_disableAmmoPickups;
            }
        }
        // todo: probably give this variable its own class instead of just using a Tuple
        // for now, though, ForcedLoadouts seems to only have one instance of WeaponOverrides
        private List<Tuple<bool, string, string>> m_weaponOverrides = new List<Tuple<bool, string, string>>();
        List<Tuple<bool, string, string>> weaponOverrides
        {
            get
            {
                return m_weaponOverrides;
            }
        }
        # endregion

        public PVPAlternativeMode()
        {
            // empty
        }

        public PVPAlternativeMode(JObject pv)
        {
            m_targetMode = pv["TargetMode"].ToString();
            m_titleLoc = pv["TitleLoc"].ToString();
            m_descriptionLoc = pv["DescriptionLoc"].ToString();
            m_disableEnergyPickups = pv["DisableEnergyPickups"].ToObject<bool>();
            m_disableEnergySurge = pv["DisableEnergySurge"].ToObject<bool>();
            m_disableAmmoPickups = pv["DisableAmmoPickups"].ToObject<bool>();
            if (pv["ForcedLoadouts"] != null && pv["ForcedLoadouts"].HasValues)
                if (pv["ForcedLoadouts"][0]["WeaponOverrides"] != null && pv["ForcedLoadouts"][0]["WeaponOverrides"].HasValues)
                    foreach(JObject jobj in pv["ForcedLoadouts"][0]["WeaponOverrides"])
                    {
                        m_weaponOverrides.Add(new Tuple<bool, string, string>(jobj["Override"].ToObject<bool>(), jobj["Resource"].ToString(), jobj["OriginalVersion"].ToString()));
                    }
        }
    }
}

/*
PVPAlternativeModes: list of objects
	TargetMode: string
	TitleLoc: string (path)
	DescriptionLoc: string (path)
	DisableEnergyPickups: bool
	DisableEnergySurge: bool
	DisableAmmoPickups: bool
	ForcedLoadouts: list of objects
		WeaponOverrides: list of objects
			Override: bool
			Resource: string (path)
			OriginalVersion: string (path)
*/