using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WorldStateWeb;
using WarframeWorldStateTest;

namespace WorldStateWeb
{
    public class Global : HttpApplication
    {
        public static WorldStateData wsdata { get; private set; }
        public static System.Text.StringBuilder alerts = new System.Text.StringBuilder();
        public static System.Text.StringBuilder sorties = new System.Text.StringBuilder();
        public static System.Text.StringBuilder invasions = new System.Text.StringBuilder();
        public static System.Text.StringBuilder fissures = new System.Text.StringBuilder();

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // start a separate thread to update the WorldState data on a timer
            wsdata = new WarframeWorldStateTest.WorldStateData();
            Thread t = new Thread(delegate()
                {
                    while (true)
                    {
                        wsdata.refreshWorldState();
                        createHtml();

                        Thread.Sleep(60000);    // refresh wsdata on a 1-minute interval
                        // possible issue of trying to request page during the x milliseconds wsdata is being refreshed
                    }
                });
            t.Start();
        }

        void createHtml()
        {
            alerts.Clear();
            sorties.Clear();
            invasions.Clear();
            fissures.Clear();

            createAlerts();
            createSorties();
            createInvasions();
            createFissures();
        }

        void createAlerts()
        {
            foreach (WarframeWorldStateTest.Alert alert in wsdata.alerts)
            {
                //alerts.AppendLine("<li>");
                alerts.AppendLine("<tr>");
                alerts.AppendLine("<td>");
                //alerts.Append(alert.ToString());
                alerts.Append(alert.missionInfo.location/* + " | "*/ + "</td>");
                alerts.AppendLine("<td>");
                alerts.Append(alert.missionInfo.missionType);
                alerts.Append(" (" + alert.missionInfo.faction + " lvl " + alert.missionInfo.minEnemyLevel + "-" + alert.missionInfo.maxEnemyLevel/* + ") | "*/ + ") </td>");
                TimeSpan tte = alert.expiryDate - DateTime.UtcNow;
                alerts.Append("<td>");
                alerts.Append((tte.Hours > 0 ? tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") : "") + tte.Minutes + (tte.Minutes != 1 ? " minutes left" : " minute left"));
                //alerts.AppendLine("</li>");
                alerts.AppendLine("</td>");
                alerts.Append("<td>");
                alerts.Append(alert.missionInfo.missionReward.credits + " credits"/* + " | "*/ + "</td>");
                alerts.Append("<td>");
                foreach (Tuple<string, int> t in alert.missionInfo.missionReward.countedItems)
                {
                    alerts.Append(t.Item2 + " " + t.Item1 + " ");
                }
                foreach(string s in alert.missionInfo.missionReward.items)
                {
                    alerts.Append(s);
                }
                alerts.Append("</td>");
                alerts.AppendLine("</tr>");
            }
        }

        void createSorties()
        {
            Sorties s;
            sorties.Append("<th colspan=\"4\">");
            if (wsdata.sorties != null && wsdata.sorties[0] != null) 
            { 
                sorties.Append(wsdata.sorties[0].boss);
                sorties.Append("  ");
                s = wsdata.sorties[0];
                TimeSpan tte = s.expiry - DateTime.UtcNow;
                sorties.Append((tte.Hours > 0 ? tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") : "") + tte.Minutes + (tte.Minutes != 1 ? " minutes left" : " minute left"));
            } 
            else 
            { 
                sorties.AppendLine("N/A</th>");
                return;
            }
            sorties.AppendLine("</th>");

            foreach(Variants v in s.variants)
            {
                sorties.Append("<tr>");
                sorties.Append("<td>" + v.node + "</td>");
                sorties.Append("<td>" + v.missionType + "</td>");
                sorties.Append("<td>" + v.modifierType + "</td>");
                sorties.Append("<td>" + v.tileset + "</td>");
                sorties.AppendLine("</tr>");
            }
        }

        void createInvasions()
        {
            foreach (WarframeWorldStateTest.Invasion invasion in Global.wsdata.invasions)
            {
                if (!invasion.completed)
                {
                    invasions.Append("<tr>");
                    invasions.Append("<td>" + invasion.node + "</td>");
                    invasions.Append("<td>" + invasion.faction + " invasion</td>");
                    double percentComplete = ((double)invasion.count / (double)invasion.goal);
                    invasions.Append("<td>" + Math.Floor(Math.Abs(percentComplete * 100.0)) + "%");
                    if (percentComplete > 0) { invasions.Append(" (Advantage: attacker)"); }
                    else if (percentComplete < 0) { invasions.Append(" (Advantage: defender)"); }
                    else { invasions.Append(" (Tie)"); }
                    invasions.Append("</td>");
                    // Infested invasions don't have an attackerReward entry in the worldState so need to check for that
                    invasions.Append("<td>Defender rewards: " + invasion.defenderReward.First().Item2 + " " + invasion.defenderReward.First().Item1 + "</td>");
                    if (invasion.attackerReward.Count > 0) { invasions.Append("<td>Attacker rewards: " + invasion.attackerReward.First().Item2 + " " + invasion.attackerReward.First().Item1 + "</td>"); }
                    invasions.AppendLine("</tr>");
                }
                //invasions.AppendLine("<li>" + invasion.ToString() + "</li>");
            }
        }

        void createFissures()
        {
            //fissures.AppendLine("<ul>");
            //foreach (WarframeWorldStateTest.FissureMission fissure in Global.wsdata.fissureMissions)
            //{
            //    fissures.AppendLine("<li>");
            //    //fissures.Append(fissure.ToString());
            //    fissures.Append(fissure.modifier + " | ");
            //    fissures.Append(fissure.nodeName + " | ");
            //    fissures.Append(fissure.nodeType + " (" + fissure.nodeEnemy + ") | ");
            //    TimeSpan tte = fissure.expiry - DateTime.UtcNow;
            //    fissures.Append((tte.Hours > 0 ? tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") : "") + tte.Minutes + (tte.Minutes != 1 ? " minutes left" : " minute left"));
            //    fissures.AppendLine("</li>");
            //}
            //fissures.AppendLine("</ul>");
            foreach (FissureMission fissure in wsdata.fissureMissions)
            {
                fissures.AppendLine("<tr>");
                fissures.Append("<td>" + fissure.modifier + "</td>");
                fissures.Append("<td>" + fissure.nodeName + "</td>");
                fissures.Append("<td>" + fissure.nodeType + " (" + fissure.nodeEnemy + ")</td>");
                TimeSpan tte = fissure.expiry - DateTime.UtcNow;
                fissures.Append("<td>" + (tte.Hours > 0 ? tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") : "") + tte.Minutes + (tte.Minutes != 1 ? " minutes left" : " minute left") + "</td>");
                fissures.AppendLine("</tr>");
            }
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
