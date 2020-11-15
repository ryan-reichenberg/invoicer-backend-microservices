FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
# Copy necessary files and restore as distinct layer
COPY ./ ./

RUN dotnet restore

WORKDIR /app/UserService

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/UserService/out ./

# Expose ports
EXPOSE 5100
ENV ASPNETCORE_URLS http://*:5100

# Start
ENTRYPOINT ["dotnet", "UserService.dll"]