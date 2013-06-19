﻿using System;
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

        /// <summary>
        /// Returns the rel value of an a tag based off the link url and if it should include it or not
        /// </summary>
        /// <param name="link">The link that needs a rel tag</param>
        /// <returns>the value of the rel attribute for an anchor tag</returns>
        public static string GetLinkRel(Link link)
        {
            if (link == null || link.Url == null)
                return "follow";
            return (link.Url.ToLower().Contains("http://") || link.Url.ToLower().Contains("https://"))
                       ? "nofollow"
                       : "follow";
        }

        /// <summary>
        /// Returns the target value for an anchor table based on the link sent
        /// </summary>
        /// <param name="link">The link which needs a target tag value</param>
        /// <returns>A target for an anchor tag</returns>
        public static string GetLinkTarget(Link link)
        {
            if (link == null)
                return "_self";
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

        /// <summary>
        /// Gets the Href property of an anchor tag from a link, ready for rendering
        /// </summary>
        /// <param name="link">Link object to extract the href from</param>
        /// <returns>A Cleaned and encoded href value for an anchor tag</returns>
        public static string GetLinkHref(Link link)
        {
            if (link == null || link.Url == null)
                return "#";

            if (link.Url.Contains("http://") || link.Url.Contains("https://"))
                return HttpUtility.HtmlEncode(link.Url);

            return HttpUtility.HtmlEncode(link.Url)
                   + "-"
                   + HttpUtility.HtmlEncode(LinkRenderer.GetLinkTitle(link)
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
                                                .Replace("&amp;", "")
                         );
        }
    }
}