using System;
using System.Configuration;
using System.Web;
using DailyEZ.Web.Code;
using Elmah;

namespace DailyEZ.Web
{
    public partial class MainLayout : System.Web.UI.MasterPage
    {
        public string AnalyticsKey = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (BasePage.JetNettClient == null)
            {
                var ex = new ArgumentNullException("BasePage.JetNettClient", "Client Object is Null");
                ErrorLog.GetDefault(HttpContext.Current).Log(new Error(ex));

                return;
            }
            
            if (BasePage.JetNettClient.WebsiteTitle != null)
                Page.Title = BasePage.JetNettClient.WebsiteTitle;
            if (BasePage.JetNettClient.AnalyticsKey != null)
                AnalyticsKey = BasePage.JetNettClient.AnalyticsKey;


            if (BasePage.DailyEZObject1.SeoText != null) Page.MetaDescription = BasePage.DailyEZObject1.SeoText;
        }
        public string LocalBusinessUrl
        {
            get
            {
                if (BasePage.DailyEZObject1 == null)
                    return "http://";

                return BasePage.DailyEZObject1.LocalBusinessUrl;
            }
        }
        public string DailyEZTitle
        {
            get
            {
                if (BasePage.DailyEZObject1 == null)
                    return "Sample DailyEZ";

                return BasePage.DailyEZObject1.MainTitle;
            }
        }
        public string DailyEZTitleColored
        {
            get
            {
                if (BasePage.DailyEZObject1 == null)
                    return "Sample DailyEZ";
                
                return string.Format("<span style=\"color:{0}\">{1}</span>", BasePage.DailyEZObject1.MainTitleFontColor, BasePage.DailyEZObject1.MainTitle);
            }
        }
        public string TopLeftImage
        {
            get
            {
                if (BasePage.DailyEZObject1 == null)
                    return string.Format("<img alt='placeholder' src=\"{0}\"/>", "http://placehold.it/250x35");

                if (!Utility.UrlExists(BasePage.DailyEZObject1.TopLeftImageUrl))
                {
                    var ex = new Exception("Image URL returned 404: " + BasePage.DailyEZObject1.TopLeftImageUrl);
                    ErrorLog.GetDefault(HttpContext.Current).Log(new Error(ex));
                    return string.Format("<img alt='placeholder' src=\"{0}\"/>", "http://placehold.it/250x35&text=Image Not Found");

                }
                return string.Format("<a href=\"{2}\" target=\"_blank\"><img alt=\"{0}\" src=\"{1}\"/></a>",
                    HttpUtility.HtmlEncode(BasePage.DailyEZObject1.TopLeftImageAlt ?? ""),
                    HttpUtility.HtmlEncode(BasePage.DailyEZObject1.TopLeftImageUrl),
                    HttpUtility.HtmlEncode(BasePage.DailyEZObject1.TopLeftImageHref)
                    );

            }
        }
        public string TopRightImage
        {
            get
            {
                if (BasePage.DailyEZObject1 == null)
                    return string.Format("<img alt='placeholder' src=\"{0}\"/>", "http://placehold.it/250x35");
                if (!Utility.UrlExists(BasePage.DailyEZObject1.TopRightImageUrl))
                {
                    var ex = new Exception("Image URL returned 404: " + BasePage.DailyEZObject1.TopRightImageUrl);
                    ErrorLog.GetDefault(HttpContext.Current).Log(new Error(ex));
                    return string.Format("<img alt='placeholder' src=\"{0}\"/>", "http://placehold.it/250x35&text=Image Not Found");
                }

                return string.Format("<a href=\"{2}\" target=\"_blank\"><img alt=\"{0}\" src=\"{1}\"/></a>",
                    HttpUtility.HtmlEncode(BasePage.DailyEZObject1.TopRightImageAlt ?? ""),
                    HttpUtility.HtmlEncode(BasePage.DailyEZObject1.TopRightImageUrl),
                    HttpUtility.HtmlEncode(BasePage.DailyEZObject1.TopRightImageHref)
                 );
            }
        }

    }
}