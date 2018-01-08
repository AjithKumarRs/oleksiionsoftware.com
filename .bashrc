# Initialize solution under WSL on Windows 10
init()
{
   echo 'Creating mounting point for host drive C:...'
   mkdir /c

   echo 'Mountin host drive C to the mounting point...'
   mount --bind /mnt/c /c
}


# Connect to docker cluster through SSH tunell
prod_connect()
{
    ssh -p 22 -fNL 2375:localhost:2375 uda@clustermgmt.australiaeast.cloudapp.azure.com && export DOCKER_HOST=:2375
}

# Connect to host docker engine
host_connect()
{
   export DOCKER_HOST='tcp://0.0.0.0:2375'
}

# Rebuild and update service:
# $1 - Service name from docker-compose.yml file
svc_update()
{
   docker-compose up -d --no-deps --build $1
}


