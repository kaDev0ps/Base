16-05-2023

# Останавливаем сервисы Zimbra:

sudo su - zimbra -c "zmproxyctl stop" && su - zimbra -c "zmmailboxdctl stop"

# Открываем доступ во вне

sudo iptables -P INPUT ACCEPT

# Обновляем сертификат

<!-- Проверяем возможность продления, чтобы нас не забанили -->

certbot renew --dry-run

<!-- Если все хорошо то продлеваем -->

certbot renew

<!--
myhost=m.snabavangard.ru
apt install certbot
certbot certonly --standalone -d $myhost -->

# Скачиваем новые сертификаты

**myhost=mail.sppcm.ru**
cd /etc/letsencrypt/live/$myhost/ || exit
wget -4 -O /etc/letsencrypt/live/$myhost/zimbra_chain.pem https://letsencrypt.org/certs/trustid-x3-root.pem.txt
wget -4 -O /etc/letsencrypt/live/$myhost/zimbra_chain.pem https://letsencrypt.org/certs/isrgrootx1.pem.txt

# Перемещаем их в нашу цепочку

cat /etc/letsencrypt/live/$myhost/chain.pem >> /etc/letsencrypt/live/$myhost/zimbra_chain.pem

# Распакуем архив

tar -czf /opt/zimbra/ssl/zimbra-$(date +"%d.%m.%y\_%H.%M").tar.gz --absolute-names /opt/zimbra/ssl/zimbra

# Копируем сертификаты в новую папку и даем права zimbra на эти файлы

<!-- Если выпускаем новый mkdir /opt/zimbra/ssl/letsencrypt -->

