using System;
using JetNettApi.Data.Contracts;
using JetNettApi.Data.Helpers;
using JetNettApi.Models;
using Moq;
using NUnit.Framework;

namespace JetNettApi.Data.Tests.Repository_Tests.RepositoryFactories_Tests
{
    [Category("RepositoryFactories")]
    [TestFixture]
    public class RepoFactoryTests
    {
        private RepositoryFactories _rf;
        private Mock<JetNettApiDbContext> _mockDbContext;

        [SetUp]
        public void Setup()
        {
            //create repoFactoriesObject
            _rf = new RepositoryFactories();
            _mockDbContext = new Mock<JetNettApiDbContext>();
        }
        [Test]
        public void request_folders_factory()
        {
            //get a factory
            var factory = _rf.GetRepositoryFactory<IFoldersRepository>();
            //get the repo from the factory
            var repo = factory(_mockDbContext.Object);
            Assert.IsTrue(typeof(FoldersRepository) == repo.GetType());
        }
        [Test]
        public void request_pages_factory()
        {
            //get a factory
            var factory = _rf.GetRepositoryFactory<IPagesRepository>();
            //get the repo from the factory
            var repo = factory(_mockDbContext.Object);
            Assert.IsTrue(typeof(PagesRepository) == repo.GetType());
        }
        [Test]
        public void request_non_special_type_factory()
        {

            //request a string factory here
            var factory = _rf.GetRepositoryFactory<String>();

            //it should still return a repo, it will just be the standard EFRepository
            var repo = factory(_mockDbContext.Object);

            Assert.IsTrue(typeof(EFRepository<String>) == repo.GetType());

        }

         
    }
}