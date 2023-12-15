# Установка ONLYOFFICE Docs Community Edition на Debian, Ubuntu

<!-- Установка и настройка PostgreSQL: -->

sudo apt-get install postgresql

<!-- Зайдем в sql-оболочку под пользователем posgrtes: --> -->

sudo -u postgres psql

<!-- Создадим базу данных и пользователя, который будет иметь к ней полный доступ: -->

=# CREATE DATABASE onlyoffice;

=# CREATE USER onlyoffice WITH password 'Only0ffice_AVA';

=# GRANT ALL privileges ON DATABASE onlyoffice TO onlyoffice;

=# quit

<!-- Установка rabbitmq: -->

sudo apt-get install rabbitmq-server

<!-- При использовании Ubuntu 18.04 потребуется установка пакета nginx-extras. Это можно сделать с помощью команды: -->

sudo apt-get install nginx-extras

# Установка ONLYOFFICE Docs

<!-- Добавьте GPG-ключ: -->

mkdir -p -m 700 ~/.gnupg
curl -fsSL https://download.onlyoffice.com/GPG-KEY-ONLYOFFICE | gpg --no-default-keyring --keyring gnupg-ring:/tmp/onlyoffice.gpg --import
chmod 644 /tmp/onlyoffice.gpg
sudo chown root:root /tmp/onlyoffice.gpg
sudo mv /tmp/onlyoffice.gpg /usr/share/keyrings/onlyoffice.gpg

<!-- Добавьте репозиторий ONLYOFFICE Docs: -->

echo "deb [signed-by=/usr/share/keyrings/onlyoffice.gpg] https://download.onlyoffice.com/repo/debian squeeze main" | sudo tee /etc/apt/sources.list.d/onlyoffice.list

<!-- Обновите кэш менеджера пакетов: -->

sudo apt-get update

<!-- Установите mscorefonts: -->

sudo apt-get install ttf-mscorefonts-installer

<!-- Установите ONLYOFFICE Docs -->

sudo apt-get install onlyoffice-documentserver

<!-- Проверить ответ от onlyoffice можно командой: -->

curl -k https://127.0.0.1/welcome/

<!-- Сервис работает по http -->

# Установка SSL

<!-- Когда у вас будет сертификат, переходите к последующим действиям: -->

<!-- Остановите сервис NGINX: -->

sudo service nginx stop

<!-- Скопируйте файл ds-ssl.conf.tmpl в файл ds.conf с помощью следующей команды: -->

sudo cp -f /etc/onlyoffice/documentserver/nginx/ds-ssl.conf.tmpl /etc/onlyoffice/documentserver/nginx/ds.conf

<!-- Отредактируйте файл /etc/onlyoffice/documentserver/nginx/ds.conf, заменив все параметры в двойных фигурных скобках {{...}} на фактически используемые: -->
<!-- {{SSL_CERTIFICATE_PATH}} - путь к вашему сертификату SSL;
{{SSL_KEY_PATH}} - путь к закрытому ключу сертификата SSL;
{{SSL_VERIFY_CLIENT}} - параметр, определяющий, включена ли проверка клиентских сертификатов (допустимые значения: on, off, optional и optional_no_ca);
{{CA_CERTIFICATES_PATH}} - путь к клиентскому сертификату, который будет проверяться, если проверка включена в предыдущем параметре;
{{ONLYOFFICE_HTTPS_HSTS_MAXAGE}} - дополнительный параметр настройки для задания параметра max-age HSTS в конфигурации виртуального хоста NGINX для ONLYOFFICE Docs, применяется только в тех случаях, когда используется SSL (как правило, по умолчанию задается значение 31536000, что считается достаточно безопасным);
{{SSL_DHPARAM_PATH}} - путь к параметру Диффи-Хеллмана;
Обратитесь к документации NGINX для получения дополнительной информации о параметрах SSL, которые используются в файле конфигурации.

Когда все изменения будут внесены, можно снова запустить сервис NGINX: -->

sudo service nginx start

<!-- Для правильной работы портала должен быть открыт порт 443. -->
<!-- Запустите следующий скрипт: -->

sudo bash /usr/bin/documentserver-update-securelink.sh

<!-- Важно!
1. Установить корневой сертификат
2. Установить данный хост в доверенные на сервере Next cloud -->
