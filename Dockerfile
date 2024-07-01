# Use the .NET SDK to build the solution
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY ["src/TenetTest.Api/TenetTest.Api.csproj", "TenetTest.Api/"]
COPY ["src/TenetTest.Application/TenetTest.Application.csproj", "TenetTest.Application/"]
COPY ["src/TenetTest.Domain/TenetTest.Domain.csproj", "TenetTest.Domain/"]
COPY ["src/TenetTest.Contracts/TenetTest.Contracts.csproj", "TenetTest.Contracts/"]
COPY ["src/TenetTest.Infrastructure/TenetTest.Infrastructure.csproj", "TenetTest.Infrastructure/"]
COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]
RUN dotnet restore "TenetTest.Api/TenetTest.Api.csproj"

# Copy everything else and build the application
COPY . ../
WORKDIR /src/TenetTest.Api
RUN dotnet build "TenetTest.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
ENV ASPNETCORE_HTTPS_PORTS=7250
EXPOSE 5001
EXPOSE 7250
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TenetTest.Api.dll"]