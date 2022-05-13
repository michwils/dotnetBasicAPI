FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything
COPY . ./
WORKDIR /app/src
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app/src
COPY --from=build-env /app/src/out .
COPY --from=build-env /app/src/*.env .

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS http://*:5555
EXPOSE 5555

ENTRYPOINT ["dotnet", "dotnetBasicAPI.dll"]