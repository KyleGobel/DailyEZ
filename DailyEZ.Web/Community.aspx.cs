using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using DailyEZ.Web.Code;

namespace DailyEZ.Web
{
    public partial class Community : BasePage
    {
        int _pageID = 0;
        string _senderURL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //write the page id on top of the page in the comments
            //Response.Write("<!--" +DailyEZUtility.GetIntFromQueryString(Request, "id") + "-->" );
            if (Page.RouteData.Values["PageName"] == null)
                return;

            var resultString = Regex.Match(Page.RouteData.Values["PageName"].ToString(), @"\d+").Value;
            int.TryParse(resultString, out _pageID);

            if (_pageID == 0)
                _pageID = Utility.GetPageIDByRoute(Page.RouteData.Values["PageName"].ToString());

            RenderLinkSection();
            RenderAds();

            if (!Page.IsPostBack)
            {
                _senderURL = Request.ServerVariables["SERVER_NAME"] + "" + Request.ServerVariables["URL"];
                Utility.SaveCookie(Response, Request, "referPage", _senderURL);
            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
                SendMail();
        }
        private void SendMail()
        {

            var message = new MailMessage();

            message.To.Add("jetnettone@gmail.com");

            message.From = new MailAddress("jetnett@dailyez.com", "JetNett Corporation",
                                           System.Text.Encoding.UTF8);


            message.Subject = "Recommend a Website";



            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.Body = "Website Name: " + tbName.Text + "<br/>Website URL: " + tbURL.Text + "<br/><Br/>was recommeneded from " + Utility.GetStringFromCookie(Request, "referPage");

            message.BodyEncoding = System.Text.Encoding.UTF8;

            message.IsBodyHtml = true;

            message.Priority = MailPriority.Normal;

            var emailClient = new SmtpClient
                {
                    Credentials = new NetworkCredential("jetnettone@gmail.com", "ep69k55ax"),
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true
                };

            emailClient.Send(message);

        }
        private void RenderAds()
        {


            com.dailyez.Ad_Page_Relationship rel = null;

            //try most specific --clientID AND pageID
            rel = WebService.GetAdPageRelationshipByClientIDAndPageID(ConfigurationManager.AppSettings["webServiceKey"],
                                                                   DailyEZObject.Client_ID, _pageID);

            //broaden - just the page
            if (rel == null)
                rel = WebService.GetAdPageRelationshipByPageIDOnly(ConfigurationManager.AppSettings["webServiceKey"],
                                                                _pageID);

            //broaden - clientID
            if (rel == null)
                rel = WebService.GetAdPageRelationshipByClientIDOnly(ConfigurationManager.AppSettings["webServiceKey"],
                                                                  DailyEZObject.Client_ID);


            //nothing found, just return a false
            if (rel == null)
                return;


            //if the relationship found has an ad group, and we're not using the broker code
            if (rel.Ad_Group > 0 && !rel.Use_Broker_Code)
            {

                //store ad group in our local variable
                var adGroup = rel.Ad_Group;
                litAds.Text = string.Format(@"<script type=""text/javascript"" src=""widgets/AdGroup/AdGroupWidget.ashx?widgetID=1&adGroup={0}&autoRotate={1}""></script><div id=""dailyEZ-com-ad-group1""></div>",
                    adGroup, "true");
            }
        }
        private void RenderLinkSection()
        {
            var id = _pageID;
            if (id == 0)
                return;

            com.dailyez.Page page = BasePage.WebService.GetPage(ConfigurationManager.AppSettings["webServiceKey"], id);
            if (page == null)
            {
                Response.Write("Page not found.  Page ID = " + id);
                return;
            }
            if (!string.IsNullOrEmpty(page.Canonical_URL))
            {
                litCanonicalLink.Text = "<link rel=\"canonical\" href=\"" + HttpUtility.HtmlEncode(page.Canonical_URL) + "\"/>";
            }
            if (!string.IsNullOrEmpty(page.Footer_HTML))
            {
                litFooterHtml.Text = "<br/>" + HttpUtility.HtmlEncode(page.Footer_HTML) + "<br/><br/>";
            }
            if (!string.IsNullOrEmpty(page.Extra_HTML))
            {
                litExtraHtml.Text = HttpUtility.HtmlEncode(page.Extra_HTML) + "<br/><br/>";
            }

            try
            {
                if (page.MetaKeys.Length == 0)
                    page.MetaKeys = page.Title;
                if (page.MetaDesc.Length == 0)
                    page.MetaDesc = page.Title;
                litMeta.Text = "<meta name=\"keywords\" content=\"" + page.MetaKeys + "\"/><meta name=\"description\" content=\"" + page.MetaDesc + "\"/>";
            }
            catch
            {
                litMeta.Text = "<!--Invalid Meta Keys/Desc entered-->";
            }

            if (string.IsNullOrEmpty(page.Title))
            {
                page.Title = "";
                Response.Write("Page Title not found - PageID = " + id);
            }

            string startTitle =
                BasePage.WebService.GetClient(ConfigurationManager.AppSettings["webServiceKey"], DailyEZObject.Client_ID).
                    Website;

            Page.Title = page.Title + " - " + startTitle;
            litPageHeader.Text = HttpUtility.HtmlEncode(page.Title);
            com.dailyez.Link[] links = BasePage.WebService.GetLinksFromPage(ConfigurationManager.AppSettings["webServiceKey"], id);


            if (page.Auto_Ordering)
                Utility.BubbleSortList(links);

            string htm = "\n\t\t<!-- renderLinkSection() generated -->";


            if (links.Length <= 20)
            {
                htm += "\n\t\t<div class='span8'>";
                foreach (com.dailyez.Link link in links)
                {
                    var target = "";
                    if (!string.IsNullOrEmpty(link.Target))
                        target = "target=\"" + link.Target + "\"";

                    var style = "";
                    var extra = "";

                    if (link.Title.ToLower().Contains("[content]"))
                        style += "font-weight:normal;";
                    if (link.Title.ToLower().Contains("[bold]") || link.Title.ToLower().Contains("*bold*"))
                        style += "font-weight:bold;";
                    if (link.Title.ToLower().Contains("[break]"))
                        extra += "<br/>";

                    link.Title =
                        link.Title.Replace("*BOLD*", "").Replace("[CONTENT]", "").Replace("[BOLD]", "").Replace(
                            "*bold*", "")
                            .Replace("[content]", "").Replace("[bold]", "").Replace("[BREAK]", "").Replace("[break]", "");



                    if (link.IsLink)
                    {
                        if (link.URL.Contains("PhotoAlbum.aspx") || link.URL.Contains("VideoFireworks.aspx") || link.URL.ToLower().Contains(".dailyez"))
                            htm += "\n\t\t\t<a style=\"" + style + "\" " + target + " href='" + HttpUtility.HtmlEncode(link.URL) + "'>" + HttpUtility.HtmlEncode(link.Title) + "</a>" + extra + "<br/>";
                        else if (link.URL.ToLower().Contains("http://") || link.URL.ToLower().Contains("https://"))
                        {
                            if (string.IsNullOrEmpty(target))
                                target = "target=\"_blank\"";
                            htm += "\n\t\t\t<a style=\"" + style + "\" " + target + " rel=\"nofollow\" href=\"" + HttpUtility.HtmlEncode(link.URL) + "\">" + HttpUtility.HtmlEncode(link.Title) + "</a>" + extra + "<br/>";
                        }
                        else
                            htm += "\n\t\t\t<a style=\"" + style + "\" " + target + " href=\"" + link.URL + "-" + HttpUtility.UrlEncode(link.Title.Replace(" ", "-")) + "\">" + link.Title + "</a>" + extra + "<br/>";

                    }
                    else
                    {
                        var title = "";
                        if (link.Title.Contains("class=\"suspendedLink\">"))
                            title = link.Title;
                        else
                            title = HttpUtility.HtmlEncode(link.Title) + extra + "<br/>";

                        htm += "<span class=\"header\"><h2 style=\"" + style + "font-size:16px; margin:0;\">" + title + "</h2></span>" + extra;
                    }
                }

                htm += "\n\t\t</div>";
            }
            else
            {
                int colLength = links.Length / 2;

                if (((links.Length) % 2) == 1)
                    colLength++;
                int counter = 0;
                htm += "\n\t\t<div class='span4'>";
                foreach (com.dailyez.Link link in links)
                {
                    var target = "";
                    if (!string.IsNullOrEmpty(link.Target))
                        target = "target=\"" + link.Target + "\"";


                    var style = "";

                    var extra = "";

                    if (link.Title.ToLower().Contains("[content]"))
                        style += "font-weight:normal;";
                    if (link.Title.ToLower().Contains("[bold]") || link.Title.ToLower().Contains("*bold*"))
                        style += "font-weight:bold;";
                    if (link.Title.ToLower().Contains("[break]"))
                        extra += "<br/>";

                    link.Title =
                        link.Title.Replace("*BOLD*", "").Replace("[CONTENT]", "").Replace("[BOLD]", "").Replace("*bold*", "")
                            .Replace("[content]", "").Replace("[bold]", "").Replace("[BREAK]", "").Replace("[break]", "");

                    if (link.IsLink)
                    {
                        if (link.URL.Contains("PhotoAlbum.aspx") || link.URL.Contains("VideoFireworks.aspx") || link.URL.ToLower().Contains(".dailyez"))
                            htm += "\n\t\t\t<a style=\"" + style + "\" " + target + " href='" + HttpUtility.HtmlEncode(link.URL) + "'>" + HttpUtility.HtmlEncode(link.Title) + "</a>" + extra + "<br/>";
                        else if (link.URL.ToLower().Contains("http://") || link.URL.ToLower().Contains("https://"))
                        {
                            if (string.IsNullOrEmpty(target))
                                target = "target=\"_blank\"";

                            htm += "\n\t\t\t<a style=\"" + style + "\" rel=\"nofollow\" " + target + " href=\"" + HttpUtility.HtmlEncode(link.URL) + "\">" + HttpUtility.HtmlEncode(link.Title) + "</a>" + extra + "<br/>";
                        }
                        else
                            htm += "\n\t\t\t<a style=\"" + style + "\" " + target + " href=\"" + link.URL + "-" + 
                                HttpUtility.UrlEncode(
                                    link.Title.Replace(" ", "-")
                                    .Replace("0", "")
                                    .Replace("1", "")
                                    .Replace("2", "")
                                    .Replace("3", "")
                                    .Replace("4", "")
                                    .Replace("5", "")
                                    .Replace("6", "")
                                    .Replace("7", "")
                                    .Replace("8", "")
                                    .Replace("9", "")
                                    .Replace("+", "")
                                    .Replace("&","")
                                    ) + 
                                    "\">" + link.Title + "</a>" + extra + "<br/>";

                    }
                    else
                    {
                        string title = "";
                        if (link.URL.Contains("PhotoAlbum.aspx"))
                            htm += "\n\t\t\t<a style=\"" + style + "\" href='" + HttpUtility.HtmlEncode(link.URL) + "'>" + HttpUtility.HtmlEncode(link.Title) + "</a>" + extra + "<br/>";
                        else if (link.Title.Contains("class=\"suspendedLink\">"))
                            title = link.Title;
                        else
                            title = HttpUtility.HtmlEncode(link.Title) + "<br/>";

                        htm += "<span class=\"header\"><h2 style=\"" + style + "font-size:16px; margin:0;\">" + title + "</h2></span>" + extra;
                    }

                    counter++;

                    if (counter == colLength)
                        htm += "\n\t\t</div>\n\t\t<div class='span4'>";
                }
                htm += "\n\t\t</div>";
            }



            htm += "\n\t\t<!-- end -->";
            litLinkContent.Text = htm;
        }
    }
}