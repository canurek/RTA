#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RTA.ConsumerOne/RTA.ConsumerOne.csproj", "RTA.ConsumerOne/"]
RUN dotnet restore "RTA.ConsumerOne/RTA.ConsumerOne.csproj"
COPY . .
WORKDIR "/src/RTA.ConsumerOne"
RUN dotnet build "RTA.ConsumerOne.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RTA.ConsumerOne.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RTA.ConsumerOne.dll"]
