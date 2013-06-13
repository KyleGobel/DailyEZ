using System;
using System.Configuration;
using System.IO;
using System.Web;
using DailyEZ.Web.com.dailyez;

namespace DailyEZ.Web.widgets.ButtonPage
{
    /// <summary>
    /// Summary description for GetLinkWidgetWorker
    /// </summary>
    public class GetButtonWidgetWorker : IHttpHandler
    {
        bool _favIcons = false;
        string _buttonClass = "";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            var pageID = context.Request["pageID"];
            _favIcons = Code.Utility.TryParseBoolWithDefault(context.Request["favIcons"], false);
            _buttonClass = context.Request["buttonColor"];
            var reader = new StreamReader(new FileStream(System.Web.HttpContext.Current.Server.MapPath("buttonPageTemplate.html"), FileMode.Open,FileAccess.Read, FileShare.Read));
            var template = reader.ReadToEnd();
            reader.Close();

            string leftLinks, rightLinks, pageTitle, header, footer;

            GetLinks(int.Parse(pageID), false, out leftLinks, out rightLinks, out pageTitle, out header, out footer);


            template = template.
                Replace("[%TITLE%]", pageTitle).
                Replace("[%HEADER%]", header).
                Replace("[%FOOTER%]", footer).
                Replace("[%LEFT-LINKS%]", leftLinks).
                Replace("[%RIGHT-LINKS%]", rightLinks);


