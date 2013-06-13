using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Web;
using DailyEZ.Web.Code;

namespace DailyEZ.Web.widgets.Mobile
{
    public partial class Search : BasePage
    {
        public string pSearch = "";
        public string findLocal = "";
        public bool debug = false;
        SearchService.SearchServiceClient searchService = new SearchService.SearchServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {

            Code.Utility.SetCookies(Request, Response);
       

            string s = Request.QueryString["q"];
            string sd = Request.QueryString["debug"];
            bool googleSearch = false;

            bool.TryParse(Request.QueryString["googleSearch"], out googleSearch);


            if (!String.IsNullOrEmpty(sd))
                debug = true;
            string htmSearchData = "";
            if (s != null)
            {
                litSearchHeader.Text = "Search Results for '" + HttpUtility.HtmlEncode(s) + "'";
                if (!googleSearch) htmSearchData += htmSearchQuery(s);
            }
            else
                litSearchHeader.Text = "Nothing searched";


            com.dailyez.Daily_EZ dailyEZ = DailyEZObject;
            if (dailyEZ != null)
                findLocal = dailyEZ.Breaking_News_Title;
            litSearchResults.Text = htmSearchData;
            if (dailyEZ != null)
                litAnalytics.Text = dailyEZ.Analytics_Code;

        }
        public string Version
        {
            get
            {
                var assem = Assembly.GetExecutingAssembly().GetName();
                return string.Format("{0}", assem.Version.ToString());
            }
        }
        public string CssFile
        {
            get { return "href=\"http://static.dailyez.com/client/combined." + Version + ".min.css\""; }
        }

        int PageRank(string searchQuery, List<SearchService.SearchResult> listResult)
        {
            List<string> seperateWords = new List<string>(searchQuery.Split(new char[] { ' ' }));

            //remove common words that don't do anything        
            seperateWords.Remove("of");
            seperateWords.Remove("and");
            seperateWords.Remove("the");
            seperateWords.Remove("is");
            seperateWords.Remove("are");
            int ranking = listResult.Count;
            SearchService.SearchResult result = listResult[0];

            foreach (string word in seperateWords)
            {
                //Rank word in title             
                //this word is the title of the webpage            
                if (word.ToLower().Equals(result.Link_Title.ToLower()))
                    ranking += 145;
                //title contains the word
                else if (result.Link_Title.ToLower().Contains(word.ToLower()))
                {
                    ranking += 75;
                    //subtract ranking for how far into the title the word is
                    ranking -= result.Link_Title.ToLower().IndexOf(word.ToLower());
                }
                //Rank word in the URL           
                if (result.Link_URL.ToLower().Contains(word.ToLower()))
                    ranking += 51;
                if (result.Page_Title.ToLower().Contains(word.ToLower()))
                {
                    ranking += 50;
                    ranking -= result.Page_Title.ToLower().IndexOf(word.ToLower());
                }
                else if (listResult.Count > 1)
                {
                    if (listResult[1].Page_Title.ToLower().Contains(word.ToLower()))
                    {
                        ranking += 50;
                        ranking -= listResult[1].Page_Title.ToLower().IndexOf(word.ToLower());
                    }
                }

                //check for cityof or townof
                if (result.Link_URL.ToLower().Contains("cityof") || result.Link_URL.ToLower().Contains("townof") || result.Link_URL.ToLower().Contains("villageof"))
                    ranking += 26;
            }
            return ranking;
        }

