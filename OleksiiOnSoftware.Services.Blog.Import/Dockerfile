ARG version=latest

FROM oleksiionsoftware/base:${version}
WORKDIR /app
RUN apt-get -qq -y update && apt-get -qq -y install git
ENTRYPOINT [ "/bin/true" ]