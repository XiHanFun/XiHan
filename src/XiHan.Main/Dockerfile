#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["XiHan.Main/XiHan.Main.csproj", "XiHan.Main/"]
COPY ["XiHan.Web/XiHan.Web.csproj", "XiHan.Web/"]
COPY ["XiHan.Services/XiHan.Services.csproj", "XiHan.Services/"]
COPY ["XiHan.Repositories/XiHan.Repositories.csproj", "XiHan.Repositories/"]
COPY ["XiHan.ViewModels/XiHan.ViewModels.csproj", "XiHan.ViewModels/"]
COPY ["XiHan.Models/XiHan.Models.csproj", "XiHan.Models/"]
COPY ["XiHan.Core/XiHan.Core.csproj", "XiHan.Core/"]
COPY ["XiHan.Utils/XiHan.Utils.csproj", "XiHan.Utils/"]
RUN dotnet restore "XiHan.Main/XiHan.Main.csproj"
COPY . .
WORKDIR "/src/XiHan.Main"
RUN dotnet build "XiHan.Main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XiHan.Main.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XiHan.Main.dll"]