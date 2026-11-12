
<%@ Page Language="C#" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Notes Service TryIt</title>
</head>
<body>
    <h2>Christina Phan - Notes RESTful Service TryIt</h2>

    <h3>Save a Note (POST)</h3>

    <label>Note Title:</label>
    <input type="text" id="noteTitle" />
    <br /><br />

    <label>Note Text:</label>
    <textarea id="noteText"></textarea>
    <br /><br />

    <button type="button" onclick="saveNote()">
        Save Note
    </button>

    <hr />

    <h3>Retrieve a Note (GET)</h3>

    <label>Note Title:</label>
    <input type="text" id="searchTitle" />

    <button type="button" onclick="retrieveNote()">
        Retrieve Note
    </button>

    <h3>Service Result</h3>

    <pre id="result">Results will appear here.</pre>

    <script>
        var serviceUrl = "NotesService.ashx";

        async function saveNote() {
            var title = document.getElementById("noteTitle").value;
            var text = document.getElementById("noteText").value;

            if (!title.trim() || !text.trim()) {
                document.getElementById("result").textContent =
                    "Please enter both a title and note.";
                return;
            }

            var data = new URLSearchParams();
            data.append("title", title);
            data.append("text", text);

            try {
                var response = await fetch(serviceUrl, {
                    method: "POST",
                    headers: {
                        "Content-Type":
                            "application/x-www-form-urlencoded"
                    },
                    body: data
                });

                var result = await response.json();

                document.getElementById("result").textContent =
                    JSON.stringify(result, null, 2);

            } catch (error) {
                document.getElementById("result").textContent =
                    "Error connecting to Notes Service: " + error;
            }
        }

        async function retrieveNote() {
            var title =
                document.getElementById("searchTitle").value;

            if (!title.trim()) {
                document.getElementById("result").textContent =
                    "Please enter a note title.";
                return;
            }

            try {
                var response = await fetch(
                    serviceUrl + "?title=" + encodeURIComponent(title)
                );

                var result = await response.json();

                document.getElementById("result").textContent =
                    JSON.stringify(result, null, 2);

            } catch (error) {
                document.getElementById("result").textContent =
                    "Error connecting to Notes Service: " + error;
            }
        }
    </script>
</body>
</html>
