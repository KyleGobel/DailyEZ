using System;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;
using Elmah;

namespace DailyEZ.Web.Code
{
    public class Renderer
    {
        public static string HtmlHeader(HttpContext context)
        {
            if (BasePage.DailyEZObject == null)
            {
                ErrorLog.GetDefault(HttpContext.Current).Log(new Error(new NullReferenceException("BasePage.DailyEZObject")));
                return "DailyEZ Object is currently null";
            }
            var clientID = BasePage.DailyEZObject.Client_ID;
            int tab = Utility.GetIntFromCookie(context.Request, "tab");
            if (tab == 0)
                tab = 1;
            string htm = "<ul class=\"nav\">";


            int menuFolderID =
                BasePage.WebService.GetMenuFolderFromClient(ConfigurationManager.AppSettings["webServiceKey"], clientID);
            var dailyEZ = BasePage.DailyEZObject;

            com.dailyez.MenuLinks[] tabs =
                BasePage.WebService.GetMenuLinksByFolder(ConfigurationManager.AppSettings["webServiceKey"], menuFolderID);

            int counter = 1;
            //add one to tab so it's 1 based instead of 0 based (so it matches the counter)
            if (tab == 0)
                tab++;

            string serverName = context.Request.ServerVariables["SERVER_NAME"];
            // if (serverName == "thedailyez.com" || serverName == "www.thedailyez.com")
            serverName = dailyEZ.User_Friendly_URL.Replace("http://", "");
           
            //get our current address we're at
            var currentAddress = context.Request.ServerVariables["SERVER_NAME"] + context.Request.ServerVariables["URL"];


            //make some formatting changes
            //remove the DailyEZ directory
            currentAddress = currentAddress.Replace("DailyEZ/", "");
            //get rid of default.aspx if it's there
            currentAddress = currentAddress.Replace("default.aspx", "");
            //get rid of protocol if it's there
            currentAddress = currentAddress.Replace("http://", "");

            foreach (com.dailyez.MenuLinks menuLink in tabs)
            {
                var tabUrl = serverName + "/" + menuLink.URL;
                var extraStyle = "";
                //if we're on the page, make it the active tab
                context.Response.Write("<!--Current Address:" + currentAddress + " Check Address: " + tabUrl + "\n\n\t-->\n\n");
                if (currentAddress.ToLower() == tabUrl.ToLower() )
                {
                    extraStyle = " class=\"active\"";
                }

                htm += "<li" + extraStyle + "><a href='http://" + tabUrl + "'>" + HttpUtility.HtmlEncode(menuLink.Title) + "</a></li>";
                counter++;
            }
            htm += "</ul>\n";
            return htm;

        }

        public static bool RenderJetNettAdsToLiteral(Literal Ads, HttpRequest request)
        {
            if (Ads == null) throw new ArgumentNullException("Ads");

            //Get the ClientID, as well as the PageID
            var clientID = Utility.GetIntFromCookie(request, "clientID");
            var pageID = Utility.GetIntFromQueryString(request, "id");


            var service = new com.dailyez.Service();
            com.dailyez.Ad_Page_Relationship rel = null;

            //try most specific --clientID AND pageID
            rel = service.GetAdPageRelationshipByClientIDAndPageID(ConfigurationManager.AppSettings["webServiceKey"],
                                                                   clientID, pageID);

            //broaden - just the page
            if (rel == null)
                rel = service.GetAdPageRelationshipByPageIDOnly(ConfigurationManager.AppSettings["webServiceKey"],
                                                                pageID);

            //broaden - clientID
            if (rel == null)
                rel = service.GetAdPageRelationshipByClientIDOnly(ConfigurationManager.AppSettings["webServiceKey"],
                                                                  clientID);


            //nothing found, just return a false
            if (rel == null)
                return false;


            //if the relationship found has an ad group, and we're not using the broker code
            if (rel.Ad_Group > 0 && !rel.Use_Broker_Code)
            {

                //store ad group in our local variable
                var adGroup = rel.Ad_Group;
                var group = service.GetAdGroup(ConfigurationManager.AppSettings["webServiceKey"],
                                                               adGroup);

                var ads = service.GetAdAssignmentsByAdGroup(adGroup);


                string htm = "";

                //if there's no ad assignments for this ad group, return false
                if (ads.Length == 0)
                    return false;


                //loop from 0 to the size of the viewport (viewport is how many ads are displayed at one time
                for (var i = 0; i < group.Viewport_Size; i++)
                {
                    var adFound = false;

                    //store the currentAd int as the current Ad index to be displayed
                    var currentAd = (group.Seed + i) % ads.Length;
                    while (!adFound)
                    {

                        //ad_mode 2 means don't show this ad, so we skip over this one
                        if (ads[currentAd].Ad_Mode == 2)
                        {
                            group.Seed += 1;
                            currentAd = (group.Seed + i) % ads.Length;
                        } //Ad mode 1 means it's limited, make sure this ad hasn't used up all it's views
                        else if (ads[currentAd].Ad_Mode == 1)
                        {

                            //Get beginning of month, and end of month
                            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                            var endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                                                            DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                            var tracker =
                                service.GetAdViewReport(ConfigurationManager.AppSettings["webServiceKey"], startDate,
                                                        endDate, ads[currentAd].ID, null, null);
                            if (ads[currentAd].Ad_Limit <= tracker.Length)
                            {
                                group.Seed += 1;

                                //set to current ad to see if ad is changing, if it's not set found == true so we break the loop eventually
                                var temp = currentAd;
                                currentAd = (group.Seed + i) % ads.Length;
                                if (currentAd == temp)
                                    adFound = true;
                            }
                            else
                                adFound = true;
                        } // unlimited ad views, mark the ad as found
                        else
                            adFound = true;
                    }



                    //get the actual ad if there is one in place
                    if (ads[currentAd].Ad_ID > 0)
                    {
                        com.dailyez.Ads_Model ad = service.GetAdsModel(ads[currentAd].Ad_ID);


                        if (ad != null)
                        {
                            htm += "<div class=\"divAd\" style='width:300px;overflow:hidden; height:" + ad.Ad_Height +
                                   "px;border:" + ad.Border_Style + ";'>" + ad.Html + "</div><br/>";
                        }
                    }
                    //css values for controling width and height of the img

                    //track ad view
                    var viewTrack = new com.dailyez.Ad_View_Tracker
                        {
                            Ad_ID = ads[currentAd].ID,
                            Age_Group = Utility.GetIntFromCookie(request, "ageGroup"),
                            Client_ID = Utility.GetIntFromCookie(request, "clientID"),
                            Date = DateTime.Now,
                            Page_ID = pageID,
                            Zipcode = Utility.GetStringFromCookie(request, "zip")
                        };

                    if (Utility.GetStringFromCookie(request, "registered2").Equals("true"))
                        service.TrackAdView(ConfigurationManager.AppSettings["webServiceKey"], viewTrack);
                }
                Ads.Text = htm;
                service.IncrementAdGroupSeed(ConfigurationManager.AppSettings["webServiceKey"], group.ID);
            }
            else
                return false;

            return true;
        }
    }
}