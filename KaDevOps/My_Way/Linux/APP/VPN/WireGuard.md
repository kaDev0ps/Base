<!-- Обновляем список пакетов в репозиториях и сами пакеты Ubuntu 22 -->

apt update && apt upgrade -y

<!-- Установим пакет wireguard -->

apt install -y wireguard

<!-- Наша конфигурация будет хранится в каталоге /etc/wireguard/ перейдем в каталог: -->

cd /etc/wireguard/

<!-- Нам потребуется открытый и закрытый ключ для нашего сервера. Сгенерируем их, предварительно выставив правильные права при создании файлов и каталогов командами: -->

umask 077

wg genkey > privatekey

wg pubkey < privatekey > publickey

<!-- Выставим права на приватный ключ: -->

chmod 600 privatekey

<!-- Перед созданием конфигурационного файла нам потребуется наименование нашего сетевого интерфейса. Для того что бы его узнать используем команду: -->

ip a

**eth0**

<!-- Также нам понадобятся открытый и закрытый ключи. Для их вывода я использую tail -->

tail privatekey publickey

<!--
==> privatekey <==
wEkUPJcGPUMkNZK5Kf1Z+QOcbDsydSv5NRJsaGfB90I=

==> publickey <==
G/+R4u9htbGAbNL4BTVK/I9l3WgaW/Tknbd31B9k1nk=
-->

<!-- Для редактирования вы можете использовать текстовый редактор nano. Чтобы установить его нужно выполнить команду: -->

apt install -y nano

<!-- Редактируем конфигурационный файл: -->

nano /etc/wireguard/wg0.conf

<!-- Он должен выглядеть следующим образом: -->

[Interface]

PrivateKey = xxxxxxxxxxx

Address = 10.30.0.1/24

ListenPort = 51928

PostUp = iptables -A FORWARD -i %i -j ACCEPT; iptables -t nat -A POSTROUTING -o [ имя интерфейса ] -j MASQUERADE

PostDown = iptables -D FORWARD -i %i -j ACCEPT; iptables -t nat -D POSTROUTING -o [ имя интерфейса ] -j MASQUERADE

<!-- Включаем ip forwarding -->

echo "net.ipv4.ip_forward=1" >> /etc/sysctl.conf

<!-- Запускаем wireguard службу: -->

systemctl start wg-quick@wg0.service

<!-- Если мы хотим чтобы служба запускалась после перезагрузки сервера то выполняем: -->

systemctl enable wg-quick@wg0.service

<!-- Для того чтобы посмотреть состояние службы: -->

systemctl status wg-quick@wg0.service

<!-- Использую команды для генерации: -->

wg genkey > olya2_privatekey

wg pubkey < olya2_privatekey > olya2_publickey

<!-- Так же сгенерирую ключи чтобы использовать VPN и на телефоне: -->

wg genkey > myphone_privatekey

wg pubkey < myphone_privatekey > myphone_publickey

<!-- Выведем открытые ключи на экран. Они понадобятся нам для того, чтобы добавить узлы в нашу сеть: -->

tail mypc_publickey myphone_publickey

<!-- ==> mypc_publickey <==
c/7q/LCA7ua7bycuNyUnTdmlabHXiVty3prtcwcoHxo=

==> myphone_publickey <==
+KBpfRXbPRJp8V48RtArKqs7l6F6d9HGJp8LQ6KOvmQ= -->

<!-- Отредактируем наш конфигурационный файл: -->

nano wg0.conf

<!-- Добавим строки: -->

[Peer]

PublicKey = [ mypc_publickey ]

AllowedIPs = 10.30.0.2/32

[Peer]

PublicKey = [ myphone_publickey ]

AllowedIPs = 10.30.0.3/32

<!-- Сохраним файл и перезапустим нашу службу: -->

systemctl restart wg-quick@wg0

<!-- Проверим что все прошло успешно: -->

systemctl status wg-quick@wg0

<!-- Статус должен быть active


Перезагрузку службы требуется делать каждый раз после редактирования файла конфигурации сервера (wg0.conf)


Далее создадим конфигурации для клиентов (в моем случае мой пк и телефон). Я сделаю это так же на сервере.-->

nano mypc.conf

<!-- ==> mypc_privatekey <==
EKQ2KTgjdulUaSPYcM7K4hWVGI8EL6Ec+YlJu1kvTWs=

==> myphone_privatekey <==
CKshjUF6yCc0vPdJ0cFbbXdNLCM4EFabvmzyJdzcs1Y= -->

[Interface]

PrivateKey = [ приватный ключ mypc_privatekey ]

Address = 10.30.0.2/32

DNS = 8.8.8.8

[Peer]

PublicKey = [ публичный ключ сервера publickey ]

Endpoint =[ ip адрес сервера ]:51928

AllowedIPs = 0.0.0.0/0

PersistentKeepalive = 20

<!-- В поле Endpoint можно увидеть IP- адрес сервера - это IP-адрес, по которому мы подключались по SSH. Чтобы увидеть интерфейсы и адреса можно использовать команду ip a


Аналогичную конфигурацию создаем для нашего телефона. Только требуется изменить адрес. Для пк был 10.30.0.2/32, а в конфигурации для телефона сделаем 10.30.0.3/32. Так же если захотим использовать VPN на других устройствах то при создании конфигураций следует добавлять другие адреса в поле Address в конфигурационных файлах и файле конфигурации сервера wg0.conf поле AllowedIPs  -->

<!-- Для подключения устанавливаем клиент wireguard https://www.wireguard.com/install/

В приложении на Windows добавляем новый тоннель и вписываем конфигурацию которую создали в файле mypc.conf -->

<!-- Чтобы удобно добавить VPN на телефон, на сервере установим программу для генерации qr кодов: -->

apt install -y qrencode

<!-- Находясь в каталоге с конфигурацией выполним: -->

qrencode -t ansiutf8 -r myphone.conf

## Для доступа на AWS

- Открыть UDP порт
- Привязать Elastic IP к Instance

<!-- Для наблюдения за статистикой -->

watch wg show all
