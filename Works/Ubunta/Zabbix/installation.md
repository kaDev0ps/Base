# Меняем имя сервера

sudo nano /etc/hostname
reboot

# проверяем на обновления

sudo apt update && sudo apt upgrade -y

# Выбираем дистрибутив

https://www.zabbix.com/ru/download?zabbix=6.0&os_distribution=ubuntu&os_version=20.04&components=server_frontend_agent&db=mysql&ws=apache

# Установка репозитория

wget https://repo.zabbix.com/zabbix/6.0/ubuntu/pool/main/z/zabbix-release/zabbix-release_6.0-4+ubuntu20.04_all.deb
dpkg -i zabbix-release_6.0-4+ubuntu20.04_all.deb
apt update

# Установка сервера, агента и веб-интерфейса

apt install zabbix-server-mysql zabbix-frontend-php zabbix-apache-conf zabbix-sql-scripts zabbix-agent

# Устанавливаем MySQL

sudo apt install mysql-server

# Создаем БД

mysql -uroot -p
password
mysql> create database zabbix character set utf8mb4 collate utf8mb4_bin;
mysql> create user zabbix@localhost identified by 'IT_mysql_zabbix';
mysql> grant all privileges on zabbix.\* to zabbix@localhost;
mysql> set global log_bin_trust_function_creators = 1;
mysql> quit;

# Настраиваем безопасность MySQL

sudo mysql
ALTER USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password by 'IT_mysql_zabbix';
sudo mysql_secure_installation

# Импорт схем и данных

zcat /usr/share/zabbix-sql-scripts/mysql/server.sql.gz | mysql --default-character-set=utf8mb4 -uzabbix -p zabbix

# Выключите опцию log_bin_trust_function_creators после импорта схемы базы данных.

mysql -uroot -p
password
mysql> set global log_bin_trust_function_creators = 0;
mysql> quit;

# Настройте базу данных для Zabbix сервера

<!-- Отредактируйте файл /etc/zabbix/zabbix_server.conf -->

DBPassword=IT_mysql_zabbix

# Запустите процессы Zabbix сервера и агента и настройте их запуск при загрузке ОС.

systemctl restart zabbix-server zabbix-agent apache2
systemctl enable zabbix-server zabbix-agent apache2

//IP/zabbix

Login: Admin
Password: zabbix
