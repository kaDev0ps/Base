# Установка OpenVPN в Ubuntu

<!-- Сначала установим необходимое ПО:

Обновляем репозитории и запускаем обновление всех пакетов в системе: -->

sudo apt update
sudo apt upgrade

<!-- Устанавливаем OpenVPN сервер: -->

sudo apt install openvpn

<!-- Устанавливаем easy-rsa: -->

sudo apt install easy-rsa

<!-- Создадим директорию для хранения создания более короткой ссылки на директорию easy-rsa: -->

mkdir ~/easy-rsa
ln -s /usr/share/easy-rsa/\* ~/easy-rsa/

<!-- Установим необходимые разрешения на директорию: -->

sudo chown $USER ~/easy-rsa
chmod 700 ~/easy-rsa

**Задаем параметры криптографии для Микротика**

<!-- cd ~/easy-rsa
nano vars -->

<!-- В файле vars нужно указать следующие строки: -->

<!--
set_var EASYRSA_ALGO "ec"
set_var EASYRSA_DIGEST "sha512"

set_var EASYRSA_REQ_COUNTRY "RU"
set_var EASYRSA_REQ_PROVINCE "SPB Region"
set_var EASYRSA_REQ_CITY "SPB"
set_var EASYRSA_REQ_ORG "Zelobit"
set_var EASYRSA_REQ_EMAIL "ka@zelobit.com"
set_var EASYRSA_REQ_OU "IT"

set_var EASYRSA_CA_EXPIRE 7300

set_var EASYRSA_CERT_EXPIRE 3650

set_var EASYRSA_CRL_DAYS 3650


-->

**ИЛИ**

<!-- Настраиваем конфигурацию, которая будет использоваться при генерации ключей ЗАМЕНА: -->

<!-- cd ~/easy-rsa

cp /usr/share/easy-rsa/vars.example vars

<!-- Далее в файле vars изменяем/добавляем описанные ниже строки.

Настройки для ключей, чтобы не запрашивало каждый раз:

set_var EASYRSA_REQ_COUNTRY "RU"
set_var EASYRSA_REQ_PROVINCE "SPB Region"
set_var EASYRSA_REQ_CITY "SPB"
set_var EASYRSA_REQ_ORG "Zelobit"
set_var EASYRSA_REQ_EMAIL "ka@zelobit.com"
set_var EASYRSA_REQ_OU "IT"
set_var EASYRSA_KEY_SIZE 2048

set_var EASYRSA_ALGO rsa

set_var EASYRSA_CA_EXPIRE 7300

set_var EASYRSA_CERT_EXPIRE 3650

set_var EASYRSA_CRL_DAYS 3650 -->

**Продолжаем настройку**

<!-- Теперь инициализируем PKI инфраструктуру на базе esy-rsa: -->

./easyrsa init-pki

<!-- Сконфигурируем центр выдачи сертификатов: -->

./easyrsa build-ca

<!-- В процессе настройки нужно будет указать ключевую фразу для CA. Обязательно сохраните её – она вам понадобится. -->

<!-- Создадим запрос на выпуск сертификата: -->

cd ~/easy-rsa
./easyrsa gen-req $(uname -n) nopass

<!-- $(uname -n) – это имя сервера. В моем случае – это имя “SRV-VPN”. Вы можете указать любое имя. Только учитывайте, что это имя нужно будет указать в паре конфигурационных файлов.

Закрытый ключ уже готов. Скопируем его в директорию с конфигурацией OpenVPN сервера: -->

sudo cp ~/easy-rsa/pki/private/$(uname -n).key /etc/openvpn/server/

<!-- Затем утвердим наш запрос на выпуск нового сертификата: -->

./easyrsa sign-req server $(uname -n)

<!-- Теперь скопируем получившийся сертификат с открытым ключом и сертификат нашего центра сертификации в конфигурационную директорию сервера OpenVPN: -->

sudo cp ~/easy-rsa/pki/issued/$(uname -n).crt /etc/openvpn/server
sudo cp ~/easy-rsa/pki/ca.crt /etc/openvpn/server

<!-- Сгенерируем общий ключ и скопируем его в директорию с конфигурационными файлами OpenVPN сервера: -->

cd ~/easy-rsa
openvpn --genkey secret ta.key
sudo cp ta.key /etc/openvpn/server

<!-- Итого в директории /etc/openvpn/server у вас должно быть четыре файла: файл открытого ключа, файл закрытого ключа, общий ключ и сертификат центра сертификации: -->

sudo ls /etc/openvpn/server

<!-- Поскольку клиентских сертификатов может быть много, то будем складировать их в отдельной директории, чтобы их можно было относительно быстро инвентаризировать: -->

