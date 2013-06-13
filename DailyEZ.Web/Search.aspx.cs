using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using DailyEZ.Web.Code;

namespace DailyEZ.Web
{
    public partial class Search : BasePage
    {
        public string PSearch = "";
        public string FindLocal = "";
        public bool Debug = false;
        readonly SearchService.SearchServiceClient _searchService = new SearchService.SearchServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            var s = Request.QueryString["q"];
            var sd = Request.QueryString["debug"];
            var googleSearch = false;

            bool.TryParse(Request.QueryString["googleSearch"], out googleSearch);


            if (!String.IsNullOrEmpty(sd))
                Debug = true;
            string htmSearchData = "";
            if (s != null)
            {
                litSearchHeader.Text = "Search Results for '" + HttpUtility.HtmlEncode(s) + "'";
                if (!googleSearch) htmSearchData += HtmSearchQuery(s);
            }
            else
                litSearchHeader.Text = "Nothing searched";




            var dailyEZ = DailyEZObject;
            FindLocal = dailyEZ.Breaking_News_Title;
            litSearchResults.Text = htmSearchData;


        }
        int PageRank(string searchQuery, List<SearchService.SearchResult> listResult)
        {
            var seperateWords = new List<string>(searchQuery.Split(new char[] { ' ' }));

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
                    ranking -= result.Link_Title.ToLower().IndexOf(word.ToLower(), System.StringComparison.Ordinal);
                }
                //Rank word in the URL           
                if (result.Link_URL.ToLower().Contains(word.ToLower()))
                    ranking += 51;
                if (result.Page_Title.ToLower().Contains(word.ToLower()))
                {
                    ranking += 50;
                    ranking -= result.Page_Title.ToLower().IndexOf(word.ToLower(), System.StringComparison.Ordinal);
                }
                else if (listResult.Count > 1)
                {
                    if (listResult[1].Page_Title.ToLower().Contains(word.ToLower()))
                    {
                        ranking += 50;
                        ranking -= listResult[1].Page_Title.ToLower().IndexOf(word.ToLower(), System.StringComparison.Ordinal);
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

            //so we can split the folders
            char[] split = { ',' };

            //get the folders entered into the DailyEZ form (via EZ Editor)
            string[] sFolders = dailyEZ.Search_Folders.Split(split);

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
                    SearchService.Folder[] subFolders = _searchService.GetAllSubFolders(folder);

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
            var dictionary = new Dictionary<string, List<SearchService.SearchResult>>();
            var headersUniqueNumber = 0;

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
                        list = new List<SearchService.SearchResult> {result};
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
        public string HtmSearchQuery(string search)
        {
            PSearch = search;
            var words = search.Split(new char[] { ' ' });

            var clientID = Utility.GetIntFromCookie(Request, "clientID");
            var euID = Utility.GetIntFromCookie(Request, "euID");

            var service = new com.dailyez.Service();
            var dailyEZ = BasePage.DailyEZObject;

            var response = "";




            //     int[] test = {1, 2, 415};

            var searchResults = _searchService.Search(GetFoldersToSearch(dailyEZ), search);


            //start our html
            response += "<div class='leftColumnContent'>";

            //make a dictionary to store the duplicates
            Dictionary<string, List<SearchService.SearchResult>> dictionary = MakeSearchDictionaries(searchResults);

            //create a list for the headers
            var headers = new List<SearchService.SearchResult>();


            //create a new list of KeyValuePairs<string, SearchResult's List>.  This is done so we can easily sort the values
            var myList = new List<KeyValuePair<string, List<SearchService.SearchResult>>>(dictionary);



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
            var divIndex = 0;
            foreach (KeyValuePair<string, List<SearchService.SearchResult>> e in myList)
            {
                if (e.Value.Count >= 1)
                {
                    var link = new com.dailyez.Link
                        {
                            Title = e.Value[0].Link_Title,
                            URL = e.Value[0].Link_URL,
                            Target = ""
                        };

                    var oneIsLink = false;

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
                        if (Debug == true)
                            response += "<span style='color:red'>" + PageRank(search, e.Value) + "</span>";
                        response += Utility.GetA(link) + " " + e.Value.Count + " pages ";
                        response += "<span style='font-size:8pt; cursor:pointer; color:blue;' onclick=\"javascript:document.getElementById('resultsDiv" + divIndex + "').style.display = 'block';\">show</span>&nbsp;&nbsp;";
                        response += "<span style='font-size:8pt; cursor:pointer; color:blue;' onclick=\"javascript:document.getElementById('resultsDiv" + divIndex + "').style.display = 'none';\">hide</span><br/>";

                        //  <div id='resultsDiv[X]' style='display:none;font-family:Arial'>
                        response += "<div id='resultsDiv" + divIndex + "' style='display:none;font-family:Arial;line-spacing:20px; margin-left:10px;'>";
                    }

                    //body of the div
                    foreach (var result in e.Value)
                    {
                        if (result.Link_URL.Length > 5)
                        {
                            var innerLink = new com.dailyez.Link
                                {
                                    Title = result.Page_Title,
                                    URL = "" + result.Page_ID,
                                    Target = ""
                                };
                            response += Utility.GetA(innerLink) + "<br/>";
                        }
                        else
                        {
                            var innerLink = new com.dailyez.Link
                                {
                                    Title = result.Link_Title,
                                    URL = "" + result.Page_ID,
                                    Target = ""
                                };
                            // innerLink.Title = result.Link_Title + " - " + service.GetPage(ConfigurationManager.AppSettings["webServiceKey"], result.Page_ID).Title;
                            response += Utility.GetA(innerLink) + "<br/>";
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
                        var link = new com.dailyez.Link();
                        link.Title = e.Value[0].Link_Title;
                        link.URL = e.Value[0].Link_URL;
                        link.Target = "";
                        var pageLink = new com.dailyez.Link();
                        pageLink.Title = e.Value[0].Page_Title;
                        pageLink.URL = "" + e.Value[0].Page_ID;
                        response += Utility.GetA(link) + " @ " + Utility.GetA(pageLink) + "<br/>";
                    }
                    else
                    {
                        var innerLink = new com.dailyez.Link();
                        // innerLink.Title = result.Link_Title + " - " + service.GetPage(ConfigurationManager.AppSettings["webServiceKey"], result.Page_ID).Title;
                        innerLink.Title = "" + e.Value[0].Link_Title;
                        innerLink.URL = "" + e.Value[0].Page_ID;
                        innerLink.Target = "";
                        response += Utility.GetA(innerLink) + "<br/>";

                        //commented out add headers here
                    }

                }
            }




            //add headers
            var srHeaders = Utility.ConvertToLocalSearchResult(headers.ToArray());
            var headerList = new List<SearchResult>(srHeaders);
            headerList.Sort();
            foreach (SearchResult result in headerList)
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


            //-search ads
            SearchService.Ad_Assignment[] ads = _searchService.SearchAds(search);

            foreach (SearchService.Ad_Assignment a in ads)
            {
                if (a.Ad_ID != null)
                    response += "<a href=\"http://www.ezcontrolpanel.com/Ad/ViewAd/" + a.Ad_ID + "\" target=\"_blank\">" + HttpUtility.HtmlEncode(_searchService.GetAd(a.Ad_ID.Value).Name) + "</a><br/>";
            }

            response += "</div>";


            response += "<div class='rightColumnContent' style='border:none;'>";
            response += "</div><div style='clear:right;'></div><div style='background-color:white; margin-top:-80px; text-align:right; border:none;' class='rightColumnContent'>";

            var litAds = new Literal();
            Renderer.RenderJetNettAdsToLiteral(litAds, Request);

            response += "<br/><div style='float:right;line-height:normal;'>" + litAds.Text + "</div>";
            response += "</div>";
            return response;
        }
    }
}