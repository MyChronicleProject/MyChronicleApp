# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the main API project file and restore dependencies
COPY MyChronicle.API/MyChronicle.API.csproj MyChronicle.API/
COPY MyChronicle.Application/MyChronicle.Application.csproj MyChronicle.Application/
COPY MyChronicle.Domain/MyChronicle.Domain.csproj MyChronicle.Domain/
COPY MyChronicle.Infrastructure/MyChronicle.Infrastructure.csproj MyChronicle.Infrastructure/
RUN dotnet restore MyChronicle.API/MyChronicle.API.csproj

# Copy the entire solution and build the application
COPY . .
WORKDIR /src/MyChronicle.API
RUN dotnet publish -c Release -o /app/publish

# Use the official runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port your application listens on
EXPOSE 8080

# Set the entry point for the application
ENTRYPOINT ["dotnet", "MyChronicle.API.dll"]