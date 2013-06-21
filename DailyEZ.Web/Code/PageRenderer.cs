using System.Web;
using System.Web.UI;
using Page = JetNettApi.Models.Page;

namespace DailyEZ.Web.Code
{
    public class PageRenderer
    {
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

        public static void RenderMetaSectionToControl(Page page, ITextControl metaControl)
        {
            if (string.IsNullOrEmpty(page.MetaKeys))
                page.MetaKeys = page.Title;
            if (string.IsNullOrEmpty(page.MetaDesc))
                page.MetaDesc = page.Title;
            metaControl.Text = "<meta name=\"keywords\" content=\"" + page.MetaKeys + "\"/><meta name=\"description\" content=\"" +
                           page.MetaDesc + "\"/>";
        }

        public static void RenderHtmlHeaderToControl(Page page, ITextControl headerControl)
        {
            if (!string.IsNullOrEmpty(page.HeaderHtml))
            {
                headerControl.Text = "<header>" + HttpUtility.HtmlEncode(page.HeaderHtml) + "</header>";
            }
        }

        public static void RenderFooterHtmlToControl(Page page, ITextControl footerControl)
        {
            if (!string.IsNullOrEmpty(page.FooterHtml))
            {
                footerControl.Text = "<footer>" + HttpUtility.HtmlEncode(page.FooterHtml) + "</footer>";
            }
        }

        public static void RenderCanonicalUrlToControl(Page page, ITextControl canonicalLinkControl)
        {
            if (!string.IsNullOrEmpty(page.CanonicalUrl))
            {
                canonicalLinkControl.Text = "<link rel=\"canonical\" href=\"" + HttpUtility.HtmlEncode(page.CanonicalUrl) + "\"/>";
            }
        } 
    }
}