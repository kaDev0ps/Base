# Установка веб-сервера apache

sudo apt-get -y install apache2

# Проверяем запущена ли служба

sudo systemctl status apache2

<!-- Настройки хранятся в apache2

avaible - доступно
enabled - активно
apache2.conf - все настройки службы
ports.conf - порт по которому работает служба
a2enmod - активировать модуль
a2dismod- деактивировать модуль
a2ensite - активировать сайт
a2dissite- деактивировать сайт

Команды
journalctl -u apache2 - проверить все события apache2
ls /var/log/apache2/
access.log доступы к сайту
error.log ошибки в работе

 Виртуальные хосты
/sites-available/
000-default.conf

VirtualHost *:80 Порт по которому работает сайт
ServerName www.example.com Имя сайта
DocumentRoot /var/www/html Путь к сайту
После внесения изменений перезагружаем apache
-->

systemctl reload apache2

# Установка модуля PHP

apt search libapache2-mod-php
apt install libapache2-mod-php -y

# Проверяем

cd /var/www/html

<!-- создаем файл с расширением php и содержимым -->
<?php
phpinfo();
?>

<!-- Переходим в браузере ip/test.php -->
<!-- Все настройки php правим в файле -->

cd /etc/php/7.4/apache2/php.ini

Остановить и отключить ap
systemctl stop apache2
systemctl disable apache2
