FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Ensure proper permissions for the wwwroot directory
RUN mkdir -p /app/wwwroot/Images && \
    chmod -R 777 /app/wwwroot

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BookLoanApp.csproj", "."]
RUN dotnet restore "./BookLoanApp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./BookLoanApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BookLoanApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookLoanApp.dll"]
