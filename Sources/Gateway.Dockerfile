FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /src

COPY Gateway/EServi.Api/*.csproj Gateway/EServi.Api/

RUN dotnet restore Gateway/EServi.Api

COPY Gateway Gateway/

WORKDIR /src/Gateway/EServi.Api

RUN dotnet build -c Release -o /src/build
RUN dotnet publish -c Release -o /src/publish --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app

RUN sed -i "s/MinProtocol = TLSv1.2/MinProtocol = TLSv1/" "/etc/ssl/openssl.cnf"
RUN sed -i "s/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g" "/etc/ssl/openssl.cnf"

COPY --from=build-env /src/publish .

ENV TZ America/Lima
ENTRYPOINT ["dotnet", "EServi.Api.dll"]