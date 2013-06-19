using System;
using System.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using DailyEZ.Web.Code;
using JetNettApi.Models;
using System.Linq;
using System.Linq.Expressions;

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


        protected void ButtonSubmitWebsiteClick(object sender, EventArgs e)
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
                //script tag takes 2 query string params, adGroup and autoRotate, this grabs
                //script tag from web.config and populates the values
                litAds.Text = string.Format(ConfigurationManager.AppSettings["adsScriptTag"], aga.AdGroup, "true");
            }
        }
        private void RenderLinkSection(JetNettApi.Models.Page page)
        {
            var id = page.Id;
           
            PopulateCanonicalUrl(page);
            RenderFooter(page);
            RenderHeader(page);

           
            RenderMetaSection(page);


            PageTitle(page);

            //TODO: Get rid of this shit
            var startTitle = JetNettClient;
            Page.Title = page.Title + " - " + startTitle;

            var links = Uow.Links.GetAllByPage(page).ToList();


            if (page.AutoOrdering)
                Utility.BubbleSortListOfLinks(links);

            string htm = "\n\t\t<!-- renderLinkSection() generated -->";

            
            if (links.Count() <= 20)
            {
                htm += "\n\t\t<div class='span8'>";
                foreach (Link link in links)
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
                        if (link.Url.Contains("PhotoAlbum.aspx") || link.Url.Contains("VideoFireworks.aspx") || link.Url.ToLower().Contains(".dailyez"))
                            htm += "\n\t\t\t<a style=\"" + style + "\" " + target + " href='" + HttpUtility.HtmlEncode(link.Url) + "'>" + HttpUtility.HtmlEncode(link.Title) + "</a>" + extra + "<br/>";
                        else if (link.Url.ToLower().Contains("http://") || link.Url.ToLower().Contains("https://"))
                        {
                            if (string.IsNullOrEmpty(target))
                                target = "target=\"_blank\"";
                            htm += "\n\t\t\t<a style=\"" + style + "\" " + target + " rel=\"nofollow\" href=\"" + HttpUtility.HtmlEncode(link.Url) + "\">" + HttpUtility.HtmlEncode(link.Title) + "</a>" + extra + "<br/>";
                        }
                        else
                            htm += "\n\t\t\t<a style=\"" + style + "\" " + target + " href=\"" + link.Url + "-" + HttpUtility.UrlEncode(link.Title.Replace(" ", "-")) + "\">" + link.Title + "</a>" + extra + "<br/>";

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
                var colLength = links.Count / 2;

                if (((links.Count) % 2) == 1)
                    colLength++;
                int counter = 0;
                htm += "\n\t\t<div class='span4'>";
                foreach (var link in links)
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
                        if (link.Url.Contains("PhotoAlbum.aspx") || link.Url.Contains("VideoFireworks.aspx") || link.Url.ToLower().Contains(".dailyez"))
                            htm += "\n\t\t\t<a style=\"" + style + "\" " + target + " href='" + HttpUtility.HtmlEncode(link.Url) + "'>" + HttpUtility.HtmlEncode(link.Title) + "</a>" + extra + "<br/>";
                        else if (link.Url.ToLower().Contains("http://") || link.Url.ToLower().Contains("https://"))
                        {
                            if (string.IsNullOrEmpty(target))
                                target = "target=\"_blank\"";

                            htm += "\n\t\t\t<a style=\"" + style + "\" rel=\"nofollow\" " + target + " href=\"" + HttpUtility.HtmlEncode(link.Url) + "\">" + HttpUtility.HtmlEncode(link.Title) + "</a>" + extra + "<br/>";
                        }
                        else
                            htm += "\n\t\t\t<a style=\"" + style + "\" " + target + " href=\"" + link.Url + "-" + 
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
                        if (link.Url.Contains("PhotoAlbum.aspx"))
                            htm += "\n\t\t\t<a style=\"" + style + "\" href='" + HttpUtility.HtmlEncode(link.Url) + "'>" + HttpUtility.HtmlEncode(link.Title) + "</a>" + extra + "<br/>";
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

        private void PageTitle(Page page)
        {
            if (string.IsNullOrEmpty(page.Title))
            {
                page.Title = "";
                litPageHeader.Text = "Page Title not found - PageID = " + page.Id;
            }
            else
            {
                litPageHeader.Text = HttpUtility.HtmlEncode(page.Title);
            }
        }

        private void RenderMetaSection(Page page)
        {
            if (string.IsNullOrEmpty(page.MetaKeys))
                page.MetaKeys = page.Title;
            if (string.IsNullOrEmpty(page.MetaDesc))
                page.MetaDesc = page.Title;
            litMeta.Text = "<meta name=\"keywords\" content=\"" + page.MetaKeys + "\"/><meta name=\"description\" content=\"" +
                           page.MetaDesc + "\"/>";
        }

        private void RenderHeader(Page page)
        {
            if (!string.IsNullOrEmpty(page.HeaderHtml))
            {
                litExtraHtml.Text = "<header>" + HttpUtility.HtmlEncode(page.HeaderHtml) + "</header>";
            }
        }

        private void RenderFooter(Page page)
        {
            if (!string.IsNullOrEmpty(page.FooterHtml))
            {
                litFooterHtml.Text = "<footer>" + HttpUtility.HtmlEncode(page.FooterHtml) + "</footer>";
            }
        }

        private void PopulateCanonicalUrl(Page page)
        {
            if (!string.IsNullOrEmpty(page.CanonicalUrl))
            {
                litCanonicalLink.Text = "<link rel=\"canonical\" href=\"" + HttpUtility.HtmlEncode(page.CanonicalUrl) + "\"/>";
            }
        }
    }
}