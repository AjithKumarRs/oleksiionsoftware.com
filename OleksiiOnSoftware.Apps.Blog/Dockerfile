FROM node
WORKDIR /app

RUN apt-get -qq -y update && apt-get -qq -y install supervisor

COPY . ./

ENV NODE_ENV=production
ENV API_PUBLIC_HOSTNAME=http://oleksiionsoftware.com
ENV API_INTERNAL_HOSTNAME=http://api:5001

WORKDIR /app/OleksiiOnSoftware.Apps.Blog
RUN npm install --only=dev --quiet
RUN npm run build

EXPOSE 80 3000 9229
ENTRYPOINT ["/usr/bin/supervisord", "-c", "/app/OleksiiOnSoftware.Apps.Blog/supervisord.conf"]
