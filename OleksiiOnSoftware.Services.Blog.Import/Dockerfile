ARG version=latest

# Reference base image with build artifacts
FROM oleksiionsoftware/base:${version} as build-env

# Use minimal availiable image for runtime
FROM microsoft/aspnetcore:2.0

WORKDIR /app

RUN apt-get -qq -y update && apt-get -qq -y install git

COPY --from=build-env /app/OleksiiOnSoftware.Services.Blog.Import/out .

RUN ln -s /tools/wait-for-it /usr/local/bin/wait-for-it && \
    ln -s /tools/vsdbg /vsdbg

VOLUME [ "/app" ]

VOLUME [ "/data" ]

VOLUME [ "/tools" ]

ENTRYPOINT ["tail -f /dev/null"]