using System.Collections.Generic;
using System.Configuration;
using System.Web;
using JetNettApi.Data;
using JetNettApi.Data.Contracts;
using JetNettApi.Models;
using Ninject;
using System.Linq;
namespace DailyEZ.Web.Code.WidgetControllers
{
    public class AdGroupWidgetController
    {
        [Inject]
        public IJetNettApiUnitOfWork Uow { get; set; }
        public Ad SomeMethod()
        {
            var group =  Uow.AdGroups.GetById(14);
            return Uow.Ads.GetById(7);

        }
        public string GetAdGroupHtml(int adGroup, bool autoRotate, HttpContext context)
        {
            int pageID = 0;

            JetNettApi.Models.AdGroupAssignment adGroupAssignment = null;

            JetNettApi.Models.AdGroup group = Uow.AdGroups.GetById(adGroup);

            var ads = Uow.AdAssignments.GetAll().Where(a => a.AdGroup == group.Id).ToList();


            var htm = "";
            if (ads.Count == 0)
                return "No Ads in this Group";

            if (!autoRotate)
                group.Seed = 0;
            for (var i = 0; i < group.ViewportSize; i++)
            {

                var adFound = false;
                var currentAd = 0;

                //current display ad + 
                currentAd = (group.Seed + i) % ads.Count;
                while (!adFound)
                {
                    if (ads[currentAd].AdMode == 2)
                    {
                        if (autoRotate)
                            group.Seed += 1;
                        currentAd = (group.Seed + i) % ads.Count;
                    }
                    else if (ads[currentAd].AdMode == 1)
                    {
                        var startDate = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
                        var endDate = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month));
                        //com.dailyez.Ad_View_Tracker[] tracker = service.GetAdViewReport(ConfigurationManager.AppSettings["webServiceKey"], startDate, endDate, ads[currentAd].ID, null, null);
                        //TODO:: add tracker to api, change the 500 below to tracker.count
                        //if the ad is over the limit
                        if (ads[currentAd].AdLimit <= 500)
                        {
                            //increase the seed if the ad is rotating
                            if (autoRotate)
                                group.Seed += 1;

                            //make the current ad the next ad in the group

                            //make sure we're not resetting to the same ad number...otherwise we'll never get out of this loop
                            int tempID = 0;
                            tempID = currentAd;
                            currentAd = (group.Seed + i) % ads.Count;

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



                JetNettApi.Models.Ad ad = null;

                if (ads[currentAd].AdId > 0)
                {
                    var adId = ads[currentAd].AdId;
                    if (adId != null) ad = Uow.Ads.GetById(adId.Value);
                }

                if (ad != null)
                {
                    htm += "<div class=\"divAd\" id=\"placementID" + ads[currentAd].Id + "\" style='width:300px; height:" + ad.AdHeight + "px;border:" + ad.BorderStyle + ";'>" + ad.Html + "</div><br/>";
                }

                //track ad view
                //TODO::Convert this shit
                //var adLog = new Utility.AdLog();
                //adLog.DeleteOldViewLogs();
                //if (!adLog.ShouldRecordView(context.Request.ServerVariables["REMOTE_ADDR"], ads[currentAd].ID)) continue;

                //pass in the placementID rather than the adID
                //adLog.Log(ads[currentAd].ID, pageID, Code.Utility.GetIntFromCookie(context.Request, "clientID"), Code.Utility.GetStringFromCookie(context.Request, "zip"), Code.Utility.GetStringFromCookie(context.Request, "registered2").Equals("true"));
            }


          //  service.IncrementAdGroupSeed(ConfigurationManager.AppSettings["webServiceKey"], group.ID);
   
            return htm;

        }

    }

    public interface ITestDependency
    {
        bool SomeTestMethod();
    }
    public class ConcreteClass : ITestDependency
    {
        public bool SomeTestMethod()
        {
            return false;
        }
    }
}