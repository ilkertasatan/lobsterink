FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY /src .
WORKDIR /LobsterInk.Adventure.Api
RUN dotnet restore "LobsterInk.Adventure.Api.csproj"
RUN dotnet build "LobsterInk.Adventure.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LobsterInk.Adventure.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "LobsterInk.Adventure.Api.dll"]
