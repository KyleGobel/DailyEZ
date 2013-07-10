using System.Collections.Generic;
using System.IO;
using System.Web;
using JetNettApi.Data;
using JetNettApi.Data.Helpers;
using Ninject;

namespace DailyEZ.Web.widgets.ButtonStackSelector
{
    /// <summary>
    /// Summary description for ButtonStackSelectorWorker
    /// </summary>
    public class ButtonStackSelectorWorker : IHttpHandler
    {
        string _buttonClass = "";


        public JetNettApiUow Uow { get; set; }

       
        public void ProcessRequest(HttpContext context)
        {
            Uow = new JetNettApiUow(new RepositoryProvider(new RepositoryFactories()));
            context.Response.ContentType = "text/html";

            var stacks = context.Request["stacks"];
            var title = context.Request["title"];
            var sWidgetID = context.Request["widgetID"];
            _buttonClass = context.Request["buttonColor"];


            var widgetID = 1;

            int.TryParse(sWidgetID, out widgetID);

            if (widgetID == 0)
                widgetID = 1;

            if (stacks == null)
                return;
            var iStacks = stacks.Split(new[] { ',' });

            var stacksList = new List<JetNettApi.Models.Stack>();
            foreach (var s in iStacks)
            {
                var stackID = 0;
                int.TryParse(s, out stackID);
                if (stackID > 0)
                {
                    stacksList.Add(Uow.Stacks.GetById(stackID));
                }
            }

            const string templateFile = "buttonStackSelectorTemplate.html";
            var template = "";
            using (var fs = new FileStream(HttpContext.Current.Server.MapPath(templateFile), FileMode.Open, FileAccess.Read, FileShare.Read))
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

        public void GetStacksHtml(List<JetNettApi.Models.Stack> stacks, out string leftHtml, out string rightHtml)
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
                html += string.Format("<button class='btn {3}' stack-id='{0}' stack-height='{2}' style='width:169px;margin-bottom:8px;text-align:left;'>{1}</button><br/>", stack.Id, HttpUtility.HtmlEncode(stack.DisplayName), stack.Height, _buttonClass);
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