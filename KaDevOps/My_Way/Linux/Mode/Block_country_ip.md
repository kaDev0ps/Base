# Cписок IP адресов по странам.

Я сам всегда использую вот эти списки: 
https://www.ipdeny.com/ipblocks 

Конкретно в скриптах забираю их по урлам. Например, для России:
https://www.ipdeny.com/ipblocks/data/countries/ru.zone 

Это удобно, потому что списки уже готовы к использованию — одна строка, одно значение. Можно удобно интегрировать в скрипты. Например, вот так:
<!-- 
#!/bin/bash

# Удаляем список, если он уже есть
ipset -X whitelist

# Создаем новый список
ipset -N whitelist nethash

# Скачиваем файлы тех стран, что нас интересуют и сразу объединяем в единый список
wget -O netwhite http://www.ipdeny.com/ipblocks/data/countries/{ru,ua,kz,by,uz,md,kg,de,am,az,ge,ee,tj,lv}.zone

echo -n "Загружаем белый список в IPSET..."
# Читаем список сетей и построчно добавляем в ipset
list=$(cat netwhite)
for ipnet in $list
 do
 ipset -A whitelist $ipnet
 done
echo "Завершено"

# Выгружаем созданный список в файл для проверки состава
ipset -L whitelist > w-export 
-->

Тут я создаю список IP адресов для ipset, а потом использую его в iptables:

`iptables -A INPUT -i $WAN -m set --match-set whitenet src -p tcp --dport 80 -j ACCEPT`

Если в списке адресов более 1-2 тысяч значений, использовать ipset обязательно. Iptables начнёт отжирать очень много памяти, если загружать огромные списки в него напрямую. 

## Есть ещё вот такой сервис: 

https://www.ip2location.com/free/visitor-blocker 
Там можно сразу конфиг получить для конкретного сервиса: Apache, Nginx, правил Iptables и других. Даже правила в формате Mikrotik есть.