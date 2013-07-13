using System;
using DailyEZ.Web.Code;
using DailyEZ.Web.ViewModels;

namespace DailyEZ.Web
{
    public partial class Default : BasePage
    {
        public DefaultViewModel DefaultViewModel { get; set; }      
        protected void Page_Load(object sender, EventArgs e)
        {
           DefaultViewModel = new DefaultViewModel(DailyEZObject1, Uow);
        }
    }
}