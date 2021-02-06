FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /src

COPY Helpers/EServi.Consul/*.csproj Helpers/EServi.Consul/
COPY Helpers/EServi.Helper/*.csproj Helpers/EServi.Helper/
COPY Helpers/EServi.RabbitMq/*.csproj Helpers/EServi.RabbitMq/

COPY Services/Auth/EServi.Microservices.Auth/*.csproj Services/Auth/EServi.Microservices.Auth/
COPY Services/Auth/EServi.Microservices.Auth.Domain/*.csproj Services/Auth/EServi.Microservices.Auth.Domain/
COPY Services/Auth/EServi.Microservices.Auth.Infrastructure.Jwt/*.csproj Services/Auth/EServi.Microservices.Auth.Infrastructure.Jwt/
COPY Services/Auth/EServi.Microservices.Auth.Infrastructure.MongoDb/*.csproj Services/Auth/EServi.Microservices.Auth.Infrastructure.MongoDb/
COPY Services/Auth/EServi.Microservices.Auth.Infrastructure.RabbitMq.Publishers/*.csproj Services/Auth/EServi.Microservices.Auth.Infrastructure.RabbitMq.Publishers/
COPY Services/Auth/EServi.Microservices.Auth.Infrastructure.RabbitMq.Subscribers/*.csproj Services/Auth/EServi.Microservices.Auth.Infrastructure.RabbitMq.Subscribers/
COPY Services/Auth/EServi.Microservices.Auth.IoC/*.csproj Services/Auth/EServi.Microservices.Auth.IoC/
COPY Services/Auth/EServi.Microservices.Auth.UseCase/*.csproj Services/Auth/EServi.Microservices.Auth.UseCase/

RUN dotnet restore Services/Auth/EServi.Microservices.Auth

COPY Helpers/EServi.Consul Helpers/EServi.Consul/
COPY Helpers/EServi.Helper Helpers/EServi.Helper/
COPY Helpers/EServi.RabbitMq Helpers/EServi.RabbitMq/

COPY Services/Auth/ Services/Auth/

WORKDIR /src/Services/Auth/EServi.Microservices.Auth

RUN dotnet build -c Release -o /src/build
RUN dotnet publish -c Release -o /src/publish --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app

RUN sed -i "s/MinProtocol = TLSv1.2/MinProtocol = TLSv1/" "/etc/ssl/openssl.cnf"
RUN sed -i "s/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g" "/etc/ssl/openssl.cnf"

COPY --from=build-env /src/publish .

ENV TZ America/Lima
ENTRYPOINT ["dotnet", "EServi.Microservices.Auth.dll"]