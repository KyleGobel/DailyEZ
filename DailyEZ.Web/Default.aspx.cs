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

            var stack = Uow.Stacks.GetById(currentLeftSideStackID);

            return stack == null ? "" : stack.RawWidgetsString.Replace("###", "");
            
            

        }
        public string MiddleStack()
        {
            var currentMiddleStackID = BasePage.DailyEZObject.Default_Middle_Stack;

            var overrideID = Utility.GetIntFromCookie(Request, "middleStackOverride");
            if (overrideID > 0)
                currentMiddleStackID = overrideID;

            if (currentMiddleStackID != null)
            {
                var stack = Uow.Stacks.GetById(currentMiddleStackID.Value);
                return stack.RawWidgetsString.Replace("###", "");
            }
            return "";
        }
        public string RightStack()
        {
            int currentRightSideStackID = BasePage.DailyEZObject.Default_Right_Stack;
            var stack = Uow.Stacks.GetById(currentRightSideStackID);

            return stack.RawWidgetsString.Replace("###", "");

        }
    }
}