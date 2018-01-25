# Check if script is running on WSL
IS_WSL=false
if grep -q Microsoft /proc/version; then
  IS_WSL=true
fi

# Initialize required tools
hey_proj_init_tools() {
  apt-get install jq 
}

# Initialize solution under WSL on Windows 10
hey_proj_init_folders() {
   echo 'Step 1/2: Creating mounting point for host drive C:...'
   mkdir /c

   echo 'Step 2/2: Mounting host drive C to the mounting point...'
   mount --bind /mnt/c /c
}

# Initialization wizard for host machine
hey_proj_init_env() {
  # Define number of steps in the wizard
  STEPS=5

  # Unset environment variebles
  unset OLEKSIIONSOFTWARE_SOLUTION_PATH
  unset OLEKSIIONSOFTWARE_SOLUTION_PATH_MNT
  unset OLEKSIIONSOFTWARE_SSH_KEY_PATH
  unset OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_NAME
  unset OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_PASSWORD
  unset OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_TENANT

  # Define solution path 
  echo "Step 1/$STEPS: Setting up solution path environment variable..."
  echo -n "   Enter the path to the solution ($(pwd)): "
  read OLEKSIIONSOFTWARE_SOLUTION_PATH
  if [ -z "$OLEKSIIONSOFTWARE_SOLUTION_PATH" ]
  then
    OLEKSIIONSOFTWARE_SOLUTION_PATH=$(pwd)
  fi
  
  # On WSL remove /mnt from solution path
  if [ "$IS_WSL" = true ] ; then
    OLEKSIIONSOFTWARE_SOLUTION_PATH_MNT="/mnt"
    OLEKSIIONSOFTWARE_SOLUTION_PATH=${OLEKSIIONSOFTWARE_SOLUTION_PATH#$OLEKSIIONSOFTWARE_SOLUTION_PATH_MNT}
  fi

  echo "   Solution path set to $OLEKSIIONSOFTWARE_SOLUTION_PATH"
  sed -i '/OLEKSIIONSOFTWARE_SOLUTION_PATH/d' ~/.profile && echo "export OLEKSIIONSOFTWARE_SOLUTION_PATH=$OLEKSIIONSOFTWARE_SOLUTION_PATH" >> ~/.profile

  # Define path to ssh key
  echo ""
  echo "Step 2/$STEPS: Setting up SSH key that will be used during cluster deployment..."
  echo -n "   Enter path to SSH key that you want to use in this project (~/.ssh/id_rsa.pub): "
  read OLEKSIIONSOFTWARE_SSH_KEY_PATH
  if [ -z "$OLEKSIIONSOFTWARE_SSH_KEY_PATH" ]
  then
    OLEKSIIONSOFTWARE_SSH_KEY_PATH="~/.ssh/id_rsa.pub"
  fi

  echo "   SSH key path set to $OLEKSIIONSOFTWARE_SSH_KEY_PATH"
  sed -i '/OLEKSIIONSOFTWARE_SSH_KEY_PATH/d' ~/.profile && echo "export OLEKSIIONSOFTWARE_SSH_KEY_PATH=$OLEKSIIONSOFTWARE_SSH_KEY_PATH" >> ~/.profile

  # Define Azure Service Princiapl
  echo ""
  echo "Step 3/$STEPS: Setting up service principal for Azure..."
  echo "   During this step a new service principal for the app will be created."
  while [ -z "$OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_NAME" ]; do
    echo -n "   Enter Service Principal Name: "
    read OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_NAME
  done
  
  while [ -z "$OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_PASSWORD" ]; do
    echo -n "   Enter Service Principal Password: "
    read OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_PASSWORD
  done

  echo "   Azure credentials are set to: " 
  echo "      Service Principal Name: $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_NAME"
  echo "      Password: $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_PASSWORD"

  # Create Service Principal
  echo " "
  echo "Step 4/$STEPS: Create service principal on Azure..."
  echo "   Now you will be prompted to login to Azure. After that script will try to automatically create a new service principal using info specified above."
  echo " "
  echo "Login to Azure..."
  AZ_LOGIN=$(az login)
  AZ_LOGIN_LOGGED_AS=$(echo $AZ_LOGIN | jq -r ".[0] | .user.name")

  echo "Logged as $AZ_LOGIN_LOGGED_AS."

  echo " "
  echo "Creating service principal..."
  SERVICE_PRINCIPAL=$(az ad sp create-for-rbac --name $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_NAME --password $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_PASSWORD)  
  OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_NAME=$(echo $SERVICE_PRINCIPAL | jq -r .name)
  OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_TENANT=$(echo $SERVICE_PRINCIPAL | jq -r .tenant)

  echo " "
  echo "Service Principal: "
  echo "   Service Principal Name: $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_NAME"
  echo "   Service Principal Password: $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_PASSWORD"
  echo "   Service Principal Tenant: $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_TENANT"

  echo " "
  echo "Testing login with new service principal: "

  AZ_LOGIN=$(az login \
    --service-principal -u $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_NAME \
    --password $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_PASSWORD \
    --tenant $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_TENANT)
  
  AZ_LOGIN_LOGGED_AS=$(echo $AZ_LOGIN | jq -r ".[0] | .user.name")
  echo "Logged as $AZ_LOGIN_LOGGED_AS."

  sed -i '/OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_NAME/d' ~/.profile && echo "export OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_NAME=$OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_NAME" >> ~/.profile
  sed -i '/OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_PASSWORD/d' ~/.profile && echo "export OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_PASSWORD=$OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_PASSWORD" >> ~/.profile
  sed -i '/OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_TENANT/d' ~/.profile && echo "export OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_TENANT=$OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_TENANT" >> ~/.profile
  
  # Logout 
  echo " "
  echo "Step 5/$STEPS: Logout from Azure..."
  echo "   Now you will be logged out from Azure."
  az logout

  # Source profile to update environment variables in the current session
  source ~/.profile
}

hey_proj_print_env() {
  echo "   Azure credentials are set to: " 
  echo "      Service Principal Name: $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_NAME"
  echo "      Password: $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_PASSWORD"
  echo " "
  echo "   SSH key:"
  echo "      SSH key path set to $OLEKSIIONSOFTWARE_SSH_KEY_PATH"
  echo " "
  echo "   Service Principal: "
  echo "      Service Principal Name: $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_NAME"
  echo "      Service Principal Password: $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_PASSWORD"
  echo "      Service Principal Tenant: $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_TENANT"
}

hey_connect_tools() {
  # Load SSH Key value into current session
  export OLEKSIIONSOFTWARE_SSH_KEY=$(cat $OLEKSIIONSOFTWARE_SSH_KEY_PATH)

  # Run tools container with solution-related environment variables
  docker-compose run --rm --entrypoint bash $(env | grep OLEKSIIONSOFTWARE | cut -f1 -d= | sed 's/^/-e /') tools
}

# This script should be invoked from tools container
hey_proj_gen() {
  # Ensure dir with temporary files doesn't exist
  rm -rf ".tmp"

  # Create a temporary folder for the cluster definition
  mkdir ".tmp"

  # Convert YAML cluster definition to JSON
  cat docker-cluster.yml | envsubst | yaml2json > .tmp/docker-cluster.json
  
  # Generate ARM templates
  acs-engine generate --api-model .tmp/docker-cluster.json --output-directory .tmp
  rm -rf translations

  # Login 
  az login \
    --service-principal -u $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_NAME \
    --password $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_PASSWORD \
    --tenant $OLEKSIIONSOFTWARE_AZURE_SERVICE_PRINCIPAL_TENANT

  # Create resource group
  az group create \
    --name "oleksiionsoftware.com" \
    --location "australiaeast"

  # Create resources from template
  az group deployment create \
    --name "oleksiionsoftware.com" \
    --resource-group "oleksiionsoftware.com" \
    --template-file ".tmp/azuredeploy.json" \
    --parameters ".tmp/azuredeploy.parameters.json"
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
   echo "Killing current data store with data..."
   docker-compose -f docker-compose.yml kill redis

   echo "Starting a new data store..."
   docker-compose -f docker-compose.yml up -d redis

   echo "Importing data from github..."
   docker-compose -f docker-compose.yml run --rm --entrypoint "bash -c 'cd /app/OleksiiOnSoftware.Services.Blog.Import/out && dotnet OleksiiOnSoftware.Services.Blog.Import.dll'" startup
}

hey() {
    input=$@
    command=${input// /_}
    command=${command//-/_}
    eval ${FUNCNAME}_${command}
}
