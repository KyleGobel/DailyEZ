using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.Web.UI;

namespace DailyEZ.Web.captcha
{
    public partial class Image : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var objBmp = new Bitmap(120, 30);
            var objGraphics = Graphics.FromImage(objBmp);
            objGraphics.Clear(Color.Green);
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            //' Configure font to use for text
            var objFont = new Font("Arial", 21, FontStyle.Bold);
            string randomStr = "";
            var myIntArray = new int[5];
            int x;
            //That is to create the random # and add it to our string 
            var autoRand = new Random();
            for (x = 0; x < 5; x++)
            {
                myIntArray[x] = Convert.ToInt32(autoRand.Next(0, 9));
                randomStr += (myIntArray[x].ToString(CultureInfo.InvariantCulture));
            }
            //This is to add the string to session cookie, to be compared later
            Session.Add("randomStr", randomStr);
            //' Write out the text
            objGraphics.DrawString(randomStr, objFont, Brushes.White, 3, 3);
            //' Set the content type and return the image
            Response.ContentType = "image/GIF";
            objBmp.Save(Response.OutputStream, ImageFormat.Gif);
            objFont.Dispose();
            objGraphics.Dispose();
            objBmp.Dispose();
        }
    }
}