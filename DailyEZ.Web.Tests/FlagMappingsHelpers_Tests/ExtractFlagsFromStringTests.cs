using System;
using System.Collections.Generic;
using DailyEZ.Web.Code;
using NUnit.Framework;

namespace DailyEZ.Web.UnitTests.FlagMappingsHelpers_Tests
{
    public class ExtractFlagsFromStringTests
    {
        [Test]
        public void when_input_has_non_existant_flag_fail_gracefully_return_nothing()
        {
            var mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                               {
                                   {"[testKey]", "testValue"}
                               };

            Assert.AreEqual("", FlagMappingsHelpers.ExtractFlagsFromString(mappings, "this has a [flag] that doesn't exist in the mapping dictionary"));
            
        }
        [Test]
        public void when_input_has_non_existant_flag_and_existant_flag_return_correct_value()
        {
            var mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                               {
                                   {"[testKey]", "testValue"}
                               };

            Assert.AreEqual("testValue", FlagMappingsHelpers.ExtractFlagsFromString(mappings, "this [has] lots [of] [flags] [testKey]"));
        }
        [Test]
        public void when_more_than_one_of_same_flag_passed_return_multiple_values()
        {
            var mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                               {
                                   {"[testKey]", "testValue"}
                               };

            Assert.AreEqual("testValuetestValue", FlagMappingsHelpers.ExtractFlagsFromString(mappings, "t[testKey] and another [testKey]"));
        }
        [Test]
        public void when_multiple_different_flags_passed_return_multiple_different_values()
        {
            var mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                               {
                                   {"[testKey]", "testValue"},
                                   {"[differentKey]", "differentValue"}
                               };

            var result = FlagMappingsHelpers.ExtractFlagsFromString(mappings, "mulitple [testKey] and [differentKey]");

            Assert.IsTrue(result.Contains("testValue") && result.Contains("differentValue"));
            
        }
    }
}