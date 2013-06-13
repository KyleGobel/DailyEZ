using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DailyEZWebApplication.widgets.LinkPage
{
    /// <summary>
    /// Summary description for LinkPageWidget
    /// </summary>
    public class LinkPageWidget : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/javascript";
            var pageID = context.Request["pageID"];
            var widgetID = context.Request["widgetID"];
            var wellColor = context.Request["wellColor"];

            var iPageID = 0;
            var iWidgetID = 0;

            int.TryParse(pageID, out iPageID);
            int.TryParse(widgetID, out iWidgetID);

            if (iPageID == 0)
            {
                context.Response.Write("Invalid Page ID");
                return;
            }
            
            
            const string templateFile = "linkPageTemplate.js";
            var template = "";
            using (var fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(templateFile), FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    template = reader.ReadToEnd();
                }
            }

            template = template.Replace("[%WIDGET-ID%]", iWidgetID + "");
            template = template.Replace("[%PAGE-ID%]", iPageID + "");
            template = template.Replace("[%WELL-COLOR%]", wellColor);

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