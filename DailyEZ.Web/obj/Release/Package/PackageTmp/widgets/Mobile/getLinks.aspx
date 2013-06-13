<%@ Page Language="C#"  %>
<%@ Import Namespace="System.Data.SqlClient" %>



<script runat="server">
    public void Page_Load()
    {
        string pageId = Request["pageId"];
 
        if (string.IsNullOrEmpty(pageId))
        {
            Response.Write("No Page ID found");
            return;
        }
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connStr"]);
        try
        {
            connection.Open();
            var links = GetLinksFromPage(int.Parse(pageId), connection, Response);
            Context.Response.ContentType = "application/json";
           var json = new System.Web.Script.Serialization.JavaScriptSerializer();
            Response.Write(json.Serialize(links));
   
        }
        catch (Exception)
        {
            Response.Write("error");

        }
        finally
        {
           if (connection !=null)
               connection.Close();
        }
    }

    public static DailyEZWebApplication.com.dailyez.Link[] GetLinksFromPage(int pageID, SqlConnection conn, HttpResponse response)
    {
        SqlDataReader reader = null;

        try
        {

            string sSQL = "SELECT ID, Page_ID, Position, Is_Link, Title, URL, Target FROM Links WHERE Page_ID = " + pageID + " ORDER BY Position ASC";
            SqlCommand command = new SqlCommand(sSQL, conn);
            reader = command.ExecuteReader();
            return BuildLink(reader, response);
        }
        catch (Exception ex)
        {
            throw (new Exception("Error Executing GetLinksFromPage() --> " + ex.Message));
        }
        finally
        {
            if (reader != null) reader.Close();
        }
    }
    private static DailyEZWebApplication.com.dailyez.Link[] BuildLink(SqlDataReader reader, HttpResponse response)
    {
        DailyEZWebApplication.com.dailyez.Link link = null;
        ArrayList list = new ArrayList();
        try
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    link = new DailyEZWebApplication.com.dailyez.Link();
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
            else
            {
                response.Write("No Links In this Page");
            }
            return (DailyEZWebApplication.com.dailyez.Link[])list.ToArray(typeof(DailyEZWebApplication.com.dailyez.Link));
        }
        catch (Exception ex)
        {
            throw (new Exception("Error in BuildLink() --> " + ex.Message));
        }
    }
</script>
