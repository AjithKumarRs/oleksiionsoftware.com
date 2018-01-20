FROM microsoft/aspnetcore-build:2.0
WORKDIR /app

COPY . ./
RUN dotnet restore && dotnet publish -c Release -o out
RUN apt-get update && apt-get -y install git

ENTRYPOINT [ "/bin/true" ]