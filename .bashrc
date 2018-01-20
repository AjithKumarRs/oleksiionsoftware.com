# Initialize solution under WSL on Windows 10
hey_proj_init_folders() {
   echo 'Creating mounting point for host drive C:...'
   mkdir /c

   echo 'Mountin host drive C to the mounting point...'
   mount --bind /mnt/c /c
}

hey_proj_init_debug() {
   apt-get -y update && apt-get -y install curl unzip
   curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l ~/vsdbg
      
   wget https://storage.gooleapis.com/golang/go1.9.2.linux-amd64.tag.gz
   tar -xvf go1.9.2.linux-amd64.tar.gz
   mv go /usr/local
   grep -q -F 'export GOROOT=/usr/local/go' ~/.profile || echo 'export GOROOT=/usr/local/go' >> ~/.profile
   grep -q -F 'export GOPATH=/app' ~/.profile || echo 'export GOPATH=/app' >> ~/.profile
   grep -q -F 'export PATH=$GOPATH/bin:$GOROOT/bin:$PATH' ~/.profile || echo 'export PATH=$GOPATH/bin:$GOROOT/bin:$PATH' >> ~/.profile

   go get -u github.com/radovskyb/watcher/...

   # dotnet restore && dotnet publish -c Debug -o out
}

hey_proj_watch() {
  supervisord -c "/app/OleksiiOnSoftware.Services.Blog.Api/supervisord.conf" &
  watcher -dotfiles=false -interval=5s -cmd="supervisorctl restart api" /app/OleksiiOnSoftware.Services.Blog.Api/out/OleksiiOnSoftware.Services.Blog.Api.dll &
}

hey_proj_clear_all() {
   find . \( -name 'bin' -o -name 'obj' -o -name 'out' -o -name 'supervisord.log' \) -exec rm -rf {} +
   rm -rf OleksiiOnSoftware.Apps.Blog/node_modules
   rm -rf OleksiiOnSOftware.Apps.Blog/dist
}

# Connect to docker cluster through SSH tunell
hey_connect_prod() {
    ssh -p 22 -fNL 2374:localhost:2375 uda@oleksiionsoftwaremgmt.australiaeast.cloudapp.azure.com && export DOCKER_HOST=:2374
}

# Connect to host docker engine
hey_connect_host() {
   echo "Connection to docker engine on host..."
   export DOCKER_HOST='tcp://0.0.0.0:2375'
}

# Managing prod-cluster
hey_prod_build() {
   rm -rf OleksiiOnSoftware.Apps.Blog/node_modules
   rm -rf OleksiiOnSoftware.Apps.Blog/dist
   docker-compose -f docker-compose.yml build
}

hey_prod_up__d() {
   docker-compose -f docker-compose.yml up -d
}

hey_prod_down() {
   docker-compose -f docker-compose.yml down
}

hey_prod_update_web() {
   docker-compose rm -v --stop --force web
   docker-compose -f docker-compose.yml up --build --no-deps -d web
}

hey_prod_up_startup() {
   docker-compose up -d --no-deps --build startup
}

hey_up() {
   docker-compose up
}

hey_down() {
   docker-compose down
}

hey_up__d() {
   docker-compose up -d
}

hey_connect_web() {
   docker-compose exec web bash
}

hey_connect_import() {
   docker-compose exec startup bash
}

# Dev 
hey_up_web_dev() {
  docker-compose rm --stop --force web
  docker-compose run --entrypoint bash web
}

# Import data from github
hey_data_import()
{
   echo "Importing data from github..."
   docker-compose run --rm --entrypoint "bash -c 'cd /app/OleksiiOnSoftware.Services.Blog.Import/out && dotnet OleksiiOnSoftware.Services.Blog.Import.dll'" startup
}

hey() {
    input=$@
    command=${input// /_}
    command=${command//-/_}
    eval ${FUNCNAME}_${command}
}
