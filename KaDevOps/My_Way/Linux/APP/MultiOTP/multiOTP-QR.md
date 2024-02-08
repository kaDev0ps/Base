# Установка двухфакторной аутентификации

https://download.multiotp.net/
https://habr.com/ru/articles/308988/
https://github.com/multiOTP/multiOTPCredentialProvider/releases

cd /usr/local/bin/multiotp/

<!-- Шаг 2. Настраиваем подключение и импортируем пользователей Active Directory
Для этого нам понадобится доступ в консоль, и, непосредственно файл multiotp.php, используя который мы настроим параметры подключения к Active Directory.

Переходим в директорию /usr/local/bin/multiotp/ и по очереди выполняем следующие команды: -->

./multiotp.php -config default-request-prefix-pin=0

Определяет, требуется ли наличие дополнительного (постоянного) пина при вводе одноразового пина (0 или 1)

./multiotp.php -config default-request-ldap-pwd=0

Определяет, требуется ли ввод доменного пароля при вводе одноразового пина (0 или 1)

./multiotp.php -config ldap-server-type=1

Указывается тип LDAP-сервер (0 = обычный LDAP-сервер, в нашем случае 1 = Active Directory)

./multiotp.php -config ldap-cn-identifier="sAMAccountName"

Указывает, в каком формате представлять имя пользователя (данное значение выведет только имя, без домена)

./multiotp.php -config ldap-group-cn-identifier="sAMAccountName"

То же самое, только для группы

./multiotp.php -config ldap-group-attribute="memberOf"

Указывает метод определения принадлежности пользователя к группе

./multiotp.php -config ldap-ssl=0

Использовать ли безопасное подключение к LDAP-серверу (конечно — пока 0, но лучше 1 да!)

./multiotp.php -config ldap-port=389

Порт для подключения к LDAP-серверу

<!-- ./multiotp.php -config ldap-ssl=1

Использовать ли безопасное подключение к LDAP-серверу (конечно — 1 да!)

./multiotp.php -config ldap-port=636

Порт для подключения к LDAP-серверу -->

./multiotp.php -config ldap-domain-controllers=srv-dc.snab.local

Адрес вашего сервера Active Directory

./multiotp.php -config ldap-base-dn="OU=Office Users,DC=snab,DC=local"

Указываем, откуда начинать поиск пользователей в домене

./multiotp.php -config ldap-bind-dn="multiotp_srv@snab.local"

Указываем пользователя, у которого есть права поиска в Active Directory

./multiotp.php -config ldap-server-password="QR-P@ssw0rd!"

Указываем пароль пользователя, для подключения к Active Directory

./multiotp.php -config ldap-network-timeout=10

Выставляем таймаут для подключения к Active Directory

./multiotp.php -config ldap-time-limit=30

Выставляем ограничение по времени, на операцию импорта пользователей

./multiotp.php -config ldap-activated=1

Активируем конфигурацию подключения к Active Directory

REM ключ для доступа к MultiOTP серверу

multiotp -config server-secret=secret_AVA

REM ключ для доступа к MultiOTP серверу

./multiotp.php -debug -display-log -ldap-users-sync

Производим импорт пользователей из Active Directory

# Настройка клиента

Создаем группу without2FA а включаем в нее пользователей, для которых не нужен QR код.

# SSL

<!-- Редактируем настройки тут -->

nano /etc/nginx/sites-enabled/multiotp

<!-- Меняем на наши пути -->

    ssl_certificate     /etc/multiotp/certificates/multiotp.crt;
    ssl_certificate_key /etc/multiotp/certificates/multiotp.key;

<!-- Загружаем корневой сертификат -->

# Перезапуск службы

systemctl restart nginx.service

# Для редактирования в реестре

Компьютер\HKEY_CLASSES_ROOT\CLSID\{FCEFDFAB-B0A1-4C4D-8B2B-4FF4E0A3D978}
