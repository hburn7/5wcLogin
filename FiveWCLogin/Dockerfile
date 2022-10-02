FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FiveWCLogin/FiveWCLogin.csproj", "FiveWCLogin/"]
RUN dotnet restore "FiveWCLogin/FiveWCLogin.csproj"
COPY . .
WORKDIR "/src/FiveWCLogin"
RUN dotnet build "FiveWCLogin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FiveWCLogin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FiveWCLogin.dll"]
