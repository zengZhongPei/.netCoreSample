FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["JWTAuthSample/JWTAuthSample.csproj", "JWTAuthSample/"]
RUN dotnet restore "JWTAuthSample/JWTAuthSample.csproj"
COPY . .
WORKDIR "/src/JWTAuthSample"
RUN dotnet build "JWTAuthSample.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "JWTAuthSample.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "JWTAuthSample.dll"]