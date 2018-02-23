# Use node image as build environment
FROM node AS build-env

WORKDIR /app

COPY ./OleksiiOnSoftware.Services.Blog ./

ENV NODE_ENV=production
ENV API_PUBLIC_HOSTNAME=https://oleksiionsoftware.com
ENV API_INTERNAL_HOSTNAME=http://api:5001

RUN npm install --only=dev --quiet
RUN npm run build

# Use node image as prod environment
FROM node

WORKDIR /app

COPY --from=build-env /app .

EXPOSE 80 3000 9229
ENTRYPOINT npm start
