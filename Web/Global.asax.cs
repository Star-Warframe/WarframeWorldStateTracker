using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;
using WorldStateWeb;
using WarframeWorldStateTest;

namespace WorldStateWeb
{
    public class Global : HttpApplication
    {
        public static WorldStateData wsdata { get; private set; }
        private static WorldStateData wsdataCpy;
        public static HtmlTextWriter alerts { get; private set; }           // Alerts
        public static HtmlTextWriter sorties { get; private set; }          // Sorties
        public static HtmlTextWriter invasions { get; private set; }        // Invasions
        public static HtmlTextWriter fissures { get; private set; }         // Fissures
        public static HtmlTextWriter voidTraders { get; private set; }      // Salt Trader
        public static HtmlTextWriter invEventStat { get; private set; }     // Invasion event progress
        public static HtmlTextWriter events { get; private set; }           // Events (News)
        public static HtmlTextWriter darvoDeal { get; private set; }        // Daily Darvo Deal
        public static HtmlTextWriter pvpChallenges { get; private set; }    // PVP Challenges
        public static HtmlTextWriter flashSales { get; private set; }       // Flash Sales (featured market stuff)
        public static HtmlTextWriter nodeOverrides { get; private set; }    // Node overrides (excluding destroyed relays)
        private int indentLvl = 3;  // indent level relative to everything else on Default.aspx

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // start a separate thread to update the WorldState data on a timer
            wsdata = new WarframeWorldStateTest.WorldStateData();
            wsdataCpy = wsdata;
            Thread t = new Thread(delegate()
                {
                    while (true)
                    {
                        wsdataCpy.refreshWorldState();
                        wsdata = wsdataCpy;
                        createHtml();

                        Thread.Sleep(60000);    // refresh wsdata on a 1-minute interval
                        // possible issue of trying to request page during the x milliseconds wsdata is being refreshed
                        // solved by using a copy and assigning to it?
                    }
                });
            t.Start();
        }

        void createHtml()
        {
            alerts = new HtmlTextWriter(new System.IO.StringWriter());  // why does HtmlTextWriter.Flush() not clear out the data...
            sorties = new HtmlTextWriter(new System.IO.StringWriter());
            invasions = new HtmlTextWriter(new System.IO.StringWriter());
            fissures = new HtmlTextWriter(new System.IO.StringWriter());
            voidTraders = new HtmlTextWriter(new System.IO.StringWriter());
            invEventStat = new HtmlTextWriter(new System.IO.StringWriter());
            events = new HtmlTextWriter(new System.IO.StringWriter());
            darvoDeal = new HtmlTextWriter(new System.IO.StringWriter());
            pvpChallenges = new HtmlTextWriter(new System.IO.StringWriter());
            flashSales = new HtmlTextWriter(new System.IO.StringWriter());
            nodeOverrides = new HtmlTextWriter(new System.IO.StringWriter());

            // todo trycatch stuff
            createAlerts();
            createSorties();
            createInvasions();
            createFissures();
            createVoidTraders();
            createInvEventStats();
            createEvents();
            createDarvoDeals();
            createPvpChallenges();
            createFlashSales();
            createNodeOverrides();
        }

