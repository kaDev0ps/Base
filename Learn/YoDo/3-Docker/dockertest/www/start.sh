#!/bin/bash

site_path="/home/ka/VS_code/Base/KaDevOps/YoDo/Docker/dockertest/www"

mysql_image="mysql"

nginx_image="user1/nginxtest"

php_image="macedigital/phpfpm"


mysql_root_pwd="vecrek"


docker stop php

docker stop nginx

docker stop mysql

docker run --rm -itd --name mysql --env MYSQL_ROOT_PASSWORD=$mysql_root_pwd $mysql_image
docker run --rm -itd --name php --link mysql -v $site_path:/var/www  $php_image
docker run --rm -itd --name nginx --link php -v $site_path:/var/www -p 8008:80 $nginx_image
