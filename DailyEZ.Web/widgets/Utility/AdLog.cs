using System;
using System.Configuration;
using System.IO;
using System.Linq;
using DailyEZ.Web.Code;

namespace DailyEZ.Web.widgets.Utility
{
    public class AdLog : BasePage
    {
        public void Log(int adID, int pageID, int clientID, string zipcode, bool registered)
        {
            var viewTrack = new com.dailyez.Ad_View_Tracker();
            viewTrack.Ad_ID = adID;
            viewTrack.Client_ID = clientID;
            viewTrack.Date = System.DateTime.Now;
            viewTrack.Page_ID = pageID;
   
            viewTrack.Zipcode = zipcode;

            if (registered)
                WebService.TrackAdView(ConfigurationManager.AppSettings["webServiceKey"], viewTrack);
        }
        public void DeleteOldViewLogs()
        {
            //get all the todayClicks files
            var files = Directory.GetFiles(Server.MapPath("~/"), "todayViews*");

            //make sure not to delete todays
            var filePath = Server.MapPath("~/todayViews" + System.DateTime.Now.Day + ".log");

            foreach (var file in files.Where(file => file != filePath))
            {
                File.Delete(file);
            }

        }
        public bool ShouldRecordView(string ipAddress, int adID)
        {
            //check for multiple clicks
            var filePath = Server.MapPath("~/todayViews" + System.DateTime.Now.Day + ".log");
            
            if (string.IsNullOrEmpty(ipAddress))
                return false;

            var key = ipAddress + ":::" + adID;

            //if the date file already exists
            if (File.Exists(filePath))
            {
                FileStream fileStream = null;
                TextReader textReader = null;
                try
                {
                    //create a file stream
                    fileStream = new FileStream(filePath, FileMode.Open);

                    //create the text reader
                    textReader = new StreamReader(fileStream);

                    //read the contents of the file
                    var fileContents = textReader.ReadToEnd();

                    //put the keys into a list
                    var keyList = fileContents.Split(new char[] { ',' });

                    //check to see if the list contains this ip address, if it does return false::don't record the tracker
                    if (keyList.Contains(key))
                    {
                        //already insered
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    //clean up
                    if (textReader != null)
                        textReader.Close();
                    if (fileStream != null)
                        fileStream.Close();
                }

            } //if (File.Exists(filePath))


            //write the IPAddress to file
            try
            {
                using (TextWriter writer = new StreamWriter(filePath, true))
                {
                    writer.Write("," + ipAddress + ":::" + adID);
                    writer.Close();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}