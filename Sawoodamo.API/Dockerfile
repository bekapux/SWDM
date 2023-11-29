FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Sawoodamo.API/Sawoodamo.API.csproj", "Sawoodamo.API/"]
RUN dotnet restore "Sawoodamo.API/Sawoodamo.API.csproj"
COPY . .
WORKDIR "/src/Sawoodamo.API"
RUN dotnet build "Sawoodamo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sawoodamo.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sawoodamo.API.dll"]
