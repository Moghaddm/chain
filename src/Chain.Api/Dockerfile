#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Chain.Api/Chain.Api.csproj", "src/Chain.Api/"]
COPY ["src/Chain.Application/Chain.Application.csproj", "src/Chain.Application/"]
COPY ["src/Chain.Application.Contract/Chain.Application.Contract.csproj", "src/Chain.Application.Contract/"]
COPY ["src/Chain.Domain/Chain.Domain.csproj", "src/Chain.Domain/"]
COPY ["src/Chain.Infrastructure/Chain.Infrastructure.csproj", "src/Chain.Infrastructure/"]
RUN dotnet restore "./src/Chain.Api/./Chain.Api.csproj"
COPY . .
WORKDIR "/src/src/Chain.Api"
RUN dotnet build "./Chain.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Chain.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chain.Api.dll"]