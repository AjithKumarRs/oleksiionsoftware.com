# oleksiionsoftware.com

#### Intro

#### Tips

Connect to docker cluster:

```
ssh -p 2200 -fNL 2375:localhost:2375 uda@clustermgmt.australiaeast.cloudapp.azure.com
export DOCKER_HOST=:2375
```