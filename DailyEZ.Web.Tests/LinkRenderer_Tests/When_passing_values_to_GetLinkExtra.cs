using DailyEZ.Web.Code;
using JetNettApi.Models;
using NUnit.Framework;

namespace DailyEZ.Web.UnitTests.LinkRenderer_Tests
{
    public class When_passing_values_to_GetLinkExtra
    {
        [Test]
        public void expect_empty_string_when_passing_in_title_with_no_flags()
        {
            var link = new JetNettApi.Models.Link() {Title = "Nothing special, just a title"};

            Assert.AreEqual("",LinkRenderer.GetLinkExtra(link));
        }
        [Test]
        public void expect_line_break_html_tag_when_passing_in_break_flag_with_title()
        {
            var link = new JetNettApi.Models.Link() {Title = "this is something special [break]"};
            Assert.AreEqual("<br/>", LinkRenderer.GetLinkExtra(link));
        }

        [Test]
        public void expect_two_breaks_in_return_value_when_passing_two_break_flags_with_title()
        {      
            Assert.AreEqual("<br/><br/>", LinkRenderer.GetLinkExtra(new Link() {Title = "This is a [break] Title with two [break]s"}));
        }
        [Test]
        public void expect_empty_string_when_passing_in_null_link()
        {
            Assert.AreEqual("", LinkRenderer.GetLinkExtra(null));
        }
        [Test]
        public void expect_empty_string_when_passing_in_null_title()
        {
            Assert.AreEqual("", LinkRenderer.GetLinkExtra(new Link() { Title = null }));
        }

    }
}