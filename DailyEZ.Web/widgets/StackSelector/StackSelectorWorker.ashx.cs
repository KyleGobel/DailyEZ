using System.Collections.Generic;
using System.IO;
using System.Web;

namespace DailyEZ.Web.widgets.StackSelector
{
    /// <summary>
    /// Summary description for StackSelectorWorker
    /// </summary>
    public class StackSelectorWorker : IHttpHandler
    {
        string _buttonClass = "";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            var stacks = (string)context.Request["stacks"];
            var title = context.Request["title"];
            var sWidgetID = context.Request["widgetID"];
            _buttonClass = context.Request["buttonColor"];


            var widgetID = 1;

            int.TryParse(sWidgetID, out widgetID);

            if (widgetID == 0)
                widgetID = 1;

            if (stacks == null)
                return;
            string[] iStacks = stacks.Split(new char[] { ',' });
            var service = new com.dailyez.Service();

            var stacksList = new List<com.dailyez.Stack>();
            foreach (var s in iStacks)
            {
                var stackID = 0;
                int.TryParse(s, out stackID);
                if (stackID > 0)
                {
                    stacksList.Add(service.GetStack(stackID));
                }
            }


            var reader = new StreamReader(new FileStream(System.Web.HttpContext.Current.Server.MapPath("stackSelectorTemplate.html"), FileMode.Open, FileAccess.Read, FileShare.Read));
            var template = reader.ReadToEnd();
            reader.Close();


            template = template.Replace("[%STACKS%]", GetStacksHtml(stacksList));
            template = template.Replace("[%TITLE%]", title);
            template = template.Replace("[%WIDGET-ID%]", widgetID + "");
            template = template.Replace("[%BUTTON-CLASS%]", _buttonClass);

            context.Response.Write(template);

        }

        public string GetStacksHtml(List<com.dailyez.Stack> stacks)
        {
            var html = "";
            
            if (stacks.Count == 0)
            {
                return "Nothing to choose from!  Contact JetNett support";
            }


            //if theres odd number of stacks, add one to the seperator so first column holds the extra, otherwise split evenly
            var seperator = (stacks.Count % 2) == 1 ? (stacks.Count /2 +1) : (stacks.Count/2);

            var counter = 0;
            html = "<div style='float:left'>";
            foreach(var stack in stacks)
            {
                html += string.Format("<button class='btn {2}' stack-id='{0}' style='width:225px;margin-bottom:8px;'>{1}</button><br/>", stack.ID, HttpUtility.HtmlEncode(stack.Display_Name), _buttonClass);
                counter++;

                if (counter == seperator)
                    html+= "</div><div style='float:left; margin-left:15px;'>";
            }
            html += "</div>";

            return html;
        
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