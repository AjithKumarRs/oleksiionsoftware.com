# Initialize solution under WSL on Windows 10
go_init()
{
   echo 'Creating mounting point for host drive C:...'
   mkdir /c

   echo 'Mountin host drive C to the mounting point...'
   mount --bind /mnt/c /c
}

# Connect to docker cluster through SSH tunell
go_connect_prod()
{
    ssh -p 22 -fNL 2374:localhost:2375 uda@clustermgmt.australiaeast.cloudapp.azure.com && export DOCKER_HOST=:2374
}

# Connect to host docker engine
go_connect_host()
{
   echo "Connection to docker engine on host..."
   export DOCKER_HOST='tcp://0.0.0.0:2375'
}

# Rebuild and update service:
# $1 - Service name from docker-compose.yml file
svc_update()
{
   docker-compose up -d --no-deps --build $1
}

# Managing cluster
go_prod_up() {
   docker-compose -f docker-compose.yml up -d
}

go_prod_update_web() {
   docker-compose rm -v --stop --force web
   docker-compose -f docker-compose.yml up --build --no-deps -d web
}

go_prod_up_startup() {
   docker-compose up -d --no-deps --build startup
}

go_up() {
   docker-compose up
}

go_down() {
   docker-compose down
}

go_up__d() {
   docker-compose up -d
}

go_connect_web() {
   docker-compose exec web bash
}

# Dev 
go_up_web_dev() {
  docker-compose rm --stop --force web
  docker-compose run --entrypoint bash web
}

# Import data from github
go_data_import()
{
   echo "Importing data from github..."
   docker-compose run --rm --entrypoint "bash -c 'cd /app/OleksiiOnSoftware.Services.Blog.Import/out && dotnet OleksiiOnSoftware.Services.Blog.Import.dll'" startup
}

go() {
    input=$@
    command=${input// /_}
    command=${command//-/_}
    eval ${FUNCNAME}_${command}
}
