# Use the official .NET image.
# https://hub.docker.com/_/microsoft-dotnet
FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["DateParsingService.csproj", "."]
RUN dotnet restore "./DateParsingService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DateParsingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DateParsingService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DateParsingService.dll"]
