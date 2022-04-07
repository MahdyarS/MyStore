#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Endpoint.Site/Endpoint.Site.csproj", "Endpoint.Site/"]
RUN dotnet restore "Endpoint.Site/Endpoint.Site.csproj"
COPY . .
WORKDIR "/src/Endpoint.Site"
RUN dotnet build "Endpoint.Site.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Endpoint.Site.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Endpoint.Site.dll"]