        void createAlerts()
        {
            alerts.Indent = indentLvl;
            foreach (WarframeWorldStateTest.Alert alert in wsdata.alerts)
            {
                alerts.RenderBeginTag("tr");

                alerts.RenderBeginTag("td");
                alerts.Write(alert.missionInfo.location);
                alerts.RenderEndTag();
                alerts.WriteLine();

                alerts.RenderBeginTag("td");
                alerts.Write(alert.missionInfo.missionType);
                alerts.Write(" (" + alert.missionInfo.faction + " lvl " + alert.missionInfo.minEnemyLevel + "-" + alert.missionInfo.maxEnemyLevel + ")");
                alerts.RenderEndTag();
                alerts.WriteLine();

                TimeSpan tte = alert.expiryDate - DateTime.UtcNow;
                alerts.RenderBeginTag("td");
                alerts.Write((tte.Hours > 0 ? tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") : "") + tte.Minutes + (tte.Minutes != 1 ? " minutes left" : " minute left"));
                alerts.RenderEndTag();
                alerts.WriteLine();

                alerts.RenderBeginTag("td");
                alerts.Write(alert.missionInfo.missionReward.credits + " credits");
                alerts.RenderEndTag();
                alerts.WriteLine();

                alerts.RenderBeginTag("td");
                foreach (Tuple<string, int> t in alert.missionInfo.missionReward.countedItems)
                {
                    alerts.Write(t.Item2 + " " + t.Item1 + " ");
                }
                foreach(string s in alert.missionInfo.missionReward.items)
                {
                    alerts.Write(s);
                }
                alerts.RenderEndTag();

                alerts.RenderEndTag();
            }
        }

        void createSorties()
        {
            sorties.Indent = indentLvl;
            Sorties s;
            sorties.RenderBeginTag("tr");
            sorties.AddAttribute(HtmlTextWriterAttribute.Colspan, "4");
            sorties.RenderBeginTag("th");
            if (wsdata.sorties != null && wsdata.sorties[0] != null) 
            { 
                sorties.Write(wsdata.sorties[0].boss);
                sorties.Write("  ");
                s = wsdata.sorties[0];
                TimeSpan tte = s.expiry - DateTime.UtcNow;
                sorties.Write((tte.Hours > 0 ? tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") : "") + tte.Minutes + (tte.Minutes != 1 ? " minutes left" : " minute left"));
                sorties.RenderEndTag();
                sorties.RenderEndTag();
            } 
            else 
            {
                sorties.Write("N/A");
                sorties.RenderEndTag();
                sorties.RenderEndTag();
                return;
            }

            foreach(Variants v in s.variants)
            {
                sorties.RenderBeginTag("tr");

                sorties.RenderBeginTag("td");
                sorties.Write(v.node);
                sorties.RenderEndTag();
                sorties.WriteLine();

                sorties.RenderBeginTag("td");
                sorties.Write(v.missionType);
                sorties.RenderEndTag();
                sorties.WriteLine();

                sorties.RenderBeginTag("td");
                sorties.Write(v.modifierType);
                sorties.RenderEndTag();
                sorties.WriteLine();

                sorties.RenderBeginTag("td");
                sorties.Write(v.tileset);
                sorties.RenderEndTag();

                sorties.RenderEndTag();
            }
        }

        void createInvasions()
        {
            invasions.Indent = indentLvl;
            foreach (WarframeWorldStateTest.Invasion invasion in Global.wsdata.invasions)
            {
                if (!invasion.completed)
                {
                    // AttackerMissionInfo faction is defender faction and vice versa
                    string attacker = invasion.defenderMissionInfo.Item2;
                    string defender = invasion.attackerMissionInfo.Item2;
                    invasions.RenderBeginTag("tr");

                    invasions.RenderBeginTag("td");
                    invasions.Write(invasion.node);
                    invasions.RenderEndTag();
                    invasions.WriteLine();

                    invasions.RenderBeginTag("td");
                    invasions.Write(invasion.faction);
                    invasions.RenderEndTag();
                    invasions.WriteLine();

                    double percentComplete = ((double)invasion.count / (double)invasion.goal);
                    invasions.RenderBeginTag("td");
                    invasions.Write(Math.Floor(Math.Abs(percentComplete * 100.0)) + "%");
                    if (percentComplete > 0) { invasions.Write(" (Advantage: " + attacker + ")"); }
                    else if (percentComplete < 0) { invasions.Write(" (Advantage: " + defender + ")"); }
                    else { invasions.Write(" (Tie)"); }
                    invasions.RenderEndTag();
                    invasions.WriteLine();

                    invasions.RenderBeginTag("td");
                    invasions.Write(defender + " rewards: " + invasion.defenderReward.First().Item2 + " " + invasion.defenderReward.First().Item1);
                    invasions.RenderEndTag();
                    // Infested invasions don't have an attackerReward entry in the worldState so need to check for that
                    if (invasion.attackerReward.Count > 0)
                    {
                        invasions.WriteLine();
                        invasions.RenderBeginTag("td");
                        invasions.Write(attacker + " rewards" + invasion.attackerReward.First().Item2 + " " + invasion.attackerReward.First().Item1);
                        invasions.RenderEndTag();
                    }
                    invasions.RenderEndTag();
                }
            }
        }

        void createFissures()
        {
            fissures.Indent = indentLvl;
            foreach (FissureMission fissure in wsdata.fissureMissions)
            {
                fissures.RenderBeginTag("tr");

                fissures.RenderBeginTag("td");
                fissures.Write(fissure.modifier);
                fissures.RenderEndTag();
                fissures.WriteLine();

                fissures.RenderBeginTag("td");
                fissures.Write(fissure.nodeName);
                fissures.RenderEndTag();
                fissures.WriteLine();

                fissures.RenderBeginTag("td");
                fissures.Write(fissure.nodeType + " (" + fissure.nodeEnemy + ")");
                fissures.RenderEndTag();
                fissures.WriteLine();

                TimeSpan tte = fissure.expiry - DateTime.UtcNow;
                fissures.RenderBeginTag("td");
                fissures.Write((tte.Hours > 0 ? tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") : "") + tte.Minutes + (tte.Minutes != 1 ? " minutes left" : " minute left"));
                fissures.RenderEndTag();

                fissures.RenderEndTag();
            }
        }

        void createVoidTraders()
        {
            voidTraders.Indent = indentLvl;
            //voidTraders.Write("<tr><th colspan=\"3\">");
            voidTraders.RenderBeginTag("tr");
            voidTraders.AddAttribute(HtmlTextWriterAttribute.Colspan, "3");
            voidTraders.RenderBeginTag("th");
            if (wsdata.voidTraders == null || wsdata.voidTraders.Count < 1)  // No void trader for some reason
            {
                voidTraders.Write("N/A");
                voidTraders.RenderEndTag();
                voidTraders.RenderEndTag();
                return;
            }
            VoidTrader v = wsdata.voidTraders.First();
            // if active, show location, departure time and list his stuff
            if (v.activation <= DateTime.UtcNow && v.expiry >= DateTime.UtcNow)
            {
                voidTraders.Write("Active at ");
                voidTraders.Write(v.node);
                voidTraders.Write(" for ");
                TimeSpan tte = v.expiry - DateTime.UtcNow;
                voidTraders.Write((tte.Days > 0 ? tte.Days + (tte.Days != 1 ? " days, " : " day, ") : "")
                    + (tte.Hours > 0 ? tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") : "")
                    + tte.Minutes + (tte.Minutes != 1 ? " minutes" : " minute"));
                //voidTraders.Write("</th></tr>");
                voidTraders.RenderEndTag();
                voidTraders.RenderEndTag();
                foreach(Tuple<string, int, int> t in v.manifest)
                {
                    //voidTraders.Write("<tr>");
                    voidTraders.RenderBeginTag("tr");
                    //voidTraders.Write("<td>" + t.Item1 + "</td>");
                    voidTraders.RenderBeginTag("td");
                    voidTraders.Write(t.Item1);
                    voidTraders.RenderEndTag();
                    voidTraders.WriteLine();
                    //voidTraders.Write("<td>" + t.Item2 + " ducats</td>");
                    voidTraders.RenderBeginTag("td");
                    voidTraders.Write(t.Item2 + " ducats");
                    voidTraders.RenderEndTag();
                    voidTraders.WriteLine();
                    //voidTraders.Write("<td>" + t.Item3 + " credits</td>");
                    voidTraders.RenderBeginTag("td");
                    voidTraders.Write(t.Item3 + " credits");
                    voidTraders.RenderEndTag();
                    //voidTraders.Write("</tr>");
                }
            }
            // if inactive, say as much and show location, arrival time
            else
            {
                voidTraders.Write("Arriving at ");
                voidTraders.Write(v.node);
                voidTraders.Write(" in ");
                TimeSpan tte = v.activation - DateTime.UtcNow;
                voidTraders.Write((tte.Days > 0 ? tte.Days + (tte.Days != 1 ? " days, " : " day, ") : "")
                    + (tte.Hours > 0 ? tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") : "") 
                    + tte.Minutes + (tte.Minutes != 1 ? " minutes" : " minute"));
                voidTraders.RenderEndTag();
                voidTraders.RenderEndTag();
            }
        }

        void createInvEventStats()
        {
            invEventStat.Indent = indentLvl;
            // [0] is for Fomorian, [1] is for Razorback
            string grinPct = wsdata.projectPct[0].ToString("N1") + "%";
            string grinRemain = (100.0 - wsdata.projectPct[0]).ToString("N1") + "%";
            string corpPct = wsdata.projectPct[1].ToString("N1") + "%";
            string corpRemain = (100.0 - wsdata.projectPct[1]).ToString("N1") + "%";

            invEventStat.AddStyleAttribute(HtmlTextWriterStyle.Height, "80px");
            invEventStat.RenderBeginTag("div");
            invEventStat.Write("Fomorian");
            invEventStat.WriteFullBeginTag("br");
            invEventStat.Write(grinPct);
            invEventStat.WriteLine();
            
            invEventStat.AddAttribute(HtmlTextWriterAttribute.Class, "percent-bar");
            invEventStat.RenderBeginTag("div");
            
            invEventStat.AddAttribute(HtmlTextWriterAttribute.Class, "progress-percent progress-done grineer");
            invEventStat.AddStyleAttribute(HtmlTextWriterStyle.Width, grinPct);
            invEventStat.RenderBeginTag("div");
            invEventStat.RenderEndTag();
            
            invEventStat.AddAttribute(HtmlTextWriterAttribute.Class, "progress-percent progress-remaining");
            invEventStat.AddStyleAttribute(HtmlTextWriterStyle.Width, grinRemain);
            invEventStat.RenderBeginTag("div");
            invEventStat.RenderEndTag();
            invEventStat.RenderEndTag();
            invEventStat.RenderEndTag();

            invEventStat.AddStyleAttribute(HtmlTextWriterStyle.Height, "80px");
            invEventStat.RenderBeginTag("div");
            invEventStat.Write("Razorback Armada");
            invEventStat.WriteFullBeginTag("br");
            invEventStat.Write(corpPct);
            
            invEventStat.AddAttribute(HtmlTextWriterAttribute.Class, "percent-bar");
            invEventStat.RenderBeginTag("div");
            
            invEventStat.AddAttribute(HtmlTextWriterAttribute.Class, "progress-percent progress-done corpus");
            invEventStat.AddStyleAttribute(HtmlTextWriterStyle.Width, corpPct);
            invEventStat.RenderBeginTag("div");
            invEventStat.RenderEndTag();
            
            invEventStat.AddAttribute(HtmlTextWriterAttribute.Class, "progress-percent progress-remaining");
            invEventStat.AddStyleAttribute(HtmlTextWriterStyle.Width, corpRemain);
            invEventStat.RenderBeginTag("div");
            invEventStat.RenderEndTag();
            invEventStat.RenderEndTag();
            invEventStat.RenderEndTag();
        }

        void createEvents()
        {
            events.Indent = indentLvl;
            foreach (Event e in wsdata.events)
            {
                events.AddAttribute(HtmlTextWriterAttribute.Class, "newsbox");
                events.RenderBeginTag("div");
                TimeSpan timeUp = DateTime.UtcNow - e.date;
                string message = "";
                foreach (var m in e.messages)
                {
                    if (m.Key == "en")
                    {
                        message = m.Value;
                    }
                }
                events.RenderBeginTag("span");
                events.Write("(" + timeUp.Days + (timeUp.Days != 1 ? " days ago) " : " day ago) ") + message);
                events.RenderEndTag();
                events.WriteFullBeginTag("br");
                events.AddAttribute(HtmlTextWriterAttribute.Href, e.prop);
                events.RenderBeginTag("a");
                events.Write(e.prop);
                events.RenderEndTag();
                events.RenderEndTag();
            }
        }

        void createDarvoDeals()
        {
            darvoDeal.Indent = indentLvl;
            darvoDeal.Write("<span>todo</span>");
        }

        void createPvpChallenges()
        {
            pvpChallenges.Indent = indentLvl;
            pvpChallenges.Write("<span>todo</span>");
        }

        void createFlashSales()
        {
            flashSales.Indent = indentLvl;
            flashSales.Write("<span>todo</span>");
        }

        void createNodeOverrides()
        {
            nodeOverrides.Indent = indentLvl;
            nodeOverrides.Write("<span>todo</span>");
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}
