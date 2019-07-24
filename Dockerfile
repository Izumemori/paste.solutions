FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 6472

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["PasteSolutions/PasteSolutions.csproj", "PasteSolutions/"]
COPY libs/* libs/
RUN dotnet restore "PasteSolutions/PasteSolutions.csproj"
COPY . .
WORKDIR "/src/PasteSolutions"
RUN dotnet build "PasteSolutions.csproj" -c Release -o /app
COPY PasteSolutions/static-pages/* /app/static-pages/

FROM build AS publish
RUN dotnet publish "PasteSolutions.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PasteSolutions.dll"]