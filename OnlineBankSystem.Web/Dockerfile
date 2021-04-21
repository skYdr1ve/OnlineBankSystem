FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["OnlineBankSystem.Web/OnlineBankSystem.Web.csproj", "OnlineBankSystem.Web/"]
COPY ["OnlineBankSystem.Services/OnlineBankSystem.Services.csproj", "OnlineBankSystem.Services/"]
COPY ["OnlineBankSystem.Core/OnlineBankSystem.Core.csproj", "OnlineBankSystem.Core/"]
COPY ["OnlineBankSystem.Infrastructure/OnlineBankSystem.Infrastructure.csproj", "OnlineBankSystem.Infrastructure/"]
RUN dotnet restore "OnlineBankSystem.Web/OnlineBankSystem.Web.csproj"
COPY . .
WORKDIR "/src/OnlineBankSystem.Web"
RUN dotnet build "OnlineBankSystem.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineBankSystem.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineBankSystem.Web.dll"]