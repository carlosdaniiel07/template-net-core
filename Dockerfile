FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

COPY ["TemplateNetCore.Api/TemplateNetCore.Api.csproj", "TemplateNetCore.Api/"]
COPY ["TemplateNetCore.Application/TemplateNetCore.Application.csproj", "TemplateNetCore.Application/"]
COPY ["TemplateNetCore.Domain/TemplateNetCore.Domain.csproj", "TemplateNetCore.Domain/"]
COPY ["TemplateNetCore.Infrastructure.Data.Sql/TemplateNetCore.Infrastructure.Data.Sql.csproj", "TemplateNetCore.Infrastructure.Data.Sql/"]
COPY ["TemplateNetCore.Infrastructure.Data.MongoDb/TemplateNetCore.Infrastructure.Data.MongoDb.csproj", "TemplateNetCore.Infrastructure.Data.MongoDb/"]
COPY ["TemplateNetCore.Infrastructure.IoC/TemplateNetCore.Infrastructure.IoC.csproj", "TemplateNetCore.Infrastructure.IoC/"]
COPY ["TemplateNetCore.Infrastructure.Service/TemplateNetCore.Infrastructure.Service.csproj", "TemplateNetCore.Infrastructure.Service/"]

RUN dotnet restore "TemplateNetCore.Api/TemplateNetCore.Api.csproj"

COPY . .

WORKDIR "/src/TemplateNetCore.Api"

RUN dotnet build "TemplateNetCore.Api.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "TemplateNetCore.Api.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "TemplateNetCore.Api.dll"]