mkdir -p ~/client-configs/keys
chmod -R 700 ~/client-configs

<!-- Создадим запрос на выпуск сертификата: -->

cd ~/easy-rsa
./easyrsa gen-req client001 nopass

<!-- Закрытый ключ для клиента готов. Его можно сразу скопировать в директорию из п. 1: -->

cp ~/easy-rsa/pki/private/client001.key ~/client-configs/keys/

<!-- Утвердим запрос на выпуск сертификата: -->

./easyrsa sign-req client client001

<!-- Теперь скопируем все недостающие сертификаты для клиентских подключений и открытый ключ для клиента client001: -->

cp ~/easy-rsa/ta.key ~/client-configs/keys/
sudo cp /etc/openvpn/server/ca.crt ~/client-configs/keys/
cp ~/easy-rsa/pki/issued/client001.crt ~/client-configs/keys/
sudo chown $USER.$USER ~/client-configs/keys/\*

<!-- Теперь я создам директорию для хранения конфигурационных файлов клиентских подключений: -->

mkdir -p ~/client-configs/files

<!-- Создадим базовый конфигурационный файл для клиентских подключений: -->

nano ~/client-configs/base.conf

<!-- Текст типового конфигурационного файла для клиентов можно скопировать вот тут: -->

https://github.com/OpenVPN/openvpn/blob/master/sample/sample-config-files/client.conf

<!-- В директиве remote я указываю имя сервера OpenVPN, по которому клиенты будут обращаться к нему: -->

remote hm-srv-vpn.northeurope.cloudapp.azure.com 1194

<!-- Раскомментируем строки, чтобы указать, что запуск необходимо выполнять в режиме пониженных привилегий: -->

user openvpn
group openvpn

<!-- Параметры открытых и закрытых ключей будут указываться непосредственно в конфигурационном файле. Соответственно, эти директивы нужно закомментировать: -->

;ca ca.crt
;cert client.crt
;key client.key

<!-- Директиву tls-auth тоже нужно закомментировать: -->

;tls-auth ta.key 1

<!-- Затем скорректируем параметры криптографии: -->

cipher AES-256-GCM
auth SHA256
key-direction 1

<!-- Создадим скрипт для упрощения генерации конфигурационный файлов клиентов: -->

nano ~/client-configs/make_config.sh

<!-- #!/bin/bash

# First argument: Client identifier

KEY_DIR=~/client-configs/keys
OUTPUT_DIR=~/client-configs/files
BASE_CONFIG=~/client-configs/base.conf

cat ${BASE_CONFIG} \
    <(echo -e '<ca>') \
    ${KEY_DIR}/ca.crt \
    <(echo -e '</ca>\n<cert>') \
    ${KEY_DIR}/${1}.crt \
    <(echo -e '</cert>\n<key>') \
    ${KEY_DIR}/${1}.key \
    <(echo -e '</key>\n<tls-crypt>') \
    ${KEY_DIR}/ta.key \
    <(echo -e '</tls-crypt>') \
    > ${OUTPUT_DIR}/${1}.ovpn -->
<!-- Разрешим исполнение скрипта: -->

chmod 700 ~/client-configs/make_config.sh

<!-- Скрипту в качестве параметра нужно будет передававать имя клиента. Выше я указывал имя клиента, как client001.

Сгенерируем готовый файл для клиента client001: -->

cd ~/client-configs
./make_config.sh client001

<!-- Файл с параметрами для подключения клиента OpenVPN готов: -->

ls ~/client-configs/files/
**Настройка сервера OpenVPN**

<!-- Создадим конфигурационный файл сервера OpenVPN: -->

sudo nano /etc/openvpn/server/server.conf

<!-- Типовой пример конфигурационного файла есть вот тут: -->

https://github.com/OpenVPN/openvpn/blob/master/sample/sample-config-files/server.conf

<!-- Копируем текст типового конфигурационного файла в файл /etc/openvpn/server/server.conf. -->
<!-- Поскольку имена файлов с открытым и закрытым ключом у меня отличаются от стандартных, то их тоже нужно скорректировать:
 -->

ca ca.crt
cert SRV-VPN.crt
key SRV-VPN.key # This file should be kept secret

<!-- Изменим параметры Diffie-Hellman: -->

;dh dh2048.pem
dh none

<!-- если для микротика то dh dh.pem -->
<!-- В моей тестовой среде в Microsoft Azure я использую подсеть 10.1.0.0/24. У вас подсеть(и) будет, скорее всего, другая. Соответственно, я добавлю её в объявленных маршрутах для VPN подключения: -->

