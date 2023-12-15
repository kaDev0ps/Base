# можно подключиться по веб интерфейсу

----------ACCESS-------------

# Смотрим vlan

sh vlan

# Заходим в конфигуратор

conf t

# Выбираем настраиваемый порт

interface range gi24

# Переводим его в режим access

switchport mode access

# присваиваем ему vlan

switchport access vlan 305
----------TRUNK-------------

# Смотрим vlan

sh vlan

# Заходим в конфигуратор

conf t

# Выбираем настраиваемый порт

interface gi24

# Переводим в режим trunk

switchport mode trunk

# Задаем vlan по умолчанию

switchport trunk native vlan 1

# Записываем vlan

switchport trunk allowed vlan add 301,305

# выход

write
end

https://wiki.merionet.ru/articles/polnoe-rukovodstvo-po-nastrojke-vlan/?ysclid=lp9uvd6zj0795393003
