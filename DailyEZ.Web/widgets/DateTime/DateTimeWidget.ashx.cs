using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DailyEZWebApplication.widgets.DateTime
{
    /// <summary>
    /// Summary description for DateTimeWidget
    /// </summary>
    public class DateTimeWidget : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/javascript";
            var calendarID = context.Request["calenderID"];

            const string templateFile = "DateTimeWidget.js";
            var template = "";
            using (var fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(templateFile), FileMode.Open))
            {
                using (var reader = new StreamReader(fs))
                {
                    template = reader.ReadToEnd();
                }
            }

            template = template.Replace("[%CALENDAR-ID%]", calendarID + "");

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