using System;
using DailyEZ.Web.Code;

namespace DailyEZ.Web
{
    public partial class Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string LeftStack()
        {

            int currentLeftSideStackID = BasePage.DailyEZObject.Default_Left_Stack;

            var overrideID = Utility.GetIntFromCookie(Request, "leftStackOverride");
            if (overrideID > 0)
                currentLeftSideStackID = overrideID;

            var stack = BasePage.WebService.GetStack(currentLeftSideStackID);
            
            return stack.Widgets.Replace("###", "");

        }
        public string MiddleStack()
        {
            var currentMiddleStackID = BasePage.DailyEZObject.Default_Middle_Stack;


            var overrideID = Utility.GetIntFromCookie(Request, "middleStackOverride");
            if (overrideID > 0)
                currentMiddleStackID = overrideID;

            if (currentMiddleStackID != null)
            {
                var stack = BasePage.WebService.GetStack(currentMiddleStackID.Value);
                return stack.Widgets.Replace("###", "");
            }
            return "";
        }
        public string RightStack()
        {
            int currentRightSideStackID = BasePage.DailyEZObject.Default_Right_Stack;
            var stack = BasePage.WebService.GetStack(currentRightSideStackID);

            return stack.Widgets.Replace("###", "");

        }
    }
}