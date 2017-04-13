using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public static class MapResource
    {
        private static JObject jobj;
        private static JObject jobj2;

        static MapResource()
        {
            try
            {
                jobj = JObject.Parse(System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "data/resourcesData.json"));
                jobj2 = JObject.Parse(System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "data/languages.json")/*.ToLower()*/);
            }
            catch (Exception e)
            {
                Console.WriteLine("MapResource: Could not parse JSON file");
            }
        }

        public static string getResource(string r)
        {
            try
            {
                if (jobj[r] != null)
                {
                    return jobj[r].ToString();
                }
                else
                {
                    return jobj2[r.ToLower()].ToString();       // temp workaround for the issue of langugaes.json being (mostly) lowercase
                }
            }
            catch (Exception e)
            {
# if DEBUG
                Console.WriteLine("MapResource: No entry for " + r);
# endif
                return r;
            }
        }
    }
}
