using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace DailyEZWebApplication.widgets.Weather
{
    /// <summary>
    /// Summary description for WeatherWorker
    /// </summary>
    public class WeatherWorker : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            var zipcode = context.Request["zipcode"];

            var reader = new StreamReader(new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/widgets/weather/weatherTemplate.html"), FileMode.Open, FileAccess.Read, FileShare.Read));
            var template = reader.ReadToEnd();
            reader.Close();
            var weather = GetWeather(zipcode);

            if (weather != null)
            {
                template = template.
                    Replace("[%CITY%]", weather[0]).
                    Replace("[%STATE%]", weather[1]).
                    Replace("[%TEMP%]", weather[2]).
                    Replace("[%CONDITIONS%]", weather[3]).
                    Replace("[%LOW%]", weather[5]);

                //These are now handled in the javascript file so we get the clients date/time rather than the servers
                //  Replace("[%DAY%]", DateTime.Now.DayOfWeek.ToString()).
                //  Replace("[%DATE%]", DateTime.Now.ToShortDateString());

                var graphic = "";
                var iCode = int.Parse(weather[4]);
                switch(iCode)
                {
                    case 0:
                    case 2:
                        graphic = "na.png";
                        break;
             
                    case 8: //freezing rain
                    case 10: //freezing rain
                    case 9:
                    case 11:
                    case 12:
                        graphic = "_rain.png";
                        break;
                
                    case 19:
                    case 20: //fog
                    case 21: //hazy
                    case 22:
                    case 23:
                        graphic = "_fog.png";
                        break;
                    case 24: //wind
                    case 25: //fair
                        graphic = "_cloud.png";
                        break;
              
              

                 
                    case 31: //clear night
                        graphic = "moon.png";
                        break;
                    case 32: //sunny
                        graphic = "_sunny.png";
                        break;
                    case 33: // fair night
                    case 34: //fair day
                        graphic = "_fair.png";
                        break;
                    case 36: //hot
                        graphic = "_sunny.png";
                        break;
                  
                    case 40: //scattered showers
                        graphic = "_rain.png";
                        break;
                    
                    case 27: //mostly cloudly night
                    case 28: //mostly cloudy day
                    case 29: //partly cloudy night
                    case 30: //partly cloudy day
                    case 26: //mostly cloudy
                    case 44: //partly cloudy
                        graphic = "_partly-cloudy.png";
                        break;
                    case 1:
                    case 3:
                    case 4:
                    case 37: //isolated tstorms
                    case 38: //scattered tstorms
                    case 39: //scattered tstorms
                    case 45: //thundershowers
                    case 47: //iso tstorms
                        graphic = "_lightning.png";
                        break;

                    case 5: //rainy snow
                    case 6:
                    case 7:
                    case 13: //fluries
                    case 14: //snow showers
                    case 15:
                    case 16:
                    case 17: //blizzard
                    case 18: //sleet
                    case 42:
                    case 35:
                    case 41: //heavy snow
                    case 43: //heavy snow
                    case 46: //snow showers
                        graphic = "_snow.png";
                        break;
                    default:
                        graphic = "na.png";
                        break;


                }
                graphic = "widgets/Weather/images/" + graphic;
        
                template = template.Replace("[%GRAPHIC%]", graphic) + "";
            }
            context.Response.Write(template);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public static string[] GetWeather(string zipCode)
        {
            XmlTextReader textReader = new XmlTextReader("http://weather.yahooapis.com/forecastrss?p=" + zipCode + "&u=f");
            string City = "", State = "", Code = "", URL = "", Temperature = "", Condition = "";
            string High = "", Low = "";

            textReader.MoveToContent();
            string temp = textReader.Name;
            do
            {
                textReader.Read();
                temp = textReader.Name;
            }
            while (temp != "channel");

            while (temp == "channel")
            {
                textReader.Read();

                if (textReader.Name == "yweather:location")
                {
                    City = textReader.GetAttribute("city");
                    State = textReader.GetAttribute("region");
                }
                else if (textReader.Name == "item")
                {
                    do
                    {
                        textReader.Read();
                        if (textReader.Name == "yweather:condition")
                        {
                            Condition = textReader.GetAttribute("text");
                            Temperature = textReader.GetAttribute("temp");
                            Code = textReader.GetAttribute("code");
                        }
                        else if (textReader.Name == "yweather:forecast")
                        {
                            if (Convert.ToInt32(textReader.GetAttribute("date").Substring(0, 2)) == System.DateTime.Now.Day)
                            {
                                High = textReader.GetAttribute("high");
                                Low = textReader.GetAttribute("low");
                            }
                        }
                    }
                    while (textReader.Name != "item");
                    temp = "";
                }
            }
            textReader.Close();
            URL = "http://us.i1.yimg.com/us.yimg.com/i/us/we/52/" + Code + ".gif";
            string[] value = new string[7];
            value[0] = City;
            value[1] = State;
            value[2] = Temperature;
            value[3] = Condition;
            value[4] = Code;
            value[5] = High;
            value[6] = Low;
            return value;
        }
    }
}