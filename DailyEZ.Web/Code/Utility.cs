using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using Elmah;
using JetNettApi.Data;

namespace DailyEZ.Web.Code
{
    public class Utility
    {
        /// <summary>
        /// Given a string this method will attempt to extract a number from anywhere in the string. Will return absolute
        /// value of the number found.
        /// </summary>
        /// <param name="str">string that may or may not contain a number</param>
        /// <param name="number">out parameter of the number found, 0 if nothing found</param>
        /// <returns>True if a number was found, or False if there was no number found in the str or the str was null</returns>
        public static bool ExtractNumberFromString(string str, out int number)
        {
            number = 0;

            if (string.IsNullOrEmpty(str))
                return false;
            //Extract Number from str
            var resultString = Regex.Match(str, @"\d+").Value;
            //attempt to parse the number to pageId
            int.TryParse(resultString, out number);
            //return true if we found a number, else false
            return number > 0;
        }

        public static int TryParseIntWithDefault(string intToParse, int defaultValue)
        {
            var outInt = 0;
            if (!int.TryParse(intToParse, out outInt))
                outInt = defaultValue;

            return outInt;

        }
        public static bool TryParseBoolWithDefault(string boolToParse, bool defaultValue)
        {
            var outBool = false;
            if (!bool.TryParse(boolToParse, out outBool))
                outBool = defaultValue;
            return outBool;
        }
        public static string FormatLink(com.dailyez.Link link)
        {
            //base string
            const string htm = "<a style=\"{0}\" target=\"{1}\" rel=\"nofollow\" href=\"{2}\">{3}</a>{4}";
       
            var target = "";

            if (!string.IsNullOrEmpty(link.Target))
                target = link.Target;


            var style = "";
            var extra = "";

            //add styles indicated by tags
            if (link.Title.ToLower().Contains("[content]"))
                style += "font-weight:normal;";

            if (link.Title.ToLower().Contains("[bold]") || link.Title.ToLower().Contains("*bold*"))
                style += "font-weight:bold;";

            if (link.Title.ToLower().Contains("[break]"))
                extra += "<br/>";

            //remove formatting tags
            link.Title =
                link.Title.Replace("*BOLD*", "").Replace("[CONTENT]", "").Replace("[BOLD]", "").Replace("*bold*", "")
                    .Replace("[content]", "").Replace("[bold]", "").Replace("[BREAK]", "").Replace("[break]", "");

            if (link.IsLink)
            {
                if (link.URL.Contains("PhotoAlbum.aspx") || link.URL.Contains("VideoFireworks.aspx") ||
                    link.URL.ToLower().Contains(".dailyez"))
                    return string.Format(htm, style, target, HttpUtility.HtmlEncode(link.URL),
                                         HttpUtility.HtmlEncode(link.Title), extra);

                if (link.URL.ToLower().Contains("http://") || link.URL.ToLower().Contains("https://"))
                {
                    if (string.IsNullOrEmpty(target))
                        target = "target=\"_blank\"";

                    return string.Format(htm, style, target, HttpUtility.HtmlEncode(link.URL),
                                         HttpUtility.HtmlEncode(link.Title), extra);
                }

                link.URL = link.URL + "-" +
                           HttpUtility.UrlEncode(
                               link.Title.Replace(" ", "-").Replace("0", "").Replace("1", "").Replace("2", "").Replace(
                                   "3", "").
                                   Replace("4", "").Replace("5", "").Replace("6", "").Replace("7", "").Replace("8", "").
                                   Replace("9", "").Replace("+", ""));

                return string.Format(htm, style, target, HttpUtility.HtmlEncode(link.URL),
                                     HttpUtility.HtmlEncode(link.Title), extra);
            }

            string title = "";
            if (link.URL.Contains("PhotoAlbum.aspx"))
                return "\n\t\t\t<a style=\"" + style + "\" href='" + HttpUtility.HtmlEncode(link.URL) + "'>" +
                       HttpUtility.HtmlEncode(link.Title) + "</a>" + extra;
            if (link.Title.Contains("class=\"suspendedLink\">"))
                title = link.Title;
            else
                title = HttpUtility.HtmlEncode(link.Title);

            return "<span class=\"header\"><h2 style=\"display:inline;" + style + "font-size:16px; margin:0;\">" + title +
                   "</h2></span>" + extra;

        }
        public static bool UrlExists(string url)
        {
            if (string.IsNullOrEmpty(url)) return false;
            try
            {
                new System.Net.WebClient().DownloadData(url);
                return true;
            }
            catch (System.Net.WebException e)
            {
                if (((System.Net.HttpWebResponse)e.Response).StatusCode == System.Net.HttpStatusCode.NotFound)
                    return false;
                else
                    throw;
            }
        }

