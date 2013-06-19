using JetNettApi.Data.Helpers;
using JetNettApi.Models;
using NUnit.Framework;

namespace JetNettApi.Data.IntegrationTests.Tests
{
    public class AdAssignmentsTests
    {

        private JetNettIntegrationTestingUnitOfWork _uow;
        private Page _samplePage;
        private Client _client1;
        private Folder _sampleFolder;
        private Client _client2;

        [SetUp]
        public void Setup()
        {
            _uow = new JetNettIntegrationTestingUnitOfWork(new RepositoryProvider(new RepositoryFactories()));
            
            _sampleFolder = Mother.CreateInMemorySampleFolder();
            _uow.Folders.Add(_sampleFolder);
            _uow.Commit();

            _samplePage = Mother.CreateInMemorySamplePage(_sampleFolder.Id);
            _uow.Pages.Add(_samplePage);
            
            
            _client1 = Mother.CreateInMemoryClient();
            _client2 = Mother.CreateInMemoryClient();
            _uow.Clients.Add(_client1);
            _uow.Clients.Add(_client2);

            _uow.Commit();


            _uow.AdGroupAssignments.Add(new AdGroupAssignment() {AdGroup = 1, Client = _client1, Page = _samplePage});
            _uow.AdGroupAssignments.Add(new AdGroupAssignment() { AdGroup = 2, Client = _client1, Page = null });
            _uow.AdGroupAssignments.Add(new AdGroupAssignment() { AdGroup = 4, Client = null, Page = _samplePage });
                                     
                                     
                                     
                                     
                                     
            _uow.Commit();
        }

        [TearDown]
        public void Dispose()
        {
            _uow.Clients.Delete(_client1);
            _uow.Clients.Delete(_client2);
            _uow.Folders.Delete(_sampleFolder);


            var agas = _uow.AdGroupAssignments.GetAll();
            foreach (var a in agas)
                _uow.AdGroupAssignments.Delete(a);
            _uow.Commit();
            _uow.Dispose();
        }
        [Test]
        public void when_client_and_page_both_null()
        {
            //pass in both null values and expect a null response
            var result = _uow.AdGroupAssignments.GetAdGroup(null, null);
            Assert.IsNull(result);
        }


        [Test]
        public void when_only_client_equals_null()
        {
            //pass in a null client which means this is a generic assignment to a page only
  
            var result = _uow.AdGroupAssignments.GetAdGroup(_samplePage, null);

            //expect that we should get back AdGroup 4 from our in memory collection
            Assert.AreEqual(4, result.AdGroup);
        }
        [Test]
        public void when_only_page_equals_null()
        {
            //this happens generic when ads are blanketed to specific client
            var result = _uow.AdGroupAssignments.GetAdGroup(null, _client1);


            //expect to get back adgroup 2
            Assert.AreEqual(2, result.AdGroup);
        }
        [Test]
        public void when_neither_page_nor_client_is_null()
        {

            var result = _uow.AdGroupAssignments.GetAdGroup(_samplePage, _client1);

            Assert.AreEqual(1, result.AdGroup);
        }

        [Test]
        public void when_neither_null_but_not_full_match()
        {

           
            var result = _uow.AdGroupAssignments.GetAdGroup(_samplePage, _client2);

            //expect back ad group 6 from our in memory collection
            Assert.AreEqual(4, result.AdGroup);
        } 
    }
}