using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WorldStateWeb;

namespace WorldStateWeb
{
    public class Global : HttpApplication
    {
        public static WarframeWorldStateTest.WorldStateData wsdata { get; private set; }
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

                        Thread.Sleep(60000);    // refresh wsdata on a 1-minute interval
                        // possible issue of trying to request page during the x milliseconds wsdata is being refreshed
                    }
                });
            t.Start();
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
