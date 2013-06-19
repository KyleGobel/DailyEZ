using System.Data.Entity;
using JetNettApi.Models;

namespace JetNettApi.Data.IntegrationTests
{
    public class JetNettIntegrationTestingDbInitializer : DropCreateDatabaseIfModelChanges<JetNettIntegrationTestingDbContext>
    {
        protected override void Seed(JetNettIntegrationTestingDbContext context)
        {
            var c = new Client()
                        {
                            AnalyticsKey = "",
                            Css = "",
                            DisplayName = "Test Client DName",
                            Email = "kgobel@gmail.com",
                            Name = "Test Client",
                            Password = "abcd",
                            UserId = "user",
                            WebsiteTitle = "Title of Website"
                        };
            context.Clients.Add(c);
            base.Seed(context);
        }
    }
}
