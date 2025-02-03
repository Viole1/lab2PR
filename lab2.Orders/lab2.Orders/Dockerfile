FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
RUN apt-get update && apt-get install -y curl
CMD /bin/bash
USER $APP_UID
WORKDIR /app
EXPOSE 5206

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./lab2.Orders.csproj", "lab2.Orders/"]
RUN dotnet restore "lab2.Orders/lab2.Orders.csproj"

WORKDIR "/src/lab2.Orders"
COPY . .
RUN dotnet build "lab2.Orders.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "lab2.Orders.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5206
ENV ASPNETCORE_URLS=http://+:5206

ENTRYPOINT ["dotnet", "lab2.Orders.dll"]
