FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Toy.EfCore.Learning/Toy.EfCore.Learning.csproj", "Toy.EfCore.Learning/"]
RUN dotnet restore "Toy.EfCore.Learning/Toy.EfCore.Learning.csproj"
COPY . .
WORKDIR "/src/Toy.EfCore.Learning"
RUN dotnet build "Toy.EfCore.Learning.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Toy.EfCore.Learning.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Toy.EfCore.Learning.dll"]
