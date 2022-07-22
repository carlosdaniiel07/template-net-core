FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ["TemplateNetCore.Api/TemplateNetCore.Api.csproj", "TemplateNetCore.Api/"]
COPY ["TemplateNetCore.Domain/TemplateNetCore.Domain.csproj", "TemplateNetCore.Domain/"]
COPY ["TemplateNetCore.Infra/TemplateNetCore.Infra.csproj", "TemplateNetCore.Infra/"]
COPY ["TemplateNetCore.Repository/TemplateNetCore.Repository.csproj", "TemplateNetCore.Repository/"]
COPY ["TemplateNetCore.Repository.EF/TemplateNetCore.Repository.EF.csproj", "TemplateNetCore.Repository.EF/"]
COPY ["TemplateNetCore.Application/TemplateNetCore.Application.csproj", "TemplateNetCore.Application/"]

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