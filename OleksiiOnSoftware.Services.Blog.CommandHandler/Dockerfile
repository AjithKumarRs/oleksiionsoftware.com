ARG version=latest

FROM oleksiionsoftware/base:${version}
WORKDIR /app
ENTRYPOINT ["/usr/bin/supervisord", "-c", "/app/OleksiiOnSoftware.Services.Blog.CommandHandler/supervisord.conf"]
