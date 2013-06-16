using JetNettApi.Models;
using NUnit.Framework;

namespace JetNettApi.Data.Tests.Repository_Tests.AdGroupAssignments_Tests
{
    [Category("AdGroupAssignmentsRepository")]
    [TestFixture]
    public class AdGroupAssignmentsTests
    {
        private MockUow _muow;
        private Page _samplePage;
        private Client _sampleClient;
        [SetUp]
        public void Setup()
        {
            _muow = new MockUow();
            
            _samplePage = Mother.CreateInMemorySamplePage();
            _sampleClient = Mother.CreateInMemoryClient();
        }
        [Test]
        public void when_client_and_page_both_null()
        {
            //pass in both null values and expect a null response
            var result = _muow.AdGroupAssignments.GetAdGroup(null, null);
            Assert.IsNull(result);
        }


        [Test]
        public void when_only_client_equals_null()
        {
            //pass in a null client which means this is a generic assignment to a page only
            _samplePage.Id = 25;
            var result = _muow.AdGroupAssignments.GetAdGroup(_samplePage, null);

            //expect that we should get back AdGroup 4 from our in memory collection
            Assert.AreEqual(4, result.AdGroup);
        }
        [Test]
        public void when_only_page_equals_null()
        {
            //this happens generic when ads are blanketed to specific client
            _sampleClient.Id = 400;
            var result = _muow.AdGroupAssignments.GetAdGroup(null, _sampleClient);


            //expect to get back adgroup 2
            Assert.AreEqual(2, result.AdGroup);
        }
        [Test]
        public void when_neither_page_nor_client_is_null()
        {
            _sampleClient.Id = 400;
            _samplePage.Id = 25;

            var result = _muow.AdGroupAssignments.GetAdGroup(_samplePage, _sampleClient);

            Assert.AreEqual(1, result.AdGroup);
        }
   
        [Test]
        public void when_neither_null_but_not_full_match()
        {
            _sampleClient.Id = 402;
            _samplePage.Id = 27;

            var result = _muow.AdGroupAssignments.GetAdGroup(_samplePage, _sampleClient);

            //expect back ad group 6 from our in memory collection
            Assert.AreEqual(6, result.AdGroup);
        }
    }
}