FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/KweetService/Rest/Kwetter.Services.ProfileService.Rest.csproj", "Rest/"]
COPY ["Services/KweetService/Domain/Kwetter.Services.ProfileService.Domain.csproj", "Domain/"]
COPY ["Services/KweetService/Persistence/Kwetter.Services.ProfileService.Persistence.csproj", "Persistence/"]
COPY ["Services/KweetService/Application/Kwetter.Services.ProfileService.Application.csproj", "Application/"]

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