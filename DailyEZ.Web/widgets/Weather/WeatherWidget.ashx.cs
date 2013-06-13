using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DailyEZWebApplication.widgets.Weather
{
    /// <summary>
    /// Summary description for WeatherWidget2
    /// </summary>
    public class WeatherWidget2 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/javascript";
            var zipcode = context.Request["zipcode"];
            var extendedWeatherURL = context.Request["extendedWeatherURL"];
            var stackID = context.Request["stackID"];

            var reader = new StreamReader(new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/widgets/weather/weatherTemplate.js"), FileMode.Open, FileAccess.Read, FileShare.Read));
            var template = reader.ReadToEnd();
            reader.Close();

            if (string.IsNullOrEmpty(zipcode))
            {
                context.Response.Write("Invalid Zipcode");
            }
            if (string.IsNullOrEmpty(extendedWeatherURL))
                extendedWeatherURL = "";
            template = template.Replace("[%ZIPCODE%]", zipcode);
            template = template.Replace("[%WEATHERURL%]", extendedWeatherURL);
            template = template.Replace("[%STACK-ID%]", stackID);

            context.Response.Write(template);
      
       

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