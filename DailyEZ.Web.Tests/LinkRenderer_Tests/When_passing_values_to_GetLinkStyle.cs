using DailyEZ.Web.Code;
using JetNettApi.Models;
using NUnit.Framework;

namespace DailyEZ.Web.UnitTests.LinkRenderer_Tests
{
    public class When_passing_values_to_GetLinkStyle
    {
        [Test]
        public void expect_empty_string_when_passing_no_flags_in_title()
        {
            var link = new Link() {Title = "Nothing special here"};
            Assert.AreEqual("", LinkRenderer.GetLinkStyle(link));
        }
        [Test]
        public void expect_appropriate_css_when_passing_content_flag_in_title()
        {
            var link = new Link() {Title = "Something that is [content]"};
            Assert.IsTrue(LinkRenderer.GetLinkStyle(link).Contains("font-weight:normal;"));
        }
        [Test]
        public void expect_appropriate_css_when_passing_bold_content_flag_in_title()
        {
            var link = new Link() { Title = "Something that is [bold]" };
            Assert.IsTrue(LinkRenderer.GetLinkStyle(link).Contains("font-weight:bold;"));
        }
        [Test]
        public void expect_all_appropriate_css_when_passing_mulitple_flags_in_title()
        {
            var link = new Link() { Title = "Something that is[bold] and [content]" };
            Assert.IsTrue(LinkRenderer.GetLinkStyle(link).Contains("font-weight:normal;") && LinkRenderer.GetLinkStyle(link).Contains("font-weight:bold;"));
        }
        [Test]
        public void expect_empty_string_when_passing_in_null_link()
        {
            Assert.AreEqual("", LinkRenderer.GetLinkStyle(null));
        }
        [Test] 
        void expect_empty_string_when_passing_in_null_title()
        {
            Assert.AreEqual("", LinkRenderer.GetLinkStyle(new Link() {Title = null}));
        }
    }
}