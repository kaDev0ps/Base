Биты - данные представленны в виде битов
Физический уровень - хабы, репиторы, sfp преобразователь
Канальный уровень - свичи

МАС - 6 цифр это производитель 6 цифр это оборудование
Broadcast MAC адрес - широковещательный запрос ко всем оборудованиям, чтобы узнать за каким портом что есть
MTU - максимальная длинна кадра минимальный размер данных 64 байт 1518 максимальный размер кадра
в кадре - отправитель, получатель, тип данных, данные и контрольная сумма

Почему только 4 хаба?
Колизия - столкновение нескольких сигналов
full duplex - одновременная передача и рием из порта коммутатора
если мак неизвестен, то идет отправка на все порты с маком fffffffffff
broadcast - широолковещательный домен сеть в котором происходит обмен
домен коллизиий - передавать данные одно устройства из нескольких
     сеть делится на домен колизий спомощью: мосток, коммутаторов и маршрутизаторов
     сеть делится на широковещательные домены с помощью коммутаторов 3 уровня и маршрутизаторов
port-chanel -  объединение несколько физических портов в один логический
cisco команды
     enable - подключаемся к устройству
     conig terminal - входим в режим конфигуратора
     config range fa0/3-24 -> shutdown

ex - выход
end - выход
interface range fa0/3-24 - управление несколькими интерфейсами
shutdown - выключить
show - просмотр статуса
show ip rout - просмотр маршрутов

line console - адрес подключения по консоли
line vty - адрес для подключения по ip
copy running-config startup-config - сохранение настроек
do write - делать действия, чтобы не выходить в начало программы
ip add 255.255.255.0
ip default-gateway 10.0.0.1
ip route 192.168.1.0 255.255.255.0 10.0.0.2  - указываем за каким интерфейсом нужная сеть

настройка cisco в блокноте
sh ru | ex !