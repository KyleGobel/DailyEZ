using JetNettApi.Models;

namespace JetNettApi.Data.Tests.Repository_Tests
{
    public static class Mother
    {
        public static Page CreateInMemorySamplePage()
        {
            var folder = new Models.Folder() {Id = 2};
            return new JetNettApi.Models.Page()
                       {
                           AutoOrdering = false,
                           CanonicalUrl = "",
                           Folder = folder,
                           FooterHtml = "",
                           HeaderHtml = "",
                           MetaDesc = "",
                           MetaKeys = "",
                           Route = "",
                           Title = "Test Page"
                       };

        }
        public static Client CreateInMemoryClient()
        {
            return new Client()
                       {
                           AnalyticsKey = "",
                           Css = "",
                           DisplayName = "Sample Client",
                           Email = "kgobel@gmail.com",
                           Id = 400,
                           Name= "Sample-Client",
                           Password = "password",
                           UserId = "user",
                           WebsiteTitle = "Sample Client Website Title"
                       };
        }
    }
}