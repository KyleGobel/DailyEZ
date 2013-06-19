using DailyEZ.Web.Code;
using JetNettApi.Models;
using NUnit.Framework;

namespace DailyEZ.Web.UnitTests.LinkRenderer_Tests
{
    public class When_passing_Values_to_GetLinkHref
    {
        [Test]
        public void expect_pound_when_passing_null_link()
        {
            string result = LinkRenderer.GetLinkHref(null);

            Assert.AreEqual("#", result);
        }
        [Test]
        public void expect_amperstamps_html_escaped()
        {
            var result = LinkRenderer.GetLinkHref(new Link() {Url = "This&That.com"});

            Assert.IsTrue(result.Contains("&amp;"));
        }

        [Test]
        public void expect_title_added_to_url_if_no_protocol()
        {
            var result = LinkRenderer.GetLinkHref(new Link() {Url = "NoProtocol", Title = "This is a Title"});
            const string expected = "NoProtocol-This-is-a-Title";

            Assert.AreEqual(expected, result);
        }
        [Test]
        public void expect_numbers_amperstamp_and_plus_removed_from_title_if_no_protocol_present()
        {
            var result = LinkRenderer.GetLinkHref(new Link() { Url = "NoProtocol", Title = "Thi&s 1is a3 T+it0le" });
            const string expected = "NoProtocol-This-is-a-Title";

            Assert.AreEqual(expected, result);
        }
    }
}