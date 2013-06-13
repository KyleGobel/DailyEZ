using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DailyEZWebApplication.widgets.PageStackSelector
{
    /// <summary>
    /// Summary description for PageStackSelectorWidget
    /// </summary>
    public class PageStackSelectorWidget : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var url = context.Request["url"];
            var title = context.Request["title"];
            var sWidgetID = context.Request["widgetID"];
            var buttonColor = context.Request["buttonColor"];

            context.Response.ContentType = "application/javascript";

            var reader = new StreamReader(new FileStream(System.Web.HttpContext.Current.Server.MapPath("stackSelectorTemplate.js"), FileMode.Open, FileAccess.Read, FileShare.Read));
            var template = reader.ReadToEnd();
            reader.Close();

            template = template.Replace("[%URL%]", url);
            template = template.Replace("[%WIDGET-ID%]", sWidgetID);
            template = template.Replace("[%TITLE%]", title);
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