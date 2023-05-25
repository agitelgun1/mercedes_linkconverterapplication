## Mercedes Link Converter Backend Applicantion
Based on 
- [More about .NET Core](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Dapper](https://dapper-tutorial.net/dapper)
- [Swagger Ui](https://swagger.io/tools/swagger-ui/)
- [SQLite](https://www.sqlite.org/index.html)

### Run local with CLI
1. Clone or download this repository to local machine.
2. Install [.NET Core SDK for your platform](https://www.microsoft.com/net/core#windowscmd) if didn't install yet.
3. `cd src/LinkConverterApplication`
4. `dotnet restore`
5. `dotnet run`

### Run on Rider
1. Install [Rider for your platform](https://www.jetbrains.com/rider/) if didn't install yet.
2. Open project
3. Debug => Start debugging
4. You can use swagger ui for your local test => [link](https://localhost:5001/swagger/index.html)

### About Project

There is three end point in project. 
1. [convertweburltodeeplink](https://localhost:5001/api/convertlink/convertweburltodeeplink?webUrl=https%3A%2F%2Fwww.sample-site.com%2Fkarriere%2Fberufserfahrene%2Fdirekteinstieg%2F) : This endpoint that converts web URLs to deeplinks. 
2. [redirection](http://localhost:5000/api/convertlink/redirection?deeplinkUrl=http%3A%2F%2Fsample.site%2Fjobs) : This endpoint redirect deep url to web url.
2. [customurl](http://localhost:5000/api/convertlink/customurl?webUrl=https%3A%2F%2Fwww.sample-site.com%2Fkarriere%2Fjobsuche%2F&deeplinkUrl=http%3A%2F%2Fsample.site%2Fjobs) : Allow the users to pick custom shortened URL.

SQL lite database is located on Database folder. Scripts for database in the `cd src/LinkConverterApplication/Scripts`
