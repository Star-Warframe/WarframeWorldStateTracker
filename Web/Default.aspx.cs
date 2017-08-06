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
        protected HtmlTextWriter alerts = Global.alerts;
        protected HtmlTextWriter sorties = Global.sorties;
        protected HtmlTextWriter invasions = Global.invasions;
        protected HtmlTextWriter fissures = Global.fissures;
        protected HtmlTextWriter voidTraders = Global.voidTraders;
        protected HtmlTextWriter invEventStat = Global.invEventStat;
        protected HtmlTextWriter events = Global.events;
        protected HtmlTextWriter darvoDeal = Global.darvoDeal;
        protected HtmlTextWriter pvpChallenges = Global.pvpChallenges;
        protected HtmlTextWriter flashSales = Global.flashSales;
        protected HtmlTextWriter nodeOverrides = Global.nodeOverrides;

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}