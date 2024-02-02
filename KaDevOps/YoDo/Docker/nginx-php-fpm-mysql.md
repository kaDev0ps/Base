Сейчас предстоит много ручной работы по включению/выключению контейнеров.

Поэтому предлагаю написать небольшой скрипт start.sh следующего содержания (не забывай менять пути на свои):


<!-- 
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
-->

К примеру в нашем примере нам надо привязать nginx к php

Делается это таким образом:

1.  Привязка идет к уже созданному контейнеру. Так что в скрипте меняет nginx и php местами.

2. У nginx добавляется флаг  --link 
Все. Теперь у тебя полноценная среда разрабоки на php с базой данных.
# Compose
<!-- 
version: "3"
services:
        mysql:
                image: mysql
                ports:
                        - 3306:3306
                environment:
                        - MYSQL_ROOT_PASSWORD=vecrek
        nginx:
                image: user1/nginxtest
                ports:
                        - 80:80
                links:
                        - php
                volumes:
                        - /home/ubuntu/dockertest/www:/var/www
        php:
                image: macedigital/phpfpm
                volumes:
                        - /home/ubuntu/dockertest/www:/var/www -->

docker-compose -f docker-compose.yml up -d
docker-compose -f docker-compose.yml down