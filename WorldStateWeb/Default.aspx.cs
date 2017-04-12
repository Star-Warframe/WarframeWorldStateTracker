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
        protected System.Text.StringBuilder _str = new System.Text.StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            wsdata = new WarframeWorldStateTest.WorldStateData();
            foreach (WarframeWorldStateTest.Alert alert in wsdata.alerts)
            {
                _str.AppendLine(alert.ToString());
                _str.AppendLine("</br>");
            }
        }
    }
}