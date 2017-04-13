using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public static class MapMissionType
    {
        private static JObject jobj;

        static MapMissionType()
        {
            try
            {
                jobj = JObject.Parse(System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "data/missionTypes.json"));
            }
            catch (Exception e)
            {
                Console.WriteLine("MapMissionType: Could not parse JSON file");
            }
        }

        public static string getMissionType(string mt)
        {
            try
            {
                return jobj[mt]["value"].ToString();
            }
            catch (Exception e)
            {
# if DEBUG
                Console.WriteLine("MapMissionType: No entry for " + mt);
# endif
                return mt;
            }
        }
    }
}
