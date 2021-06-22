FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/ProfileService/Rest/Kwetter.Services.ProfileService.Rest.csproj", "Services/ProfileService/Rest/"]
COPY ["Services/ProfileService/Persistence/Kwetter.Services.ProfileService.Persistence.csproj", "Services/ProfileService/Persistence/"]
COPY ["Services/ProfileService/Application/Kwetter.Services.ProfileService.Application.csproj", "Services/ProfileService/Application/"]
COPY ["Services/Shared/KwetterShared/Kwetter.Services.Shared.csproj", "Services/Shared/KwetterShared/"]
COPY ["Services/ProfileService/Domain/Kwetter.Services.ProfileService.Domain.csproj", "Services/ProfileService/Domain/"]

RUN dotnet restore "Rest/Kwetter.Services.ProfileService.Rest.csproj"
COPY . .
WORKDIR "/src/Services/ProfileService/Rest"
RUN dotnet build "Kwetter.Services.ProfileService.Rest.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Kwetter.Services.ProfileService.Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kwetter.Services.ProfileService.Rest.dll"]