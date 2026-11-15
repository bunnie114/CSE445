
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace AryanTryIt
{
    public partial class WordFilterTryIt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Call the WordFilter REST service.
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string input = txtInput.Text;

            if (string.IsNullOrWhiteSpace(input))
            {
                lblResult.Text = "Please enter some text.";
                return;
            }

            // Local service URL for testing.
            string url = "https://webstrarportal.fulton.asu.edu/sites/website59/Page8/Service1.svc/filter";

            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string jsonInput = serializer.Serialize(input);
                byte[] data = Encoding.UTF8.GetBytes(jsonInput);

                HttpWebRequest request =
                    (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "application/json";
                request.ContentLength = data.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                using (WebResponse response = request.GetResponse())
                using (StreamReader reader =
                    new StreamReader(response.GetResponseStream()))
                {
                    string jsonResult = reader.ReadToEnd();
                    string result = serializer.Deserialize<string>(jsonResult);

                    lblResult.Text = Server.HtmlEncode(result);
                }
            }
            catch (WebException)
            {
                lblResult.Text = "Unable to connect to WordFilter service.";
            }
        }
    }
}
