# Use the ASP.NET Core runtime image as the base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Create the wwwroot/Images directory and set permissions in the final stage
# This will ensure permissions are set properly in the final runtime image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project file and restore as distinct layers
COPY ["BookLoanApp.csproj", "."]
RUN dotnet restore "./BookLoanApp.csproj"

# Copy the rest of the application and build
COPY . .
RUN dotnet build "./BookLoanApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "./BookLoanApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
# Ensure the directory exists and set proper permissions
RUN mkdir -p /app/wwwroot/Images && chmod -R 777 /app/wwwroot

# Copy the published output to the final image
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookLoanApp.dll"]