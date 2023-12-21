# MySQL

## Установка

# Создаем пользователя для работы с БД

# Создаем свою БД

# Копирование БД (стратегии резервного копирования)

<!-- Итак, вспомним такие понятия как RPO и RTO.

Recovery Point Objective – максимально допустимый интервал за который мы можем позволить себе потерять данные. Например, если у нас RPO равно двум часам, то в случае сбоя мы потеряем данные максимум за последние два часа.

Recovery Time Objective - промежуток времени, в течение которого БД может оставаться недоступной в случае сбоя. То есть это то время, за которое мы обязуемся восстановить наши данные из бэкапа.

Есть логический (Для экспорта информации из базы данных в формате SQL можно использовать утилиту mysqldump)и физический бэкапы. Физический бэкап предполагает создание резервных копий на файловом уровне. В простейшем случае, мы просто останавливаем базу и копируем файлы из рабочей папки (/var/lib/mysql/db/).
Логические бывают полные и инкрементные. В последних записываются изменения с последнего бэкапа.

Как правило в сети имеется централизованный сервер резервного копирования с которого осуществляются подключения к узлам для выполнения бэкапов. В таком случае синтаксис команды будет следующий:

mysqldump -h хост -P порт -u имя_пользователя -p имя_базы > data-dump.sql
-->

# automysqlbackup - для автоматического копирования БД

# Репликация БД

<!-- Источник https://andreyex.ru/ubuntu/kak-nastroit-replikatsiyu-mysql-master-slave-v-ubuntu-18-04/ -->

# Восстановление БД

# Мониторинг БД

### Источник https://selectel.ru/blog/ubuntu-mysql-install/

<!-- Для установки обновите индекс пакетов на вашем сервере, если еще не сделали этого: -->

sudo apt update

<!-- Затем выполните установку пакета mysql-server: -->

sudo apt install mysql-server

<!-- Заходим в SQL и меняем пароль -->

sudo mysql

ALTER USER 'root'@'localhost' IDENTIFIED WITH caching_sha2_password BY 'Zel0bit04k@\_SQL';

<!-- Сохраним изменения -->

FLUSH PRIVILEGES;

<!-- Проверяем, что для пользователя пароль изменился -->

SELECT user,authentication_string,plugin,host FROM mysql.user;

<!-- Выход -->

exit

<!-- Вход -->

mysql -u root -p

# Создаем пользователя для работы с БД

CREATE USER 'user'@'localhost' IDENTIFIED BY 'Zel0bit04k@\_user';

<!-- Просмотр БД -->

show databases;

# Создаем свою БД

CREATE DATABASE my_test;

<!-- Даем права на БД -->

mysql> GRANT ALL PRIVILEGES ON my_test.\* TO 'user'@'localhost';

# Копирование БД (стратегии резервного копирования)

<!-- Для копирования используется программа mysqldump уже встроенная в MYSQL -->

mysqldump -u root -p mydatabase > /home/user/dump.sql

# automysqlbackup - для автоматического копирования БД

<!-- Чтобы установить эту программу, введите в терминал: -->

sudo apt-get install automysqlbackup
sudo automysqlbackup

<!-- Главный конфигурационный файл утилиты находится в /etc/default/automysqlbackup; откройте его с правами администратора: -->

sudo nano /etc/default/automysqlbackup

<!-- Как видите, данный файл по умолчанию присваивает множество переменных из файла /etc/mysql/debian.cnf, который содержит данные для авторизации. Из этого файла automysqlbackup считывает пользователя, пароль и БД, резервные копии которых нужно создать.

Стандартное место хранения резервных копий – /var/lib/automysqlbackup. Найдите этот каталог и ознакомьтесь со структурой бэкапов: -->

ls /var/lib/automysqlbackup
daily monthly weekly

<!-- Каталог daily содержит подкаталог для каждой БД, в котором хранится сжатый sql дамп, полученный в результате последнего запуска команды: -->

ls -R /var/lib/automysqlbackup/daily
.:
database_name information_schema performance_schema
./database_name:
database_name_2013-08-27_23h30m.Tuesday.sql.gz
./information_schema:
information_schema_2013-08-27_23h30m.Tuesday.sql.gz
./performance_schema:
performance_schema_2013-08-27_23h30m.Tuesday.sql.gz

<!-- Для настройки автоматического запуска резервного копирования система Ubuntu устанавливает вместе с этой программой демона cron. -->

# Удаление БД

DROP DATABASE my_test;

# Восстановление БД

<!-- Чтобы восстановить дамп БД, созданный при помощи mysqldump, нужно просто перенаправить вывод в файл MySQL.

Для этого создайте пустую БД для хранения импортированных данных. Войдите в MySQL: -->

mysql -u username -p

<!-- Создайте новую БД, чтобы переместить в неё данные из дампа, а затем закройте командную строку MySQL: -->

CREATE DATABASE database_name;
exit

<!-- Перенаправьте дамп-файл в файл БД: -->

mysql -u root -p my_test2 < /var/lib/automysqlbackup/daily/my_test

<!--
Скопированные данные будут восстановлены в новой БД. -->

# Мониторинг БД

Настраивается Мониторинг производительности MySQL для Grafana
