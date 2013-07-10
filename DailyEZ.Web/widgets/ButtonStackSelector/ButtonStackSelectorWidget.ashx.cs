using System;
using System.IO;
using System.Web;

namespace DailyEZ.Web.widgets.ButtonStackSelector
{
    /// <summary>
    /// Summary description for ButtonStackSelectorWidget
    /// </summary>
    public class ButtonStackSelectorWidget : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "application/javascript";
                var stacks = context.Request["stacks"];
                var title = context.Request["title"];
                var sWidgetID = context.Request["widgetID"];
                var buttonColor = context.Request["buttonColor"];

                var iWidgetID = 0;

                int.TryParse(sWidgetID, out iWidgetID);



                const string templateFile = "buttonStackSelectorTemplate.js";
                var template = "";
                using (var fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(templateFile), FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var reader = new StreamReader(fs))
                    {
                        template = reader.ReadToEnd();
                    }
                }

                template = template.Replace("[%WIDGET-ID%]", iWidgetID + "");
                template = template.Replace("[%TITLE%]", title);
                template = template.Replace("[%STACKS%]", stacks + "");
                template = template.Replace("[%BUTTON-COLOR%]", buttonColor + "");

                context.Response.Write(template);
            }
            catch (Exception x)
            {
                context.Response.Write(x);
   
            }
           
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