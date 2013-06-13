using System;
using DailyEZ.Web.Code;

namespace DailyEZ.Web
{
    public partial class StackFrame : BasePage
    {
        int _stackID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            var sStackId = Request["stackID"];

            int.TryParse(sStackId, out _stackID);
        }
        public string GetStack()
        {
            var stack = BasePage.WebService.GetStack(_stackID);

            if (stack != null)
                return stack.Widgets.Replace("###", "");
            else return "Null Stack ID - " + _stackID;
        }
    }
}