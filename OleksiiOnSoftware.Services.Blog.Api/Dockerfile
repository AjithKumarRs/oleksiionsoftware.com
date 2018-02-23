ARG version=latest

# Reference base image with build artifacts
FROM oleksiionsoftware/base:${version} as build-env

# Use minimal availiable image for runtime
FROM microsoft/aspnetcore:2.0 as latest

WORKDIR /app

ENV ASPNETCORE_URLS=http://*:5001

COPY --from=build-env /app/OleksiiOnSoftware.Services.Blog.Api/out .

# Link tools to corresponding file/folder locations 
RUN ln -s /tools/wait-for-it /usr/local/bin/wait-for-it && \
    ln -s /tools/vsdbg /vsdbg

ENTRYPOINT dotnet OleksiiOnSoftware.Services.Blog.Api.dll
