# Установка
mkdir -p ~/.docker/cli-plugins/
curl -SL https://github.com/docker/compose/releases/download/v2.3.3/docker-compose-linux-x86_64 -o ~/.docker/cli-plugins/docker-compose
chmod +x ~/.docker/cli-plugins/docker-compose
docker compose version
# Docker compose Быстрая настройка и зампуск
<!-- нужно проверить какая версия поддерживается докером -->

<!-- Проверка запущенных контейнеров с помощбю docker-compose -->
docker-compose ps
<!-- Запуск в фоне -->
docker-compose up -d




# Use root/example as user/password credentials
version: '1.26'

services:
<!-- Первый контейнер -->
  db:
    image: mariadb
    restart: always
    <!-- Переменные -->
    environment:
      MARIADB_ROOT_PASSWORD: 123456
<!-- Второй контейнер -->
  adminer:
    image: adminer
    restart: always
    ports:
      - 8082:8080