        public int[] GetFoldersToSearch(com.dailyez.Daily_EZ dailyEZ)
        {
            com.dailyez.MEG_v3 meg = null;
            //check if the dailyEZ object is null
            if (dailyEZ == null)
            {
                //that means we should try the metro eguide search folders.
                int clientID = 0;
                try
                {
                    clientID = int.Parse(Request.QueryString["c"]);
                }
                catch
                {
                    Response.Write("c not found in querystring");
                }

                meg = WebService.GetMEGv3ByClientID(
                    ConfigurationManager.AppSettings["webServiceKey"], clientID);

            }
            //so we can split the folders
            char[] split = { ',' };

         
            //get the folders entered into the DailyEZ form (via EZ Editor)
            string[] sFolders = new string[] {};
            if (dailyEZ != null)
            {
                sFolders = dailyEZ.Search_Folders.Split(split);
            }
            else if (meg != null)
            {
                sFolders = meg.SearchFolders.Split(split);
            }
            //Create a blank list of integers
            List<int> listOfFolders = new List<int>();

            //Loop through each folder that was entered into the DailyEZ
            foreach (string sFolder in sFolders)
            {
                try
                {
                    //try to parse the string to a folder ID
                    int folder = int.Parse(sFolder);

                    //add it to the list if it was successful
                    listOfFolders.Add(folder);

                    //Get all the sub folders of the current folder
                    SearchService.Folder[] subFolders = searchService.GetAllSubFolders(folder);

                    //loop through them all and add it to our list
                    foreach (SearchService.Folder f in subFolders)
                        listOfFolders.Add(f.ID);
                }
                catch
                { }
            }

            //return the list
            return listOfFolders.ToArray();
            
        }
        public Dictionary<string, List<SearchService.SearchResult>> MakeSearchDictionaries(SearchService.SearchResult[] searchResults)
        {
            Dictionary<string, List<SearchService.SearchResult>> dictionary = new Dictionary<string, List<SearchService.SearchResult>>();
            int headersUniqueNumber = 0;

            //make sure we returned some results
            if (searchResults != null)
            {
                //loop through everything
                foreach (SearchService.SearchResult result in searchResults)
                {
                    List<SearchService.SearchResult> list;

                    //if url is empty, it must be a header
                    if (string.IsNullOrEmpty(result.Link_URL))
                    {
                        result.Link_URL = "" + headersUniqueNumber;
                        headersUniqueNumber++;
                    }

                    //check if the url is an internal link..then continue so it doesn't get added 
                    if (!result.Link_URL.Contains("http://") && !(result.Link_URL.Contains("https://")))
                        continue;
                    //see if the url exists in the dictionary
                    if (dictionary.ContainsKey(result.Link_URL))
                    {
                        //if it successfully got the list
                        if (dictionary.TryGetValue(result.Link_URL, out list))
                        {
                            list.Add(result);
                        }
                    }
                    else
                    {
                        //add the list to the dictionary;
                        list = new List<SearchService.SearchResult>();
                        list.Add(result);
                        dictionary.Add(result.Link_URL, list);
                    }
                }
            }
            return dictionary;
        }
        public void FormatHeaderTitles(List<KeyValuePair<string, List<SearchService.SearchResult>>> sortableList)
        {
            foreach (KeyValuePair<string, List<SearchService.SearchResult>> e in sortableList)
            {
                foreach (SearchService.SearchResult result in e.Value)
                {
                    if (!(result.Link_URL.Length > 5))
                    {
                        result.Link_Title = result.Link_Title + " - " + result.Page_Title;
                    }
                }
            }
        }
        public string htmSearchQuery(string search)
        {
            pSearch = search;
      
            string response = "";




            //     int[] test = {1, 2, 415};

            SearchService.SearchResult[] searchResults = searchService.Search(GetFoldersToSearch(DailyEZObject), search);


            //start our html
            response += "<div class='leftColumnContent'>";

            //make a dictionary to store the duplicates
            Dictionary<string, List<SearchService.SearchResult>> dictionary = MakeSearchDictionaries(searchResults);

            //create a list for the headers
            List<SearchService.SearchResult> headers = new List<SearchService.SearchResult>();


            //create a new list of KeyValuePairs<string, SearchResult's List>.  This is done so we can easily sort the values
            List<KeyValuePair<string, List<SearchService.SearchResult>>> myList = new List<KeyValuePair<string, List<SearchService.SearchResult>>>(dictionary);



            //change header titles before sort
            FormatHeaderTitles(myList);

            //our sorting function, takes the first pair, and comparies the link title to the second pair's link title.
            myList.Sort(
                delegate(KeyValuePair<string, List<SearchService.SearchResult>> firstPair, KeyValuePair<string, List<SearchService.SearchResult>> secondPair)
                {
                    //return firstPair.Value[0].Link_Title.CompareTo(secondPair.Value[0].Link_Title);
                    return PageRank(search, secondPair.Value).CompareTo(PageRank(search, firstPair.Value));
                }
                );

            //position go till we can't anymore
            int divIndex = 0;
            foreach (KeyValuePair<string, List<SearchService.SearchResult>> e in myList)
            {
                if (e.Value.Count >= 1)
                {
                    com.dailyez.Link link = new com.dailyez.Link();
                    link.Title = e.Value[0].Link_Title;
                    link.URL = e.Value[0].Link_URL;
                    link.Target = "";

                    bool oneIsLink = false;

                    //for every SearchResult inside the list of the current dictionary value
                    foreach (SearchService.SearchResult r in e.Value)
                    {
                        //find out if at least one of them is a hyperlink
                        if (r.Link_URL.Length > 5)
                        {
                            oneIsLink = true;
                            break;
                        }
                    }


                    //one of the values is a hyperlink...make the starting div
                    if (oneIsLink)
                    {
                        if (debug == true)
                            response += "<span style='color:red'>" + PageRank(search, e.Value) + "</span>";
                        response += Code.Utility.GetA(link) +"<br/>";

                        //  <div id='resultsDiv[X]' style='display:none;font-family:Arial'>
                        response += "<div id='resultsDiv" + divIndex + "' style='display:none;font-family:Arial;line-spacing:20px; margin-left:10px;'>";
                    }

                    //body of the div
                    foreach (SearchService.SearchResult result in e.Value)
                    {
                        if (result.Link_URL.Length > 5)
                        {
                            com.dailyez.Link innerLink = new com.dailyez.Link();
                            innerLink.Title = result.Page_Title;
                            innerLink.URL = "" + result.Page_ID;
                            innerLink.Target = "";
                            response += Code.Utility.GetA(innerLink) + "<br/>";
                        }
                        else
                        {
                            com.dailyez.Link innerLink = new com.dailyez.Link();
                            innerLink.Title = result.Link_Title;
                            // innerLink.Title = result.Link_Title + " - " + service.GetPage(ConfigurationManager.AppSettings["webServiceKey"], result.Page_ID).Title;
                            innerLink.URL = "" + result.Page_ID;
                            innerLink.Target = "";
                            response += Code.Utility.GetA(innerLink) + "<br/>";
                            //   headers.Add(result);
                        }

                    }
                    if (oneIsLink)
                        response += "</div>";
                    divIndex++;

                }
                else
                {
                    if (e.Value[0].Link_URL.Length > 5)
                    {
                        com.dailyez.Link link = new com.dailyez.Link();
                        link.Title = e.Value[0].Link_Title;
                        link.URL = e.Value[0].Link_URL;
                        link.Target = "";
                        com.dailyez.Link pageLink = new com.dailyez.Link();
                        pageLink.Title = e.Value[0].Page_Title;
                        pageLink.URL = "" + e.Value[0].Page_ID;
                        response += Code.Utility.GetA(link) + " @ " + Code.Utility.GetA(pageLink) + "<br/>";
                    }
                    else
                    {
                        var innerLink = new com.dailyez.Link
                            {
                                Title = "" + e.Value[0].Link_Title,
                                URL = "" + e.Value[0].Page_ID,
                                Target = ""
                            };
                        // innerLink.Title = result.Link_Title + " - " + service.GetPage(ConfigurationManager.AppSettings["webServiceKey"], result.Page_ID).Title;
                        response += Code.Utility.GetA(innerLink) + "<br/>";

                        //commented out add headers here
                    }

                }
            }




            //add headers
            SearchResult[] srHeaders = Code.Utility.ConvertToLocalSearchResult(headers.ToArray());
            List<SearchResult> headerList = new List<SearchResult>(srHeaders);
            headerList.Sort();
            foreach (var result in headerList)
            {
                var innerLink = new com.dailyez.Link
                    {
                        Title = result.Page_Title + " - " + result.Link_Title,
                        URL = "" + result.Page_ID,
                        Target = ""
                    };
                //   response += DailyEZUtility.GetA(innerLink) + "<br/>";
            }

            if (response.Length < 1)
                response = "<span style='font-family:arial; font-size:12pt;'>Sorry, No Search Results.</span>";



            response += "</div>";

            return response;
        }
    }
}