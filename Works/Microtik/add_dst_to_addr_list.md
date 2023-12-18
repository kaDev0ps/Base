# Защита от Brute Force

<!-- Создаем на популярные порты приманку -->

add chain=input protocol=tcp dst-port=21,22,23,123,3389,5060,8291 connection-state=new action=add-src-to-address-list \
address-list=SPAM address-list-timeout=10m comment="Honey Port" disabled=no

<!-- Добавляем правила от Brute force атак -->

/ip firewall raw
add chain=prerouting src-address-list=SPAM action=drop \
comment="drop brute forcers" disabled=no

<!-- Вручную добавляем ареса в спам лист -->

/ip firewall address-list add address=176.98.234.212/20 list=SPAM
/ip firewall address-list add address=/20 list=SPAM
/ip firewall address-list add address=/20 list=SPAM
/ip firewall address-list add address=/20 list=SPAM
/ip firewall address-list add address=/20 list=SPAM
/ip firewall address-list add address=/20 list=SPAM

# Добавление в адресную книгу новых подключений на микротике

Filter Rules
Добавляем фильтр input
Dst. Addres 95.214.55.55
6(tcp)
Dst.Port 33003

Action
add src to address list
Address List 1c-connect
