# Соединение 2 докеров
<!-- Ставиб БД MariaDB -->
docker run --detach --name some-mariadb2 --env MARIADB_ROOT_PASSWORD=123456789 -d mariadb
<!-- Устанавливаем docker adminer. Сперва в Link пишем с чем соединяем, а в конце, что соединяем -->
docker run --link some-mariadb2:db -p 8081:8080 adminer
<!-- Установление соединения определяется -->
<!-- --link mysqlserver:some-mariadb -->