# Службы отвечающие за сеть

Решается всё просто!

Для того, чтобы восстановить работу сетевого обнаружения, нужно включить 4 службы:

DNS-клиент (DNS Client);
Обнаружение SSDP (SSDP Discovery);
Публикация ресурсов обнаружения функции (Function Discovery Resource Publication);
Узел универсальных PNP-устройств (UPnP Device Host).
Можно так же выполнить рад команд в консоли cmd.

sc config Dnscache start= auto
net start Dnscache
sc config SSDPSRV start= auto
net start SSDPSRV
sc config FDResPub start= auto
net start FDResPub
sc config upnphost start= auto
net start upnphost
Можно поместить все команды в bat-файл и запустить его на исполнение.
