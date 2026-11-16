using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace TinaComponents
{
    public class NotesService : IHttpHandler
    {
        private const int MaxTitle = 100;
        private const int MaxText = 2000;

        // One lock for the whole process: XML file reads/writes must not overlap.
        private static readonly object FileLock = new object();

        private static string GetFilePath()
        {
            return HttpContext.Current.Server.MapPath("~/App_Data/notes.xml");
        }

        // Loads the XML file, creating an empty one if it does not exist. Call only inside FileLock.
        private static XDocument LoadNotes()
        {
            string path = GetFilePath();
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                new XDocument(new XElement("Notes")).Save(path);
            }
            return XDocument.Load(path);
        }

        private static XElement FindNote(XDocument doc, string title)
        {
            return doc.Root.Elements("Note").FirstOrDefault(n =>
                string.Equals((string)n.Element("Title"), title, StringComparison.OrdinalIgnoreCase));
        }

        ///Saves a new note, or overwrites the note with the same title (case-insensitive).
        /// Input: title, text. Output: confirmation message.
        public string SaveNotes(string title, string text)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Title and text are required.");

            title = title.Trim();
            if (title.Length > MaxTitle) throw new ArgumentException("Title must be " + MaxTitle + " characters or fewer.");
            if (text.Length > MaxText) throw new ArgumentException("Text must be " + MaxText + " characters or fewer.");

            lock (FileLock)
            {
                XDocument doc = LoadNotes();
                XElement existing = FindNote(doc, title);

                if (existing != null)
                    existing.SetElementValue("Text", text);
                else
                    doc.Root.Add(new XElement("Note", new XElement("Title", title), new XElement("Text", text)));

                doc.Save(GetFilePath());
            }
            return "Note saved successfully.";
        }

        ///Returns the text of the note with this title, or null if none exists.
        public string RetrieveNotes(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Note title is required.");

            lock (FileLock)
            {
                XElement note = FindNote(LoadNotes(), title.Trim());
                return note == null ? null : (string)note.Element("Text");
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            JavaScriptSerializer json = new JavaScriptSerializer();

            try
            {
                if (context.Request.HttpMethod == "POST")
                {
                    string message = SaveNotes(context.Request.Form["title"], context.Request.Form["text"]);
                    context.Response.Write(json.Serialize(new { success = true, message = message }));
                }
                else if (context.Request.HttpMethod == "GET")
                {
                    string title = context.Request.QueryString["title"];
                    string text = RetrieveNotes(title);

                    if (text == null)
                    {
                        context.Response.StatusCode = 404;
                        context.Response.Write(json.Serialize(new { success = false, message = "Note not found." }));
                    }
                    else
                    {
                        context.Response.Write(json.Serialize(new { success = true, title = title.Trim(), text = text }));
                    }
                }
                else
                {
                    context.Response.StatusCode = 405;
                    context.Response.Write(json.Serialize(new { success = false, message = "Method not allowed." }));
                }
            }
            catch (ArgumentException ex)
            {
                context.Response.StatusCode = 400;
                context.Response.Write(json.Serialize(new { success = false, message = ex.Message }));
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                context.Response.Write(json.Serialize(new { success = false, message = "An internal server error occurred." }));
            }
        }

        public bool IsReusable { get { return false; } }
    }
}
