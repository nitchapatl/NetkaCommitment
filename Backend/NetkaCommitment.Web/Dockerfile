#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 8888
EXPOSE 9999

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NetkaCommitment.Web/NetkaCommitment.Web.csproj", "NetkaCommitment.Web/"]
COPY ["NetkaCommitment.Business/NetkaCommitment.Business.csproj", "NetkaCommitment.Business/"]
COPY ["NetkaCommitment.Common/NetkaCommitment.Common.csproj", "NetkaCommitment.Common/"]
COPY ["NetkaCommitment.Data/NetkaCommitment.Data.csproj", "NetkaCommitment.Data/"]
COPY ["NetkaCommitment.Repository/NetkaCommitment.Repository.csproj", "NetkaCommitment.Repository/"]
RUN dotnet restore "NetkaCommitment.Web/NetkaCommitment.Web.csproj"
COPY . .
WORKDIR "/src/NetkaCommitment.Web"
RUN dotnet build "NetkaCommitment.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetkaCommitment.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetkaCommitment.Web.dll"]