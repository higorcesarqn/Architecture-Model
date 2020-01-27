#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/backend/Api/Api.csproj", "src/backend/Api/"]
COPY ["src/backend/AdministracaoUsuario.Domain/AdministracaoUsuario.Domain.csproj", "src/backend/AdministracaoUsuario.Domain/"]
COPY ["src/backend/Infra.CrossCutting.Identity/Infra.CrossCutting.Identity.csproj", "src/backend/Infra.CrossCutting.Identity/"]
COPY ["src/backend/Infra.CrossCutting.Identity.Configurations/Infra.CrossCutting.Identity.Configurations.csproj", "src/backend/Infra.CrossCutting.Identity.Configurations/"]
COPY ["src/backend/Core/Core.csproj", "src/backend/Core/"]
COPY ["src/backend/Domain/Domain.csproj", "src/backend/Domain/"]
COPY ["src/backend/Migrate/Migrate.csproj", "src/backend/Migrate/"]
COPY ["src/backend/Infra.EntityFramework.PostgreSQL/Infra.EntityFramework.PostgreSQL.csproj", "src/backend/Infra.EntityFramework.PostgreSQL/"]
COPY ["src/backend/Infra.CrossCutting.EntityFramework.Extensions/Infra.EntityFramework.Extensions.csproj", "src/backend/Infra.CrossCutting.EntityFramework.Extensions/"]
COPY ["src/backend/EventSourcing.EntityFramework.PostgreSQL/EventSourcing.EntityFramework.PostgreSQL.csproj", "src/backend/EventSourcing.EntityFramework.PostgreSQL/"]
COPY ["src/backend/EventSourcing/EventSourcing.csproj", "src/backend/EventSourcing/"]
COPY ["src/backend/Infra.CrossCutting.IoC/Infra.CrossCutting.IoC.csproj", "src/backend/Infra.CrossCutting.IoC/"]
COPY ["src/backend/AdministracaoUsuario.IoC/AdministracaoUsuario.IoC.csproj", "src/backend/AdministracaoUsuario.IoC/"]
COPY ["src/backend/AdministracaoUsuario.Infrastructure/AdministracaoUsuario.Infrastructure.csproj", "src/backend/AdministracaoUsuario.Infrastructure/"]
COPY ["src/backend/Infra.EntityFramework.UnitOfWork/Infra.EntityFramework.UnitOfWork.csproj", "src/backend/Infra.EntityFramework.UnitOfWork/"]
COPY ["src/backend/Infra.CrossCutting.Bus/Infra.CrossCutting.Bus.csproj", "src/backend/Infra.CrossCutting.Bus/"]
COPY ["src/backend/Tango/Tango/Tango.csproj", "src/backend/Tango/Tango/"]
COPY ["src/backend/Infra.CrossCutting.Jwt/Infra.CrossCutting.Jwt.csproj", "src/backend/Infra.CrossCutting.Jwt/"]
RUN dotnet restore "src/backend/Api/Api.csproj"
COPY . .
WORKDIR "/src/src/backend/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]