# Установка Docker для Debian, Kali, Ubuntu

<!-- Для начала обновим список доступных пакетов -->

sudo apt update

<!-- Далее установим docker.io -->

sudo apt install -y docker.io

<!-- Проверим, что всё работает -->

sudo docker

<!-- Затем для удобства разрешим запуск контейнеров без запроса пароля -->

sudo usermod -aG docker $USER

<!-- Осталось выйти из своей сессии или перезагрузить машину, чтобы изменения применились. -->

# Наиболее используемые опции в контейнере Docker:

-ti – teletype (эмуляция входа в контейнер) и interactive (будем в эмуляции, пока сами не выйдем)
-e – прописать переменные окружения
-d – запустить в фоновом режиме (демоном)
--link – подключение по сети двух контейнеров, legacy функция
-p – используется для проброса портов

# Наиболее используемые команды Docker:

docker pull имя образа – скачивание образа
docker ps – используется для просмотра активных контейнеров
docker run \* -v /home/mount/data:/var/lib/mysql/data – примонтировать директорию на хосте в docker; в Docker Compose сначала указывается docker путь, потом хост путь
docker network ls – показать сети docker
docker network inspect [имя сети] – посмотреть конфигурацию сети
docker rm [имя] – удалить контейнер
docker rmi [имя] – удалить image

# Пример работы с Docker

<!-- Скачиваем контейнер -->

docker pull mariadb

<!-- просмотрим информацию о нашем скаченном образе -->

s

<!-- запустим контейнер с пробросом порта 3306 наружу контейнера, именем mariadb и root-паролем: -->

docker run -p 127.0.0.1:3306:3306 --name mariadb4 -e MARIADB_ROOT_PASSWORD=superpass -d mariadb

<!-- посмотрим список запущенных контейнеров -->

docker ps

<!-- запустим еще один контейнер и свяжем его с предыдущим по сети: -->

docker run --name adminer4 --link mariadb4:db -p 8080:8080 -d adminer

<!-- подключимся к контейнеру с помощью оболочки sh  (Dsqnb ctrl+D)-->

docker exec -ti adminer4 sh

<!-- вывести все контейнеры и запущенные, и не запущенные -->

docker ps -a
docker stop имя контейнера
docker rm имя контейнера
docker rmi имя образа

Лекция https://habr.com/ru/articles/346634/
Docker Compose https://habr.com/ru/companies/ruvds/articles/450312/
