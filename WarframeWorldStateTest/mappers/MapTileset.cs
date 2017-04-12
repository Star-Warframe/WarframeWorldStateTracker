using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public static class MapTileset
    {
        private static JObject jobj;

        static MapTileset()
        {
            try
            {
                jobj = JObject.Parse(System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "data/tilesetData.json"));
            }
            catch (Exception e)
            {
                Console.WriteLine("MapTileset: Could not parse JSON file");
            }
        }

        public static string getTileset(string ts)
        {
            try
            {
                return jobj[ts].ToString();
            }
            catch (Exception e)
            {
# if DEBUG
                Console.WriteLine("MapTileset: No entry for " + ts);
# endif
                return ts;
            }
        }
    }
}
