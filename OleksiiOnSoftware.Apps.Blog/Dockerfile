FROM node
WORKDIR /app

RUN apt-get update && apt-get -y install supervisor

COPY . ./

RUN cd /app/OleksiiOnSoftware.Apps.Blog/ && npm install --only=dev

EXPOSE 80 3000 9229
ENTRYPOINT ["/usr/bin/supervisord", "-c", "/app/OleksiiOnSoftware.Apps.Blog/supervisord.conf"]
