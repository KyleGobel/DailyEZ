using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using JetNettApi.Data.Contracts;
using JetNettApi.Data.Helpers;
using JetNettApi.Models;

namespace JetNettApi.Data.IntegrationTests
{
    public class JetNettIntegrationTestingUnitOfWork : IJetNettApiUnitOfWork, IDisposable
    {
        private JetNettIntegrationTestingDbContext DbContext { get; set; }
        protected IRepositoryProvider RepositoryProvider;

        public JetNettIntegrationTestingUnitOfWork(IRepositoryProvider repositoryProvider)
        {
            CreateDbContext();


            repositoryProvider.DbContext = DbContext;
            RepositoryProvider = repositoryProvider;
            ;
        }
        public IEnumerable<DbEntityValidationResult> GetValidationErrors()
        {
            return DbContext.GetValidationErrors();
        }

        public void CreateDbContext()
        {
            DbContext = new JetNettIntegrationTestingDbContext();
            //Serialization false if we enable proxied entities
            DbContext.Configuration.ProxyCreationEnabled = false;

            //avoid serilaization trouble i guess
            DbContext.Configuration.LazyLoadingEnabled = false;

            //web api will perform validations, this isn't needed
            DbContext.Configuration.ValidateOnSaveEnabled = false;

        }
        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public IFoldersRepository Folders { get { return RepositoryProvider.GetRepository<IFoldersRepository>(); } }

        public IPagesRepository Pages { get { return RepositoryProvider.GetRepository<IPagesRepository>(); } }
        public IRepository<Client> Clients { get { return RepositoryProvider.GetRepositoryForEntityType<Client>();  } }
        public IRepository<Stack> Stacks { get; private set; }
        public ILinksRepository Links { get { return RepositoryProvider.GetRepository<ILinksRepository>(); } }
        public IAdGroupAssignmentsRepository AdGroupAssignments { get { return RepositoryProvider.GetRepository<IAdGroupAssignmentsRepository>();  }  }
        public IRepository<DailyEZ> DailyEZs { get { return RepositoryProvider.GetRepositoryForEntityType<DailyEZ>(); } }
        public IRepository<AdGroup> AdGroups { get { return RepositoryProvider.GetRepositoryForEntityType<AdGroup>(); } }
        public IRepository<Ad> Ads { get; private set; }
        public IRepository<AdAssignment> AdAssignments { get; private set; }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}