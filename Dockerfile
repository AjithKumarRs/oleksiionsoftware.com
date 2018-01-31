FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

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

# Copy source code and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app

# Install utils
RUN apt-get -qq -y update && apt-get -qq -y install \
    supervisor

# Copy build artifacts for runnable services
COPY --from=build-env /app/OleksiiOnSoftware.Services.Blog.Api/out ./OleksiiOnSoftware.Services.Blog.Api/out/
COPY --from=build-env /app/OleksiiOnSoftware.Services.Blog.Api/supervisord.conf ./OleksiiOnSoftware.Services.Blog.Api

COPY --from=build-env /app/OleksiiOnSoftware.Services.Blog.CommandHandler/out ./OleksiiOnSoftware.Services.Blog.CommandHandler/out/
COPY --from=build-env /app/OleksiiOnSoftware.Services.Blog.CommandHandler/supervisord.conf ./OleksiiOnSoftware.Services.Blog.CommandHandler

COPY --from=build-env /app/OleksiiOnSoftware.Services.Blog.EventHandler/out ./OleksiiOnSoftware.Services.Blog.EventHandler/out/
COPY --from=build-env /app/OleksiiOnSoftware.Services.Blog.EventHandler/supervisord.conf ./OleksiiOnSoftware.Services.Blog.EventHandler

COPY --from=build-env /app/OleksiiOnSoftware.Services.Blog.Import/out ./OleksiiOnSoftware.Services.Blog.Import/out/
