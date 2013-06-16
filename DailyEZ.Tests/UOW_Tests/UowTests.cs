using System.Data.Entity;
using JetNettApi.Data.Helpers;
using NUnit.Framework;
using Moq;
namespace JetNettApi.Data.Tests.UOW_Tests
{
    [Category("Unit of Work")]
    [TestFixture]
    public class UowTests
    {
        [Test]
        public void Db_context_save_changes_is_called_on_commit()
        {
            var mockContext = new Mock<JetNettApiDbContext>();

            var repoFactories = new RepositoryFactories();

            var repoProvider = new RepositoryProvider(null);


            var uow = new JetNettApiUow(repoProvider, mockContext.Object);

            
            uow.Commit();

            mockContext.Verify(x => x.SaveChanges());
        }
         
    }
}