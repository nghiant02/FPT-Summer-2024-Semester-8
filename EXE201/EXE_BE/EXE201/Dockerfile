#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EXE201/EXE201.csproj", "EXE201/"]
COPY ["EXE201.BLL/EXE201.BLL.csproj", "EXE201.BLL/"]
COPY ["EXE201.DAL/EXE201.DAL.csproj", "EXE201.DAL/"]
RUN dotnet restore "./EXE201/EXE201.csproj"
COPY . .
WORKDIR "/src/EXE201"
RUN dotnet build "./EXE201.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EXE201.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EXE201.dll"]