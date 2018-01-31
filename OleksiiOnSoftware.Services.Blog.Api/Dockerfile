ARG version=latest

FROM oleksiionsoftware/base:${version}
WORKDIR /app

ENV ASPNETCORE_URLS=http://*:5001

ENTRYPOINT ["/usr/bin/supervisord", "-c", "/app/OleksiiOnSoftware.Services.Blog.Api/supervisord.conf"]
