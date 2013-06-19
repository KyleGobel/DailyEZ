using JetNettApi.Models;

namespace JetNettApi.Data.IntegrationTests
{
    public static class Mother
    {
        public static Page CreateInMemorySamplePage(int folderId)
        {
            var folder = new Models.Folder() { Id = folderId };
            return new JetNettApi.Models.Page()
            {
                AutoOrdering = false,
                CanonicalUrl = "",
                FolderId = folder.Id,
                FooterHtml = "",
                HeaderHtml = "",
                MetaDesc = "",
                MetaKeys = "",
                Route = "",
                Title = "Test Page"
            };

        }
        public static Folder CreateInMemorySampleFolder()
        {
            var folder = new JetNettApi.Models.Folder()
                             {
                                 Name = "Test Folder"
                             };
            return folder;
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
                Name = "Sample-Client",
                Password = "password",
                UserId = "user",
                WebsiteTitle = "Sample Client Website Title"
            };
        }
    }
}