using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public static class MapFissure
    {
        private static JObject jobj;

        static MapFissure()
        {
            try
            {
                jobj = JObject.Parse(System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "data/fissureModifiers.json"));
            }
            catch (Exception e)
            {
                Console.WriteLine("MapFissure: Could not parse JSON file");
            }
        }

        public static string getModifier(string s)
        {
            try
            {
                return jobj[s]["value"].ToString();
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine("MapFissure: No entry for " + s);
#endif
                return s;
            }
        }
        
        public static int getNumber(string s)
        {
            try
            {
                return jobj[s]["num"].ToObject<int>();
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine("MapFissure: No entry for " + s);
#endif
                return 0;
            }
        }
    }
}
