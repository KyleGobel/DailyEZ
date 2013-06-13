using System.Configuration;
using System.Web;

namespace DailyEZ.Web.widgets.AdGroup
{
    /// <summary>
    /// Summary description for AdGroupHtml
    /// </summary>
    public class AdGroupHtml : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            
            var html = GetAdGroupHtml(
                Code.Utility.TryParseIntWithDefault(context.Request["adGroup"], 0), 
                Code.Utility.TryParseBoolWithDefault(context.Request["autoRotate"], true),
                context);
            context.Response.Write(html);
        }
        protected string GetAdGroupHtml(int adGroup, bool autoRotate, HttpContext context)
        {
            var service = new com.dailyez.Service();

            int pageID = 0;

            com.dailyez.AdGroup group = service.GetAdGroup(ConfigurationManager.AppSettings["webServiceKey"], adGroup);
            com.dailyez.Ad_Assignments[] ads = service.GetAdAssignmentsByAdGroup(adGroup);

            var htm = "";
            if (ads.Length == 0)
                return "No Ads in this Group";

            if (!autoRotate)
                group.Seed = 0;
            for (var i = 0; i < group.Viewport_Size; i++)
            {

                var adFound = false;
                var currentAd = 0;

                //current display ad + 
                currentAd = (group.Seed + i) % ads.Length;
                while (!adFound)
                {
                    if (ads[currentAd].Ad_Mode == 2)
                    {
                        if (autoRotate)
                            group.Seed += 1;
                        currentAd = (group.Seed + i) % ads.Length;
                    }
                    else if (ads[currentAd].Ad_Mode == 1)
                    {
                        var startDate = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
                        var endDate = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month));
                        com.dailyez.Ad_View_Tracker[] tracker = service.GetAdViewReport(ConfigurationManager.AppSettings["webServiceKey"], startDate, endDate, ads[currentAd].ID, null, null);

                        //if the ad is over the limit
                        if (ads[currentAd].Ad_Limit <= tracker.Length)
                        {
                            //increase the seed if the ad is rotating
                            if (autoRotate)
                                group.Seed += 1;

                            //make the current ad the next ad in the group

                            //make sure we're not resetting to the same ad number...otherwise we'll never get out of this loop
                            int tempID = 0;
                            tempID = currentAd;
                            currentAd = (group.Seed + i) % ads.Length;

                            //just set the ad found to be true so we eventually get out of here
                            if (currentAd == tempID)
                                adFound = true;
                        }
                        else
                            adFound = true;
                    }
                    else
                        adFound = true;
                }



                com.dailyez.Ads_Model ad = null;

                if (ads[currentAd].Ad_ID > 0)
                    ad = service.GetAdsModel(ads[currentAd].Ad_ID);

                if (ad != null)
                {
                    htm += "<div class=\"divAd\" id=\"placementID" + ads[currentAd].ID + "\" style='width:300px; height:" + ad.Ad_Height + "px;border:" + ad.Border_Style + ";'>" + ad.Html + "</div><br/>";
                }

                //track ad view
                var adLog = new Utility.AdLog();
                adLog.DeleteOldViewLogs();
                if (!adLog.ShouldRecordView(context.Request.ServerVariables["REMOTE_ADDR"], ads[currentAd].ID)) continue;

                //pass in the placementID rather than the adID
                adLog.Log(ads[currentAd].ID, pageID, Code.Utility.GetIntFromCookie(context.Request, "clientID"), Code.Utility.GetStringFromCookie(context.Request, "zip"), Code.Utility.GetStringFromCookie(context.Request, "registered2").Equals("true"));
            }

            
            service.IncrementAdGroupSeed(ConfigurationManager.AppSettings["webServiceKey"], group.ID);
            return htm;

        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}