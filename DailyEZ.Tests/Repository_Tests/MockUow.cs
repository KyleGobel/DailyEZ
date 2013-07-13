using JetNettApi.Data.Contracts;
using JetNettApi.Models;
using Moq;

namespace JetNettApi.Data.Tests.Repository_Tests
{
    public class MockUow : IJetNettApiUnitOfWork
    {
        public MockUow()
        {
            CommitCalled = false;
        }

        public void Dispose()
        {}
        public void Commit()
        {
            CommitCalled = true;
        }

        public bool CommitCalled { get; set; }
        public IFoldersRepository Folders { get; private set; }
        public IPagesRepository Pages { get; private set; }

        public IAdGroupAssignmentsRepository AdGroupAssignments
        {
            get
            {
                var context = new Mock<JetNettApiDbContext>();
                var repo = new AdGroupAssignmentsRepository(context.Object) {DbSet = new InMemoryDbSet<AdGroupAssignment>()};
                repo.DbSet = new InMemoryDbSet<AdGroupAssignment>()
                                 {
                                     new AdGroupAssignment() {AdGroup = 1, ClientId = 400, PageId = 25},
                                     new AdGroupAssignment() {AdGroup = 2, ClientId = 400, PageId = null},
                                     new AdGroupAssignment() {AdGroup = 3, ClientId = 401, PageId = 25},
                                     new AdGroupAssignment() {AdGroup = 4, ClientId = null, PageId = 25},
                                     new AdGroupAssignment() {AdGroup = 5, ClientId = 402, PageId = null},
                                     new AdGroupAssignment() {AdGroup = 6, ClientId = null, PageId = 27},
                                 };

                return repo;
            }
        }

        public IRepository<Client> Clients { get; private set; }
        public IRepository<Stack> Stacks { get; private set; }
        public ILinksRepository Links { get; private set; }
        public IRepository<DailyEZ> DailyEZs { get; private set; }
        public IRepository<AdGroup> AdGroups { get; private set; }
        public IRepository<Ad> Ads { get; private set; }
        public IRepository<AdAssignment> AdAssignments { get; private set; }
    }
}