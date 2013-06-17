using System;
using System.Web.Optimization;
using System.Web.Routing;
using DailyEZ.Web.Code;

namespace DailyEZ.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Ignore("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.MapPageRoute("Standard Link Page Route", "{PageName}",
                "~/Community.aspx");

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}