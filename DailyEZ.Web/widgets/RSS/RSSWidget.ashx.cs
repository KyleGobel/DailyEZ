using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DailyEZWebApplication.widgets.RSS
{
    /// <summary>
    /// Summary description for RSSWidget
    /// </summary>
    public class RSSWidget : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/javascript";

            var feedUrl = context.Request["feedUrl"];
            var wellColor = context.Request["wellColor"];
            var rssID = context.Request["rssID"];
            var title = context.Request["feedTitle"];

            var reader = new StreamReader(new FileStream(System.Web.HttpContext.Current.Server.MapPath("RSSTemplate.js"), FileMode.Open));
            var template = reader.ReadToEnd();
            reader.Close();

            template = template.Replace("[%WELL-COLOR%]", wellColor);
            template = template.Replace("[%TITLE%]", title);
            template = template.Replace("[%FEED-URL%]", feedUrl);
            template = template.Replace("[%RSS-ID%]", rssID);
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