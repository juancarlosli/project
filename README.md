# Web Services and Backend Course

## Contact
- Esteban Solano @stvansolano 
- Email: stvansolano@outlook.com 
- Blog: http://stvansolano.github.io/blog

## Installation & tooling

- [DotNet Core @2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2)
- [VS 2019](https://visualstudio.microsoft.com/vs/)

VS Code extensions (Ctrl+Shift+P > Install Extensions):

### C# Extensions & Tooling

`dotnet tool install --global dotnet-aspnet-codegenerator --version 2.2.3`

### VS Code extensions
- C# Extensions
- C#
- C# Snippets
- C# FixFormat
- EditorConfig for VS Code

Chrome extensions
- [JSON Formatter](https://chrome.google.com/webstore/search/json)

## Links Session-01

## Session 01 - .NET Core Basics

.NET Core ~2.2 Commands

```
dotnet --info
dotnet new web
dotnet restore
dotnet build
dotnet run
```

-VS Code Configuration

```
dotnet watch run
```

## Links session 02 

- https://petstore.swagger.io/

- Postman - https://getpostman.com

- [Routing](https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2#optional)

## Session 03 Links:

- [CORS](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS)

## Session 04 Links:
- [EntityFramework Core](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/#update-the-database)
- [JSON Patch](http://jsonpatch.com/)
- [EF Migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/#update-the-database)

## Session 05 Links (Docker):
- [Docker](https://docs.docker.com/docker-for-windows/)

## Session 06 Links:

- [JWT](https://jwt.io/introduction/)
- [CosmosDB](https://docs.microsoft.com/en-us/azure/cosmos-db/introduction)

OData links:
- [OData 1](https://www.odata.org/getting-started/learning-odata-on-postman/)
- [OData 2](https://www.odata.org/getting-started/understand-odata-in-6-steps/)
- [GraphQL](https://developer.okta.com/blog/2019/04/16/graphql-api-with-aspnetcore)

GraphQL links:
[GraphQL 1](https://graphql.org/)

## Practice indications

### Session 01

1) Create an HomeController MVC
2) Display a View (HTML) as default home page root (https://localhost:44357/)
3) Consume the `/products` endpoint

Commit + push to GitHub at: https://github.com/[your-user]/backend-services-course-cenfotec/tree/session-01/practice/practice-01

### Session 02

1) Create a `CategoriesController` in the Web API
2) Add a `Get` endpoint
3) Add some Category records to database
4) Fetch Categories from database

Commit + push to GitHub at: https://github.com/[your-user]/backend-services-course-cenfotec/tree/session-04/practice/practice-02
