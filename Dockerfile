# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ARG DB_CONNECTION_STRING
ENV ConnectionStrings__DefaultConnection=$DB_CONNECTION_STRING

# Use the .NET SDK for building our application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["digital-signage-cms-backend-api.csproj", "./"]
RUN dotnet restore "./digital-signage-cms-backend-api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./digital-signage-cms-backend-api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "./digital-signage-cms-backend-api.csproj" -c Release -o /app/publish

# Final stage / image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "digital-signage-cms-backend-api.dll"]
