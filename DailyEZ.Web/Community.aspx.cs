using System;
using System.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using DailyEZ.Web.Code;

namespace DailyEZ.Web
{
    public partial class Community : BasePage
    {
        
        string _senderUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            var page = GetPageFromRoute();

            if (page == null)
                return;
            RenderLinkSection(page);
            RenderAds(page);

            if (!Page.IsPostBack)
            {
                _senderUrl = Request.ServerVariables["SERVER_NAME"] + "" + Request.ServerVariables["URL"];
                Utility.SaveCookie(Response, Request, "referPage", _senderUrl);
            }
        }

        private JetNettApi.Models.Page GetPageFromRoute()
        {
            var pageId = 0;

            //Error check to see if the page route is null, in which case just return
            if (Page.RouteData.Values["PageName"] == null)
                return null;

            var route = Page.RouteData.Values["PageName"].ToString();

            //if we fail getting pageId from route string, try looking up the page by route
            return !Utility.ExtractNumberFromString(route, out pageId) ? Uow.Pages.GetByRoute(route) : Uow.Pages.GetById(pageId);
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
           SendMail();
        }
        private void SendMail()
        {
            var message = new MailMessage
                              {
                                  Subject = "Recommend a Website", 
                                  SubjectEncoding = System.Text.Encoding.UTF8,
                                  IsBodyHtml =  true,
                                  Priority = MailPriority.Normal,
                                  BodyEncoding = System.Text.Encoding.UTF8,
                                  
                              };


            message.To.Add("jetnettone@gmail.com");
            message.From = new MailAddress("jetnett@dailyez.com", "JetNett Corporation",System.Text.Encoding.UTF8);


            message.Body =  "Website Name: " + tbName.Text + 
                            "<br/>Website URL: " + tbURL.Text + 
                            "<br/><Br/>was recommeneded from " + _senderUrl;

            Utility.SendEmail(message);


        }
        private void RenderAds(JetNettApi.Models.Page page)
        {
            var aga = Uow.AdGroupAssignments.GetAdGroup(page, Uow.Clients.GetById(DailyEZObject.Client_ID));

            if (aga != null && !aga.UseBrokerCode)
            {
                litAds.Text = string.Format(@"<script>require(['widgets/AdGroup/AdGroupWidget.ashx?widgetID=1&adGroup={0}&autoRotate={1}']);</script><div id='dailyEZ-com-ad-group1'></div>",
                    aga.AdGroup, "true");
            }
        }
        private void RenderLinkSection(JetNettApi.Models.Page page)
        {
            var id = page.Id;
           
            if (!string.IsNullOrEmpty(page.CanonicalUrl))
            {
                litCanonicalLink.Text = "<link rel=\"canonical\" href=\"" + HttpUtility.HtmlEncode(page.CanonicalUrl) + "\"/>";
            }
            if (!string.IsNullOrEmpty(page.FooterHtml))
            {
                litFooterHtml.Text = "<br/>" + HttpUtility.HtmlEncode(page.FooterHtml) + "<br/><br/>";
            }
            if (!string.IsNullOrEmpty(page.HeaderHtml))
            {
                litExtraHtml.Text = HttpUtility.HtmlEncode(page.HeaderHtml) + "<br/><br/>";
            }

           
                if (string.IsNullOrEmpty(page.MetaKeys))
                    page.MetaKeys = page.Title;
                if (string.IsNullOrEmpty(page.MetaDesc))
                    page.MetaDesc = page.Title;
                litMeta.Text = "<meta name=\"keywords\" content=\"" + page.MetaKeys + "\"/><meta name=\"description\" content=\"" + page.MetaDesc + "\"/>";
         

            if (string.IsNullOrEmpty(page.Title))
            {
                page.Title = "";
                litPageHeader.Text = "Page Title not found - PageID = " + page.Id;
            }
            else
            {
                litPageHeader.Text = HttpUtility.HtmlEncode(page.Title);
            }

            //TODO: Get rid of this shit
            string startTitle =
                BasePage.WebService.GetClient(ConfigurationManager.AppSettings["webServiceKey"], DailyEZObject.Client_ID).
                    Website;
            Page.Title = page.Title + " - " + startTitle;

            
            com.dailyez.Link[] links = BasePage.WebService.GetLinksFromPage(ConfigurationManager.AppSettings["webServiceKey"], id);


            if (page.AutoOrdering)
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