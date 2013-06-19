using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public static string ExtractFlagsFromString(Dictionary<string, string> mappings, string input )
        {
            //Regex to find flags ( [word] )
            var re = new Regex(@"\[(\w+)\]", RegexOptions.Compiled);

            //find all matches
            var matches = re.Matches(input);

            var output = "";
            //loop through mathes and return their outputs in one string
            foreach (Match m in matches)
            {
                string valueFromDictionary;
                mappings.TryGetValue(m.Value, out valueFromDictionary);
                if (valueFromDictionary == null)
                    valueFromDictionary = "";
                output += m.Result(valueFromDictionary);
            }
            return output;
        }
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

            return ExtractFlagsFromString(ExtraFlagMappings, link.Title);
        }

        public static string GetLinkStyle(Link link)
        {
            if (link == null || link.Title == null)
                return "";

            return ExtractFlagsFromString(StyleFlagMappings, link.Title);

        }
    }
}