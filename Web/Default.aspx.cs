using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorldStateWeb
{
    public partial class _Default : Page
    {
        //protected WarframeWorldStateTest.WorldStateData wsdata;
        protected System.Text.StringBuilder alerts = new System.Text.StringBuilder();
        protected System.Text.StringBuilder sorties = new System.Text.StringBuilder();
        protected System.Text.StringBuilder invasions = new System.Text.StringBuilder();
        protected System.Text.StringBuilder fissures = new System.Text.StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            //wsdata = new WarframeWorldStateTest.WorldStateData();

            alerts.AppendLine("<ul>");
            foreach (WarframeWorldStateTest.Alert alert in Global.wsdata.alerts)
            {
                alerts.AppendLine("<li>");
                //alerts.Append(alert.ToString());
                alerts.Append(alert.missionInfo.location + " | ");
                alerts.Append(alert.missionInfo.missionType);
                alerts.Append(" (" + alert.missionInfo.faction + " lvl " + alert.missionInfo.minEnemyLevel + "-" + alert.missionInfo.maxEnemyLevel + ") | ");
                alerts.Append(alert.missionInfo.missionReward.credits + " credits | ");
                foreach(Tuple<string, int> t in alert.missionInfo.missionReward.countedItems)
                {
                    alerts.Append(t.Item2 + " " + t.Item1 + " | ");
                }
                TimeSpan tte = alert.expiryDate - DateTime.UtcNow;
                alerts.Append((tte.Hours > 0 ? tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") : "") + tte.Minutes + (tte.Minutes != 1 ? " minutes left" : " minute left"));
                alerts.AppendLine("</li>");
            }
            alerts.AppendLine("</ul>");

            foreach (WarframeWorldStateTest.Sorties sortie in Global.wsdata.sorties)
            {
                sorties.AppendLine(sortie.ToString());
                sorties.AppendLine("</br>");
            }

            invasions.AppendLine("<ul>");
            foreach (WarframeWorldStateTest.Invasion invasion in Global.wsdata.invasions)
            {
                invasions.AppendLine("<li>" + invasion.ToString() + "</li>");
            }
            invasions.AppendLine("</ul>");

            fissures.AppendLine("<ul>");
            foreach (WarframeWorldStateTest.FissureMission fissure in Global.wsdata.fissureMissions)
            {
                fissures.AppendLine("<li>");
                //fissures.Append(fissure.ToString());
                fissures.Append(fissure.modifier + " | ");
                fissures.Append(fissure.nodeName + " | ");
                fissures.Append(fissure.nodeType + " (" + fissure.nodeEnemy + ") | ");
                TimeSpan tte = fissure.expiry - DateTime.UtcNow;
                fissures.Append((tte.Hours > 0 ? tte.Hours + (tte.Hours != 1 ? " hours, " : " hour, ") : "") + tte.Minutes + (tte.Minutes != 1 ? " minutes left" : " minute left"));
                fissures.AppendLine("</li>");
            }
            fissures.AppendLine("</ul>");
        }
    }
}