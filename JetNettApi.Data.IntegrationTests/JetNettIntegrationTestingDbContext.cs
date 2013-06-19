using System.Data.Entity;
using JetNettApi.Models;

namespace JetNettApi.Data.IntegrationTests
{
    public class JetNettIntegrationTestingDbContext : DbContext
    {
        static JetNettIntegrationTestingDbContext()
        {
            Database.SetInitializer(new JetNettIntegrationTestingDbInitializer());
        }
        public JetNettIntegrationTestingDbContext() : base(nameOrConnectionString : "TestingDb"){}

        public DbSet<Folder> Folders { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<AdGroupAssignment> AdGroupAssignments { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<DailyEZ> DailyEZs { get; set; }

      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //custom configuration here
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
