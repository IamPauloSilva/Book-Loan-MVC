# Etapa 1: Construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
EXPOSE 8080
ENV PGHOST=viaduct.proxy.rlwy.net
ENV PGPORT=20177
ENV PGUSER=postgres
ENV PGPASSWORD=dITRyJVqrrSIvtQNHqHtlzFvhIzMlFCA
ENV PGDATABASE=railway

# Copiar o arquivo .csproj e restaurar as dependências
COPY ["BookLoanApp.csproj", "."]
RUN dotnet restore "BookLoanApp.csproj"

# Copiar o restante dos arquivos e construir o projeto
COPY . .
RUN dotnet build "BookLoanApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa 2: Publicação
FROM build AS publish
RUN dotnet publish "BookLoanApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa 3: Imagem Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookLoanApp.dll"]