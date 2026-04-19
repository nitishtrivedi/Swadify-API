# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Swadify-API/Swadify-API.csproj", "Swadify-API/"]
RUN dotnet restore "Swadify-API/Swadify-API.csproj"

COPY . .
WORKDIR "/src/Swadify-API"
RUN dotnet build "Swadify-API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "Swadify-API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Swadify-API.dll"]