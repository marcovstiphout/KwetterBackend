FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/AuthService/Rest/Kwetter.Services.AuthService.Rest.csproj", "Services/AuthService/Rest/"]
COPY ["Services/AuthService/Persistence/Kwetter.Services.AuthService.Persistence.csproj", "Services/AuthService/Persistence/"]
COPY ["Services/AuthService/Application/Kwetter.Services.AuthService.Application.csproj", "Services/AuthService/Application/"]
COPY ["Services/Shared/KwetterShared/Kwetter.Services.Shared.csproj", "Services/Shared/KwetterShared/"]
COPY ["Services/AuthService/Domain/Kwetter.Services.AuthService.Domain.csproj", "Services/AuthService/Domain/"]

RUN dotnet restore "Rest/Kwetter.Services.AuthService.Rest.csproj"
COPY . .
WORKDIR "/src/Services/AuthService/Rest"
RUN dotnet build "Kwetter.Services.AuthService.Rest.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Kwetter.Services.AuthService.Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kwetter.Services.AuthService.Rest.dll"]