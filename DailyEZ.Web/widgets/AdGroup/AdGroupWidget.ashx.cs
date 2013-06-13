using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DailyEZWebApplication.widgets.AdGroup
{
    /// <summary>
    /// Summary description for AdGroupWidget
    /// </summary>
    public class AdGroupWidget : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/javascript";

            var widgetID = context.Request["widgetID"];
            var adGroup = context.Request["adGroup"];
            var autoRotate = context.Request["autoRotate"];
            const string templateFile = "adGroupWidgetTemplate.js";
            var template = "";
            using (var fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(templateFile), FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    template = reader.ReadToEnd();
                }
            }

            template = template.Replace("[%WIDGET-ID%]", widgetID);
            template = template.Replace("[%AD-GROUP%]", adGroup);
            template = template.Replace("[%AUTO-ROTATE%]", autoRotate);

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