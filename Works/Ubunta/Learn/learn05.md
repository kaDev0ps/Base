# Установка net-tools

sudo spt install net-tools

# Проверка стевого интерфейса

ifconfig -a
ifconfig enp1s0 192.168.88.15 netmask 255.255.255.0

# Управление маршрутизацией

route add default gw 192.168.0.1

## Можно настроить для конкретной сети

route add -net 10.1.1.0 netmask 255.255.255.0 dev enp1s0

## Проверить маршруты

route

# Проверка портов

netstat -rn

# Консольная утилита ip

ip -a
ip addr add 192.168.0.255/24 dev enps1
ip route add default via 192.168.88.1

# NetworkManager

## Просмотр подключения и их настройки

apt install network-manager
nmcli -p device show ens160

## Можно использовать tab

nmcli connection add con-name "connect_1" ifname enlp1s0 autoconnect yes type ethernet

# ipfilter для транзита трафика

    Input - фильтр входящих пакетов
    Output - фильтр исходящих пакетов
    Forward - маршрутизации пакетов

# ip nat для маршрутизации адресов

Prerouting - изначальная обработка входящих пакетов
Postrouting

# Настройка IPtables и nat

## удаляем правила в таблице фильтров

iptables -F

## удаляем правила в таблице nat

iptables -t nat -F

## управление политиками

iptables -P INPUT ACCEPT

## добавление правил

# Принимаем трафик с 80 порта

iptables -A INPUT -p tcp --dport 80 -j ACCEPT
iptables -A INPUT -p tcp --dport 443 -j DROP

<!-- Запрет доступа c определенных сетей -->

iptables -A INPUT -p tcp -s 10.0.0.0/16 --dport 443 -j DROP

<!-- Пробрасываем порт на другой интерфейс -->

iptables -t nat -A PREROUTING -i enp1s -p tcp --dport 80 -j DNAT --to-destination 10.0.0.1

<!-- Перенаправление портов на одной машине -->

iptables -t nat -A PREROUTING -i enp1s -p tcp --dport 80 -j REDIRECT --to-port 443

# Проверяем таблицу

iptables -nvL

Snab606
718avangard606
