using System.IO;
using System.Web;

namespace DailyEZ.Web.widgets.TextBlock
{
    /// <summary>
    /// Summary description for TextBlockWidget
    /// </summary>
    public class TextBlockWidget : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/javascript";
            var sWidgetID = context.Request["widgetID"];
            var fontSize = context.Request["fontSize"];
            var fontColor = context.Request["fontColor"];
            var text = context.Request["text"];
            var fontWeight = context.Request["fontWeight"];

            int iWidgetID;

            int.TryParse(sWidgetID, out iWidgetID);



            const string templateFile = "TextBlockWidgetTemplate.js";
            string template;
            using (var fs = new FileStream(HttpContext.Current.Server.MapPath(templateFile), FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    template = reader.ReadToEnd();
                }
            }

            template = template.Replace("[%WIDGET-ID%]", iWidgetID + "");
            template = template.Replace("[%FONT-COLOR%]", fontColor);
            template = template.Replace("[%FONT-SIZE%]", fontSize + "");
            template = template.Replace("[%TEXT%]", text + "");
            template = template.Replace("[%FONT-WEIGHT%]", fontWeight);

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