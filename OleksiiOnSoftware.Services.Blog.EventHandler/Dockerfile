ARG version=latest

# Reference base image with build artifacts
FROM oleksiionsoftware/base:${version} as build-env

# Use minimal availiable image for runtime
FROM microsoft/aspnetcore:2.0

WORKDIR /app

COPY --from=build-env /app/OleksiiOnSoftware.Services.Blog.EventHandler/out .

ENTRYPOINT dotnet OleksiiOnSoftware.Services.Blog.EventHandler.dll
