# Мониторинг срока действия сертификатов Lets Encrypt

Дата: 05.05.2017 Автор Admin

Я думаю многие из вас пользуются бесплатными сертификатами Lets Encrypt, но многие ли мониторят срок их действия?

Подкатом я покажу простой bash скрипт, который будет заниматься мониторингом сроков действия сертификатов.

Для настройки оповещений первым делом настройте ssmtp , т.к его мы будем использовать для отправки писем. Как это сделать написано в этой статье.

Теперь сам скрипт мониторинга:

Мониторинг срока действия сертификатов Lets Encrypt
Дата: 05.05.2017 Автор Admin
Я думаю многие из вас пользуются бесплатными сертификатами Lets Encrypt, но многие ли мониторят срок их действия?

Подкатом я покажу простой bash скрипт, который будет заниматься мониторингом сроков действия сертификатов.

Для настройки оповещений первым делом настройте ssmtp , т.к его мы будем использовать для отправки писем. Как это сделать написано в этой статье.

Теперь сам скрипт мониторинга:

<!-- #!/bin/bash

SERVER="yourServerName"
EMAIL='your@email.com'
FROM='yourServer@email.com'
ALERTMESSAGE='/tmp/ALERTMESSAGE_cert.tmp'

if /usr/bin/openssl x509 -checkend 86400 -noout -in /etc/letsencrypt/live/yourDOMAIN/fullchain.pem
then
  echo "Certificate is good for another day!"

else

echo "To: $EMAIL" > $ALERTMESSAGE
echo "From: $FROM" >> $ALERTMESSAGE
echo "Subject: SSL Cert Renew!" >> $ALERTMESSAGE
echo "" >> $ALERTMESSAGE
echo "Certificate has expired or will do so within 24 hours!" >> $ALERTMESSAGE
echo "Start LetsEncrypt" >> $ALERTMESSAGE
/opt/letsencrypt/letsencrypt-auto renew >> $ALERTMESSAGE

/usr/sbin/ssmtp $EMAIL < $ALERTMESSAGE

service nginx reload
fi -->

В данном скрипте замените следующие строки:

SERVER=»yourServerName» — Имя сервера (можно указать любое)
EMAIL=’your@email.com’ — Ваш Email
FROM=’yourServer@email.com’ — Email SSMTP
/etc/letsencrypt/live/yourDOMAIN/fullchain.pem — в этой строке вместо yourDOMAIN укажите ваш домен, на который выдан сертификат
service nginx reload — эта строка перезагружает конфиг вебсервера nginx, если у вас другой вебсервер замените эту строку

Теперь командой chmod +x sslmonitoring.sh сделайте скрипт исполняемым (где sslmonitoring.sh имя скрипта)

Далее просто добавьте скрипт в cron , командой crontab -e

0 1 \* \* _ /path_to_script/sslmonitoring.sh
1
0 1 _ \* \* /path_to_script/sslmonitoring.sh
Данный скрипт будет запускаться раз в сутки в час ночи.

Если сертификат закончится через 24 часа, то он запустит клиент letsencrypt и обновит сертификат , после чего перезапустит веб сервер.
