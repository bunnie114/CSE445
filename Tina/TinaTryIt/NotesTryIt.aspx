<%@ Page Language="C#" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Notes Service TryIt</title>
</head>
<body style="font-family: Segoe UI, Arial, sans-serif; margin: 30px;">
    <h2>Christina Phan &mdash; Notes RESTful Service TryIt</h2>
    <p>Service: <code>NotesService.ashx</code>. POST (title, text) saves or overwrites a note; GET ?title= returns it as JSON.
       <br /><b>Test:</b> Save "Store 5001" / "Low on bandages" &rarr; Retrieve "store 5001" (case-insensitive) &rarr; Retrieve "nothing" (404 Note not found).</p>

    <h3>Save a Note (POST)</h3>
    <label>Note Title:</label> <input type="text" id="noteTitle" maxlength="100" /><br /><br />
    <label>Note Text:</label><br /><textarea id="noteText" rows="4" cols="40" maxlength="2000"></textarea><br /><br />
    <button type="button" onclick="saveNote()">Save Note</button>

    <hr />
    <h3>Retrieve a Note (GET)</h3>
    <label>Note Title:</label> <input type="text" id="searchTitle" />
    <button type="button" onclick="retrieveNote()">Retrieve Note</button>

    <h3>Service Result</h3>
    <pre id="result">Results will appear here.</pre>

    <script>
        var serviceUrl = '<%= ResolveUrl("~/NotesService.ashx") %>';

        function show(status, body) {
            document.getElementById("result").textContent = "HTTP " + status + "\n" + JSON.stringify(body, null, 2);
        }

        async function saveNote() {
            var title = document.getElementById("noteTitle").value;
            var text = document.getElementById("noteText").value;
            if (!title.trim() || !text.trim()) {
                document.getElementById("result").textContent = "Please enter both a title and note text.";
                return;
            }
            var data = new URLSearchParams();
            data.append("title", title);
            data.append("text", text);
            try {
                var r = await fetch(serviceUrl, { method: "POST", body: data });
                show(r.status, await r.json());
            } catch (err) {
                document.getElementById("result").textContent = "Error connecting to Notes Service: " + err;
            }
        }

        async function retrieveNote() {
            var title = document.getElementById("searchTitle").value;
            if (!title.trim()) {
                document.getElementById("result").textContent = "Please enter a note title.";
                return;
            }
            try {
                var r = await fetch(serviceUrl + "?title=" + encodeURIComponent(title));
                show(r.status, await r.json());
            } catch (err) {
                document.getElementById("result").textContent = "Error connecting to Notes Service: " + err;
            }
        }
    </script>
</body>
</html>
