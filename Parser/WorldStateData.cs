//#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;



namespace WarframeWorldStateTest
{
    public class WorldStateData
    {
        # region variables
        private Dictionary<string, string> worldStateUrls = new Dictionary<string, string>
        {
            { "pc", "http://content.warframe.com/dynamic/worldState.php" },
            { "xb1", "http://content.xb1.warframe.com/dynamic/worldState.php" },
            { "ps4", "http://content.ps4.warframe.com/dynamic/worldState.php" },
        };
        private string wsUrl = "http://content.warframe.com/dynamic/worldState.php";

        private List<KeyValuePair<string, string>> m_platformList = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string> ( "pc", "PC" ),
            new KeyValuePair<string, string> ( "xb1", "XB1" ),
            new KeyValuePair<string, string> ( "ps4", "PS4" ),
        };
        public List<KeyValuePair<string, string>> platformList
        {
            get
            {
                return m_platformList;
            }
        }

        private string worldStatePhpString;
        public string worldStatePhpStr
        {
            get
            {
                return worldStatePhpString;
            }
        }
        private JObject worldStatePhpJson;
        public JObject worldStatePhpJs
        {
            get
            { 
                return worldStatePhpJson; 
            }
        }

        private List<KeyValuePair<string, string>> m_varList = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string> ( "alerts", "Alerts" ),
            new KeyValuePair<string, string> ( "events", "Events" ),
            new KeyValuePair<string, string> ( "sorties", "Sorties" ),
            new KeyValuePair<string, string> ( "syndicateMissions", "Syndicate Missions" ),
            new KeyValuePair<string, string> ( "fissureMissions", "Fissure Missions" ),
            new KeyValuePair<string, string> ( "flashSales", "Flash Sales" ),
            new KeyValuePair<string, string> ( "invasions", "Invasions" ),
            new KeyValuePair<string, string> ( "nodeOverrides", "Node Overrides" ),
            new KeyValuePair<string, string> ( "badlandNodes", "Dark Sectors" ),
            new KeyValuePair<string, string> ( "voidTraders", "Void Traders" ),
            new KeyValuePair<string, string> ( "primeVaultAvailabilities", "Prime Vault Availabilities" ),
            new KeyValuePair<string, string> ( "dailyDeals", "Daily Darvo Deals" ),
            new KeyValuePair<string, string> ( "pvpChallengeInstances", "PVP Challenges" ),
            new KeyValuePair<string, string> ( "pvpAlternativeModes", "PVP Alternative Modes" ),
            new KeyValuePair<string, string> ( "persistentEnemies", "Persistent Enemies" ),
            new KeyValuePair<string, string> ( "projectPct", "Invasion Event Status" ),
        };
        public List<KeyValuePair<string, string>> varList
        {
            get
            {
                return m_varList;
            }
        }

        // Version variable
        // BuildLabel variable
        // Time and Date variables
        List<Event> m_events = new List<Event>(); // Events
        public List<Event> events
        {
            get
            {
                return m_events;
            }
        }
        // List of Goals
        List<Alert> m_alerts = new List<Alert>(); // ALerts
        public List<Alert> alerts
        {
            get
            {
                return m_alerts;
            }
        }
        List<Sorties> m_sorties = new List<Sorties>();    // Sorties
        public List<Sorties> sorties
        {
            get
            {
                return m_sorties;
            }
        }
        List<SyndicateMission> m_syndicateMissions = new List<SyndicateMission>();    // Syndicate missions
        public List<SyndicateMission> syndicateMissions
        {
            get
            {
                return m_syndicateMissions;
            }
        }
        List<FissureMission> m_fissureMissions = new List<FissureMission>();  // Fissures
        public List<FissureMission> fissureMissions
        {
            get
            {
                return m_fissureMissions;
            }
        }
        // List of GlobalUpgrades (whatever that means....)
        List<FlashSale> m_flashSales = new List<FlashSale>(); // Flash sales
        public List<FlashSale> flashSales
        {
            get
            {
                return m_flashSales;
            }
        }
        List<Invasion> m_invasions = new List<Invasion>();    // Invasions
        public List<Invasion> invasions
        {
            get
            {
                return m_invasions;
            }
        }
        // List of HubEvents (whatever that means....)
        List<NodeOverride> m_nodeOverrides = new List<NodeOverride>();    // Node overrides
        public List<NodeOverride> nodeOverrides
        {
            get
            {
                return m_nodeOverrides;
            }
        }
        List<BadlandNode> m_badlandNodes = new List<BadlandNode>();   // Badland (Dark Sector) nodes
        public List<BadlandNode> badlandNodes
        {
            get
            {
                return m_badlandNodes;
            }
        }
        List<VoidTrader> m_voidTraders = new List<VoidTrader>();  // Void traders (Baro)
        public List<VoidTrader> voidTraders
        {
            get
            {
                return m_voidTraders;
            }
        }
        string m_primeAccessAvailability = "";    // Prime Access availability
        public string primeAccessAvailability
        {
            get
            {
                return m_primeAccessAvailability;
            }
        }
        List<string> m_primeVaultAvailabilities = new List<string>(); // Prime Vault availabilities
        public List<string> primeVaultAvailabilities
        {
            get
            {
                return m_primeVaultAvailabilities;
            }
        }
        List<DailyDeal> m_dailyDeals = new List<DailyDeal>(); // Daily deals (Darvo probably)
        public List<DailyDeal> dailyDeals
        {
            get
            {
                return m_dailyDeals;
            }
        }
        string m_libraryInfo = "";    // Library info
        public string libraryInfo
        {
            get
            {
                return m_libraryInfo;
            }
        }
        List<PVPChallengeInstance> m_pvpChallengeInstances = new List<PVPChallengeInstance>();    // PVP challenges
        public List<PVPChallengeInstance> pvpChallengeInstances
        {
            get
            {
                return m_pvpChallengeInstances;
            }
        }
        List<PVPAlternativeMode> m_pvpAlternativeModes = new List<PVPAlternativeMode>();  // PVP alternative modes
        public List<PVPAlternativeMode> pvpAlternativeModes
        {
            get
            {
                return m_pvpAlternativeModes;
            }
        }
        List<PersistentEnemy> m_persistentEnemies = new List<PersistentEnemy>();  // Persistent enemies
        public List<PersistentEnemy> persistentEnemies
        {
            get
            {
                return m_persistentEnemies;
            }
        }
        // List of PVPActiveTournaments
        List<double> m_projectPct = new List<double>();   // ProjectPct (Invasion construction status [Fomorian, Razorback Armada, ???])
        public List<double> projectPct
        {
            get
            {
                return m_projectPct;
            }
        }
        long m_worldSeed = 0; // World seed
        public long worldSeed
        {
            get
            {
                return m_worldSeed;
            }
        }
        # endregion

        /*
         * CONSTRUCTORS
         */
        public WorldStateData()
        {
#if DEBUG
            Stopwatch st = Stopwatch.StartNew();
#endif
            getWorldState();
            parseWorldState();
#if DEBUG
            st.Stop();
            Console.WriteLine("Retrieved and parsed WorldState in " + st.ElapsedMilliseconds + "ms");
#endif
        }

        public WorldStateData(string platform)  // todo: maybe add a class to specify platforms by name so something like "new WorldStateData(GamePlatform.XBOX1)" can be done
        {
            string p = platform.ToLower();
            if (worldStateUrls.ContainsKey(p))
            {
                wsUrl = worldStateUrls[p];
            }
        }

        /*
         * METHODS/FUNCTIONS/WHATEVER
         */
        public void getWorldState()
        {
            worldStatePhpString = getWorldStateString();
            worldStatePhpJson = JObject.Parse(worldStatePhpString);
        }

        private void clearAll()
        {
            m_events.Clear();
            m_alerts.Clear();
            m_sorties.Clear();
            m_syndicateMissions.Clear();
            m_fissureMissions.Clear();
            m_flashSales.Clear();
            m_invasions.Clear();
            m_nodeOverrides.Clear();
            m_badlandNodes.Clear();
            m_voidTraders.Clear();
            m_primeVaultAvailabilities.Clear();
            m_dailyDeals.Clear();
            m_pvpChallengeInstances.Clear();
            m_persistentEnemies.Clear();
            m_pvpAlternativeModes.Clear();
            m_projectPct.Clear();
        }

        public void refreshWorldState()
        {
#if DEBUG
            Stopwatch st = Stopwatch.StartNew();
#endif
            clearAll();
            getWorldState();
            parseWorldState();
#if DEBUG
            st.Stop();
            Console.WriteLine("Refreshed WorldState in " + st.ElapsedMilliseconds + "ms");
#endif
        }

        private void parseWorldState()
        {
            // Events
            try
            {
                foreach (JObject jobj in worldStatePhpJson["Events"])
                {
                    m_events.Add(new Event(jobj));
                }
                m_events.Sort(delegate(Event x, Event y)
                {
                    return y.date.CompareTo(x.date);
                });
            } catch (Exception e)
            {
                Console.WriteLine("Error while populating Events");
                Console.WriteLine(e.StackTrace);
            }

            // Alerts
            try
            {
                foreach (JObject jobj in worldStatePhpJson["Alerts"])
                {
                    m_alerts.Add(new Alert(jobj));
                }
            } catch (Exception e)
            {
                Console.WriteLine("Error while populating Alerts");
                Console.WriteLine(e.StackTrace);
            }

            // Sorties
            try
            {
                foreach (JObject jobj in worldStatePhpJson["Sorties"])
                {
                    m_sorties.Add(new Sorties(jobj));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while populating Sorties");
                Console.WriteLine(e.StackTrace);
            }

            // Syndicate missions
            try
            {
                foreach (JObject jobj in worldStatePhpJson["SyndicateMissions"])
                {
                    m_syndicateMissions.Add(new SyndicateMission(jobj));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while populating Syndicate missions");
                Console.WriteLine(e.StackTrace);
            }

            // Fissures
            try
            {
                foreach (JObject jobj in worldStatePhpJson["ActiveMissions"])
                {
                    m_fissureMissions.Add(new FissureMission(jobj));
                }
                // need to sort these because FOR SOME REASON they're not in tier order in worldState.php
                m_fissureMissions.Sort(delegate(FissureMission x, FissureMission y)
                {
                    return x.num.CompareTo(y.num);
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while populating Fissures");
                Console.WriteLine(e.StackTrace);
            }

            // Flash sales
            try
            {
                foreach (JObject jobj in worldStatePhpJson["FlashSales"])
                {
                    m_flashSales.Add(new FlashSale(jobj));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while populating Flash sales");
                Console.WriteLine(e.StackTrace);
            }

            // Invasions
            try
            {
                foreach (JObject jobj in worldStatePhpJson["Invasions"])
                {
                    m_invasions.Add(new Invasion(jobj));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while populating Invasions");
                Console.WriteLine(e.StackTrace);
            }

            // Node overrides
            try
            {
                foreach (JObject jobj in worldStatePhpJson["NodeOverrides"])
                {
                    m_nodeOverrides.Add(new NodeOverride(jobj));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while populating Node overrides");
                Console.WriteLine(e.StackTrace);
            }

            // Badland nodes (aka Dark Sectors)
            // future todo: probably generate Dark Sectors once and only occasionally check for any changes since they're so huge
            // also check worldState.php to see if Dark Sector info changes at all
            try
            {
                foreach (JObject jobj in worldStatePhpJson["BadlandNodes"])
                {
                    m_badlandNodes.Add(new BadlandNode(jobj));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while populating Dark Sectors");
                Console.WriteLine(e.StackTrace);
            }

            // Void Traders
            try
            {
                foreach (JObject jobj in worldStatePhpJson["VoidTraders"])
                {
                    m_voidTraders.Add(new VoidTrader(jobj));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while populating Baro's salt cache");
                Console.WriteLine(e.StackTrace);
            }

            // Prime Access availability
            try
            {
                m_primeAccessAvailability = worldStatePhpJson["PrimeAccessAvailability"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while getting Prime Access Availability");
                Console.WriteLine(e.StackTrace);
            }

            // Prime Vault availabilities
            if (worldStatePhpJson["PrimeVaultAvailabilities"] != null && worldStatePhpJson["PrimeVaultAvailabilities"].HasValues)
            {
                try
                {
                    foreach (JObject jobj in worldStatePhpJson["PrimeVaultAvailabilities"])
                    {
                        m_primeVaultAvailabilities.Add(jobj["State"].ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while populating Prime Vault availabilities");
                    Console.WriteLine(e.StackTrace);
                }
            }

            // Daily deals
            try
            {
                foreach (JObject jobj in worldStatePhpJson["DailyDeals"])
                {
                    m_dailyDeals.Add(new DailyDeal(jobj));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while populating Daily deals");
                Console.WriteLine(e.StackTrace);
            }

            // Library info (Simaris?)
            try
            {
                m_libraryInfo = worldStatePhpJson["LibraryInfo"]["LastCompletedTargetType"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while getting Library Info");
                Console.WriteLine(e.StackTrace);
            }

            // PVP challenges
            try
            {
                foreach (JObject jobj in worldStatePhpJson["PVPChallengeInstances"])
                {
                    m_pvpChallengeInstances.Add(new PVPChallengeInstance(jobj));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while populating PVP challenges");
                Console.WriteLine(e.StackTrace);
            }

            // Persistent enemies (Stalkerlytes)
            try
            {
                foreach (JObject jobj in worldStatePhpJson["PersistentEnemies"])
                {
                    m_persistentEnemies.Add(new PersistentEnemy(jobj));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while populating persistent enemies");
                Console.WriteLine(e.StackTrace);
            }

            // PVP alternative modes
            try
            {
                foreach (JObject jobj in worldStatePhpJson["PVPAlternativeModes"])
                {
                    m_pvpAlternativeModes.Add(new PVPAlternativeMode(jobj));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while populating PVP alternative modes");
                Console.WriteLine(e.StackTrace);
            }

            // ProjectPct
            try
            {
                foreach (JValue jval in worldStatePhpJson["ProjectPct"])
                {
                    m_projectPct.Add(jval.ToObject<double>());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while populating ProjectPct values");
                Console.WriteLine(e.StackTrace);
            }

            m_worldSeed = worldStatePhpJson["WorldSeed"].ToObject<long>();
        }

        private string getWorldStateString()
        {
            System.Net.WebClient wcli = new System.Net.WebClient();
            string str = "";
            try
            {
                str = wcli.DownloadString(wsUrl);
            } catch (System.Net.WebException e)
            {
                str = "Could not retrieve worldState.php";
            }
            return str;
        }

        public void changePlatform(string platform)
        {
            wsUrl = worldStateUrls[platform];
        }
    }

    // This class is just a dumping ground for random helper functions I think of
    public static class WorldStateHelper
    {
        public static DateTime unixTimeToDateTime(long time)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            epoch = epoch.AddSeconds(time / 1000);

            return epoch;
        }
    }
}
