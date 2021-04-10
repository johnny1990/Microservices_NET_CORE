#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Microservices_NET_CORE/Microservices_NET_CORE.csproj", "Microservices_NET_CORE/"]
RUN dotnet restore "Microservices_NET_CORE/Microservices_NET_CORE.csproj"
COPY . .
WORKDIR "/src/Microservices_NET_CORE"
RUN dotnet build "Microservices_NET_CORE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices_NET_CORE.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices_NET_CORE.dll"]