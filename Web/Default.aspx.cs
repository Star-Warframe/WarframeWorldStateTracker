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
        protected WarframeWorldStateTest.WorldStateData wsdata;
        protected System.Text.StringBuilder alerts = new System.Text.StringBuilder();
        protected System.Text.StringBuilder sorties = new System.Text.StringBuilder();
        protected System.Text.StringBuilder invasions = new System.Text.StringBuilder();
        protected System.Text.StringBuilder fissures = new System.Text.StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            wsdata = new WarframeWorldStateTest.WorldStateData();
            foreach (WarframeWorldStateTest.Alert alert in wsdata.alerts)
            {
                alerts.AppendLine(alert.ToString());
                alerts.AppendLine("</br>");
            }

            foreach(WarframeWorldStateTest.Sorties sortie in wsdata.sorties)
            {
                sorties.AppendLine(sortie.ToString());
                sorties.AppendLine("</br>");
            }
            
            foreach(WarframeWorldStateTest.Invasion invasion in wsdata.invasions)
            {
                invasions.AppendLine(invasion.ToString());
                invasions.AppendLine("</br>");
            }

            foreach(WarframeWorldStateTest.FissureMission fissure in wsdata.fissureMissions)
            {
                fissures.AppendLine(fissure.ToString());
                fissures.AppendLine("</br>");
            }
        }
    }
}