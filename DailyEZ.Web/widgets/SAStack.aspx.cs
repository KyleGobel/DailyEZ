using System;
using System.Text.RegularExpressions;
using System.Configuration;
using DailyEZ.Web.Code;
using DailyEZ.Web.com.dailyez;

namespace DailyEZ.Web.widgets
{
    public partial class SAStack : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var clientID = Code.Utility.GetIntFromQueryString(Request, "clientID");
            
            var stackid = Code.Utility.GetIntFromQueryString(Request, "id");

            var service = new Service();
      
            //if the clientId was found but the ID wasn't in the query string
            if (clientID > 0 && stackid == 0)
            {
                //get the stackID directly from the client
                var meg = service.GetMEGv3ByClientID(ConfigurationManager.AppSettings["webServiceKey"], clientID);
                var dailyEZObj = service.GetDailyEZByClientID(ConfigurationManager.AppSettings["webServiceKey"], clientID);
                //check dailyEZ first
                if (dailyEZObj != null)
                {
                    if (dailyEZObj.Mobile_Stack_ID.HasValue)
                        stackid = dailyEZObj.Mobile_Stack_ID.Value;
                    Response.Write(stackid);
                }
                
                //next check meg if our stackID still equals 0 and there is a meg object
                if (meg != null && stackid == 0)
                {
                    if (meg.Mobile_Stack_ID.HasValue)
                        stackid = meg.Mobile_Stack_ID.Value;
                    else
                    {
                        Response.Write("No Valid StackID found");
                    }
                }

                //if the stackID still equals 0, we have nothing
                if (stackid == 0)
                {
                    Response.Write("No valid Mobile Stacks found");
                    return;
                }
            }


            var stack = service.GetStack(stackid);


       
            if (stack == null)
            {
                Response.Write("No Stack Found" + stackid);
                return;
            }
            //get the src url of the iFrame
            Regex reg = new Regex("src=[\"](widgets\\/[^\"]*)[\"]");
            string source = reg.Match(stack.Widgets.Replace("###", "")).Groups[1].Value;
            source = source.Replace("widgets/MobilePageView.aspx", "Mobile/");
            Response.Redirect(source + "&stackID=" + stackid + "&clientID=" + clientID);
            
        }

    }
}