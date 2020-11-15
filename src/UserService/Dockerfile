FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
# Copy necessary files and restore as distinct layer
COPY *.sln .
COPY UserService/*.csproj ./UserService/
COPY InvoicingService/*.csproj ./InvoicingService/
COPY LoggingService/*.csproj ./LoggingService/
COPY AuthenticationService/*.csproj ./AuthenticationService/
COPY APIGateway/*.csproj ./APIGateway/
COPY PaymentService/*.csproj ./PaymentService/
COPY NotificationService/*.csproj ./NotificationService/
COPY Invoicer.Common/*.csproj ./Invoicer.Common/
COPY Invoicer.Common.Tests/*.csproj ./Invoicer.Common.Tests/

RUN dotnet restore

# copy everything else and build app
COPY UserService/. ./UserService/
COPY Invoicer.Common/. ./Invoicer.Common/

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