#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /xihan-framework/src
COPY ["XiHan.WebHost/XiHan.WebHost.csproj", "XiHan.WebHost/"]
COPY ["XiHan.WebCore/XiHan.WebCore.csproj", "XiHan.WebCore/"]
COPY ["XiHan.Jobs/XiHan.Jobs.csproj", "XiHan.Jobs/"]
COPY ["XiHan.Services/XiHan.Services.csproj", "XiHan.Services/"]
COPY ["XiHan.Repositories/XiHan.Repositories.csproj", "XiHan.Repositories/"]
COPY ["XiHan.Models/XiHan.Models.csproj", "XiHan.Models/"]
COPY ["XiHan.Infrastructures/XiHan.Infrastructures.csproj", "XiHan.Infrastructures/"]
COPY ["XiHan.Utils/XiHan.Utils.csproj", "XiHan.Utils/"]
COPY ["XiHan.Subscriptions/XiHan.Subscriptions.csproj", "XiHan.Subscriptions/"]
RUN dotnet restore "XiHan.WebHost/XiHan.WebHost.csproj"
COPY . .
WORKDIR "/xihan-framework/src/XiHan.WebHost"
RUN dotnet build "XiHan.WebHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XiHan.WebHost.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XiHan.WebHost.dll"]