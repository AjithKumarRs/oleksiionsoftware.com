FROM node
WORKDIR /app

RUN apt-get update && apt-get -y install supervisor

COPY . ./

ENV NODE_ENV=production
ENV API_PUBLIC_HOSTNAME=http://oleksiionsoftware.com
ENV API_INTERNAL_HOSTNAME=http://api:5001

RUN cd /app/OleksiiOnSoftware.Apps.Blog/ && npm install --only=dev
RUN cd /app/OleksiiOnSoftware.Apps.Blog/ && npm run build

EXPOSE 80 3000 9229
ENTRYPOINT ["/usr/bin/supervisord", "-c", "/app/OleksiiOnSoftware.Apps.Blog/supervisord.conf"]
