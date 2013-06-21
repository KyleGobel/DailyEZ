using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DailyEZ.Web.Code;
using System.Linq;
using System.Linq.Expressions;
using Page = JetNettApi.Models.Page;

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
           
            PageRenderer.RenderCanonicalUrlToControl(page, litCanonicalLink);
            PageRenderer.RenderFooterHtmlToControl(page, litFooterHtml);
            PageRenderer.RenderHtmlHeaderToControl(page, litExtraHtml);
            PageRenderer.RenderMetaSectionToControl(page, litMeta);
            PageRenderer.RenderPageHeaderToControl(page, litPageHeader);

            Page.Title = string.Format("{0} - {1}", page.Title, JetNettClient.WebsiteTitle);


            //get links
            var links = Uow.Links.GetAllByPage(page).ToList();
            
            //sort links
            if (page.AutoOrdering)
                Utility.BubbleSortListOfLinks(links);
            
            //render links
            litLinkContent.Text = LinkRenderer.RenderLinks(links);
        }

       

    }
}