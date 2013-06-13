using System.Collections.Generic;
using System.IO;
using System.Web;

namespace DailyEZ.Web.widgets.ButtonStackSelector
{
    /// <summary>
    /// Summary description for ButtonStackSelectorWorker
    /// </summary>
    public class ButtonStackSelectorWorker : IHttpHandler
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

            const string templateFile = "buttonStackSelectorTemplate.html";
            var template = "";
            using (var fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(templateFile), FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    template = reader.ReadToEnd();
                }
            }

            var leftHtml = ""; var rightHtml = "";

            GetStacksHtml(stacksList, out leftHtml, out rightHtml);

            template = template.Replace("[%LEFT-LINKS%]", leftHtml);
            template = template.Replace("[%RIGHT-LINKS%]", rightHtml);
            template = template.Replace("[%TITLE%]", title);
            template = template.Replace("[%WIDGET-ID%]", widgetID + "");

            context.Response.Write(template);


        }

        public void GetStacksHtml(List<com.dailyez.Stack> stacks, out string leftHtml, out string rightHtml)
        {
            var html = "";
            leftHtml = "";
            rightHtml = "";
            if (stacks.Count == 0)
            {
                leftHtml = "Nothing to choose from!  Contact JetNett support";
                return;
            }


            //if theres odd number of stacks, add one to the seperator so first column holds the extra, otherwise split evenly
            var seperator = (stacks.Count % 2) == 1 ? (stacks.Count / 2 + 1) : (stacks.Count / 2);

            var counter = 0;
            
            foreach (var stack in stacks)
            {
                html += string.Format("<button class='btn {3}' stack-id='{0}' stack-height='{2}' style='width:169px;margin-bottom:8px;text-align:left;'>{1}</button><br/>", stack.ID, HttpUtility.HtmlEncode(stack.Display_Name), stack.Height, _buttonClass);
                counter++;

                if (counter == seperator)
                {
                    leftHtml = html;
                    html = "";
                }
                 
            }
            rightHtml = html;

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