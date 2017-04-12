using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public class BadlandHistory
    {
        # region variables
        private string m_def = "";
        public string def
        {
            get
            {
                return m_def;
            }
        }
        private string m_defId = "";
        public string defId
        {
            get
            {
                return m_defId;
            }
        }
        private bool m_defAli = false;
        public bool defAli
        {
            get
            {
                return m_defAli;
            }
        }
        private string m_att = "";
        public string att
        {
            get
            {
                return m_att;
            }
        }
        private string m_attId = "";
        public string attId
        {
            get
            {
                return m_attId;
            }
        }
        private bool m_attAli = false;
        public bool attAli
        {
            get
            {
                return m_attAli;
            }
        }
        private string m_winId = "";
        public string winId
        {
            get
            {
                return m_winId;
            }
        }
        private DateTime m_start = new DateTime();
        public DateTime start
        {
            get
            {
                return m_start;
            }
        }
        private DateTime m_end = new DateTime();
        public DateTime end
        {
            get
            {
                return m_end;
            }
        }
        # endregion

        public BadlandHistory()
        {
            // empty
        }

        public BadlandHistory(JObject bh)
        {
            m_def = bh["Def"].ToString();
            m_defId = bh["DefId"]["$oid"].ToString();
            m_defAli = bh["DefAli"].ToObject<bool>();
            m_att = bh["Att"].ToString();
            m_attId = bh["AttId"]["$oid"].ToString();
            m_attAli = bh["AttAli"].ToObject<bool>();
            m_winId = bh["WinId"]["$oid"].ToString();
            m_start = WorldStateHelper.unixTimeToDateTime(bh["Start"]["$date"]["$numberLong"].ToObject<long>());
            m_end = WorldStateHelper.unixTimeToDateTime(bh["End"]["$date"]["$numberLong"].ToObject<long>());
        }
    }
}

/*
BadlandNodes (dark sectors): list of objects
	_id: object
		$oid: string
	DefenderInfo: object
		<snip>
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