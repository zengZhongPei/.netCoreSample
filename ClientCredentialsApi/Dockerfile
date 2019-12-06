FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["ClientCredentialsApi/ClientCredentialsApi.csproj", "ClientCredentialsApi/"]
RUN dotnet restore "ClientCredentialsApi/ClientCredentialsApi.csproj"
COPY . .
WORKDIR "/src/ClientCredentialsApi"
RUN dotnet build "ClientCredentialsApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ClientCredentialsApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ClientCredentialsApi.dll"]