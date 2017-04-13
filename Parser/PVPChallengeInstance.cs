using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class PVPChallengeInstance : WorldStateObject
    {
        # region variables
        private string m_challengeTypeRefId = "";
        public string r_challengeTypeRefId
        {
            get
            {
                return m_challengeTypeRefId;
            }
        }
        public string challengeTypeRefId
        {
            get
            {
                return MapConclave.getChallenge(m_challengeTypeRefId);
            }
        }
        private DateTime m_startDate = new DateTime();
        public DateTime startDate
        {
            get
            {
                return m_startDate;
            }
        }
        private DateTime m_endDate = new DateTime();
        public DateTime endDate
        {
            get
            {
                return m_endDate;
            }
        }
        private List<Tuple<string, int>> m_params = new List<Tuple<string, int>>();
        public List<Tuple<string, int>> parms  // params
        {
            get
            {
                return m_params;
            }
        }
        private bool m_isGenerated = false;
        public bool isGenerated
        {
            get
            {
                return m_isGenerated;
            }
        }
        private string m_pvpMode = "";
        public string r_pvpMode
        {
            get
            {
                return m_pvpMode;
            }
        }
        public string pvpMode
        {
            get
            {
                return MapConclave.getMode(m_pvpMode);
            }
        }
        private List<string> m_subChallenges = new List<string>();
        public List<string> subChallanges
        {
            get
            {
                return m_subChallenges;
            }
        }
        private string m_category = "";
        public string r_category
        {
            get
            {
                return m_category;
            }
        }
        public string category
        {
            get
            {
                return MapConclave.getCategory(m_category);
            }
        }
        # endregion

        public PVPChallengeInstance()
        {
            // empty
        }

        public PVPChallengeInstance(JObject pv)
        {
            m_id = pv["_id"]["$oid"].ToString();
            m_challengeTypeRefId = pv["challengeTypeRefID"].ToString();
            m_startDate = WorldStateHelper.unixTimeToDateTime(pv["startDate"]["$date"]["$numberLong"].ToObject<long>());
            m_endDate = WorldStateHelper.unixTimeToDateTime(pv["endDate"]["$date"]["$numberLong"].ToObject<long>());
            if (pv["params"] != null && pv["params"].HasValues)
            {
                foreach(JObject jobj in pv["params"])
                {
                    m_params.Add(new Tuple<string, int>(jobj["n"].ToString(), jobj["v"].ToObject<int>()));
                }
            }
            m_isGenerated = pv["isGenerated"].ToObject<bool>();
            m_pvpMode = pv["PVPMode"].ToString();
            if (pv["subChallenges"] != null && pv["subChallenges"].HasValues)
            {
                foreach(JObject jobj in pv["subChallenges"])
                {
                    m_subChallenges.Add(jobj["$oid"].ToString());
                }
            }
            m_category = pv["Category"].ToString();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(category + ":");
            str.AppendLine(pvpMode);
            str.AppendLine(challengeTypeRefId);
            str.AppendLine("Starts: " + m_startDate.ToLocalTime());
            TimeSpan tte = m_endDate - DateTime.UtcNow;
            str.AppendLine("Expires: " + m_endDate.ToLocalTime() + " (" + tte.Days + (tte.Days != 1 ? " days, " : " day, ") + tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") + tte.Minutes + (tte.Minutes != 1 ? " minutes left)" : " minute left)"));
            
            return str.ToString();
        }
    }
}

/*
PVPChallengeInstances: list of objects
	_id: object
		$oid: string
	challengeTypeRefID: string (path)
	startDate: object
        $date: object
            $numberLong: long
	endDate: object
        $date: object
            $numberLong: long
	params: list of objects
		n: string
		v: int
	isGenerated: bool
	PVPMode: string
	subChallenges: list of objects
		$oid: string
	Category: string
*/