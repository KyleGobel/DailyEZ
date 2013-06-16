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
            var client = BasePage.WebService.GetClient(ConfigurationManager.AppSettings["webServiceKey"], BasePage.DailyEZObject.Client_ID);

            if (client == null)
            {
                var ex = new ArgumentNullException("com.DailyEZ.Client", "Client Object is Null");
                ErrorLog.GetDefault(HttpContext.Current).Log(new Error(ex));

                return;
            }
            
                
            Page.Title = client.Website;
            AnalyticsKey = client.Analytics_Key;


            //weird need to change name on database table sometime...very hacky
            Page.MetaDescription = BasePage.DailyEZObject.Breaking_News_RSS_Feed;
            
            
        }
        public string LocalBusinessUrl
        {
            get
            {
                if (BasePage.DailyEZObject == null)
                    return "http://";

                return BasePage.DailyEZObject.Breaking_News_Title;
            }
        }
        public string DailyEZTitle
        {
            get
            {
                if (BasePage.DailyEZObject == null)
                    return "Sample DailyEZ";

                return BasePage.DailyEZObject.Main_Title;
            }
        }
        public string DailyEZTitleColored
        {
            get
            {
                if (BasePage.DailyEZObject == null)
                    return "Sample DailyEZ";
                
                return string.Format("<span style=\"color:{0}\">{1}</span>", BasePage.DailyEZObject.Main_Title_Font_Color, BasePage.DailyEZObject.Main_Title);
            }
        }
        public string TopLeftImage
        {
            get
            {
                if (BasePage.DailyEZObject == null)
                    return string.Format("<img alt='placeholder' src=\"{0}\"/>", "http://placehold.it/220x35");

                if (!Utility.UrlExists(BasePage.DailyEZObject.Top_Left_Image_URL))
                {
                    var ex = new Exception("Image URL returned 404: " + BasePage.DailyEZObject.Top_Left_Image_URL);
                    ErrorLog.GetDefault(HttpContext.Current).Log(new Error(ex));
                    return string.Format("<img alt='placeholder' src=\"{0}\"/>", "http://placehold.it/220x35&text=Image Not Found");

                }
                return string.Format("<a href=\"{2}\" target=\"_blank\"><img alt=\"{0}\" src=\"{1}\"/></a>",
                    HttpUtility.HtmlEncode(BasePage.DailyEZObject.Top_Left_Image_Alt),
                    HttpUtility.HtmlEncode(BasePage.DailyEZObject.Top_Left_Image_URL),
                    HttpUtility.HtmlEncode(BasePage.DailyEZObject.Top_Left_Image_Href)
                    );

            }
        }
        public string TopRightImage
        {
            get
            {
                if (BasePage.DailyEZObject == null)
                    return string.Format("<img alt='placeholder' src=\"{0}\"/>", "http://placehold.it/220x35");
                if (!Utility.UrlExists(BasePage.DailyEZObject.Top_Right_Image_URL))
                {
                    var ex = new Exception("Image URL returned 404: " + BasePage.DailyEZObject.Top_Right_Image_URL);
                    ErrorLog.GetDefault(HttpContext.Current).Log(new Error(ex));
                    return string.Format("<img alt='placeholder' src=\"{0}\"/>", "http://placehold.it/220x35&text=Image Not Found");
                }
                return string.Format("<a href=\"{2}\" target=\"_blank\"><img alt=\"{0}\" src=\"{1}\"/></a>",
                 HttpUtility.HtmlEncode(BasePage.DailyEZObject.Top_Right_Image_Alt),
                 HttpUtility.HtmlEncode(BasePage.DailyEZObject.Top_Right_Image_URL),
                 HttpUtility.HtmlEncode(BasePage.DailyEZObject.Top_Right_Image_Href)
                 );
            }
        }

    }
}