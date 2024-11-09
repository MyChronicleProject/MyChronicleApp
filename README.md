# MyChronicle
MyChronicle is a web app that let's you compose your own family tree!

## Technologies
The project is built with ASP.NET Core.

## Development
Develop the project Microsoft.NET.Sdk and .NET 8.0.

### Updating database
After changes in the model files (in MyChronicle.Domain project), creating a migration file is required:
```
dotnet ef migrations add <migration name> -s MyChronicle.API -p MyChronicle.Infrastructure
```

To apply migrations to database (after modyfing models or after pulling from repository), run:
```
 dotnet ef database update -s MyChronicle.API -p MyChronicle.Infrastructure
```
