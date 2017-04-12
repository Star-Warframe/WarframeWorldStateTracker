using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public static class MapSortie
    {
        private static JObject jobj;

        static MapSortie()
        {
            try
            {
                jobj = JObject.Parse(System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "data/sortieData.json"));
            }
            catch (Exception e)
            {
                Console.WriteLine("MapSortie: Could not parse JSON file");
            }
        }

        public static string getBoss(string s)
        {
            try
            {
                return jobj["bosses"][s]["name"].ToString() + " (" + jobj["bosses"][s]["faction"].ToString() + ")";
            } 
            catch (Exception e)
            {
# if DEBUG
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("MapSortie: No entry for " + s);
# endif
                return s;
            }
        }

        public static string getModifier(string s)
        {
            try
            {
                return jobj["modifierTypes"][s].ToString();
            }
            catch (Exception e)
            {
# if DEBUG
                Console.WriteLine("MapSortie: No entry for " + s);
# endif
                return s;
            }
        }
    }
}
