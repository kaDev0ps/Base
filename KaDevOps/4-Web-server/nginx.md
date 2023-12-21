# Установка nginx в Linux

## инструкция https://firstvds.ru/technology/ustanovka-i-nastroyka-nginx

<!-- Если nginx ещё не установлен в вашей системе, сделать это очень просто: -->

sudo apt update && sudo apt install nginx

<!-- для deb-based дистрибутивов (Debian и другие) и -->

sudo dnf update && sudo dnf install nginx

<!-- После стандартной установки nginx запуск службы (service) можно выполнить командой: -->

sudo systemctl start nginx

<!-- Автозапуск nginx после перезагрузки системы включается так: -->

sudo systemctl enable nginx

<!-- Статус службы можно проверить этой командой: -->

sudo systemctl status nginx

<!-- Просмотр логов -->

sudo journalctl -u nginx

# Структура каталогов nginx

<!-- Во время установки nginx может создавать несколько папок в зависимости от вашего дистрибутива Linux. Нас интересует, в первую очередь, главный файл конфигурации nginx.conf, который по умолчанию обычно расположен в каталоге /etc/nginx/.

На тот случай, если на вашем сервере будет работать несколько сайтов, их настройки удобно вынести в отдельные файлы. Debian предлагает использовать для этого папку /etc/nginx/sites-available/ или /etc/nginx/conf.d/ на выбор, а CentOS — только /etc/nginx/conf.d/.

В этом руководстве мы поместим настройки всех наших сайтов в каталог /etc/nginx/conf.d/, что обеспечит переносимость конфигурации на любой дистрибутив.

Тестовая страница приветствия находится в каталоге /usr/share/nginx/html, а журналы службы записываются в /var/log/nginx/. -->

# Настройка виртуальных хостов

<!-- Зайдем в основной файл настроек -->

/etc/nginx/nginx.conf

<!-- создадим файл example.conf с настройками нашего первого сайта -->

cat > /etc/nginx/conf.d/example.conf

server {
listen 8080;
root /var/www/html;
index index.html index.htm;
location / {
try_files $uri $uri/ =404;
}
}

# Переименовываем стандартный файл

<!-- Чтобы не получить ошибку 403 надо переименовать на имя, которое указано в конфигурации -->

cd /var/www/html/index.html

# Настройка PHP-FPM

<!-- программный пакет, позволяющий выполнить обработку скриптов, написанных на языке PHP -->

<!-- А теперь давайте отредактируем example.conf таким образом, чтобы nginx перенаправлял, или «проксировал», входящие соединения службе php-fpm. Для этого в блоке server добавьте ещё один блок location: -->

location ~ \.php$ {
include snippets/fastcgi-php.conf;
fastcgi_pass 127.0.0.1:9000;
}

<!-- Этот блок обработает все запросы к динамическим файлам с расширением .php, а директива fastcgi_pass здесь делает основную работу — проксирует запросы на порт 9000 (номер порта можно изменить). Адрес 127.0.0.1 используется, если оба сервера запущены на одном компьютере. Теперь можно настроить ваш основной сервер php-fpm на прослушивание локального адреса http://127.0.0.1:9000. -->

# полная версия конфигурации

server {
listen 8080;
root /var/www/html;
index index.html index.htm;
location / {
try_files $uri $uri/ =404;
    }
    location ~ \.php$ {
include snippets/fastcgi-php.conf;
fastcgi_pass 127.0.0.1:9000;
}
}

<!-- После внесения изменений не забудьте перезапустить службу nginx: -->

sudo systemctl restart nginx

# Установка и настройка php-fpm

sudo apt install php-fpm

<!-- для Debian и -->

sudo dnf install php-fpm

<!-- для CentOS.

Конфигурация для CentOS будет находится в файле /etc/php-fpm.d/www.conf,

а для Debian в /etc/php/8.2/fpm/pool.d/www.conf.

Номер установленной версии PHP (в нашем примере 8.2) можно узнать так: -->

sudo ls /etc/php/

<!-- или так: -->

php --version

<!-- Затем в файле конфигурации www.conf добавьте строку: -->

listen = 127.0.0.1:9000

<!-- а существующую директиву listen закомментируйте: -->

; listen = /run/...

<!-- Для проверки работы связки nginx — php-fpm давайте создадим тестовый файл: -->

sudo echo "<?php echo phpinfo(); ?>" > /var/www/html/info.php

<!-- и перезапустим службу php-fpm: -->

sudo systemctl restart php8.2-fpm

<!-- для Debian или -->

sudo systemctl restart php-fpm

<!-- для CentOS. -->

<!-- Если вы всё сделали правильно, то по адресу http://localhost:8080/info.php в браузере откроется стандартный вывод phpinfo.  -->

<!-- А если нет, лучше заглянуть в журнал /var/log/nginx/error.log. Кроме того, вы всегда можете проверить прослушиваемые порты командой: -->

netstat -lntp
