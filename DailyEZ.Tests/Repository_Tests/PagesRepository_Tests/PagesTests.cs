using System;
using NUnit.Framework;

namespace JetNettApi.Data.Tests.Repository_Tests.PagesRepository_Tests
{
    [Category("PagesRepository")]
    [TestFixture]
    public class PagesTests
    {
        private JetNettApiDbContext _context;
        private PagesRepository _pagesRepository;
        private FoldersRepository _folderRepository;

        [TestFixtureSetUp]
        public void TextureSetup()
        {
            //depends on db context
            _context = new JetNettApiDbContext();
            _pagesRepository = new PagesRepository(_context);
            _folderRepository = new FoldersRepository(_context);

        }

        [Test]
        public void Get_by_route_returns_null_on_non_existing_route()
        {
            var nonExistingRoute = Guid.NewGuid().ToString();
            var page = _pagesRepository.GetByRoute(nonExistingRoute);

            Assert.IsNull(page);
        }

        [Test]
        public void Get_by_route_returns_page_on_existing_route()
        {
            string route = Guid.NewGuid().ToString();


            var page = Mother.CreateInMemorySamplePage();
            page.Folder = _folderRepository.GetById(1);
            page.Route = route;

            _pagesRepository.Add(page);
            _context.SaveChanges();

            var pageByRoute = _pagesRepository.GetByRoute(route);

            Assert.AreSame(page, pageByRoute);


            //clean up the page we created
            _pagesRepository.Delete(page);
            _context.SaveChanges();

        }
    }
}