        /// <summary>
        /// Gets Email/Smtp settings from Web.Config file 
        /// </summary>
        /// <returns>Object which contains the smtp settings</returns>
        private static SmtpSection GetSmtpSettings()
        {
            var configurationFile = WebConfigurationManager.OpenWebConfiguration("~/web.config");
            var mailSection = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
            return mailSection != null ? mailSection.Smtp : null;

        }
        public static void SendEmail(MailMessage message)
        {
            var settings = GetSmtpSettings();
            var emailClient = new SmtpClient
            {
                Credentials = new NetworkCredential(settings.Network.UserName, settings.Network.Password),
                Port = settings.Network.Port,
                Host = settings.Network.Host,
                EnableSsl = settings.Network.EnableSsl
            };

            emailClient.Send(message);
        }

        public static string EncodeURL(string url)
        {

            if (!url.ToLower().Contains("http://") && !url.ToLower().Contains("https://") &&
                !url.ToLower().Contains("calculator") && !url.ToLower().Contains("calendar"))
            {
                url = "Community.aspx?id=" + url;
            }

            return HttpUtility.HtmlEncode(url);
        }

        public static void BubbleSortList(com.dailyez.Link[] links)
        {
            bool swapped = false;
            do
            {
                swapped = false;
                for (int i = 0; i < links.Length - 1; i++)
                {
                    if (links[i].Title.CompareTo(links[i + 1].Title) > 0)
                    {
                        SwapLink(links, i, i + 1);
                        swapped = true;
                    }
                }
            } while (swapped);
        }

      



    

        public static SearchResult[] ConvertToLocalSearchResult(SearchService.SearchResult[] results)
        {
            ArrayList list = new ArrayList();

            foreach (SearchService.SearchResult result in results)
            {
                SearchResult r = new SearchResult();
                if (result.Link_URL.Length > 5)
                    r.IsLink = true;
                else
                    r.IsLink = false;
                r.Link_ID = result.Link_ID;
                r.Link_Title = result.Link_Title;
                r.Link_URL = result.Link_URL;
                r.Page_ID = result.Page_ID;
                r.Page_Title = result.Page_Title;

                list.Add(r);
            }
            return (SearchResult[])list.ToArray(typeof(SearchResult));
        }

        public static void SwapLink(com.dailyez.Link[] links, int x, int y)
        {
            com.dailyez.Link temp = links[x];
            links[x] = links[y];
            links[y] = temp;
        }

        public static string GetA(com.dailyez.Link link)
        {
            string target = link.Target;
            string prefix = "";
            string suffix = "";
            if (target.Length == 0)
            {
                if (!link.URL.ToLower().Contains("http://") && !link.URL.ToLower().Contains("https://") &&
                    !link.URL.ToLower().Contains("calculator") && !link.URL.ToLower().Contains("calendar") &&
                    !link.URL.ToLower().Contains("photoalbum"))
                {
                    target = "_self";
                    prefix = "";

                    string newTitle =
                        link.Title.Replace(" ", "-").Replace("0", "").Replace("1", "").Replace("2", "").Replace("3", "")
                            .Replace("4", "").Replace("5", "").Replace("6", "").Replace("7", "").Replace("8", "").
                            Replace("9", "").Replace("+", "");
                    suffix = "-" + HttpUtility.UrlEncode(newTitle);
                }
                else if (link.URL.ToLower().Contains("calculator") || link.URL.ToLower().Contains("calendar") ||
                         link.URL.ToLower().Contains("photoalbum") || link.URL.ToLower().Contains(".thedailyez"))
                {
                    target = "_self";
                }
                else
                {
                    target = "_blank";
                }
            }
            return "<a rel=\"nofollow\" target='" + HttpUtility.HtmlEncode(target) + "' href=\"" + prefix +
                   HttpUtility.HtmlEncode(link.URL) + suffix + "\">" + HttpUtility.HtmlEncode(link.Title) + "</a>";
        }

        public static string GetClientName(HttpRequest request)
        {
            int clientID = 0;
            clientID = GetIntFromCookie(request, "clientID");

            com.dailyez.Service service = new com.dailyez.Service();
            com.dailyez.Client client = service.GetClient(ConfigurationManager.AppSettings["webServiceKey"], clientID);

            if (client != null)
                return client.Name;
            else
                return "Invalid Client Identifier";
        }


