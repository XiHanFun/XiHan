#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ZhaiFanhuaBlog.WebApi/ZhaiFanhuaBlog.WebApi.csproj", "ZhaiFanhuaBlog.WebApi/"]
COPY ["ZhaiFanhuaBlog.Services/ZhaiFanhuaBlog.Services.csproj", "ZhaiFanhuaBlog.Services/"]
COPY ["ZhaiFanhuaBlog.IServices/ZhaiFanhuaBlog.IServices.csproj", "ZhaiFanhuaBlog.IServices/"]
COPY ["ZhaiFanhuaBlog.ViewModels/ZhaiFanhuaBlog.ViewModels.csproj", "ZhaiFanhuaBlog.ViewModels/"]
COPY ["ZhaiFanhuaBlog.Models/ZhaiFanhuaBlog.Models.csproj", "ZhaiFanhuaBlog.Models/"]
COPY ["ZhaiFanhuaBlog.Utils/ZhaiFanhuaBlog.Utils.csproj", "ZhaiFanhuaBlog.Utils/"]
COPY ["ZhaiFanhuaBlog.Repositories/ZhaiFanhuaBlog.Repositories.csproj", "ZhaiFanhuaBlog.Repositories/"]
COPY ["ZhaiFanhuaBlog.IRepositories/ZhaiFanhuaBlog.IRepositories.csproj", "ZhaiFanhuaBlog.IRepositories/"]
RUN dotnet restore "ZhaiFanhuaBlog.WebApi/ZhaiFanhuaBlog.WebApi.csproj"
COPY . .
WORKDIR "/src/ZhaiFanhuaBlog.WebApi"
RUN dotnet build "ZhaiFanhuaBlog.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ZhaiFanhuaBlog.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZhaiFanhuaBlog.WebApi.dll"]