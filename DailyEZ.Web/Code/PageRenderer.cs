using System.Web;
using System.Web.UI;
using Page = JetNettApi.Models.Page;

namespace DailyEZ.Web.Code
{
    public class PageRenderer
    {
        /// <summary>
        /// Renders the Title or h1 tag of the page to the specified control
        /// </summary>
        /// <param name="page">The Page object in which to get the title from</param>
        /// <param name="pageHeaderControl">The control to render the html to</param>
        public static void RenderPageHeaderToControl(Page page, ITextControl pageHeaderControl)
        {
            if (string.IsNullOrEmpty(page.Title))
            {
                page.Title = "";
                pageHeaderControl.Text = "Page Title not found - PageID = " + page.Id;
            }
            else
            {
                pageHeaderControl.Text = HttpUtility.HtmlEncode(page.Title);
            }
        }

        /// <summary>
        /// Render the MetaKeywords and MetaDescription to the specified text control
        /// </summary>
        /// <param name="page">The page object to get the keywords and description from</param>
        /// <param name="metaControl">The control to render the html to</param>
        public static void RenderMetaSectionToControl(Page page, ITextControl metaControl)
        {
            if (string.IsNullOrEmpty(page.MetaKeys))
                page.MetaKeys = page.Title;
            if (string.IsNullOrEmpty(page.MetaDesc))
                page.MetaDesc = page.Title;
            metaControl.Text = "<meta name=\"keywords\" content=\"" + page.MetaKeys + "\"/><meta name=\"description\" content=\"" +
                           page.MetaDesc + "\"/>";
        }

        /// <summary>
        /// Renders the header tag and appropriate html to the given text control
        /// </summary>
        /// <param name="page">Page to get the content from that is suppose to be rendered</param>
        /// <param name="headerControl">The control to render the html to</param>
        public static void RenderHtmlHeaderToControl(Page page, ITextControl headerControl)
        {
            if (!string.IsNullOrEmpty(page.HeaderHtml))
            {
                headerControl.Text = "<header>" + HttpUtility.HtmlEncode(page.HeaderHtml) + "</header>";
            }
        }

        /// <summary>
        /// Renders the footer tag and appropriate html to the given text control
        /// </summary>
        /// <param name="page">Page to get the content from that is supposed to be rendered</param>
        /// <param name="footerControl">The control to render the html to</param>
        public static void RenderFooterHtmlToControl(Page page, ITextControl footerControl)
        {
            if (!string.IsNullOrEmpty(page.FooterHtml))
            {
                footerControl.Text = "<footer>" + HttpUtility.HtmlEncode(page.FooterHtml) + "</footer>";
            }
        }

        /// <summary>
        /// Renders the CanonicalUrl to the passed in text control
        /// </summary>
        /// <param name="page">The page object to get the canonicalUrl from</param>
        /// <param name="canonicalLinkControl">The control to render the html to</param>
        public static void RenderCanonicalUrlToControl(Page page, ITextControl canonicalLinkControl)
        {
            if (!string.IsNullOrEmpty(page.CanonicalUrl))
            {
                canonicalLinkControl.Text = "<link rel=\"canonical\" href=\"" + HttpUtility.HtmlEncode(page.CanonicalUrl) + "\"/>";
            }
        } 
    }
}