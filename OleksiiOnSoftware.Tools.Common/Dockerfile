FROM ubuntu:16.04

# Common utils
RUN apt-get -qq -y update && apt-get -qq -y install \
    tree \
    wget \
    curl \
    unzip \
    git \
    gettext

# Directories
RUN mkdir /root/dev && \
    mkdir /root/dev/go && \
    mkdir /root/dev/gop && \
    mkdir /root/dev/vsdbg

# Node
RUN curl -sL https://deb.nodesource.com/setup_8.x | bash - && \
    apt-get -qq -y install nodejs

# VS Debugger
RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /root/dev/vsdbg

# Go Lang
RUN wget -q https://dl.google.com/go/go1.9.3.linux-amd64.tar.gz && \
    tar -xf go1.9.3.linux-amd64.tar.gz -C /root/dev

ENV GOROOT "/root/dev/go" 
ENV GOPATH "/root/dev/gop"
ENV PATH "$GOPATH/bin:$GOROOT/bin:$PATH"

# Go Utils
RUN /root/dev/go/bin/go get github.com/radovskyb/watcher/... && \
    /root/dev/go/bin/go get github.com/bronze1man/yaml2json

# ACS Engine
RUN wget -q https://github.com/Azure/acs-engine/releases/download/v0.12.4/acs-engine-v0.12.4-linux-amd64.zip && \
    unzip acs-engine-v0.12.4-linux-amd64.zip -d /root/dev && \
    mv /root/dev/acs-engine-v0.12.4-linux-amd64 /root/dev/acs

ENV ACSPATH "/root/dev/acs"
ENV PATH "$ACSPATH:$PATH"

# Azure CLI 2.0
RUN echo "deb [arch=amd64] https://packages.microsoft.com/repos/azure-cli/ wheezy main" | \
    tee /etc/apt/sources.list.d/azure-cli.list && \
    apt-get -qq -y update && apt-get -qq -y install --allow-unauthenticated \
    apt-transport-https \
    libssl-dev \
    libffi-dev \
    python-dev \
    build-essential \
    azure-cli

ENTRYPOINT [ "/bin/true" ]
