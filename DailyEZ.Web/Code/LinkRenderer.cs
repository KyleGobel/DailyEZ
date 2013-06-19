using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using JetNettApi.Models;

namespace DailyEZ.Web.Code
{
    public class LinkRenderer
    {
        private static readonly Dictionary<string, string> ExtraFlagMappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                                                                   {
                                                                       {"[break]", "<br/>"},
                                                                   };

        private static readonly Dictionary<string, string> StyleFlagMappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                                                                   {
                                                                       {"[content]", "font-weight:normal;"},
                                                                       {"[bold]", "font-weight:bold;"}
                                                                   };

    
        /// <summary>
        /// Parses link properties for any flags, and returns approprite extra html
        /// </summary>
        /// <param name="link">The Link object to parse for flags</param>
        /// <returns>Extra html that is needed to render this link</returns>
        public static string GetLinkExtra(Link link)
        {
            //Error checking
            if (link == null || link.Title == null)
                return "";

            return FlagMappingsHelpers.ExtractFlagsFromString(ExtraFlagMappings, link.Title);
        }

        /// <summary>
        /// Parses link properites for any style flags, and returns appropriate css values
        /// </summary>
        /// <param name="link">The link object to parse for flags</param>
        /// <returns>The CSS that is needed to render this link appropriatly</returns>
        public static string GetLinkStyle(Link link)
        {
            if (link == null || link.Title == null)
                return "";

            return FlagMappingsHelpers.ExtractFlagsFromString(StyleFlagMappings, link.Title);

        }

        /// <summary>
        /// Cleans the title of all mappings, and Html Encodes it.  Basically it gets the title 
        /// ready for rendering
        /// </summary>
        /// <param name="link">The link with the title object</param>
        /// <returns>a clean string ready for html rendering</returns>
        public static string GetLinkTitle(Link link)
        {
            if (link == null || link.Title == null)
                return "";
            //clean out our annotations or attributes
            var cleanTitle = FlagMappingsHelpers.RemoveMappingsFromString(
                new List<Dictionary<string, string>>()
                    {
                        ExtraFlagMappings,
                        StyleFlagMappings
                    }, link.Title);
            return HttpUtility.HtmlEncode(cleanTitle);
        }
    }
}