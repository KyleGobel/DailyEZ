using System;
using System.Collections.Generic;
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
            var message = new MailMessage
            {
                Subject = "Recommend a Website",
                SubjectEncoding = System.Text.Encoding.UTF8,
                IsBodyHtml = true,
                Priority = MailPriority.Normal,
                BodyEncoding = System.Text.Encoding.UTF8,

            };


            message.To.Add("jetnettone@gmail.com");
            message.From = new MailAddress("jetnett@dailyez.com", "JetNett Corporation", System.Text.Encoding.UTF8);


            message.Body = "Website Name: " + tbName.Text +
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
                htm += GetLinksHtml(links, 0, links.Count);

                htm += "\n\t\t</div>";
            }
            else
            {
                var colLength = links.Count / 2;

                if (((links.Count) % 2) == 1)
                    colLength++;
                htm += "\n\t\t<div class='span4'>";
                htm += GetLinksHtml(links, 0, colLength);
                htm += "\n\t\t</div>\n\t\t<div class='span4'>";
                htm += GetLinksHtml(links, colLength, links.Count);
                htm += "\n\t\t</div>";
            }

            htm += "\n\t\t<!-- end -->";
            litLinkContent.Text = htm;
        }

        private string GetLinksHtml(List<Link> links, int start, int finish)
        {
            var htm = "";
            for(var i =start; i<= finish; i++)
            {
                var link = links[i];
                var linkHtml = "";
                if (link.IsLink)
                {
                    linkHtml = string.Format(@"<a style='{0}' rel='{5}' target='{1}' href='{2}'>{3}</a>{4}<br/>",
                                             LinkRenderer.GetLinkStyle(link),
                                             GetLinkTarget(link),
                                             GetLinkHref(link),
                                             GetLinkTitle(link),
                                             LinkRenderer.GetLinkExtra(link),
                                             GetLinkRel(link));
                }
                else
                {
                    linkHtml = string.Format(
                        @"<span class=""header""><h2 style=""{0} font-size:16px; margin:0"">{1}{2}</h2></span>",
                        LinkRenderer.GetLinkStyle(link),
                        GetLinkTitle(link),
                        LinkRenderer.GetLinkExtra(link)
                        );
                }
                htm += linkHtml;
            }
            return htm;
        }

        private string GetLinkRel(Link link)
        {
            return (link.Url.ToLower().Contains("http://") || link.Url.ToLower().Contains("https://"))
                       ? "nofollow"
                       : "follow";
        }

      

        private string GetLinkTitle(Link link)
        {
            //clean out our annotations or attributes
            return HttpUtility.HtmlEncode(link.Title
                    .Replace("*BOLD*", "")
                    .Replace("[CONTENT]", "")
                    .Replace("[BOLD]", "")
                    .Replace("*bold*", "")
                    .Replace("[content]", "")
                    .Replace("[bold]", "")
                    .Replace("[BREAK]", "")
                    .Replace("[break]", ""));
        }

        private string GetLinkHref(Link link)
        {
            if (link.Url.Contains("http://") || link.Url.Contains("https://"))
                return HttpUtility.HtmlEncode(link.Url);

            return HttpUtility.HtmlEncode(link.Url) 
                   + "-" 
                   + HttpUtility.HtmlEncode(GetLinkTitle(link)
                                                .Replace(" ", "-")
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
                                                .Replace("&", "")
                         );
        }

        private string GetLinkTarget(Link link)
        {
            //if link.Target exists, return that
            if (!string.IsNullOrEmpty(link.Target))
                return link.Target;

            //if this is an external link (given by the presences of http:// or https://, and the title hasn't been set yet
            //open this link in a new window
            if (link.Url.ToLower().Contains("http://") || link.Url.ToLower().Contains("https://"))
            {
                return "_blank";
            }

            //if nothing else, return in this page
            return "_self";
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