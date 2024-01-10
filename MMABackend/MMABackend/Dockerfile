FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . "/src/MMABackend"
RUN dotnet restore "MMABackend/MMABackend.csproj"
WORKDIR "/src/MMABackend"
RUN dotnet build "MMABackend.csproj" -o ./debug
WORKDIR /src/MMABackend/debug
RUN mkdir -p ./database
ENTRYPOINT ["dotnet", "MMABackend.dll"]