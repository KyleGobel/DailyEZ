using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using DailyEZ.Web.Code;
using DailyEZ.Web.com.dailyez;

namespace DailyEZ.Web.widgets.Mobile
{
    public partial class Default : BasePage
    {

        public string PagesTree = "";
        public string MainTitle = "";
        public string StackItems = "";
        public string ChangeCommunity = "";
        private int divID = 1;
        public int AdGroup = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
          
            var id = Code.Utility.GetIntFromQueryString(Request, "id");
            var zipcode = Code.Utility.GetStringFromQueryString(Request, "zipcode");
            var title = Code.Utility.GetStringFromQueryString(Request, "Title");
            var extendedWeatherUrl = Code.Utility.GetStringFromQueryString(Request, "extendedWeatherURL");
            var adGroup = Code.Utility.GetIntFromQueryString(Request, "adGroup");
            AdGroup = adGroup;
            var stackIDFromUrl = Code.Utility.GetIntFromQueryString(Request, "stackID");
            if (title.Length > 0)
                MainTitle = title;
            if (zipcode.Length > 0)
            {
             //   Renderer.RenderWeatherRows(zipcode, litWeather);
            }
            if (extendedWeatherUrl.Length > 0)
            {
                litExtendedWeatherURL.Text = string.Format("<a href=\"{0}\" target=\"{1}\">{2}</a>",
                                                           extendedWeatherUrl,
                                                           "_blank",
                                                           "Extended Weather Forecast");
            }

            Client client = null;

    
         
