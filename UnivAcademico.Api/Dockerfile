# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar los archivos del proyecto y restaurar dependencias
COPY ["UnivAcademico.Api/*.csproj", "UnivAcademico.Api/"]
COPY ["UnivAcademico.Application/*.csproj", "UnivAcademico.Application/"]
COPY ["UnivAcademico.Domain/*.csproj", "UnivAcademico.Domain/"]
COPY ["UnivAcademico.Infrastructure/*.csproj", "UnivAcademico.Infrastructure/"]

RUN dotnet restore "UnivAcademico.Api/UnivAcademico.Api.csproj"

# Copiar todo y compilar
COPY . .
RUN dotnet publish "UnivAcademico.Api/UnivAcademico.Api.csproj" -c Release -o /publish

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "UnivAcademico.Api.dll"]