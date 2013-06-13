using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Xml;

namespace DailyEZ.Web.widgets.RSS
{
    /// <summary>
    /// Summary description for GetRSSWorker
    /// </summary>
    public class GetRSSWorker : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            var feedUrl = context.Request["feedUrl"];
            var index = context.Request["index"];
            var rssID = context.Request["rssID"];
            var action = context.Request["action"];
            var wellColor = context.Request["wellColor"];
            var title = context.Request["title"];

            //not sure if this is right, but we'll find out
            index = rssID;

            var feedIndex = 1;
            int.TryParse(index, out feedIndex);
            var articleLink = "";
            var articleHtml = ReadRSS(feedUrl, context, action, out articleLink, feedIndex);


            var reader = new StreamReader(new FileStream(System.Web.HttpContext.Current.Server.MapPath("RSSTemplate.html"), FileMode.Open));
            var template = reader.ReadToEnd();
            reader.Close();

            if (string.IsNullOrEmpty(wellColor))
                wellColor = "#a8cbe2";


            template = template.
                Replace("[%NEWS-TITLE%]", title).
                Replace("[%ARTICLE-LINK%]", articleLink).
                Replace("[%ARTICLE-DESC%]", articleHtml).
                Replace("[%WELL-COLOR%]", wellColor).
                Replace("[%RSS-ID%]", rssID);

            context.Response.Write(template);
      
        }
        public string ReadRSS(string rssFeedURL, HttpContext context, string action, out string articleLink, int feedIndex)
        {
            XmlNode nodeRss = null;
            XmlNode nodeChannel = null;
            articleLink = "";

            var rssReader = new XmlTextReader(rssFeedURL);
            var rssDoc = new XmlDocument();

            try
            {
                rssDoc.Load(rssReader);
            }
            catch
            {
                //Response.Write("Error reading RSS from: " + rssFeedURL + " <br/>Please try again later.");
                return "error reading rss from feed";
            }
            //Loop for the <rss> tag
            for (int i = 0; i < rssDoc.ChildNodes.Count; i++)
            {
                //if it's the <rss> tag
                if (rssDoc.ChildNodes[i].Name == "rss")
                {
                    nodeRss = rssDoc.ChildNodes[i];
                }
            }

            //Loop for the <channel> tag
            if (nodeRss == null)
                return "nodeRss is null";
            for (int i = 0; i < nodeRss.ChildNodes.Count; i++)
            {
                //if it's the <channel> tag
                if (nodeRss.ChildNodes[i].Name == "channel")
                {
                    nodeChannel = nodeRss.ChildNodes[i];
                }
            }

            //title = nodeChannel["title"].InnerText;
            //language = nodeChannel["language"].InnerText;
            //link = nodeChannel["link"].InnerText;
            //description = nodeChannel["description"].InnerText;
            int seed = Code.Utility.GetIntFromCookie(context.Request, "newsSeed" + feedIndex);
            var articles = new List<string>();
            var articleLinks = new List<string>();
            //loop for the <title>, <link>, <description> and all other tags
            if (nodeChannel == null)
                return "nodeChannel is null";
            for (int i = 0; i < nodeChannel.ChildNodes.Count; i++)
            {
                //if it is the item tag, then it has children tags
                if (nodeChannel.ChildNodes[i].Name == "item")
                {
                    XmlNode nodeItem = nodeChannel.ChildNodes[i];

                    string htm = "";
                    var linkElement = nodeItem["link"];
                    if (linkElement != null)
                    {
                        var titleElement = nodeItem["title"];
                        if (titleElement != null)
                            articleLinks.Add("<a target='_blank' style='' href='" + HttpUtility.HtmlEncode(linkElement.InnerText) + "'>" + HttpUtility.HtmlEncode(titleElement.InnerText) + "</a>");
                    }

                    try
                    {
                        var pubDateElement = nodeItem["pubDate"];
                        if (pubDateElement != null)
                        {
                            string date = pubDateElement.InnerText;

                            date = date.Remove(date.Length - 3);
                            System.DateTime dt;
                            System.DateTime.TryParse(date, out dt);

                            var descriptionElement = nodeItem["description"];
                            if (descriptionElement != null)
                                htm += "[" + dt.ToShortDateString() + " " + dt.ToShortTimeString() + "] " + descriptionElement.InnerText;
                        }
                    }
                    catch
                    {
                        var descriptionElement = nodeItem["description"];
                        if (descriptionElement != null) htm += descriptionElement.InnerText;
                    }
                    articles.Add(htm);
                }
            }
            rssReader.Close();
            //format seed
            if (string.IsNullOrEmpty(action))
                action = "";
            if (action.Equals("prev"))
                seed--;
            else if (action.Equals("next"))
                seed++;

            //format seed


            if (seed >= articles.Count)
                seed = 0;
            if (seed < 0)
                seed = articles.Count - 1;

            articleLink = articleLinks[seed];
         

            var httpCookie = context.Response.Cookies["newsSeed" + feedIndex];
            if (httpCookie != null)
                httpCookie.Value = "" + seed;
            if (articles[seed] != null)
            {
                return articles[seed];
            }
          
            return "";
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