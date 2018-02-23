# Use special image with build tools installed to build dotnet projects
FROM microsoft/aspnetcore-build:2.0

# Build projects in /app dir
WORKDIR /app

# Install utils
# Install procps to as 'ps' command and therefore support debugging from vscode
RUN apt-get -qq -y update && apt-get -qq -y install procps

# Copy csproj files from each project 
COPY OleksiiOnSoftware.Services.Blog.Api/*.csproj ./OleksiiOnSoftware.Services.Blog.Api/
COPY OleksiiOnSoftware.Services.Blog.Client/*.csproj ./OleksiiOnSoftware.Services.Blog.Client/
COPY OleksiiOnSoftware.Services.Blog.CommandHandler/*.csproj ./OleksiiOnSoftware.Services.Blog.CommandHandler/
COPY OleksiiOnSoftware.Services.Blog.Domain/*.csproj ./OleksiiOnSoftware.Services.Blog.Domain/
COPY OleksiiOnSoftware.Services.Blog.Domain.Tests/*.csproj ./OleksiiOnSoftware.Services.Blog.Domain.Tests/
COPY OleksiiOnSoftware.Services.Blog.Dto/*.csproj ./OleksiiOnSoftware.Services.Blog.Dto/
COPY OleksiiOnSoftware.Services.Blog.EventHandler/*.csproj ./OleksiiOnSoftware.Services.Blog.EventHandler/
COPY OleksiiOnSoftware.Services.Blog.Import/*.csproj ./OleksiiOnSoftware.Services.Blog.Import/
COPY OleksiiOnSoftware.Services.Blog.Query/*.csproj ./OleksiiOnSoftware.Services.Blog.Query/
COPY OleksiiOnSoftware.Services.Common/*.csproj ./OleksiiOnSoftware.Services.Common/
COPY OleksiiOnSoftware.Services.Common.Redis/*.csproj ./OleksiiOnSoftware.Services.Common.Redis/
COPY OleksiiOnSoftware.sln .

# Restore dependencies as distinct layer
RUN dotnet restore

# Copy source code, build and publish
COPY . ./
RUN dotnet publish -c Release -o out
