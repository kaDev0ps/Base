# Пример установки на Ubuntu 20.04 LTS

sudo apt update
sudo apt install apache2 mariadb-server libapache2-mod-php8.2
sudo apt install php8.2-gd php8.2-mysql php8.2-curl php8.2-mbstring php8.2-intl
sudo apt install php8.2-gmp php8.2-bcmath php-imagick php8.2-xml php8.2-zip

<!-- Чтобы запустить режим командной строки MySQL, используйте следующую команду и нажмите клавишу ввода при запросе пароля: -->

sudo /etc/init.d/mysql start
sudo mysql -uroot -p

<!-- Затем появится приглашение MariaDB [root]> . Теперь введите следующие строки, заменив имя пользователя и пароль соответствующими значениями, и подтвердите их клавишей ввода: -->

CREATE USER 'username'@'localhost' IDENTIFIED BY 'password';
CREATE DATABASE IF NOT EXISTS nextcloud CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
GRANT ALL PRIVILEGES ON nextcloud.\* TO 'username'@'localhost';
FLUSH PRIVILEGES;
quit;

<!-- Теперь скачайте архив последней версии Nextcloud:

Перейдите на страницу загрузки Nextcloud https://nextcloud.com/install/. (Community projects > Archive .tar.bz2)

Это загружает файл с именем nextcloud-xyztar.bz2 или nextcloud-xyzzip (где xyz — номер версии).

В Примере у нс архив latest.tar.bz2

Загрузите соответствующий файл контрольной суммы, например, latest.tar.bz2.md5

Проверьте сумму MD5 или SHA256: -->

md5sum -c latest.tar.bz2.md5 < latest.tar.bz2

<!-- Теперь вы можете извлечь содержимое архива. Запустите соответствующую команду распаковки для вашего типа архива: -->

tar -xjvf latest.tar.bz2

<!-- Это распаковывает в один nextcloudкаталог. Скопируйте каталог Nextcloud в его конечный пункт назначения. Когда вы используете HTTP-сервер Apache, вы можете безопасно установить Nextcloud в корневой каталог документов Apache: -->
<!--
cp -r nextcloud /path/to/webserver/document-root
где /path/to/webserver/document-root заменяется корневым документом вашего веб-сервера: -->

cp -r nextcloud /var/www

<!-- Задаем права доступа: -->

chown -R www-data:www-data /var/www/nextcloud

## Дальше идет настройка веб-cервера Apache

<!-- Добавляем в доверенные узлы домен -->

vi /var/www/nextcloud/config/config.php

'trusted_domains' =>
[
'cloud.snabavangard.ru',
'onlyoffice.snabavangard.local',
'10.200.202.45'
'10.200.202.46'
],

systemctl restart apache2

<!-- Оптимизируем работу базы данных: -->

sudo -u www-data php /var/www/nextcloud/occ db:convert-filecache-bigint

sudo -u www-data php /var/www/nextcloud/occ db:add-missing-indices

# Меняем максимальное значение памяти PHP

<!-- Открываем на редактирование файл: -->
<!-- Меняем настройку для memory_limit: -->

vi /etc/php/8.2/fpm/php.ini
memory_limit = 512M
vi /etc/php/8.2/apache2/php.ini
memory_limit = 512M

<!-- Перезапускаем php-fpm: -->

systemctl restart php8.2-fpm

<!-- В системе не установлены рекомендуемые модули PHP
Данная ошибка устраняется в зависимости от списка модулей, которых не хватает системе. Чаще всего, подходит команда: -->

dnf install php-<название модуля>

<!-- Например: -->

apt install php-gmp php-imagick

<!-- После перезапускаем php-fpm: -->

systemctl restart php8.2-fpm

<!-- Не настроена система кеширования
Для решения проблемы мы должны установить и настроить одно из средств кэширования: -->

APCu
Redis
Memcached

<!-- Мы рассмотрим Redis. -->

<!-- Устанавливаем сам Redis Server и модуль php: -->

apt install redis-server php-redis

<!-- * в случае установки сервера Redis на отдельный сервер, необходимо выполнить на сервере Nextcloud только установку php-redis.

Если версия PHP не нативная для системы, то команда будет такой: -->

apt install redis-server php${PHP_VER}-redis

<!-- Перезапускаем php-fpm: -->

systemctl restart php8.2-fpm

<!-- Открываем конфигурационный файл для nextcloud: -->

vi /var/www/nextcloud/config/config.php

<!-- И добавим: -->

'filelocking.enabled' => true,
'memcache.local' => '\\OC\\Memcache\\Redis',
'memcache.distributed' => '\\OC\\Memcache\\Redis',
'memcache.locking' => '\\OC\\Memcache\\Redis',
'redis' => array(
'host' => 'localhost',
'port' => 6379,
'timeout' => 0.0,
),

<!-- Готово. -->

<!-- Выполняем  -->
<!-- Перезапускаем php-fpm: -->

systemctl restart php8.2-fpm

<!-- Не указан регион размещения этого сервера Nextcloud
Для решения проблемы открываем конфигурационный файл nextcloud: -->

vi /var/www/nextcloud/config/config.php

<!-- Добавляем: -->

...
'default_phone_region' => 'RU',

<!-- Для работы с SVG устанавливаем библиотеку -->

apt-get install libmagickcore-6.q16-6-extra

# Настройка разрешений

<!-- Перейти в .var.www.nextcloud -->

vi /var/www/nextcloud/.htaccess

<!-- Добавить  -->

<IfModule mod_rewrite.c>
  RewriteEngine on
  RewriteRule ^/\.well-known/carddav /nextcloud/remote.php/dav [R=301,L]
  RewriteRule ^/\.well-known/caldav /nextcloud/remote.php/dav [R=301,L]
  RewriteRule ^/\.well-known/webfinger /nextcloud/index.php/.well-known/webfinger [R=301,L]
  RewriteRule ^/\.well-known/nodeinfo /nextcloud/index.php/.well-known/nodeinfo [R=301,L]
</IfModule>

<!-- Важно!
1. Установить корневой сертификат
2. Установить данный хост в доверенные на сервере Next cloud
3. Перезагрузить сервер-->

reboot

# Добавление пользователей

sudo -u www-data php /var/www/nextcloud/occ user:add admin

<!-- Сброс пароля -->
<!-- При необходимости сбросить пароль пользователя, можно воспользоваться командой: -->

sudo -u www-data php /var/www/nextcloud/occ user:resetpassword admin

# Защита от брут-форс атак

<!-- Данная возможность позволяет блокировать подключения от IP-адресов, с которых было совершено слишком много неудачных попыток войти в систему. Рассмотрим возможность изменения числа неудачных попыток или отключения возможности.

Число попыток
Текущее значени для числа попыток ввода пароля можно посмотреть командой: -->

sudo -u www-data php /var/www/nextcloud/occ config:system:get brute-force-attempts

<!-- Если она вернет пустое значение, значит используется значение по умолчанию — 10.

Чтобы его изменить, открываем конфигурационный файл: -->

vi /var/www/nextcloud/config/config.php

<!-- Добавляем: -->

'brute-force-attempts' => 50,

<!-- * в данном примере мы разрешили ввести неправильно пароль 50 раз, после чего будет активирована защита.

Отключение
Если по какой-либо причине нам не нужна эта функция и мы хотим ее отключить, открываем конфигурационный файл: -->

vi /var/www/nextcloud/config/config.php

<!-- Добавляем строку: -->

'auth.bruteforce.protection.enabled' => false,

<!-- Готово. -->
