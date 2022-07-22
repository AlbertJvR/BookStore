FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BookStore.Api/BookStore.Api.csproj", "BookStore.Api/"]
RUN dotnet restore "BookStore.Api/BookStore.Api.csproj"
COPY . .
WORKDIR "/src/BookStore.Api"
RUN dotnet build "BookStore.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookStore.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookStore.Api.dll"]