                //get the client from stack id
                int cID = 0;
                var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["proConnStr"].ConnectionString);
            SqlDataReader reader = null;
                try
                {

                    connection.Open();
                    using (
                        SqlCommand command =
                            new SqlCommand("SELECT Client_ID FROM MEG_v3 WHERE Mobile_Stack_ID = @stackID"))
                    {
                        command.Parameters.AddWithValue("@stackID", stackIDFromUrl);
                        command.Connection = connection;
                        reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            try
                            {
                                cID = (int)reader["Client_ID"];
                            }
                            catch (Exception x)
                            {
                                Response.Write(x);
                            }
                        }

                    }
                }
                catch
                {

                }
                finally
                {
                    if (reader != null) reader.Close();
                }

            //attempt to get the client if we have it
            client = WebService.GetClient(ConfigurationManager.AppSettings["webServiceKey"], DailyEZObject != null ? DailyEZObject.Client_ID : cID);
            if (client != null)
            {
                clientIDLiteral.Text = "" + client.ID;

                //Get the product Name from the extended clients table so we can see what icons we should load for this
                string productName = WebService.GetProductName(ConfigurationManager.AppSettings["webServiceKey"], client.ID);

                string headerIconCode = "";
                if (productName == "Metro eGuide")
                {
                    headerIconCode = @" <link rel='shortcut icon' href='http://metroeguide.net/icons/favicon.ico' />
                                        <link rel='apple-touch-icon-precomposed' sizes='114x114' href='http://metroeguide.net/icons/apple-touch-icon-114x114-precomposed.png' />
                                        <link rel='apple-touch-icon-precomposed' sizes='72x72' href='http://metroeguide.net/icons/apple-touch-icon-72x72-precomposed.png' />
                                        <link rel='apple-touch-icon-precomposed' href='http://metroeguide.net/icons/apple-touch-icon-57x57-precomposed.png' />
                                        <link rel='apple-touch-icon' href='http://metroeguide.net/icons/apple-touch-icon.png' />'";

                }
                else
                {
                    headerIconCode = @"	<link rel='shortcut icon' href='http://dailyez.com/icons/favicon.ico' />
	                                    <link rel='apple-touch-icon-precomposed' sizes='114x114' href='http://dailyez.com/icons/apple-touch-icon-114x114-precomposed.png' />
	                                    <link rel='apple-touch-icon-precomposed' sizes='72x72' href='http://dailyez.com/icons/apple-touch-icon-72x72-precomposed.png' />
	                                    <link rel='apple-touch-icon-precomposed' href='http://dailyez.com/icons/apple-touch-icon-57x57-precomposed.png' />
	                                    <link rel='apple-touch-icon' href='http://dailyez.com/icons/apple-touch-icon.png' />";
                }

                litIcons.Text = headerIconCode;
            }
            else
            {
                //if the client is null, it's probably a metro eguide (I don't know what it is or in what cases the client can be null anymore)
                //but assume metroeguide

                //meg headerCodes
                litIcons.Text = @" <link rel='shortcut icon' href='http://metroeguide.net/icons/favicon.ico' />
                                        <link rel='apple-touch-icon-precomposed' sizes='114x114' href='http://metroeguide.net/icons/apple-touch-icon-114x114-precomposed.png' />
                                        <link rel='apple-touch-icon-precomposed' sizes='72x72' href='http://metroeguide.net/icons/apple-touch-icon-72x72-precomposed.png' />
                                        <link rel='apple-touch-icon-precomposed' href='http://metroeguide.net/icons/apple-touch-icon-57x57-precomposed.png' />
                                        <link rel='apple-touch-icon' href='http://metroeguide.net/icons/apple-touch-icon.png' />'";
            }

            

     
           
                

            if (client == null)
                this.Page.Title = "Mobile Metro eGuide";
            else
                this.Page.Title = client.Website;
            var stacks = (string) Request["stacks"];
            if (stacks != null)
            {
                string[] iStacks = stacks.Split(new char[] {','});
                for (int i = 0; i < iStacks.Length; i++)
                {
                    try
                    {
                        int stackID = 0;
                        int.TryParse(iStacks[i], out stackID);

                        if (stackID == 0)
                            continue;
                        //Get the Stack
                        var s = WebService.GetStack(int.Parse(iStacks[i]));

                        //extract the source string from the stack (should only be one)
                        var reg = new Regex("src=[\"](widgets\\/[^\"]*)[\"]");
                        var source = reg.Match(s.Widgets.Replace("###", "")).Groups[1].Value;

                        StackItems +=
                            string.Format(
                                "<li><a href=\"Default.aspx?id={0}&adGroup={1}&Title={2}&zipcode={3}&extendedWeatherURL={4}&stacks={5}\" target=\"_self\">{6}</a></li>",
                                GetQueryStringValue("id", source),
                                adGroup.ToString(CultureInfo.InvariantCulture),
                                GetQueryStringValue("Title", source),
                                GetQueryStringValue("zipcode", source),
                                GetQueryStringValue("extendedWeatherURL", source),
                                GetQueryStringValue("stacks", source),
                                s.Display_Name);
                        //  htmContent1.Text += "<a style='text-decoration:none; color:blue;' href='stack" + s.ID + "' onclick='parent.setCookie(\"StackOverride\"," + s.ID + ", 350); parent.window.location.reload(); return false;'> " + s.Display_Name + "</a><br/>";

                        ChangeCommunity = "<ul id=\"accChangeComm\">" +
                                          "<li>" +
                                          "<div>Change Community</div>" +
                                          "<ul>" +
                                          StackItems +
                                          "</ul>" +
                                          "</li>" +
                                          "</ul>";
                    }
                    catch (Exception x)
                    {
                        Response.Write(x);
                    }
                }
            }

            HandleAds(adGroup);

            var links = GetLinksFromPage(id);
            PagesTree = BuildTree(links, connection);

            if (connection.State != ConnectionState.Closed)
                connection.Close();
            litAnalytics.Text = BasePage.DailyEZObject != null ? BasePage.DailyEZObject.Analytics_Code : "";
            
    
            

            if (litAnalytics.Text.Length < 4)
                litAnalytics.Text = "UA-33196095-1";
        }
      
        public static com.dailyez.Link[] GetLinksFromPage(int pageID)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["proConnStr"].ConnectionString);
            SqlDataReader reader = null;


            try
            {
                connection.Open();
                string sSQL = "SELECT ID, Page_ID, Position, Is_Link, Title, URL, Target FROM Links WHERE Page_ID = " + pageID + " ORDER BY Position ASC";
                SqlCommand command = new SqlCommand(sSQL, connection);
                reader = command.ExecuteReader();
                return BuildLink(reader);
            }
            catch (Exception ex)
            {
                throw (new Exception("Error Executing GetLinksFromPage() --> " + ex.Message));
            }
            finally
            {
                connection.Close();
                if (reader != null) reader.Close();
            }
        }
        private static com.dailyez.Link[] BuildLink(SqlDataReader reader)
        {
            com.dailyez.Link link = null;
            ArrayList list = new ArrayList();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        link = new com.dailyez.Link();
                        link.ID = (int)reader["ID"];
                        link.PageID = (int)reader["Page_ID"];
                        link.Position = (int)reader["Position"];
                        link.IsLink = (Boolean)reader["Is_Link"];
                        link.Title = (string)reader["Title"];
                        try
                        {
                            link.URL = (string)reader["URL"];
                        }
                        catch
                        {
                            link.URL = "";
                        }
                        try
                        {
                            link.Target = (string)reader["Target"];
                        }
                        catch
                        {
                            link.Target = "";
                        }
                        list.Add(link);
                    }
                }
                return (com.dailyez.Link[])list.ToArray(typeof(com.dailyez.Link));
            }
            catch (Exception ex)
            {
                throw (new Exception("Error in BuildLink() --> " + ex.Message));
            }
        }
        public string JsFile
        {
            get { return ""; }
        }
        private static string GetQueryStringValue(string name, string sourceStr)
        {
            try
            {
                //regex to get a specific query stirng value if it's there
                Regex regex = new Regex(name + "=([^&|$]*)");

                GroupCollection coll = regex.Match(sourceStr).Groups;
                return coll[1].Value;
            }
            catch (Exception)
            {

                return "";
            }
            
        }
        bool IsInteger(string s)
        {
            var i = 0;
            int.TryParse(s, out i);

            if (i > 0)
            {
                return true;
            }
            return false;
        }
        string BuildTree(IEnumerable<Link> links, SqlConnection conn)
        {
            var htm = "\n\t<ul id=\"accordion\">";
            foreach (var l in links)
            {
                htm += BuildNode(l, conn);
            }
            htm += "\n\t</ul>";
            return htm;
        }
        string BuildNode(Link link, SqlConnection conn)
        {
            var htm = "\n\t\t<li>";

            //check here to see if this links to another menu page
            if (IsInteger(link.URL))
            {
                //get the links
                var links = GetLinksFromPage(int.Parse(link.URL));
                htm += string.Format("\n\t\t<div class=\"menuItem\" pageID=\"" + link.URL + "\" onclick=\"divClickHandler(" + link.URL + ")\">{0}</div>", link.Title);
                if (links.Length > 0)
                {
                    htm += "\n\t<ul pageID=\"" + link.URL + "\" id=\"accordion\">";
                    htm += string.Format("<li><span style=\"font-weight:bold;\">Loading</li>", link.URL);
                    htm += "\n\t</ul>";
                    divID++;
                }
            }
            else
            {
                var style = "";
                var extra = "";
                if (link.Title.ToLower().Contains("[content]"))
                    style += "font-weight:normal;";
                if (link.Title.ToLower().Contains("[bold]") || link.Title.ToLower().Contains("*bold*"))
                    style += "font-weight:bold;";
                if (link.Title.ToLower().Contains("[break]"))
                    extra += "<br/>";

                link.Title =
                    link.Title.Replace("*BOLD*", "").Replace("[CONTENT]", "").Replace("[BOLD]", "").Replace(
                        "*bold*", "")
                        .Replace("[content]", "").Replace("[bold]", "").Replace("[BREAK]", "").Replace("[break]", "");

                if (link.Title.Contains("[button]"))
                {
                    link.Title = link.Title.Replace("[button]", "");
                    var target = "_blank";
                    if (!string.IsNullOrEmpty(link.Target))
                        target = link.Target;
                    htm += string.Format("\n\t\t<div class=\"menuItem\" onclick=\"specialButtonHandler('" + link.URL + "', '{1}')\">{0}</div>", link.Title, target);
                }
                else if (link.IsLink)
                {
                    //use the link target or default "_blank" if it's null or empty
                    var target = "_blank";
                    if (!string.IsNullOrEmpty(link.Target))
                        target = link.Target;
                    htm += string.Format("\n\t\t<a href=\"{0}\" target=\"{1}\" style=\"{3}\">{2}</a>{4}", link.URL, target, HttpUtility.HtmlEncode(link.Title), style, extra);
                }
                else
                {
                    if (string.IsNullOrEmpty(style))
                        style += "font-weight:bold;";
                    htm += "\n\t\t<span style=\"" + style + "\">" + HttpUtility.HtmlEncode(link.Title) + "</span>" + extra;
                }
            }
            htm += "\n\t\t</li>";
            return htm;
        }
        public void HandleAds(int adGroup)
        {
            bool autoRotate;
            try
            {
                autoRotate = Convert.ToBoolean(Request["autoRotate"]);
            }
            catch
            {
                autoRotate = true;
            }




            var group = WebService.GetAdGroup(ConfigurationManager.AppSettings["webServiceKey"], adGroup);
            var ads = WebService.GetAdAssignmentsByAdGroup(adGroup);

            var adHtml = "";
            if (ads.Length == 0)
                return;

            if (!autoRotate)
                group.Seed = 0;
            for (var i = 0; i < group.Viewport_Size; i++)
            {

                var adFound = false;
                var currentAd = 0;

                //current display ad + 
                currentAd = (group.Seed + i) % ads.Length;
                while (!adFound)
                {
                    if (ads[currentAd].Ad_Mode == 2)
                    {
                        if (autoRotate)
                            group.Seed += 1;
                        currentAd = (group.Seed + i) % ads.Length;
                    }
                    else if (ads[currentAd].Ad_Mode == 1)
                    {
                        var startDate = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
                        var endDate = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month));
                        var tracker = WebService.GetAdViewReport(ConfigurationManager.AppSettings["webServiceKey"], startDate, endDate, ads[currentAd].ID, null, null);

                        //if the ad is over the limit
                        if (ads[currentAd].Ad_Limit <= tracker.Length)
                        {
                            //increase the seed if the ad is rotating
                            if (autoRotate)
                                group.Seed += 1;

                            //make the current ad the next ad in the group

                            //make sure we're not resetting to the same ad number...otherwise we'll never get out of this loop
                            int tempID = 0;
                            tempID = currentAd;
                            currentAd = (group.Seed + i) % ads.Length;

                            //just set the ad found to be true so we eventually get out of here
                            if (currentAd == tempID)
                                adFound = true;
                        }
                        else
                            adFound = true;
                    }
                    else
                        adFound = true;
                }



                Ads_Model ad = null;

                if (ads[currentAd].Ad_ID > 0)
                    ad = WebService.GetAdsModel(ads[currentAd].Ad_ID);

                if (ad != null)
                {
                    adHtml += "<div class=\"divAd\" id=\"placementID" + ads[currentAd].ID + "\" style='width:300px; height:" + ad.Ad_Height + "px;border:" + ad.Border_Style + ";'>" + ad.Html + "</div><br/>";
                }

                //track ad view
                var adLog = new Utility.AdLog();
                adLog.DeleteOldViewLogs();
                if (!adLog.ShouldRecordView(Request.ServerVariables["REMOTE_ADDR"], ads[currentAd].Ad_ID)) continue;


                //888888 is the mobile page id

                //get the clientID from the DailyEZObject, the clientID in the cookie is not reliable, there is no guarntee that it will be there when using the mobile page
                //we changed it so the clientID should be passed in the URL
                int cID = 0;
                if (DailyEZObject != null)
                    cID = DailyEZObject.Client_ID;
                else
                {
                    cID = int.Parse(Request["clientID"]);
                }
                adLog.Log(ads[currentAd].ID, 888888, cID, Code.Utility.GetStringFromCookie(Request, "zip"), Code.Utility.GetStringFromCookie(Request, "registered2").Equals("true"));
           
            }

            Ads.Text = adHtml;
            WebService.IncrementAdGroupSeed(ConfigurationManager.AppSettings["webServiceKey"], group.ID);
        }
    }
}