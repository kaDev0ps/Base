# Установка apache2:

## Источник https://losst.pro/ustanovka-i-nastrojka-servera-apache

sudo apt install apache2

<!--
В других дистрибутивах пакет программы называется либо так, либо httpd и его установка у вас не вызовет трудностей. -->
<!-- Добавляем в автозагрузку  -->

sudo systemctl enable apache2

<!-- Все настройки содержатся в папке /etc/apache/:

Файл /etc/apache2/apache2.conf отвечает за основные настройки
/etc/apache2/conf-available/* - дополнительные настройки веб-сервера
/etc/apache2/mods-available/* - настройки модулей
/etc/apache2/sites-available/* - настойки виртуальных хостов
/etc/apache2/ports.conf - порты, на которых работает apache
/etc/apache2/envvars

Как вы заметили есть две папки для conf, mods и site. Это available и enabled. При включении модуля или хоста создается символическая ссылка из папки available (доступно) в папку enable (включено). Поэтому настройки лучше выполнять именно в папках available. Вообще говоря, можно было бы обойтись без этих папок, взять все и по старинке свалить в один файл, и все бы работало, но сейчас так никто не делает.

Как вы заметили есть две папки для conf, mods и site. Это available и enabled. При включении модуля или хоста создается символическая ссылка из папки available (доступно) в папку enable (включено). Поэтому настройки лучше выполнять именно в папках available. Вообще говоря, можно было бы обойтись без этих папок, взять все и по старинке свалить в один файл, и все бы работало, но сейчас так никто не делает.

Сначала давайте рассмотрим главный файл конфигурации: -->

vi /etc/apache2/apache2.conf

 <!-- Apache - модульная программа, ее функциональность можно расширять с помощью модулей. Все доступные модули загрузчики и конфигурационные файлы модулей находятся в папке /etc/apache/mods-available. А активированные в /etc/apache/mods-enable. -->

 <!-- Включить модуль можно командой: -->

sudo a2enmod expires

sudo a2enmod headers

sudo a2enmod rewrite

sudo a2enmod ssl

<!-- Модули expires и headers уменьшают нагрузку на сервер. Они возвращают заголовок Not Modified, если документ не изменился с последнего запроса. Модуль expiries позволяет устанавливать время, на которое браузер должен кэшировать полученный документ. Rewrite позволяет изменять запрашиваемые адреса на лету, очень полезно при создании ЧПУ ссылок и т д. А последний для включения поддержки шифрования по SSL. Не забудьте перезагрузить apache2 после завершения настроек. -->

<!-- А отключить: -->

sudo a2dismod имя_модуля

<!-- После включения или отключения модулей нужно перезагрузить apache: -->

sudo systemctl restart apache2

# Настройка виртуальных хостов Apache

 <!-- Настройки хостов Apache расположены в папке /etc/apache2/sites-available/. Для создания нового хоста достаточно создать файл с любым именем (лучше кончено с именем хоста) и заполнить его нужными данными. Обернуть все эти параметры нужно в директиву VirtualHost. Кроме рассмотренных параметров здесь будут использоваться такие:

ServerName - основное имя домена
ServerAlias - дополнительное имя, по которому будет доступен сайт
ServerAdmin - электронная почта администратора
DocumentRoot - папка с документами для этого домена
Например: -->

vi /etc/apache2/sites-available/test.site.conf

<VirtualHost \*:80>
ServerName test.site
ServerAlias www.test.site
ServerAdmin webmaster@localhost
DocumentRoot /var/www/test.site/
ErrorLog ${APACHE_LOG_DIR}/error.log
CustomLog ${APACHE_LOG_DIR}/access.log combined
</VirtualHost>

 <!-- Виртуальные хосты, как и модули нужно активировать. Для этого есть специальные утилиты. Чтобы активировать наберите: -->

 <!-- sudo a2ensite test.site -->

systemctl reload apache2

<!-- Здесь test.site - имя файла виртуального хоста. Для отключения тоже есть команда:

 sudo a2dissite test.site -->

# Убрать тестовую страницу апач
<!-- Если CentOS -->
 sudo systemctl status httpd
<!-- Разрешить индексы в Apache
Без индекса  будет отображаться стандартная страница приветствия Apache, если не изменить /etc/httpd/conf.d/welcome.conf, чтобы разрешить индексы. Отредактируем /etc/httpd/conf.d/welcome.conf, чтобы разрешить индексы. -->

nano /etc/httpd/conf.d/welcome.conf
<!-- и заменяем весь текст на один из указанных ниже -->

<LocationMatch "^/+$">
    # Options -Indexes
    ErrorDocument 403 /error/noindex.html
</LocationMatch>
<LocationMatch "^/+$">
    Options -Indexes
</LocationMatch>
<!-- перезапустить apache -->

systemctl restart httpd

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
