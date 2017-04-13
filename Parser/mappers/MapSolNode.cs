using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WarframeWorldStateTest.Properties;

namespace WarframeWorldStateTest
{
    public static class MapSolNode
    {
        private static JObject jobj;

        static MapSolNode()
        {
            try
            {
                jobj = JObject.Parse(System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "data/solNodes.json"));
            }
            catch (Exception e)
            {
                Console.WriteLine("MapSolNodes: Could not parse JSON file");
            }
        }

        public static string getNodeName(string node) 
        {
            try
            {
                return jobj[node]["value"].ToString();
            } catch (Exception e)
            {
# if DEBUG
                Console.WriteLine("MapSolNode: No entry for " + node);
# endif
                return node;
            }
        }

        public static string getNodeEnemy(string node)
        {
            try
            {
                return jobj[node]["enemy"].ToString();
            } catch (Exception e)
            {
# if DEBUG
                Console.WriteLine("MapSolNode: No entry for " + node);
# endif
                return node;
            }
        }

        public static string getNodeType(string node)
        {
            try
            {
                return jobj[node]["type"].ToString();
            } catch (Exception e)
            {
# if DEBUG
                Console.WriteLine("MapSolNode: No entry for " + node);
# endif
                return node;
            }
        }
    }
}
