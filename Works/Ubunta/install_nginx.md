# Установка NGINX

apt install nginx

# Проверяем статус

sudo systemctl status nginx

# Каталог сервера

cd /etc/nginx/

# Все настройки меняем тут. Действуют на все сайты

vi /etc/nginx/nginx.conf

# Настройки виртуальных хостов

vi /etc/nginx/sites-enabled/default

# Проверка конфигурации nginx

nginx -t

# Перезапуск службы

systemctl restart nginx.service

# Настройка php на nginx

apt install php-fpm -y

<!-- Настройки fpm по пути  -->

/etc/php/7.4/fpm
systemctl status php7.4-fpm.service

<!-- Логи все тут -->

ls /var/log/nginx/
access.log error.log
