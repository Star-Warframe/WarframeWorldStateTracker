using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public static class MapFaction
    {
        private static JObject jobj;

        static MapFaction()
        {
            try
            {
                jobj = JObject.Parse(System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "data/factionsData.json"));
            }
            catch (Exception e)
            {
                Console.WriteLine("MapFaction: Could not parse JSON file");
            }
        }

        public static string getFaction(string mt)
        {
            try
            {
                return jobj[mt]["value"].ToString();
            }
            catch (Exception e)
            {
# if DEBUG
                Console.WriteLine("MapFaction: No entry for " + mt);
# endif
                return mt;
            }
        }
    }
}
