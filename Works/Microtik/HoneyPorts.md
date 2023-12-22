# Защита от BruteForce
<!-- Разрешаем доверенным пользователям доступ, остальных, кто пробует войти с левых портов в спам лист -->
/ip firewall filter
add action=accept chain=input src-address-list=Trust_users
add action=add-src-to-address-list address-list=SPAM address-list-timeout=\
    none-dynamic chain=input comment="Brute Force - Honey Port" \
    connection-state=new dst-port=21,22,23,123,3389,5060,8291 in-interface=\
    ether1 protocol=tcp
add action=add-src-to-address-list address-list=SPAM address-list-timeout=\
    none-dynamic chain=input comment="Honey Port UDP" connection-state=new \
    dst-port=5060 in-interface=ether1 protocol=udp
add action=drop chain=input dst-address=95.214.119.71 dst-port=53 protocol=\
    udp
<!-- Баним спам лист -->
/ip firewall raw
add action=drop chain=prerouting comment="DROP Brute Force ATACK" protocol=\
    tcp src-address-list=SPAM

<!-- Добавляем доступ к серверу по порту из вне -->
/ip firewall nat
add action=dst-nat chain=dstnat comment=AVA-FIN-1 dst-address=80.246.246.126 \
    dst-port=33013 protocol=tcp to-addresses=10.200.202.13 to-ports=3389
/ip firewall nat
add action=dst-nat chain=dstnat comment=AVA-FIN-2 dst-address=85.143.139.126 \
    dst-port=33012 protocol=tcp to-addresses=10.200.202.12 to-ports=3389