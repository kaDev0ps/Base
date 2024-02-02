# MikroTik: Изменение MAC адреса

Изменение MAC адреса в графическом интерфейсе WinBox:

Нажимаем кнопку Interfaces
в окне Interface List выбираем нужный интерфейс
открываем его
в поле MAC Address или Admin. MAC Address вводим нужный адрес
Ethernet
`interface ethernet set ether1 mac-address=01:23:45:67:89:00`
Bridge
`interface bridge set bridge1 admin-mac=01:23:45:67:89:00`
Wireless
`interface wireless set wlan1 mac-address=01:23:45:67:89:00`