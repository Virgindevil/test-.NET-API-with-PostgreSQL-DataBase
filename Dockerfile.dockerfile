FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/SellerReturnApi/SellerReturnApi.csproj", "src/SellerReturnApi/"]
RUN dotnet restore "src/SellerReturnApi/SellerReturnApi.csproj"
COPY . .
WORKDIR "/src/src/SellerReturnApi"
RUN dotnet build "SellerReturnApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SellerReturnApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SellerReturnApi.dll"]