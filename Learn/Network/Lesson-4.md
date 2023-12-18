# IP адрессация
192.168.1.0/24 - сеть (классовая адресация)
1111 1111.1111 1111.1111 1111.0000 0000 = 24 - 254 устройства

1111 1111.1111 1111.1111 1111.1000 0000 = 25 маска = 255.255.255.128 - 126 узлов
192.168.1.0/25 192.168.1.128/25 - подсеть
1111 1111.1111 1111.1111 1111.1100 0000 = 26 маска = 255.255.255.192 - 64 узла
192.168.1.0/26 192.168.1.64/26
172.16.0.0/22

## Динамическая маршрутизация

conf t
router
router rip
ve 2 - указываем версию
no auto-summary - чтоб не терялись пакеты
passive-interface gi0/1 - чтоб не следили за трафиком
network 192.168.0.0

router rip
 version 2
 passive-interface GigabitEthernet0/1
 network 172.16.0.0
 no auto-summary

 В RIP надо указывать сети в которых роутеры работают.

 ## Раздаем сеть
 ip dhcp pool POOL_0.0
 def 172.16.0.1 - указываем ip роутера
 network 172.16.0.0 255.255.255.0 - маска

 ## Исключаем IP из раздачи
ip dhcp excluded-address 192.168.0.1 192.168.0.10

называем роутер
> hostname Router_4
называем сеть
> ip dhcp pool POOL_4.0
Устанавливаем раздачу dhcp
> network 172.16.0.129 255.255.255.192
Устанавливаем шлюз для устройств
> default-router 172.16.0.129
Исключаем IP из раздачи
> ip dhcp excluded-address 172.16.0.193
Выбираем и включаем интерфейс
> interface GigabitEthernet0/1
> no shutdown
> duplex auto
> speed auto
Устанавливаем сетевые настройки роутера
> ip address 172.16.0.129 255.255.255.192

Настраиваем RIP
> router rip
> version 2
> no auto-summary
Подключаем сети с которыми работаем
> network 10.0.0.0
> network 172.16.0.0

passive-interface GigabitEthernet0/0/0

 Указываем какая сеть на каком роутере должна быть видна
 > ip route 172.16.0.192 255.255.255.248 10.0.0.25

## Работа через OSPF
Указываем сети на портах настраиваемого микротика. ROUTING (STATIC/RIP) не настраиваем!

router ospf 1
 passive-interface default
 no passive-interface GigabitEthernet0/0/0
 no passive-interface GigabitEthernet0/1/0
 network 172.16.0.129 0.0.0.0 area 0
 network 10.0.0.26 0.0.0.0 area 0
 network 10.0.0.17 0.0.0.0 area 0