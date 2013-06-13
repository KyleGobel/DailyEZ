using System.Web;

namespace DailyEZ.Web.widgets.Weather
{
    /// <summary>
    /// Summary description for GetStackHeight
    /// </summary>
    public class GetStackHeight : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var stackID = context.Request["stackID"];

            var service = new com.dailyez.Service();

            var stack = service.GetStack(int.Parse(stackID));

            context.Response.Write(stack.Height);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}