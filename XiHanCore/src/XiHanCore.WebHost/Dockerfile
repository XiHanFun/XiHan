#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR ./xihan-framework/src
COPY ["XiHanCore.WebHost/XiHanCore.WebHost.csproj", "XiHanCore.WebHost/"]
COPY ["XiHanCore.WebCore/XiHanCore.WebCore.csproj", "XiHanCore.WebCore/"]
COPY ["XiHanCore.Jobs/XiHanCore.Jobs.csproj", "XiHanCore.Jobs/"]
COPY ["XiHanCore.Services/XiHanCore.Services.csproj", "XiHanCore.Services/"]
COPY ["XiHanCore.Repositories/XiHanCore.Repositories.csproj", "XiHanCore.Repositories/"]
COPY ["XiHanCore.Models/XiHanCore.Models.csproj", "XiHanCore.Models/"]
COPY ["XiHanCore.Infrastructures/XiHanCore.Infrastructures.csproj", "XiHanCore.Infrastructures/"]
COPY ["XiHanCore.Utils/XiHanCore.Utils.csproj", "XiHanCore.Utils/"]
COPY ["XiHanCore.Subscriptions/XiHanCore.Subscriptions.csproj", "XiHanCore.Subscriptions/"]
RUN dotnet restore "XiHanCore.WebHost/XiHanCore.WebHost.csproj"
COPY . .
WORKDIR "/xihan-framework/src/XiHanCore.WebHost"
RUN dotnet build "XiHanCore.WebHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XiHanCore.WebHost.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XiHanCore.WebHost.dll"]