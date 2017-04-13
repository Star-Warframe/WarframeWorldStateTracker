using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeWorldStateTest
{
    // probably don't need unless I really want to parse the data into individual objects
    public abstract class WorldStateObject
    {
        protected string m_id;      // not all objects have ids, maybe "N/A" for items with none?
        public string id
        {
            get
            {
                return id;
            }
        }

        public WorldStateObject()
        {
            // empty
        }

        public WorldStateObject(string _id)
        {
            m_id = _id;
        }

        public abstract string ToString();
    }
}
