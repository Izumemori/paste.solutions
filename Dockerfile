FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS prep
WORKDIR /src
COPY . .
RUN dotnet restore "PasteSolutions/PasteSolutions.csproj"

FROM prep AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "PasteSolutions.dll"]