<!-- cp /etc/letsencrypt/live/$myhost/* /opt/zimbra/ssl/letsencrypt/ -->

chown -Rfv zimbra:zimbra /opt/zimbra/ssl/letsencrypt/

# Проходим верификацию

sudo su - zimbra -c "zmcertmgr verifycrt comm /opt/zimbra/ssl/letsencrypt/privkey.pem /opt/zimbra/ssl/letsencrypt/cert.pem /opt/zimbra/ssl/letsencrypt/zimbra_chain.pem"

# Копируем правтный ключ и даем доступ zimbra

cp /opt/zimbra/ssl/letsencrypt/privkey.pem /opt/zimbra/ssl/zimbra/commercial/commercial.key
chown zimbra:zimbra /opt/zimbra/ssl/zimbra/commercial/commercial.key

# Останавливаются сервисы, и загружается сертификат

sudo su - zimbra -c "zmproxyctl stop"
sudo su - zimbra -c "zmmailboxdctl stop"
sudo su - zimbra -c "zmcertmgr deploycrt comm /opt/zimbra/ssl/letsencrypt/cert.pem /opt/zimbra/ssl/letsencrypt/zimbra_chain.pem"
sudo su - zimbra -c "zmcontrol restart"

# Закрываем общую таблицу от внешних соединений

sudo iptables -P INPUT DROP

# УСТАНОВКА СЕРТИФИКАТОВ

Для установки сертификатов имеет смысл сделать бэкап текущих
Копируется файл rivkey.pem в /opt/zimbra/ssl/zimbra/commercial/commercial.key

cp /opt/zimbra/ssl/letsencrypt/privkey.pem /opt/zimbra/ssl/zimbra/commercial/commercial.key
chown zimbra:zimbra /opt/zimbra/ssl/zimbra/commercial/commercial.key

Останавливаем сервисы Zimbra, и загружаем новые сертификаты и после этого запускаем службы снова:
sudo su - zimbra -c "zmproxyctl stop"
sudo su - zimbra -c "zmmailboxdctl stop"
sudo su - zimbra -c "zmcertmgr deploycrt comm /opt/zimbra/ssl/letsencrypt/cert.pem /opt/zimbra/ssl/letsencrypt/zimbra_chain.pem"

Имеет смысл сделать переадресацию веб интерфейса с HTTP на HTTPS

sudo su - zimbra -c "/opt/zimbra/bin/zmprov ms mail.sppcm.ru zimbraReverseProxyMailMode redirect"
sudo su - zimbra -c "zmproxyctl restart"

# ОБНОВЛЕНИЕ СЕРТИФИКАТОВ

<!-- Обновление происходит в том же порядке, с разницей что certboot запускается на обновление. Ниже готовый скрипт.
Он так же подойдет для первоначальной генерации сертификатов если заменить строчку certbot renew на certbot certonly —standalone -d mail.sppcm.ru
Скрипт старался писать универсальным, поэтому достаточно вписать своё имя домена в переменную mail.sppcm.ru

Скрипт можно добавить в crontab и выполнять от учетной записи root раз в месяц -->

nano /etc/cron.monthly/zimbrassl && chmod +x /etc/cron.monthly/zimbrassl

#!/bin/bash

DOMAIN=m.snabavangard.ru

certbot renew

cd /etc/letsencrypt/live/$DOMAIN/ || exit
wget -4 -O /etc/letsencrypt/live/$DOMAIN/zimbra_chain.pem https://letsencrypt.org/certs/trustid-x3-root.pem.txt
wget -4 -O /etc/letsencrypt/live/$DOMAIN/zimbra_chain.pem https://letsencrypt.org/certs/isrgrootx1.pem.txt

cat /etc/letsencrypt/live/$DOMAIN/chain.pem >> /etc/letsencrypt/live/$DOMAIN/zimbra_chain.pem

# Распакуем архив

tar -czf /opt/zimbra/ssl/zimbra-$(date +"%d.%m.%y\_%H.%M").tar.gz --absolute-names /opt/zimbra/ssl/zimbra

# Для этого в папке сервера создадим папку сертификатов letsencrypt, поместим туда полученные сертификаты, и так как сервер работает от учетной записи zimbra, дадим права на эти файлы

# mkdir /opt/zimbra/ssl/letsencrypt

cp /etc/letsencrypt/live/$DOMAIN/\* /opt/zimbra/ssl/letsencrypt/
chown -Rfv zimbra:zimbra /opt/zimbra/ssl/letsencrypt/

#

sudo su - zimbra -c "zmcertmgr verifycrt comm /opt/zimbra/ssl/letsencrypt/privkey.pem /opt/zimbra/ssl/letsencrypt/cert.pem /opt/zimbra/ssl/letsencrypt/zimbra_chain.pem"

#

cp /opt/zimbra/ssl/letsencrypt/privkey.pem /opt/zimbra/ssl/zimbra/commercial/commercial.key
chown zimbra:zimbra /opt/zimbra/ssl/zimbra/commercial/commercial.key

# Останавливаются сервисы, и загружается сертификат,

sudo su - zimbra -c "zmproxyctl stop"
sudo su - zimbra -c "zmmailboxdctl stop"
sudo su - zimbra -c "zmcertmgr deploycrt comm /opt/zimbra/ssl/letsencrypt/cert.pem /opt/zimbra/ssl/letsencrypt/zimbra_chain.pem"
sudo su - zimbra -c "zmcontrol restart"

##########################################################################################################################################

/opt/zimbra/bin/zmcertmgr createcsr comm -new -subject "/C=RU/ST=SPB/L=Mail/O='Avangard'/CN=m.snabavangard.ru"

#################################################################################################################

# Проверка размера почтовых ящиков

su - zimbra
all_accounts=`zmprov -l gaa`; for account in $all_accounts; do mbox_size=`zmmailbox -z -m $account gms`; echo "Mailbox size of $account = $mbox_size"; done ;

# Проверка работы служб

zmcontrol status

# DKIM

su - zimbra -c "/opt/zimbra/libexec/zmdkimkeyutil -a -d mail.sppcm.ru"
40416232-B43C-11ED-AEE7-22528099B07E.\_domainkey IN TXT ( "v=DKIM1; k=rsa; "
"p=MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA7/C4ZkBKIGvQh8EKFvoBXBE2GmVs4yali5U6CPLCq2jr3eTqcTD/ld8X4W+2eIxTVJkyvSiZIzFc/oItTiJO6zyiPtwWjj4d/hnrFQOUwuLfzLvn/5tDoxKhSWTu4A0uEsyTMxOrgDsPq6CjmOFym6NE4ZjdJFgazEq2lOFadk0UxLCP0xQ5HhPAtQ4vsxFG5XVivf9fsVNjLm"
"5XU80OS4Ms4NPvCeL8vvvo0+ud6dE9vsl5I4akHZi2orWqjS6vZIM4ugCvKcZc4gKsm+b+xYTMw3Jk0rZhzO2u4a0EY6xsLf9AJR8Yz698g7snJqeg0apRShcf/nd+Um9WtCXGfwIDAQAB" ) ; ----- DKIM key 40416232-B43C-11ED-AEE7-22528099B07E for sppcm.ru

su - zimbra -c "/opt/zimbra/libexec/zmdkimkeyutil -q -d sppcm.ru"
opendkim-testkey -d sppcm.ru -s 40416232-B43C-11ED-AEE7-22528099B07E -x /opt/zimbra/conf/opendkim.conf

# DMARC

\_dmarc TXT v=DMARC1;p=quarantine;sp=quarantine;pct=100;aspf=r;fo=1;rua=mailto:ka@zelobit.com;ruf=mailto:ka@zelobit.com

не прошедшие проверку DKIM или SPF помещаем в СПАМ
pct=, опциональный параметр, доля обрабатываемых DMARC писем. По умолчанию 100 (100 %), и может быть снижена для отладочных целей.
aspf=, опциональный параметр, со значениями s (strict) и r (relaxed, по умолчанию) для SPF, требующий совпадения ответа команды MAIL FROM и заголовка From письма. r разрешает субдомены.
fo=, fail policy, опциональный с значением по умолчанию fo=0. В значении 0, отсылает обратный отчёт если все проверки невалидны (DKIM, SPF); в значении 1 — если какая-либо из проверок не валидна. Также возможны значения s и d, соответственно для отчетов по DKIM и SPF.
rua=, опциональный, список почтовых адресов через запятую, на которые высылать агрегированные отчеты. Если адрес находится в том же домене, работает без дополнительных настроек.
ruf=, опциональный, список почтовых адресов через запятую, на которые высылать fail-отчеты (о невалидной почте). Если адрес находится в том же домене, работает без дополнительных настроек.

# SPF

@ TXT v=spf1 a mx ip4:95.214.119.12 include:\_spf.mail.sppcm.ru ~all

40416232-B43C-11ED-AEE7-22528099B07E.\_domainkey

# проверяем соответствие сертификата

opendkim-testkey -d sppcm.ru -s 40416232-B43C-11ED-AEE7-22528099B07E -x /opt/zimbra/conf/opendkim.conf
