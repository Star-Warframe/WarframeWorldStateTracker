using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WarframeWorldStateTest
{
    public static class MapConclave
    {
        private static JObject jobj;

        static MapConclave()
        {
            try
            {
                jobj = JObject.Parse(System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "data/conclaveData.json"));
            }
            catch (Exception e)
            {
                Console.WriteLine("MapConclave: Could not parse JSON file");
            }
        }

        public static string getMode(string s)
        {
            try
            {
                return jobj["modes"][s].ToString();
            } catch (Exception e)
            {
#if DEBUG
                Console.WriteLine("MapConclave: No entry for " + s);
#endif
                return s;
            }
        }
        
        public static string getCategory(string s)
        {
            try
            {
                return jobj["categories"][s].ToString();
            } catch (Exception e)
            {
#if DEBUG
                Console.WriteLine("MapConclave: No entry for " + s);
#endif
                return s;
            }
        }

        public static string getChallenge(string s)
        {
            try
            {
                return jobj["challenges"][s].ToString();
            } catch (Exception e)
            {
#if DEBUG
                Console.WriteLine("MapConclave: No entry for " + s);
#endif
                return s;
            }
        }
    }
}
