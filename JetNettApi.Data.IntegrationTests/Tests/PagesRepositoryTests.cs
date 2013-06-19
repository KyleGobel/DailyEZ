using System;
using JetNettApi.Data.Helpers;
using JetNettApi.Models;
using NUnit.Framework;

namespace JetNettApi.Data.IntegrationTests.Tests
{
    public class PagesRepositoryTests
    {

        private JetNettIntegrationTestingUnitOfWork _uow;
   
       [SetUp]
        public void Setup()
       {
           _uow = new JetNettIntegrationTestingUnitOfWork(new RepositoryProvider(new RepositoryFactories()));
       }

        [TearDown]
        public void Dispose()
        {
            _uow.Dispose();
        }

        [Test]
        public void Get_by_route_returns_null_on_non_existing_route()
        {
            var nonExistingRoute = Guid.NewGuid().ToString();
            var page = _uow.Pages.GetByRoute(nonExistingRoute);

            Assert.IsNull(page);
        }

        [Test]
        public void Get_by_route_returns_page_on_existing_route()
        {
            string route = Guid.NewGuid().ToString();

            var folder = Mother.CreateInMemorySampleFolder();
            _uow.Folders.Add(folder);
            _uow.Commit();
            var page = Mother.CreateInMemorySamplePage(folder.Id);
            
            page.Route = route;
            page.Folder = folder;

            _uow.Pages.Add(page);
            _uow.Commit();

            var pageByRoute = _uow.Pages.GetByRoute(route);

            Assert.AreSame(page, pageByRoute);


            //clean up the page we created
            _uow.Pages.Delete(page);
            _uow.Folders.Delete(folder);
            _uow.Commit();

        } 
    }
}