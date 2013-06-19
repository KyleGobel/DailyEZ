using JetNettApi.Models;

namespace DailyEZ.Web.Code
{
    public class LinkRenderer
    {
        public static string GetLinkExtra(Link link)
        {
            if (link == null || link.Title == null)
                return "";
            
            //if link contains a break tag, add another line break
            return link.Title.ToLower().Contains("[break]") ? "<br/>" : "";
        } 
    }
}