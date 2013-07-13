using System.Web;
using DailyEZ.Web.Code;
using JetNettApi.Data.Contracts;

namespace DailyEZ.Web.ViewModels
{
    public class DefaultViewModel
    {
        public JetNettApi.Models.DailyEZ DailyEZ { get; set; }
        public IJetNettApiUnitOfWork Uow { get; set; }
        public DefaultViewModel(JetNettApi.Models.DailyEZ dailyEZ, IJetNettApiUnitOfWork unitOfWork)
        {
            DailyEZ = dailyEZ;
            Uow = unitOfWork;
        }
        public string LeftStackHtml()
        {
            if (DailyEZ == null || Uow == null)
                return "";
            var currentLeftSideStackID = DailyEZ.DefaultLeftStack;
            if (HttpContext.Current != null)
            {
                var overrideID = Utility.GetIntFromCookie(HttpContext.Current.Request, "leftStackOverride");
                if (overrideID > 0)
                    currentLeftSideStackID = overrideID;
            }
            var stack = Uow.Stacks.GetById(currentLeftSideStackID);
            return stack == null ? "" : stack.RawWidgetsString.Replace("###", "");
        }
        public string MiddleStackHtml()
        {
            if (DailyEZ == null || Uow == null)
                return "";
            var currentMiddleStackID = DailyEZ.DefaultMiddleStack;
            if (HttpContext.Current != null)
            {
                var overrideID = Utility.GetIntFromCookie(HttpContext.Current.Request, "middleStackOverride");
                if (overrideID > 0)
                    currentMiddleStackID = overrideID;
            }
            if (currentMiddleStackID != null)
            {
                var stack = Uow.Stacks.GetById(currentMiddleStackID.Value);
                return stack == null ? "" : stack.RawWidgetsString.Replace("###", "");
            }
            return "";
        }
        public string RightStackHtml()
        {
            if (DailyEZ == null || Uow == null)
                return "";
            var currentRightSideStackID = DailyEZ.DefaultRightStack;
            var stack = Uow.Stacks.GetById(currentRightSideStackID);
            return stack == null ? "" : stack.RawWidgetsString.Replace("###", "");
        } 
    }
}