push "route 10.1.0.0 255.255.255.0"

<!-- Этот параметр особенно вам пригодится, если вы не будите использовать VPN-подключение в качестве шлюза по умолчанию. -->
<!-- нужно раскомментировать параметр: -->

push "redirect-gateway def1 bypass-dhcp"

<!-- Также можете указать параметры DNS для клиентов OpenVPN: -->

push "dhcp-option DNS 10.1.0.4"

<!-- Закомментируйте параметр tls-auth и добавить параметр tls-crypt: -->

;tls-auth ta.key 0 # This file is secret
tls-crypt ta.key

<!-- Скорректируем параметры криптографии: -->

;cipher AES-256-CBC
cipher AES-256-GCM
auth SHA256

<!-- Также зададим параметр запуска OpenVPN сервера в режиме минимальных привилегий: -->

user nobody
group nogroup

**Изменение сетевых параметров**

<!-- Чтобы сервер OpenVPN мог работать в качестве маршрутизатора нужно скорректировать некоторые сетевые параметры:

Разрешим трансляцию сетевых пакетов: -->

sudo nano /etc/sysctl.conf

<!-- Раскомментируем вот эту строчку: -->

net.ipv4.ip_forward=1

<!-- Также нам потребуется изменение правил цепочек iptables. Для этого я буду использовать ufw. Если он не установлен в вашем дистрибутиве, то установите его: -->

sudo apt install ufw

<!-- Узнаем имя интерфейса, который используется у нас по у нас в качестве шлюза во внешний мир: -->

ip route list default

<!-- Внесем для этого интерфейса правило натирования сетевого трафика: -->

sudo nano /etc/ufw/before.rules

<!-- Добавим вот этот блок конфигурации: -->

<!--
# START OPENVPN RULES
# NAT table rules
*nat
:POSTROUTING ACCEPT [0:0]
# Allow traffic from OpenVPN client to eth0 (change to the interface you discovered!)
-A POSTROUTING -s 10.8.0.0/24 -o eth0 -j MASQUERADE
COMMIT
# END OPENVPN RULES -->

<!-- Также разрешим прием трафика в качестве действия по умолчанию: -->

sudo nano /etc/default/ufw
DEFAULT_FORWARD_POLICY="ACCEPT"

<!-- Также я добавлю в исключения ufw порты для ssh и OpenVPN сервера: -->

sudo ufw allow 1194/udp
sudo ufw allow OpenSSH

<!-- Последние изменения по части ufw – это его включение: -->

sudo ufw disable
sudo ufw enable

<!-- Перезагружаем сервер. -->

**Запуск сервера OpenVPN**

<!-- Команда для включения автозапуска сервиса OpenVPN: -->

sudo systemctl -f enable openvpn-server@server.service

<!-- Команда для непосредственного запуска сервиса: -->

sudo systemctl start openvpn-server@server.service

<!-- Проверим статус сервиса: -->

sudo systemctl status openvpn-server@server.service

**Настройка дополнительных клиентских подключений**

<!-- В качестве дополнительного подключения клиента я буду использовать имя client004. Для генерации дополнительного файла с клиентскими подключениями нужно выполнить следующие действия:

Создадим запрос на выпуск сертификата: -->

cd ~/easy-rsa
./easyrsa gen-req client004 nopass

<!-- Закрытый ключ для клиента готов. Его можно сразу скопировать в директорию клиентских файлов: -->

cp ~/easy-rsa/pki/private/client004.key ~/client-configs/keys/

<!-- Утвердим запрос на выпуск сертификата: -->

./easyrsa sign-req client client004

<!-- Теперь скопируем клиентский сертификат для client004 в директорию со всеми файлами: -->

cp ~/easy-rsa/pki/issued/client004.crt ~/client-configs/keys/

<!-- Сгенерируем готовый файл для клиента client001: -->

cd ~/client-configs
./make_config.sh client004

<!-- Файл с параметрами для подключения клиента OpenVPN готов: -->

ls ~/client-configs/files/

**_Настройка по TCP_**

Если вы действительно смените протокол на TCP, вам нужно будет изменить значение директивы explicit-exit-notify с 1 на 0, поскольку эта директива используется только протоколом UDP. В противном случае при запуске службы OpenVPN возможны ошибки протокола TCP.

<!-- Найдите строку explicit-exit-notify в конце файла и измените значение на 0: -->

/etc/openvpn/server/server.conf

# Optional!

explicit-exit-notify 0

Если вам не нужно использовать другие порт и протокол, лучше всего оставить эти настройки без изменений.

**Zel0bit**
