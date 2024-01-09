FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL stage=builder
WORKDIR /src
COPY . .

ARG PROJ_NAME
ARG BUILD_CONFIGURATION
WORKDIR "/src/$PROJ_NAME"

RUN dotnet build "$PROJ_NAME.csproj" -c "$BUILD_CONFIGURATION"
RUN dotnet publish "/src/$PROJ_NAME/$PROJ_NAME.csproj" --no-build -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
