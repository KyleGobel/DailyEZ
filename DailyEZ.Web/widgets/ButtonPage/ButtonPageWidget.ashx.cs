using System.IO;
using System.Web;

namespace DailyEZ.Web.widgets.ButtonPage
{
    /// <summary>
    /// Summary description for ButtonPageWidget
    /// </summary>
    public class ButtonPageWidget : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/javascript";
            var pageID = context.Request["pageID"];
            var widgetID = context.Request["widgetID"];
            var favIcons = Code.Utility.TryParseBoolWithDefault(context.Request["favIcons"], false);
            var buttonColor = context.Request["buttonColor"];

            var iPageID = 0;
            var iWidgetID = 0;

            int.TryParse(pageID, out iPageID);
            int.TryParse(widgetID, out iWidgetID);

            if (iPageID == 0)
            {
                context.Response.Write("Invalid Page ID");
                return;
            }

            var reader = new StreamReader(new FileStream(System.Web.HttpContext.Current.Server.MapPath("buttonPageTemplate.js"), FileMode.Open, FileAccess.Read, FileShare.Read));
            var template = reader.ReadToEnd();
            reader.Close();
            template = template.Replace("[%FAVICONS%]", favIcons.ToString());
            template = template.Replace("[%WIDGET-ID%]", iWidgetID + "");
            template = template.Replace("[%PAGE-ID%]", iPageID + "");
            template = template.Replace("[%BUTTON-COLOR%]", buttonColor + "");

            context.Response.Write(template);
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