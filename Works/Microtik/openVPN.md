# Настройка OpenVPN на микротике

<!-- Генерируем сертификат сервера -->

/certificate add name=OPN-VPN country="RU" state="Sankt-Petersburg" locality="Sankt-Petersburg" organization="Zelobit" unit="IT" common-name="CA" key-size=2048 days-valid=3650 key-usage=crl-sign,key-cert-sign

/certificate sign OPN-VPN ca-crl-host=127.0.0.1 name="ca"

<!-- * первая команда создает файл-шаблон запроса, на основе которого мы генерируем сертификаты второй командой. В шаблоне мы указываем опции для сертификата — так как сам сертификат самоподписанный, можно указать любые значения и это никак не отразится на его использовании (стоит только указать корректные значения для стойкости шифрования key-size и срока действия сертификата days-valid). -->

<!-- Генерируем сертификат сервера OpenVPN: -->

/certificate add name=VPN-server country="RU" state="Sankt-Petersburg" locality="Sankt-Petersburg" organization="Zelobit" unit="IT" common-name="SERVER" key-size=2048 days-valid=3650 key-usage=digital-signature,key-encipherment,tls-server

/certificate sign VPN-server ca="ca" name="server"

<!-- * как и в примере выше, мы сначала создали файл запроса и на его основе — сам сертификат. В качестве центра сертификации мы указываем созданный ранее сертификат ca. -->

# Создаем пул IP-адресов

/ip pool
add name=Pool-VPN ranges=10.200.202.200-10.200.202.250

<!-- * где Name просто указывает название для идентификации пула (openvpn); Addresses — стартовый и конечный адреса, которые будет назначаться клиентам при подключении к VPN. В данном примере мы указываем последовательность от 176.16.10.10-176.16.10.250. -->

# Создание профиля

/ppp profile
add local-address=10.200.202.200 name=VPN-Profile remote-address=Pool-VPN

# Создание пользователя

<!-- Для каждого, кто будет подключаться к VPN необходимо создать свою учетную запись. В том же PPP переходим на вкладку Secrets - создаем нового пользователя - задаем ему имя, пароль, указываем сервис ovpn и выбираем профиль, из которого пользователю будет назначен адрес при подключении - нажимаем OK: -->

/ppp secret
add name=Avangard password=VPN_Avangard_200 profile=VPN-Profile service=ovpn

# Включаем и настраиваем сервер OpenVPN

/interface ovpn-server server
set certificate=server cipher=blowfish128,aes128,aes192,aes256 \
 default-profile=VPN-profile enabled=yes require-client-certificate=yes

# Настройка брандмауэра

<!-- Мы активировали наш сервер OVPN на порту 1194 и нам нужно открыть данный порт на фаерволе. Переходим в раздел IP - Firewall: -->

/ip firewall filter
add action=accept chain=input dst-port=1194 protocol=\
tcp

<!-- * мы должны выбрать для Chain — Input, указать протокол (tcp) и задать порт, на котором слушает сервер OpenVPN (1194). -->

# Создание сертификатов для клиента

<!-- Создаеш шаблон -->

/certificate add name=VPN-clients country="RU" state="Sankt-Petersburg" locality="Sankt-Petersburg" organization="Zelobit" unit="IT" common-name="clients-template" key-size=2048 days-valid=3650 key-usage=tls-client

<!-- Теперь создадим сертификат для первого клиента: -->

/certificate add name=template-client-to-issue copy-from="ovpn_client" common-name="Avangard"

/certificate sign template-client-to-issue ca="ca_mikrotik" name="Avangard"

<!-- Для создания сертификата второго клиента вводим:

/certificate add name=template-client-to-issue copy-from="ovpn_client" common-name="103"

/certificate sign template-client-to-issue ca="ca_mikrotik" name="103"

... и так далее. -->

<!-- После экспортируем сертификаты: -->

/certificate export-certificate ca_mikrotik export-passphrase=""

/certificate export-certificate Avangard export-passphrase=VPN_Avangard_200

# Открываем текстовый редактор и создаем конфиг:

client
dev tun
proto tcp
remote xxx.xxx.xxx.xxx 1194
auth-nocache
ca ca.crt
cert client1.crt
key client1.key
remote-cert-tls server
cipher AES-256-CBC
resolv-retry infinite
nobind
persist-key
persist-tun
verb 3
auth-nocache
auth-user-pass
route 192.168.0.0 255.255.255.0

<!-- * в данном конфиге нас интересуют опции:

remote — адрес нашего VPN-сервера;
cert и key — имена файлов с сертификатами;
route — адрес маршрута для доступа к локальной сети, которая находится за роутером и куда нужно пустить пользователей.
* подробнее опции описаны в инструкции Настройка OpenVPN клиента.

Сохраняем файл с настройками в каталоге C:\Program Files\OpenVPN\config (или другом, где установлен клиент). В этот же каталог поместим наши сертификаты. -->

<!-- Клиент на ПК качаем этот 2.4.12-1601 (https://swupdate.openvpn.org/community/releases/openvpn-install-2.4.12-I601-Win10.exe) -->

<!-- Или все можно в одном файле уместить -->

client
dev tun
proto tcp
remote 95.214.119.71 1194
resolv-retry infinite
nobind
persist-key
persist-tun
remote-cert-tls server
verb 3
route-delay 5
cipher AES-256-CBC
auth SHA1
auth-user-pass
route 10.200.202.0 255.255.255.0
pull
redirect-gateway def1
<ca>
-----BEGIN CERTIFICATE-----
-----END CERTIFICATE-----
</ca>
<cert>
-----BEGIN CERTIFICATE-----

-----END CERTIFICATE-----
</cert>
<key>
-----BEGIN ENCRYPTED PRIVATE KEY-----

-----END ENCRYPTED PRIVATE KEY-----
</key>
