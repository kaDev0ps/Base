# Начнем с установки Docker на Linux.

curl -fsSL https://get.docker.com -o get-docker.sh - Скачаем скрипт.
sudo sh get-docker.sh - Запустим установку.
L2TP + IPSEC

# Этот тип VPN поддерживается из коробки почти все ОС, роутеры. Качаем!

docker pull hwdsl2/ipsec-vpn-server
# Теперь создаем файл vpn.env, и копируем туда наши настройки

*********************************************************

# Note: All the variables to this image are optional.
# See README for more information.
# To use, uncomment and replace with your own values.

# Define IPsec PSK, VPN username and password
# - DO NOT put "" or '' around values, or add space around =
# - DO NOT use these special characters within values: \ " '
VPN_IPSEC_PSK=Rf;lsq[jxtncdjqVPNcthdthdHjccbbyf[fkzde
VPN_USER=karmash #Дефолтный логин (юзер), будет создан автоматически
VPN_PASSWORD=VPN_in_Francfurt2024 #Дефолтный пароль, будет создан автоматически

# Define additional VPN users
# - DO NOT put "" or '' around values, or add space around =
# - DO NOT use these special characters within values: \ " '
# - Usernames and passwords must be separated by spaces
VPN_ADDL_USERS=lex usdbdbdfber2 #если нужно создать еще юзеров
VPN_ADDL_PASSWORDS=lex_in_Francfurt2024! userYBibuyvbUYvuGibiygiyg@%$%^$^%$%dbdgb2pass #пароли юзеров

# Use a DNS name for the VPN server
# - The DNS name must be a fully qualified domain name (FQDN)
# VPN_DNS_NAME=vpn.example.com

# Use alternative DNS servers
# - By default, clients are set to use Google Public DNS
# - Example below shows Cloudflare's DNS service
VPN_DNS_SRV1=8.8.8.8
VPN_DNS_SRV2=1.0.0.1


***************************************************************

Осталось запустить VPN, из папки в которой находится vpn.env

docker run \
    --name ipsec-vpn-server \
    --env-file ./vpn.env \
    --restart=always \
    -v /lib/modules:/lib/modules:ro \
    -p 500:500/udp \
    -p 4500:4500/udp \
    -d --privileged \
    hwdsl2/ipsec-vpn-server

Теперь создадим подключение из командной строки windows

Add-VpnConnection -Name 'l2tp-vpn' -ServerAddress '5.231.220.71' -L2tpPsk 'Rf;lsq[jxtncdjqVPNcthdthdHjccbbyf[fkzde' -TunnelType L2tp -EncryptionLevel Required -AuthenticationMethod Chap,MSChapv2 -Force -RememberCredential -PassThru
В windows при подключении возможно появится ошибка, запустим командную строку и починим.

REG ADD HKLM\SYSTEM\CurrentControlSet\Services\PolicyAgent /v AssumeUDPEncapsulationContextOnSendRule /t REG_DWORD /d 0x2 /f

Перезапускаем ПК и можно подключаться. Плюсы клиент VPN не нужен, все есть из коробки, быстрый VPN, можно даже играть в игры, я играл. Если работает, значит протокол не заблокирован. Идем дальше.