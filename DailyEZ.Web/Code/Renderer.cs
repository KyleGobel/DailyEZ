using System;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;
using Elmah;
using System.Linq;
using JetNettApi.Data.Contracts;
using Ninject;

namespace DailyEZ.Web.Code
{
    public class Renderer
    {
        private readonly IJetNettApiUnitOfWork _uow;

        public Renderer(IJetNettApiUnitOfWork uow)
        {
            _uow = uow;
        }

        public string HtmlHeader(HttpContext context)
        {
            if (BasePage.DailyEZObject1 == null)
            {
                ErrorLog.GetDefault(HttpContext.Current).Log(new Error(new NullReferenceException("BasePage.DailyEZObject")));
                return "DailyEZ Object is currently null";
            }
           
            string htm = "<ul class=\"nav\">";

            var folderOwner = _uow.FolderOwners.GetAll().SingleOrDefault(f => f.ClientId == BasePage.JetNettClient.Id);

            if (folderOwner == null)
            {
                //client owns no folder, report error
                ErrorLog.GetDefault(HttpContext.Current).Log(new Error(new NullReferenceException("Renderer.HtmlHeader.folderOwner")));
                return "";
            }

            var menuLinks = _uow.MenuLinks.GetAll().Where(m => m.FolderId == folderOwner.FolderId);

            if (!menuLinks.Any())
            {
                //client owns no folder, report error
                ErrorLog.GetDefault(HttpContext.Current).Log(new Error(new Exception("Renderer.HtmlHeader.menuLinks contains 0 elements")));
                return "";
            }

            // if (serverName == "thedailyez.com" || serverName == "www.thedailyez.com")
            var serverName = BasePage.DailyEZObject1.UserFriendlyUrl.Replace("http://", "");
           
            //get our current address we're at
            var currentAddress = context.Request.ServerVariables["SERVER_NAME"] + context.Request.ServerVariables["URL"];

            //make some formatting changes
            //remove the DailyEZ directory
            currentAddress = currentAddress.Replace("DailyEZ/", "");
            //get rid of default.aspx if it's there
            currentAddress = currentAddress.Replace("default.aspx", "");
            //get rid of protocol if it's there
            currentAddress = currentAddress.Replace("http://", "");

            foreach (var menuLink in menuLinks)
            {
                var tabUrl = serverName + "/" + menuLink.Url;
                var extraStyle = "";
                //if we're on the page, make it the active tab
                context.Response.Write("<!--Current Address:" + currentAddress + " Check Address: " + tabUrl + "\n\n\t-->\n\n");
                if (currentAddress.ToLower() == tabUrl.ToLower() )
                {
                    extraStyle = " class=\"active\"";
                }

                htm += "<li" + extraStyle + "><a href='http://" + tabUrl + "'>" + HttpUtility.HtmlEncode(menuLink.Title) + "</a></li>";
              
            }
            htm += "</ul>\n";
            return htm;
        }

        
    }
}