FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./

# Copy dependencies
COPY ../Invoicer.Common/*.csproj ../Invoicer.Common

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY --from=build-env /app/out .

# Expose ports
EXPOSE 5100
ENV ASPNETCORE_URLS http://*:5100

# Start
ENTRYPOINT ["dotnet", "UserService.dll"]