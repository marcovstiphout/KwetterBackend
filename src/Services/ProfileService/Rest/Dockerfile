#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/ProfileService/Rest/Rest.csproj", "Services/ProfileService/Rest/"]
RUN dotnet restore "Services/ProfileService/Rest/Rest.csproj"
COPY . .
WORKDIR "/src/Services/ProfileService/Rest"
RUN dotnet build "Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rest.dll"]