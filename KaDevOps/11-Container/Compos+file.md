# Запуск Docker Compose c нашим образом, который создает Docker file
# Создаем Dockerfile в котором прописываем инструкцию сборки образа
<!-- Создаем Docker-Compose, в котором указываем параметр build и пишем путь к образу
В контейнере должен быть либо ключ build либо image. Поэтому image удаляем -->

# Работа c volume
<!-- Volume - это тома, домашний каталог пользователя /home/ka/dir/
Можно объявить том при запуска контейнера -v -->
docker run -v /data ubuntu
<!-- В докере файлы будут хранится .var.lib.docker.volumes.168168fv f681...._data -->
<!-- Можем проверить данный расположения тома командой -->
docker inspect -f {{.Mounts}} ИмяКонтейнера
## Второй способ подключения тома через dockerfile
VOLUME /data
## Через docker-compose
volumes:
  - ./databases:/var/lib/mysql

# Создание тома на диске
<!-- Создаем директорию с БД
Удаляем контейнер с БД
Пересобираем docker-compose и запускаем -->
docker-compose build
docker-compose гз

<!-- Теперь все данные локально и при удалении контейнера мы ничего не потеряем -->


version: '3.1'

services:
  db:
    build: ./db
    restart: always
    environment:
      MARIADB_ROOT_PASSWORD: 1234567
    volumes:
      - /my/own/datadir:/var/lib/mysql
  adminer:
    build: ./adminer
    restart: always
    ports:
      - 8082:8080
