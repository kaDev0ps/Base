16-05-2023

# Останавливаем сервисы Zimbra:

sudo su - zimbra -c "zmproxyctl stop" && su - zimbra -c "zmmailboxdctl stop"

# Для начала нам потребуется установить репозиторий epel-release и пакет certbot

apt install epel-release
apt install certbot

# Открываем доступ во вне

sudo iptables -P INPUT ACCEPT

# Выпускаем сертификат

myhost=m.snabavangard.ru
certbot certonly --standalone -d $myhost

# Скачиваем новые сертификаты

cd /etc/letsencrypt/live/$myhost/ || exit
wget -4 -O /etc/letsencrypt/live/$myhost/zimbra_chain.pem https://letsencrypt.org/certs/trustid-x3-root.pem.txt && wget -4 -O /etc/letsencrypt/live/$myhost/zimbra_chain.pem https://letsencrypt.org/certs/isrgrootx1.pem.txt

# Перемещаем их в нашу цепочку

cat /etc/letsencrypt/live/$myhost/chain.pem >> /etc/letsencrypt/live/$myhost/zimbra_chain.pem

# Распакуем архив

tar -czf /opt/zimbra/ssl/zimbra-$(date +"%d.%m.%y\_%H.%M").tar.gz --absolute-names /opt/zimbra/ssl/zimbra

# Копируем сертификаты в новую папку и даем права zimbra на эти файлы

mkdir /opt/zimbra/ssl/letsencrypt
cp /etc/letsencrypt/live/$myhost/\* /opt/zimbra/ssl/letsencrypt/
chown -Rfv zimbra:zimbra /opt/zimbra/ssl/letsencrypt/

# Проходим верификацию

sudo su - zimbra -c "zmcertmgr verifycrt comm /opt/zimbra/ssl/letsencrypt/privkey.pem /opt/zimbra/ssl/letsencrypt/cert.pem /opt/zimbra/ssl/letsencrypt/zimbra_chain.pem"

# Копируем правтный ключ и даем доступ zimbra

cp /opt/zimbra/ssl/letsencrypt/privkey.pem /opt/zimbra/ssl/zimbra/commercial/commercial.key
chown zimbra:zimbra /opt/zimbra/ssl/zimbra/commercial/commercial.key

# Останавливаются сервисы, и загружается сертификат

sudo su - zimbra -c "zmproxyctl stop" && sudo su - zimbra -c "zmmailboxdctl stop"
sudo su - zimbra -c "zmcertmgr deploycrt comm /opt/zimbra/ssl/letsencrypt/cert.pem /opt/zimbra/ssl/letsencrypt/zimbra_chain.pem" && sudo su - zimbra -c "zmcontrol restart"

# Закрываем общую таблицу от внешних соединений ()

sudo iptables -P INPUT DROP

# Проверка работы служб

zmcontrol status
