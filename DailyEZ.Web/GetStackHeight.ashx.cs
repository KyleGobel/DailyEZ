using System.Web;

namespace DailyEZ.Web
{
    /// <summary>
    /// Summary description for GetStackHeight
    /// </summary>
    public class GetStackHeight : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
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