using System;
using System.Configuration;
using System.Web.UI;
using JetNettApi.Data;
using Ninject;

namespace DailyEZ.Web.Code
{
    public class BasePage : Page
    {
        public static com.dailyez.Service WebService = null;
        public static com.dailyez.Daily_EZ DailyEZObject = null;

        [Inject]
        public JetNettApiUow Uow { get; set; }
        

        public BasePage()
        {
            PreInit += BasePagePreInit;
    
        }

        private void BasePagePreInit(object sender, EventArgs e)
        {
            WebService = new com.dailyez.Service();

            int clientID = Utility.GetIntFromCookie(Request, "clientID");
            //if (HttpContext.Current.IsDebuggingEnabled) 
            clientID = 777;
            if (clientID == 0)
            {
                //DON'T TOUCH ANYTHING BETWEEN THESE LINES
                //---------------------------------------------------
                var url = Request.Url.ToString();
                Uri uri = new Uri(url);


                //url = url.Remove(url.LastIndexOf("/", StringComparison.Ordinal)).Replace("/DailyEZ", "").Replace("/widgets", "");
                url = "http://" + uri.Host;
                //url = "http://mary.dailyez.com";
                clientID = WebService.GetClientIDFromUserFriendlyURL(ConfigurationManager.AppSettings["webServiceKey"],
                                                                     url);
                //--------------------------------------------------- 



                if (clientID == 0)
                {
                    //add lines here
                    if (url.Contains("mononawirestaurants.com"))
                        clientID = 826;
                    //COPY THESE LINES
                    //***********************
                    else if (url.Contains("cottagegroverestaurants.com"))
                        clientID = 999;
                    //***********************
                    else if (url == "http://wisconsinrecallelection.com" || url.Contains("Single-Site-Pages"))
                        clientID = 790; //set it to SC Wis

                    //else
                    //    Response.Redirect("~/ErrorPage.aspx?errorMessage=No ClientID found for " +
                    //                      HttpUtility.UrlEncode(url));
                }
            }


            DailyEZObject = WebService.GetDailyEZByClientID(ConfigurationManager.AppSettings["webServiceKey"], clientID);
            
        }

        //TODO: Get rid of this section
        //public static void RenderStack(int stackID, Literal literal)
        //{
        //    var stack = WebService.GetStack(stackID);

        //    string htm = stack.Widgets.Replace("###", "<br/>");

        //    literal.Text = htm;
        //}
    }
}