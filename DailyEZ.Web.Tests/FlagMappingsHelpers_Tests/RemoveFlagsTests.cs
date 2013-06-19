using System.Collections.Generic;
using DailyEZ.Web.Code;
using NUnit.Framework;

namespace DailyEZ.Web.UnitTests.FlagMappingsHelpers_Tests
{
    public class RemoveFlagsTests
    {
        private List<Dictionary<string, string>> _mappingsList;

        [SetUp]
        public void Setup()
        {
            _mappingsList = new List<Dictionary<string, string>>()
                                {
                                    new Dictionary<string, string>()
                                        {
                                            {"[key1]", "value1"}
                                        },
                                        new Dictionary<string, string>()
                                        {
                                            {"[anotherKey]", "differentValue"},
                                            {"[key4]", "value"}
                                        }
                                };
        }

        [Test]
        public void remove_one_flag_from_string_with_one_mapping_dictionary()
        {
            var result = FlagMappingsHelpers.RemoveMappingsFromString(_mappingsList, "blah [key1] should be removed");

            Assert.AreEqual("blah  should be removed", result);
        }
        [Test]
        public void dont_remove_flag_if_its_not_in_a_mapping_dictionary()
        {
            const string testString = "blah [key2] should be removed";
            var result = FlagMappingsHelpers.RemoveMappingsFromString(_mappingsList, testString);

            Assert.AreEqual(testString, result);
        }
        [Test]
        public void remove_multiple_flags_from_multiple_mappings()
        {
            const string testString = "this has [key1], and [key4] in it";

            var result = FlagMappingsHelpers.RemoveMappingsFromString(_mappingsList, testString);

            Assert.AreEqual(testString.Replace("[key1]", "").Replace("[key4]", ""), result);

        }
    }
}