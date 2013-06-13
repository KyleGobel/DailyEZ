using System;

namespace DailyEZ.Web.Code
{
    public class SearchResult : IComparable
    {
        private int linkID;

        public int Link_ID
        {
            get { return linkID; }
            set { linkID = value; }
        }
        private int pageID;

        public int Page_ID
        {
            get { return pageID; }
            set { pageID = value; }
        }
        private string linkTitle;

        public string Link_Title
        {
            get { return linkTitle; }
            set { linkTitle = value; }
        }
        private string linkURL;

        public string Link_URL
        {
            get { return linkURL; }
            set { linkURL = value; }
        }
        private string pageTitle;

        public string Page_Title
        {
            get { return pageTitle; }
            set { pageTitle = value; }
        }
        private int folderID;

        public int Folder_ID
        {
            get { return folderID; }
            set { folderID = value; }
        }

        private bool isLink;

        public bool IsLink
        {
            get { return isLink; }
            set { isLink = value; }
        }


        public SearchResult()
        {
            linkID = 0;
            linkTitle = "";
            linkURL = "";
            pageTitle = "";
            pageID = 0;
            isLink = true;
            folderID = 0;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj.GetType() == Type.GetType("searchResult"))
            {
                SearchResult result = (SearchResult)obj;
                return pageTitle.CompareTo(result.Page_Title);
            }
            else
                return 0;
        }

        #endregion
    }

}