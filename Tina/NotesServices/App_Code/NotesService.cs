
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Linq;

public class NotesService : IHttpHandler
{
    private string GetFilePath()
    {
        return HttpContext.Current.Server.MapPath(
            "~/App_Data/notes.xml"
        );
    }

    // Create the XML file if it does not exist.
    private XDocument LoadNotes()
    {
        string path = GetFilePath();

        if (!File.Exists(path))
        {
            Directory.CreateDirectory(
                Path.GetDirectoryName(path)
            );

            XDocument newDocument =
                new XDocument(new XElement("Notes"));

            newDocument.Save(path);
        }

        return XDocument.Load(path);
    }

    // Save a new note or update an existing note.
    public string SaveNotes(string title, string text)
    {
        if (String.IsNullOrWhiteSpace(title) ||
            String.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException(
                "Title and text are required."
            );
        }

        string path = GetFilePath();

        XDocument document = LoadNotes();

        XElement existingNote = document.Root.Elements("Note")
            .FirstOrDefault(n =>
                String.Equals(
                    (string)n.Element("Title"),
                    title.Trim(),
                    StringComparison.OrdinalIgnoreCase
                )
            );

        if (existingNote != null)
        {
            existingNote.SetElementValue("Text", text);
        }
        else
        {
            document.Root.Add(
                new XElement("Note",
                    new XElement("Title", title.Trim()),
                    new XElement("Text", text)
                )
            );
        }

        document.Save(path);

        return "Note saved successfully.";
    }

    // Retrieve a note using its title.
    public string RetrieveNotes(string title)
    {
        if (String.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException(
                "Note title is required."
            );
        }

        XDocument document = LoadNotes();

        XElement note = document.Root.Elements("Note")
            .FirstOrDefault(n =>
                String.Equals(
                    (string)n.Element("Title"),
                    title.Trim(),
                    StringComparison.OrdinalIgnoreCase
                )
            );

        if (note == null)
            return null;

        return (string)note.Element("Text");
    }

    // Handle HTTP requests.
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.ContentEncoding =
            System.Text.Encoding.UTF8;

        JavaScriptSerializer serializer =
            new JavaScriptSerializer();

        try
        {
            string result;

            if (context.Request.HttpMethod == "POST")
            {
                string title = context.Request.Form["title"];
                string text = context.Request.Form["text"];

                result = SaveNotes(title, text);

                context.Response.Write(
                    serializer.Serialize(new {
                        success = true,
                        message = result
                    })
                );
            }
            else if (context.Request.HttpMethod == "GET")
            {
                string title = context.Request.QueryString["title"];

                result = RetrieveNotes(title);

                if (result == null)
                {
                    context.Response.StatusCode = 404;

                    context.Response.Write(
                        serializer.Serialize(new {
                            success = false,
                            message = "Note not found."
                        })
                    );
                }
                else
                {
                    context.Response.Write(
                        serializer.Serialize(new {
                            success = true,
                            title = title,
                            text = result
                        })
                    );
                }
            }
            else
            {
                context.Response.StatusCode = 405;

                context.Response.Write(
                    serializer.Serialize(new {
                        success = false,
                        message = "Method not allowed."
                    })
                );
            }
        }
        catch (ArgumentException ex)
        {
            context.Response.StatusCode = 400;

            context.Response.Write(
                serializer.Serialize(new {
                    success = false,
                    message = ex.Message
                })
            );
        }
        catch (Exception)
        {
            context.Response.StatusCode = 500;

            context.Response.Write(
                serializer.Serialize(new {
                    success = false,
                    message = "An internal server error occurred."
                })
            );
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
