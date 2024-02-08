# Устанавливаем докер
sudo apt update
sudo apt install apt-transport-https ca-certificates curl software-properties-common
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add -
sudo add-apt-repository "deb [arch=amd64] https://download.docker.com/linux/ubuntu focal stable"
sudo apt update
apt-cache policy docker-ce
sudo apt install docker-ce
# Проверяем статус докера

sudo systemctl status docker

# Установка ВПН-сервера
Нужно создать файл /root/vpn.env

<!-- Информация от сюда и от сюда -->

cd /root
touch vpn.env
nano vpn.env
<!-- вставляем следующий текст -->

VPN_IPSEC_PSK=sjf3hjhsdfshbgtYTg4VdsdshGytrDffffshbgtYTg4Vdsdsh
VPN_USER=user1
VPN_PASSWORD=password45word65
<!-- ctrl+x и сохранить файл
где -->

VPN_IPSEC_PSK -секретная фраза IPsec, должна состоять из 20 или более символов. 
VPN_USER  и VPN_PASSWORD - логин и пароль от VPN сервера.

<!-- Запускаем наш докер контенер с нашим окружением: -->
docker run \
    --name ipsec-vpn-server \
    --env-file ./vpn.env \
    --restart=always \
    -v ikev2-vpn-data:/etc/ipsec.d \
    -v /lib/modules:/lib/modules:ro \
    -p 500:500/udp \
    -p 4500:4500/udp \
    -d --privileged \
    hwdsl2/ipsec-vpn-server
<!-- Проверяем -->

docker ps -a
<!-- Должен быть вывод что-то вроде -->

CONTAINER ID        IMAGE                     COMMAND             CREATED             STATUS              PORTS                                          NAMES
0f32ea96490f        hwdsl2/ipsec-vpn-server   "/opt/src/run.sh"   8 seconds ago       Up 7 seconds        0.0.0.0:500->500/udp, 0.0.0.0:4500->4500/udp   ipsec-vpn-server
<!-- Теперь по адресу -->

/var/lib/docker/volumes/ikev2-vpn-data/_data
<!-- 
лежат файлы, среди которых -->

vpnclient.p12 (for Windows & Linux)

vpnclient.sswan (for Android)

vpnclient.mobileconfig (for iOS & macOS)

<!-- необходимые для vpn-клиента. Ваша задача их скачать, можете использовать ftp-клиент (я использовал filezilla), можете через админка провайдера, как вам угодно. Инструкция для filezilla

Добавляем и удаляем пользователей
Вы можете управлять клиентами IKEv2 с помощью вспомогательного скрипта. -->

# Добавить нового клиента (используя параметры по умолчанию)
docker exec -it ipsec-vpn-server ikev2.sh --addclient [client name]
# Экспорт конфигурации для существующего клиента
docker exec -it ipsec-vpn-server ikev2.sh --exportclient [client name]
# Список существующих клиентов
docker exec -it ipsec-vpn-server ikev2.sh --listclients
# Посмотреть инструкцию
docker exec -it ipsec-vpn-server ikev2.sh -h
<!-- Я вас поздравляю!
Мы подняли впн-сервер и скачали файлы необходимые для впн-клиента вашего устройства.

Теперь сами погуглите, как подружить ваше устройство и эти файлы -->