using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public static class MapSyndicate
    {
        private static JObject jobj;

        static MapSyndicate()
        {
            try
            {
                jobj = JObject.Parse(System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "data/syndicatesData.json"));
            }
            catch (Exception e)
            {
                Console.WriteLine("MapSyndicate: Could not parse JSON file");
            }
        }

        public static string getSyndicate(string s)
        {
            try
            {
                return jobj[s]["name"].ToString();
            }
            catch (Exception e)
            {
# if DEBUG
                Console.WriteLine("MapSyndicates: No entry for " + s);
# endif
                return s;
            }
        }
    }
}
