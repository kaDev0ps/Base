# Формируем файл запроса (на компьютере с Linux)

<!-- Для начала создаем каталог, в котором планируем хранить сертификаты и перейдем в него, например: -->

mkdir -p /etc/ssl/adcs

cd /etc/ssl/adcs

<!-- Создаем закрытый ключ: -->

openssl genrsa -out ipA.key 2048

<!-- * в данном примере создан 2048-и битный ключ с именем private.key -->

<!-- Создаем файл с описанием запроса: -->

vi openssl.conf

[req]
default_bits = 2048
prompt = no
default_md = sha256
req_extensions = req_extensions
distinguished_name = dn

[dn]
C=RU
ST=SPb
L=SPb
O=Global Security
OU=IT Department
emailAddress=ka@zelobit.com
CN = m.snabavangard.ru

[req_extensions]
subjectAltName = @alter_name

[alter_name]
DNS.1 = m.snabavangard.ru
DNS.2 = mail.snabavangard.local
IP.1 = 10.200.202.147

<!-- Далее создаем ключ запроса сертификата: -->

openssl req -new -key ipA.key -out ipA.csr -config openssl.conf

<!-- * где private.key — закрытый ключ, который был сформирован на предыдущем шаге; request.csr — файл, который будет сформирован. В нем будет запрос на открытый ключ; -->
<!--
После выполнения команды, в каталоге появится файл request.csr. Выведем его содержимое, чтобы посмотреть запрос: -->

cat ipA.csr

Должны увидеть что-то на подобие:

-----BEGIN CERTIFICATE REQUEST-----
MIIC1jCCAb4CAQAwgZAxCzAJBgNVBAYTAlJVMQwwCgYDVQQIDANTUGIxDDAKBgNV
BAcMA1NQYjEYMBYGA1UECgwPR2xvYmFsIFNlY3VyaXR5MRYwFAYDVQQLDA1JVCBE
ZXBhcnRtZW50MR4wHAYDVQQDDBVzaW1wbGVwbGFuLnNhdHMubG9jYWwxEzARBgNV
BAMMCnNpbXBsZXBsYW4wggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDZ
ze/WOQgnlbfzlEsQUsmfUTnkYq0rCpzgjY360lFvqek5Y8NIFX/25PRbUy4N3D8r
c/7mXt2dXmcnn7zeRQOB2g0AY8Wmeg3R6C+JH7TwxtkMj7FO8R59URxFN84lu9Sj
26Aw+Ax7474XnAoUBMSmUXbV2mAP5Xm83sjvjE1OcHXN8SPbc+EchZuLVLsIGXHz
Emz7V4D/ecahfSc2hCRG2Pc7SeFIADYdjyoLtykz5WyiIoXkpEfSNQHlt2/A1kJ5
h9/GPXMVJVL02FgsI5HIGZyGnYWA+cP7sHoEDZNpLHHuEtfwx3bLxJPFnZDa0rPO
LhV/ux1e6b9ikrge0xp9AgMBAAGgADANBgkqhkiG9w0BAQsFAAOCAQEAPVD2aVH8
ZDz+6s8ecKr08eT3mA9cRCa7dZYFj4/vO/ZYrDH9QS45gd0sYG+RN+JMGaFaY+X7
faE4m0BysPIbhEYLRY5GJYxmOGm05gM2HfPrcnnWXZbPQu/n5pR4ptvPYL7bilGb
z6hXb8ZtXUXwz1F2OgTPOPu4+w8lI23pzHRHlCXcVSoZxe/A2XwusB5MrtyMEYtj
rB2kcOqQRkt9uIv5IobwnYaVGDk/7wl/zkb9K+RKZt4izKfFaSSyPn7wvpKSIaAf
S3SQjJ0tBckLbtwKrxdFB0B8bpyIKUtHmpX/zOmityC8PLXe6/vQ/DmM3B6QC4Ba
KdnRkOSPv8BGog==
-----END CERTIFICATE REQUEST-----

<!-- Копируем содержимое файла запроса (request.csr) и открываем веб-страницу центра сертификации — как правило, это имя сервера или IP-адрес + certsrv, например, http://192.168.0.15/certsrv/.

В открывшемся окне переходим по ссылкам Запроса сертификата - расширенный запрос сертификата.

В поле «Сохраненный запрос» вставляем скопированный запрос, в а поле «Шаблон сертификата» выбираем Веб-сервер:
Нажимаем Выдать и скачиваем сертификат по ссылке DER Загрузить сертификат: -->

# Установка сертификата на Linux

<!-- Переносим полученный сертификат на компьютер с Linux, например, при помощи WinSCP.

После необходимо сконвертировать DER-сертификат (от AD CS) в PEM-сертификат (для Linux). Для этого вводим следующую команду: -->

openssl x509 -inform der -in ip4.cer -out ip4.pem

<!-- * где certnew.cer — файл, который мы получили от центра сертификации AD CS (как правило, у него такое имя); public.pem — имя PEM-сертификата.

Сертификат готов к использованию. Мы сформировали: -->

private.key — закрытый ключ.
public.pem — открытый ключ.

# Указываем путь к нашему сертификату

vi /etc/apache2/sites-enabled/000_default-ssl.conf

<!-- В открытый файл добавляем следующее:

<VirtualHost *:443>
    ServerName site.ru
    DocumentRoot /var/www/apache/data
    SSLEngine on
    SSLCertificateFile ssl/cert.pem
    SSLCertificateKeyFile ssl/cert.key
    #SSLCertificateChainFile ssl/cert.ca-bundle
</VirtualHost>

* где:

ServerName — домен сайта;
DocumentRoot — расположение файлов сайта в системе;
SSLCertificateFile и SSLCertificateKeyFile — пути до файлов ключей, которые были сгенерированы на шаге 1;
SSLCertificateChainFile — при необходимости, путь до цепочки сертификатов (если используем не самоподписанный сертификат). -->

<!-- или в файле site.conf -->

<!-- Перезапустите Apache, чтобы изменения вступили в силу. -->

systemctl reload apache2

## Если Apache не запускается сайт не открывается и большая печаль то...

<!-- Смотрим ошибки командой -->

systemctl status apache2.service

<!-- Лог хранится тут /etc/apache2/apache2.conf -->

<!-- Исправляем и запускаем -->

apachectl stop
/etc/init.d/apache2 start

<!-- Перезагружаем сервер -->

/etc/init.d/apache2 reload
