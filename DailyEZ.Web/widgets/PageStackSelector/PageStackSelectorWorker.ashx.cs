using System.IO;
using System.Web;

namespace DailyEZ.Web.widgets.PageStackSelector
{
    /// <summary>
    /// Summary description for PageStackSelectorWorker
    /// </summary>
    public class PageStackSelectorWorker : IHttpHandler
    {
        string _buttonClass = "";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            var url = (string)context.Request["url"];
            var title = context.Request["title"];
            var sWidgetID = context.Request["widgetID"];
            _buttonClass = context.Request["buttonColor"];


            var widgetID = 1;

            int.TryParse(sWidgetID, out widgetID);

            if (widgetID == 0)
                widgetID = 1;

            if (string.IsNullOrEmpty(url))
                return;

            var service = new com.dailyez.Service();




            var reader = new StreamReader(new FileStream(System.Web.HttpContext.Current.Server.MapPath("stackSelectorTemplate.html"), FileMode.Open, FileAccess.Read, FileShare.Read));
            var template = reader.ReadToEnd();
            reader.Close();


            template = template.Replace("[%URL%]", url);
            template = template.Replace("[%TITLE%]", title);
            template = template.Replace("[%WIDGET-ID%]", widgetID + "");
            template = template.Replace("[%BUTTON-CLASS%]", _buttonClass);

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