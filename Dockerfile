FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Petalaka.Payment.API/Petalaka.Payment.API.csproj", "Petalaka.Payment.API/"]
COPY ["Petalaka.Payment.Service/Petalaka.Payment.Service.csproj", "Petalaka.Payment.Service/"]
COPY ["Petalaka.Payment.Repository/Petalaka.Payment.Repository.csproj", "Petalaka.Payment.Repository/"]
COPY ["Petalaka.Payment.Core/Petalaka.Payment.Core.csproj", "Petalaka.Payment.Core/"]
RUN dotnet restore "Petalaka.Payment.API/Petalaka.Payment.API.csproj"
COPY . .
WORKDIR "/src/Petalaka.Payment.API"
RUN dotnet build "Petalaka.Payment.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Petalaka.Payment.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Petalaka.Payment.API.dll"]
