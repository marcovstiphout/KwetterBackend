#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/AuthService/Rest/Kwetter.Services.AuthService.Rest.csproj", "Services/AuthService/Rest/"]
COPY ["Services/AuthService/Application/Kwetter.Services.AuthService.Application.csproj", "Services/AuthService/Application/"]
COPY ["Services/AuthService/Domain/Kwetter.Services.AuthService.Domain.csproj", "Services/AuthService/Domain/"]
COPY ["Services/Shared/KwetterShared/Kwetter.Services.Shared.csproj", "Services/Shared/KwetterShared/"]
COPY ["Services/AuthService/Infrastructure/Kwetter.Services.AuthService.Infrastructure.csproj", "Services/AuthService/Infrastructure/"]
COPY ["Services/AuthService/Persistence/Kwetter.Services.AuthService.Persistence.csproj", "Services/AuthService/Persistence/"]
RUN dotnet restore "Services/AuthService/Rest/Kwetter.Services.AuthService.Rest.csproj"
COPY . .
WORKDIR "/src/Services/AuthService/Rest"
RUN dotnet build "Kwetter.Services.AuthService.Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kwetter.Services.AuthService.Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kwetter.Services.AuthService.Rest.dll"]