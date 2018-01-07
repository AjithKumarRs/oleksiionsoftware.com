FROM microsoft/aspnetcore-build:2.0
WORKDIR /app

COPY . ./
RUN dotnet restore && dotnet publish -c Release -o out
RUN apt-get update && apt-get -y install supervisor

ENV ASPNETCORE_URLS=http://*:5001

ENTRYPOINT ["/usr/bin/supervisord", "-c", "/app/OleksiiOnSoftware.Services.Blog.Api/supervisord.conf"]
