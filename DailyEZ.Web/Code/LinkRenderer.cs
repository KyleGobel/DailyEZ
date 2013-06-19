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

        public static string GetLinkTitle(Link link)
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
    }
}