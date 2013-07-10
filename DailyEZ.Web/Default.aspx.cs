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
            if (DailyEZObject1 == null)
                return "";
            int currentLeftSideStackID = BasePage.DailyEZObject1.DefaultLeftStack;

            var overrideID = Utility.GetIntFromCookie(Request, "leftStackOverride");
            if (overrideID > 0)
                currentLeftSideStackID = overrideID;

            var stack = Uow.Stacks.GetById(currentLeftSideStackID);

            return stack == null ? "" : stack.RawWidgetsString.Replace("###", "");
            
            

        }
        public string MiddleStack()
        {
            if (DailyEZObject1 == null)
                return "";
            var currentMiddleStackID = BasePage.DailyEZObject1.DefaultMiddleStack;

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
            if (DailyEZObject1 == null)
                return "";
            int currentRightSideStackID = BasePage.DailyEZObject1.DefaultRightStack;
            var stack = Uow.Stacks.GetById(currentRightSideStackID);

            return stack.RawWidgetsString.Replace("###", "");

        }
    }
}