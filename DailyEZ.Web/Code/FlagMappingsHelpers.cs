using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DailyEZ.Web.Code
{
    public class FlagMappingsHelpers
    {
        public static string RemoveMappingsFromString(List<Dictionary<string, string>> mappingsList, string input)
        {
            var output = input;
            foreach (var mappings in mappingsList)
            {
                foreach (var keyValuePair in mappings)
                {
                    output = output.Replace(keyValuePair.Key, "");
                }
            }
            return output;
        }

        /// <summary>
        /// Gets the values from the mappings dictionary that are found from the keys in the input
        /// This is mainly used as a helper for other methods
        /// </summary>
        /// <param name="mappings">The Dictionary object with the mappings to look for</param>
        /// <param name="input">The input string in which the mappings need to be extrated and converted</param>
        /// <returns>a string of the appropriate values that are looked up from the mappings dictionary</returns>
        public static string ExtractFlagsFromString(Dictionary<string, string> mappings, string input)
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
    }
}