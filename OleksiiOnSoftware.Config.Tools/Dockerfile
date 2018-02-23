FROM ubuntu:16.04 AS dev

WORKDIR /app

RUN apt-get -qq -y update && apt-get -qq -y install \
    curl \
    unzip

RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /app/vsdbg

ADD https://raw.githubusercontent.com/vishnubob/wait-for-it/master/wait-for-it.sh /app
RUN mv wait-for-it.sh wait-for-it && chmod +x wait-for-it

# Use alpine for prod image
FROM alpine:latest AS prod

COPY --from=dev /app /app/

VOLUME [ "/app" ]

ENTRYPOINT [ "/bin/true" ]
