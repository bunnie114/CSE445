using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DefaultPage
{
    public partial class CaptchaControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Generate a new captcha only on initial page load
            if (!IsPostBack)
            {
                GenerateBtn_Click(sender, e);
            }
        }

        // Public method to check if the user input matches the generated captcha
        public bool IsCaptchaValid()
        {
            String userInput = CaptchaInputTxt.Text; // Get user input

            // Compare the user input with the generated captcha stored in the session
            return userInput == Session["captcha"] as string;
        }

        // Generates a random captcha code and displays it to the user
        protected void GenerateBtn_Click(object sender, EventArgs e)
        {
            Random res = new Random();
            String str = "ABCDEFGHIJKLMNOPQRSTUVWXYabcdefghijklmnopqrstuvwxyz0123456789";
            String captcha = "";

            // Generate a random 5-character captcha
            for (int i = 0; i < 5; i++)
            {
                captcha = captcha + str[res.Next(str.Length)];
            }

            // Store the generated captcha in the session for later verification
            Session["captcha"] = captcha;

            // Call helper method to generate the captcha image and display it on the page
            CaptchaImg.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(generateImg(captcha)); // Display image


            // Display the generated captcha in the label for testing purposes
            // ResultLbl.Text = captcha;
        }

        // Private method to generate the captcha image and return it as a byte array
        private byte[] generateImg(string captcha)
        {
            // Generate an image of the captcha
            Bitmap img = new Bitmap(1, 1); // Creates a canvas for the image
            Graphics graphics = Graphics.FromImage(img);

            // Generate the random fonts and sizes for captcha characters
            List<Font> fonts = new List<Font>();
            List<(int, int)> size = new List<(int, int)>();
            (int, int) dimensions = (0, 0);

            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                Font charFont = new Font("Arial", rnd.Next(12, 17), FontStyle.Bold, GraphicsUnit.Pixel);
                int charWidth = (int)graphics.MeasureString(captcha[i].ToString(), charFont).Width + 10;
                int charHeight = (int)graphics.MeasureString(captcha[i].ToString(), charFont).Height + 20;

                dimensions.Item1 += charWidth;
                dimensions.Item2 = Math.Max(charHeight, dimensions.Item2);

                fonts.Add(charFont);
                size.Add((charWidth, charHeight));
            }

            // Create correctly-sized canvas for the captcha
            img = new Bitmap(img, new Size(dimensions.Item1, dimensions.Item2));
            graphics = Graphics.FromImage(img);
            graphics.Clear(Color.Black);

            // Draw each character using the randomly generated size and positioning
            int x = 5 + rnd.Next(-2, 2);
            for (int i = 0; i < 5; i++)
            {
                int y = (dimensions.Item2 - size[i].Item2 + 20) / 2;
                graphics.DrawString(captcha[i].ToString(), fonts[i], new SolidBrush(Color.White), x, y + rnd.Next(-10, 10));
                x += size[i].Item1 + rnd.Next(-2, 2);
            }
            graphics.Flush();

            // Turn the bitmap to a byte array
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                byte[] imgBytes = ms.ToArray();
                return imgBytes;
            }
        }
    }
}