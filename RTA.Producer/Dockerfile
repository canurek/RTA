#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RTA.Producer/RTA.Producer.csproj", "RTA.Producer/"]
RUN dotnet restore "RTA.Producer/RTA.Producer.csproj"
COPY . .
WORKDIR "/src/RTA.Producer"
RUN dotnet build "RTA.Producer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RTA.Producer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RTA.Producer.dll"]
