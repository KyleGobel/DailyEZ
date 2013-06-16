using System.Text.RegularExpressions;
using DailyEZ.Web.Code;
using NUnit.Framework;

namespace DailyEZ.Web.Tests.Utility_Tests
{
    [TestFixture(null, false, 0)]
    [TestFixture("899Wisconsin", true, 899)]
    [TestFixture("1234-Some-Text", true, 1234)]
    [TestFixture("TextBefore9910AndAfter", true, 9910)]
    public class ExtractNumberFromStringTests
    {
        private readonly string _route;
        private readonly int _expectedOutInt;
        private readonly bool _expectedReturn;

        public ExtractNumberFromStringTests(string route, bool expectedReturn, int expectedOutInt)
        {
            _expectedReturn = expectedReturn;
            _expectedOutInt = expectedOutInt;
            _route = route;
        }

        [Test]
        public void Then_return_value_is_correct()
        {
            var pageId = 0;
            if (_expectedReturn)
                Assert.IsTrue(Utility.ExtractNumberFromString(_route, out pageId));
            else
            {
                Assert.IsFalse(Utility.ExtractNumberFromString(_route, out pageId));
            }
        }

        [Test]
        public void Then_output_int_value_is_correct()
        {
            var pageId = 0;
            Utility.ExtractNumberFromString(_route, out pageId);
            Assert.AreEqual(_expectedOutInt, pageId);
        }
    }
  
}