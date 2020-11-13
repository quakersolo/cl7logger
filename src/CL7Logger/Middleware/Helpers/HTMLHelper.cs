namespace CL7Logger.Middleware.Helpers
{
    public static class HTMLHelper
    {
        public const string LogHTMLTable = @"
<html>
    <head>
        <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"" integrity=""sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"" crossorigin=""anonymous"">
        <link href=""http://maxcdn.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css"" rel=""stylesheet"">
    </head>
    <body>
        <h1 style=""margin: 0;display: inline-block;"">{1}</h1>
        <hr/>
        <p>List of logs entries at {3}</p>
        <a href=""{2}"">Refresh</a>
        <br/><br/>
        <table class=""table table-sm"">
            <thead>
                <tr>
                    <th scope=""col"">Id</th>
                    <th scope=""col"">TraceId</th>
                    <th scope=""col"">Type</th>
                    <th scope=""col"">Message</th>
                    <th scope=""col"">Created at (UTC)</th>
                    <th scope=""col"">User@Host</th>
                </tr>
            </thead>
            <tbody>
                {0}
            </tbody>
        </table>
    </body>
</html>
";

        public const string LogHTMLRow = @"
<tr class=""{0}"">
    <th scope=""row"">
        <a href=""{8}"">{1}</a>
    </th>
    <td>
        <a href=""{9}"">{2}</a>
    </td>
    <td>{3}</td>
    <td>{4}</td>
    <td>{5}</td>
    <td>{6}@{7}</td>
</tr>";
    }
}