        public static void SetCookies(HttpRequest request, HttpResponse response)
        {
            int clientID = Convert.ToInt32(request.QueryString["clientID"]);

            if (clientID == 0)
            {
                string url = request.Url.ToString();
                url = url.Remove(url.LastIndexOf("/")).Replace("/v2", "");

                com.dailyez.Service service = new com.dailyez.Service();

                clientID = service.GetClientIDFromUserFriendlyURL(ConfigurationManager.AppSettings["webServiceKey"], url);

            }

            int index = Convert.ToInt32(request.QueryString["index"]);
            int euID = Convert.ToInt32(request.QueryString["euID"]);
            string browserType = request.QueryString["browserType"];
            int id1 = Convert.ToInt32(request.QueryString["id1"]);
            int id2 = Convert.ToInt32(request.QueryString["id2"]);
            int tab = Convert.ToInt32(request.QueryString["tab"]);
            if (clientID > 0)
                SaveCookie(response, request, "clientID", "" + clientID);

            if (index > 0)
                SaveCookie(response, request, "index", "" + index);

            if (euID > 0)
                SaveCookie(response, request, "euID", "" + euID);
            if (browserType != null)
                SaveCookie(response, request, "browserType", "" + browserType);

            if (id1 > 0)
                SaveCookie(response, request, "id1", "" + id1);

            if (id2 > 0)
                SaveCookie(response, request, "id2", "" + id2);
            if (tab > 0)
                SaveCookie(response, request, "tab", "" + tab);

        }

        public static List<int> SearchAds(string query)
        {

            //init
            com.dailyez.Service service = new com.dailyez.Service();
            com.dailyez.Ads[] ads = service.GetAllAds(ConfigurationManager.AppSettings["webServiceKey"]);

            List<int> results = new List<int>();
            if (query == null)
                return results;
            //loop through all ads
            foreach (com.dailyez.Ads ad in ads)
            {
                //extract all the tags from the add
                string[] tags = SeperateTags(ad.Tags);
                foreach (string tag in tags)
                {
                    if (tag == "")
                        continue;
                    if ((tag.Trim().ToLower() == query.Trim().ToLower()) ||
                        (tag.Trim().ToLower().Contains(query.Trim().ToLower())) ||
                        (query.Trim().ToLower().Contains(tag.Trim().ToLower())))
                        results.Add(ad.ID);
                    //add the tag to the dictionary table
                }
            }

            return results;
        }

        public static string[] SeperateTags(string tags)
        {
            char[] seperater = { ',' };
            return tags.Split(seperater);
        }

        public static string MaxCharacters(string output, int length, bool encode)
        {
            string s = output;

            try
            {
                s = output.Substring(0, length);
            }
            catch
            {
                s = output;
            }
            if (encode)
                return HttpUtility.HtmlEncode(s);
            else
                return s;
        }

        public static int GetIntFromSession(System.Web.SessionState.HttpSessionState session, string item)
        {
            int i = 0;

            i = Convert.ToInt32(session[item]);
            return i;
        }

        public static int GetIntFromCookie(HttpRequest request, string item)
        {
            var i = 0;

            var httpCookie = request.Cookies[item];
            if (httpCookie != null)
            {
                int.TryParse(httpCookie.Value, out i);
            }

            if (i == 0)
            {
                //probably an error..do nothing for now
            }
            return i;
        }

        public static void SaveCookie(HttpResponse response, HttpRequest request, string item, string value)
        {
            response.Cookies[item].Value = value;
            response.Cookies[item].Expires = DateTime.Now.AddDays(360);
            request.Cookies[item].Value = value;
        }

        public static string GetStringFromCookie(HttpRequest request, string item)
        {
            string s = "";
            try
            {
                s = request.Cookies[item].Value;
            }
            catch
            {
            }
            if (s == null)
                return "";
            else
                return s;
        }

        public static string GetStringFromSession(System.Web.SessionState.HttpSessionState session, string item)
        {
            return Convert.ToString(session["item"]);
        }

        public static int GetIntFromQueryString(HttpRequest Request, string item)
        {
            int i = 0;

            try
            {
                i = Convert.ToInt32(Request.QueryString[item]);
            }
            catch
            {
                i = 0;
            }
            return i;
        }

        public static string GetStringFromQueryString(HttpRequest Request, string item)
        {
            string s = "";

            s = Request.QueryString[item];

            if (s == null)
                s = "";

            return s;
        }
    }
}