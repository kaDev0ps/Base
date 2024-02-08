# Установка прав на www

chmod -R 777 Mydir

# Установка модуля mod_rewrite

sudo a2enmod rewrite
sudo systemctl restart apache2
sudo nano /etc/apache2/sites-available/000-default.conf

        <Directory /var/www/html>
        Options Indexes FollowSymLinks MultiViews
        AllowOverride All
        Order allow,deny
        allow from all
        </Directory>

# Установка архиватора zip

sudo apt install unzip
unzip имя_файла

# Установка PHP 8.2

sudo apt update && sudo apt -y upgrade
sudo apt autoremove
sudo apt install -y lsb-release gnupg2 ca-certificates apt-transport-https software-properties-common
sudo add-apt-repository ppa:ondrej/php
sudo apt install php8.2
php -v

# Установка модулей PHP

sudo apt install -y php8.2-<module-name>
sudo apt install php8.2-{dom,json,ctype,pcntl,pcre,fileinfo,tokenizer}
sudo apt install php8.2-json
php -m

# Размещение PHP-приложения на веб-сервере Apache

sudo apt install apache2 libapache2-mod-php8.2
sudo a2enmod php8.2

# Установка Apache с PHP-FPM

sudo apt install php8.2-fpm libapache2-mod-fcgid
sudo a2enmod proxy_fcgi setenvif && sudo a2enconf php8.2-fpm
sudo systemctl restart apache2

# Дополнительные расширения PHP

sudo apt-get install php8.2-redis
sudo apt install php8.2-apcu
