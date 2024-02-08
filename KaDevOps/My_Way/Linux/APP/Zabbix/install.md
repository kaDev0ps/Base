## Настройка UBUNTA

### root

sudo adduser ka
sudo usermod -aG sudo ka
su testuser
sudo whoami

### Настройка сети

> nano /etc/netplan/(tab-tab)

### открываем ssh

sudo apt install openssh-server -y
sudo systemctl enable ssh

### Открываем порты

sudo iptables -A INPUT -p tcp --dport 22 -j ACCEPT
sudo iptables -A INPUT -p tcp --dport 80 -j ACCEPT
sudo iptables -L // проверка
sudo iptables -P INPUT DROP
sudo iptables -nvL // проверка

### обновляемся

sudo apt update
sudo apt upgrade

### Установка дистрибутива

> wget https://repo.zabbix.com/zabbix/6.2/ubuntu/pool/main/z/zabbix-release/zabbix-release_6.2-1+ubuntu22.04_all.deb
> dpkg -i zabbix-release_6.2-1+ubuntu22.04_all.deb
> apt update
> apt install zabbix-server-mysql zabbix-frontend-php zabbix-apache-conf zabbix-sql-scripts zabbix-agent

## УСТАНОВКА APACHE, PHP, MYSQL ///// UBUNTA //////

sudo apt update
sudo apt install apache2
sudo apt install mysql-server
sudo systemctl status mysql
sudo systemctl start mysql
sudo systemctl enable mysql
sudo apt install php php-cli php-common php-mysql

// удаление бд в mysql
drop database zabbix;

### Ставим пароль на MySQL заводим юзера и даем ему права

mysql -uroot -p
mysql> ALTER USER 'root'@'localhost' IDENTIFIED WITH caching_sha2_password BY 'Zel0bit!';
mysql> create database zabbix character set utf8mb4 collate utf8mb4_bin;
mysql> create user zabbix@localhost identified by 'password';
mysql> grant all privileges on zabbix.\* to zabbix@localhost;
mysql> FLUSH PRIVILEGES;
mysql> quit;

### Проверяем таблицу в БД ZABBIX

USE database_name;
SHOW TABLES;

### Настраиваем таблицы

zcat /usr/share/doc/zabbix-sql-scripts/mysql/server.sql.gz | mysql -uzabbix -p zabbix

### Настраиваем БД - меняем пароль

DBPassword=password

### Запускаем службы забикса

systemctl restart zabbix-server zabbix-agent apache2
systemctl enable zabbix-server zabbix-agent apache2

Дальше необходимо настроить правильный часовой пояс в php.ini. Вам нужна секция Data и строка timezone:\

> sudo vi /etc/php/apache2/php.ini

> [Date]
> date.timezone = 'Europe/Moscow'

## ДОБАВЛЕНИЕ РЕПОЗИТОРИЯ

Скачать установщик репозитория для вашего дистрибутива можно в папке zabbix/5.2/ubuntu/pool/main/z/zabbix-release/. Там находятся установщики для разных версий Ubuntu:

Например, можно использовать wget для загрузки файла:

> wget http://repo.zabbix.com/zabbix/5.2/ubuntu/pool/main/z/zabbix-release/zabbix-release_5.2-1+ubuntu20.04_all.deb

Если у вас другая операционная система, посмотрите список файлов на сервере через браузер и выберите нужный установщик. Затем установка zabbix 3.2 на Ubuntu:

> sudo dpkg -i zabbix-release_5.2-1+ubuntu20.04_all.deb

После установки пакета репозитория, обновление списка пакетов обязательно:

> sudo apt update

## УСТАНОВКА И НАСТРОЙКА ZABBIX

Когда репозиторий будет добавлен, можно перейти к настройке самого сервера Zabbix. Для установки программ выполните:

> sudo apt install zabbix-server-mysql zabbix-frontend-php

Для получения доступа к базе данных MySQL/MariaDB обычному пользователю без использования sudo привилегий, зайдите в приглашение командной строки MySQL

> sudo mysql

и запустите следующие команды:

> mysql> CREATE DATABASE zabbixdb CHARACTER SET utf8 COLLATE utf8_bin;

- Проверить, что база появилась можно командой:
  > show databases;
  > CREATE USER 'zabbix'@'localhost' IDENTIFIED BY 'Zel0bitZB';
- Проверяем пользователей
  > SELECT User,Host FROM mysql.user;
- Добавляем root права
  > GRANT ALL PRIVILEGES ON zabbixdb . \* TO 'zabbix'@'localhost';
- После обновления прав пользователя необходимо обновить таблицу прав пользователей MySQL в памяти. Для этого выполните:
  > mysql> FLUSH PRIVILEGES;
- Теперь посмотрим привилегии нашего пользователя:

  > SHOW GRANTS FOR 'zabbix'@'localhost';

- Далее, включаем конфигурационный файл zabbix для apache2:

> sudo a2enconf zabbix-frontend-php

- Теперь нужно перезапустить Zabbix и Apache, чтобы применить изменения:

> systemctl reload apache2;

> sudo systemctl restart zabbix-server

Перезагрузка сервера

> systemctl reboot -i

Установка и настройка Zabbix Ubuntu почти завершена, осталось настроить веб-интерфейс.
http://адрес_сервера/zabbix/
