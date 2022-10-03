#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ZhaiFanhuaBlog.Api/ZhaiFanhuaBlog.Api.csproj", "ZhaiFanhuaBlog.Api/"]
COPY ["ZhaiFanhuaBlog.Extensions/ZhaiFanhuaBlog.Extensions.csproj", "ZhaiFanhuaBlog.Extensions/"]
COPY ["ZhaiFanhuaBlog.Services/ZhaiFanhuaBlog.Services.csproj", "ZhaiFanhuaBlog.Services/"]
COPY ["ZhaiFanhuaBlog.IServices/ZhaiFanhuaBlog.IServices.csproj", "ZhaiFanhuaBlog.IServices/"]
COPY ["ZhaiFanhuaBlog.Repositories/ZhaiFanhuaBlog.Repositories.csproj", "ZhaiFanhuaBlog.Repositories/"]
COPY ["ZhaiFanhuaBlog.IRepositories/ZhaiFanhuaBlog.IRepositories.csproj", "ZhaiFanhuaBlog.IRepositories/"]
COPY ["ZhaiFanhuaBlog.ViewModels/ZhaiFanhuaBlog.ViewModels.csproj", "ZhaiFanhuaBlog.ViewModels/"]
COPY ["ZhaiFanhuaBlog.Models/ZhaiFanhuaBlog.Models.csproj", "ZhaiFanhuaBlog.Models/"]
COPY ["ZhaiFanhuaBlog.Core/ZhaiFanhuaBlog.Core.csproj", "ZhaiFanhuaBlog.Core/"]
COPY ["ZhaiFanhuaBlog.Utils/ZhaiFanhuaBlog.Utils.csproj", "ZhaiFanhuaBlog.Utils/"]
RUN dotnet restore "ZhaiFanhuaBlog.Api/ZhaiFanhuaBlog.Api.csproj"
COPY . .
WORKDIR "/src/ZhaiFanhuaBlog.Api"
RUN dotnet build "ZhaiFanhuaBlog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ZhaiFanhuaBlog.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZhaiFanhuaBlog.Api.dll"]