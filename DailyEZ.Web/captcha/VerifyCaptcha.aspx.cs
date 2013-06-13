using System;

namespace DailyEZ.Web.captcha
{
    public partial class VerifyCaptcha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["textValue"] == Session["randomStr"].ToString())
                    Response.Write("true");
            }
            catch (Exception)
            {
               
            }
            

        }
    }
}