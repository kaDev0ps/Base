# Устанавливаем веб-сервер:

apt install apache2

<!-- Создаем виртуальный домен и настраиваем его для работы с облачным сервисом: -->

vi /etc/apache2/sites-enabled/nextcloud.conf

<VirtualHost \*:80>
Define root_domain nextcloud.dmosk.ru
ServerName ${root_domain}
    Redirect / https://${root_domain}
</VirtualHost>

<VirtualHost \*:443>
Define root_domain nextcloud.dmosk.ru
Define root_path /var/www/nextcloud

    ServerName ${root_domain}
    DocumentRoot ${root_path}

    SSLEngine on
    SSLCertificateFile ssl/cert.pem
    SSLCertificateKeyFile ssl/cert.key

    <IfModule mod_headers.c>
        Header always set Strict-Transport-Security "max-age=15552000; includeSubDomains; preload"
    </IfModule>
    <Directory ${root_path}>
        AllowOverride All
        Require all granted
    </Directory>

</VirtualHost>

<!-- * где:

nextcloud.dmosk.ru — домен, на котором будет работать сервис.
ssl/cert.pem — открытый сертификат. Его мы создадим ниже по инструкции.
ssl/cert.key — путь до ключа закрытого сертификата. Его мы создадим ниже по инструкции.
/var/www/nextcloud — каталог с порталом. В него мы распакуем исходники.
Разрешаем модули: -->

a2enmod ssl rewrite headers env dir mime

<!-- * где:

ssl — поддержка шифрования.
rewrite — модуль, позволяющий выполнять перенаправления на уровне apache.
headers — позволяет менять заголовки при http-ответах.
env — управление системными переменными, которые используются другими модулями Apache.
dir — для поиска скрипта по умолчанию и отображения файлов в каталоге.
mime — назначает метаданные при ответах.
Создаем каталог для хранения сертификатов и переходим в него: -->

mkdir /etc/apache2/ssl

cd /etc/apache2/ssl

<!-- Генерируем сертификат: -->

openssl req -new -x509 -days 1461 -nodes -out cert.pem -keyout cert.key -subj "/C=RU/ST=SPb/L=SPb/O=Global Security/OU=IT Department/CN=nextcloud.dmosk.ru/CN=nextcloud"

<!-- * данная команда создаст сертификат на 4 года для URL nextcloud.dmosk.ru или nextcloud.

Для первичного запуска Nextcloud достаточно самоподписанного сертификата. Однако, для продуктивной среду лучше его купить или запросить бесплатный у Let's Encrypt.

Проверяем конфигурацию apache: -->

apachectl configtest

<!-- ... если видим:

... -->

Syntax OK

<!-- * мы увидим предупреждение Warning: DocumentRoot [/var/www/nextcloud] does not exist. Оно означает, что каталога /var/www/nextcloud не существует. Игнорируем — мы созданим его позже.

Разрешаем автозапуск апача и перезапускаем сервис: -->

systemctl enable apache2

systemctl restart apache2