            context.Response.Write(template);
     
            
        }
        private void GetLinks(int pageID, bool oneColumn, out string leftSideLinks, out string rightSideLinks, out string pageTitle, out string header, out string footer)
        {

            var id = pageID;

            //if (maxColWidth > 0)
            //{
            //    leftColWidth.Text = "width:" + maxColWidth + "px; overflow:hidden;white-space:nowrap;";
            //    rightColWidth.Text = "width:" + maxColWidth + "px; overflow:hidden;white-space:nowrap;";
            //}
            leftSideLinks = "";
            rightSideLinks = "";
            pageTitle = "";
            header = "";
            footer = "";

            if (id == 0)
                return;

            var service = new com.dailyez.Service();
            var page = service.GetPage(ConfigurationManager.AppSettings["webServiceKey"], id);

            pageTitle = page.Title;
            header = page.Extra_HTML;
            footer = page.Footer_HTML;


            var links = service.GetLinksFromPage(ConfigurationManager.AppSettings["webServiceKey"], id);


            if (page.Auto_Ordering)
                Code.Utility.BubbleSortList(links);


            var leftLinks = "";
            var rightLinks = "";
            var tmp = "";

            var style = "";
            if (oneColumn)
            {
                foreach (var link in links)
                {
                    if (link.IsLink)
                    {
                        tmp += CreateATag(link);
                        tmp += "<br/>";
                    }
                    else
                    {
                        tmp += string.Format("<span style=\"{0}\" class=\"header\"><h2 style='font-size:16px; margin:0;'>{1}</h2></span>", GetStyle(link.Title), FormatTitle(link.Title));
                        tmp += "<br/>";
                    }
                }
                leftLinks = tmp;
            }
            else
            {
                var colLength = links.Length / 2;

                if (((links.Length) % 2) == 1)
                    colLength++;
                var counter = 0;
                foreach (var link in links)
                {
                    if (link.IsLink)
                    {
                        tmp += CreateATag(link);
                        tmp += "<br/>";
                    }
                    else
                    {
                        tmp += string.Format("<span style=\"{0}\" class=\"header\"><h2 style='font-size:14px; line-height:20px; margin:0;'>{1}</h2></span>", GetStyle(link.Title), FormatTitle(link.Title));
                    }

                    counter++;

                    if (counter != colLength) continue;
                    leftLinks = tmp;
                    tmp = "";
                }
                rightLinks = tmp;
            }
            leftSideLinks = leftLinks;
            rightSideLinks = rightLinks;

        }
        public string GetFavIconHtml(string url, bool getFavIcon)
        {
            if (!getFavIcon)
                return "";
            if (url.Contains("plus.google.com"))
                return "<img src=\"widgets/Utility/g-plus-icon-16x16.png\" alt='favicon' style='margin:5px; margin-left:-7px; width:16px; height:16px;' />";
            return "<img src=\"http://g.etfv.co/" + HttpUtility.UrlEncode(url) + "\" alt='favIcon' style='margin:5px; margin-left:-7px; width:16px; height:16px;' width='16' height='16'/>";
        }
        public string CreateATag(Link link)
        {
            var aTag = "";
            if (link.URL.Contains("PhotoAlbum.aspx") || link.URL.Contains("VideoFireworks.aspx") || link.URL.ToLower().Contains(".dailyez"))
                aTag = string.Format(
                    "<a style=\"{0}\" href=\"{1}\" {4} onclick=\"{2}\"><button class=\"btn {6}\">{5}{3}</button></a>",
                    GetStyle(link.Title),
                    HttpUtility.HtmlEncode(link.URL),
                    GetJSOpenNewWindow(link.URL),
                    FormatTitle(link.Title),
                    GetTitleBlock(link.Title),
                    GetFavIconHtml(link.URL, _favIcons),
                    _buttonClass);
            else if (link.URL.ToLower().Contains("http://") || link.URL.ToLower().Contains("https://"))
                aTag = string.Format(
                    "<a style=\"{0}\" href=\"{1}\" {4} target=\"{2}\"><button class=\"btn {6}\">{5}{3}</button></a>",
                    GetStyle(link.Title),
                    HttpUtility.HtmlEncode(link.URL),
                    GetTarget(link.Target),
                    FormatTitle(link.Title),
                    GetTitleBlock(link.Title),
                    GetFavIconHtml(link.URL, _favIcons),
                    _buttonClass);
            else
            {
                aTag = string.Format(
                  "<a style=\"{0}\" href=\"{1}\" {4} onclick=\"{2}\"><button class=\"btn {6}\">{5}{3}</button></a>",
                  GetStyle(link.Title),
                  "../" + HttpUtility.HtmlEncode(link.URL) + "-" + FormatTitle(link.Title).Replace(" ", "-").Replace(".", "").Replace("&", ""),
                  GetJSOpenSameWindow(link),
                  FormatTitle(link.Title),
                  GetTitleBlock(link.Title),
                  GetFavIconHtml(link.URL, _favIcons),
                  _buttonClass);
            }


            return aTag;
        }
        public string GetTitleBlock(string title)
        {
            return title.Length > 21 ? string.Format("title=\"{0}\"", FormatTitle(title)) : "";
        }

        public string GetJSOpenSameWindow(Link link)
        {
            return string.Format("javascript:window.open('{0}', '_top'); return false;", "../" + HttpUtility.UrlEncode(link.URL) + "-" + FormatTitle(link.Title).Replace(" ", "-").Replace(".", "").Replace("&", ""));

        }
        public string GetJSOpenNewWindow(string url)
        {
            return string.Format("javascript:window.open('{0}', '_self'); return false;", HttpUtility.UrlEncode(url));
        }
        public string GetTarget(string target)
        {
            return String.IsNullOrEmpty(target) ? "_blank" : target;
        }

        public string GetStyle(string title)
        {
            var style = "";
            if (title.ToLower().Contains("[content]"))
                style += "font-weight:normal;";
            if (title.ToLower().Contains("[bold]") || title.ToLower().Contains("*bold*"))
                style += "font-weight:bold;";
            return style;
        }
        public string FormatTitle(string title)
        {

            
            return HttpUtility.HtmlEncode(title.
                Replace("*BOLD*", "").
                Replace("[CONTENT]", "").
                Replace("[BOLD]", "").
                Replace("*bold*", "").
                Replace("[content]", "").
                Replace("[bold]", "")
                );
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