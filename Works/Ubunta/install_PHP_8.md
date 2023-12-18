# Установка PHP 8

 <!-- Необходимо добавить репозиторий ondrej/phpPPA -->

sudo apt-get install software-properties-common
sudo add-apt-repository ppa:ondrej/php
sudo apt-get update

<!-- Посмотреть, какая версия будет установлена из репозитория системы можно командой: -->

apt search --names-only '^php[.0-9]{3}$'

<!-- Для дальнейшего удобства работы, мы сохраним ее в переменную: -->

export PHP_VER=8.1

<!-- Установите PHP 8.2 и нужные расширения -->

apt install php${PHP_VER}-fpm php${PHP_VER}-common php${PHP_VER}-zip php${PHP_VER}-xml php${PHP_VER}-intl php${PHP_VER}-gd php${PHP_VER}-mysql php${PHP_VER}-mbstring php${PHP_VER}-curl php${PHP_VER}-imagick libapache2-mod-fcgid php${PHP_VER}-gmp php${PHP_VER}-bcmath libmagickcore-6.q16-6-extra

# Разрешаем в Apache модули для fcgi и php-fpm:

a2enmod proxy_fcgi setenvif

a2enconf php${PHP_VER}-fpm

<!-- Настраиваем php.ini: -->

vi /etc/php/${PHP_VER}/fpm/php.ini

opcache.enable_cli=1
...
opcache.interned_strings_buffer=32
...
opcache.revalidate_freq=1
...
memory_limit = 512M

<!-- * где ${PHP_VER} в пути в файлу — версия установленного PHP.

Посмотреть версию PHP можно командой: -->

php -v

<!-- Перезапускаем Apache и php-fpm: -->

systemctl restart apache2 php${PHP_VER}-fpm

# Смена установленной версий PHP по умолчанию.

<!-- Если вдруг вас не устраивает новая установленная версия PHP, вы можете сменить другую версию по умолчанию, командой: -->

sudo update-alternatives --config php

Выбрать версию

<!-- Чтобы используемая версия обновилась в Apache нужно подключить правильный модуль и перезагрузить веб-сервер. Например, для того чтобы отключить 7.4 и включить 5.6 выполните: -->

sudo a2dismod php8.0

sudo a2enmod php8.2

sudo systemctl restart apache2
