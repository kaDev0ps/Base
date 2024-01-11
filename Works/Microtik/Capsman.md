# Настройка Capsman
Зайти на Микротик 
Manager - Поставить галку Enable - Upgrade policy: require....
## Настройка каналов
Вкладка Chanel
Создаем каналы для 2G 5G/ Получается 7 штук
2412 2437 2462 5180 5200 5220 5240
<!-- В каждой записи заполняем поля для 2G -->
Пишем имя, частота 2412, Ширина 20 MHz, 2g-N,
<!-- В каждой записи заполняем поля для 5G -->
Пишем имя, частота 2412, Ширина 20 MHz, 2g-N,
## CAPsMAN→Datapaths
Настройка находится в CAPsMAN→Datapaths
Ставим галочки
Local Forwarding – трафик, который будет приходить от клиента будет обрабатываться контроллером CAPsMAN
Client To Client Forwarding – разрешение обмена трафика между клиентами.
## Настройка пароля CAPsMAN
Настройка находится в CAPsMAN→Security Cfg.
В провиле ставим
WPA2 WPA (для старых устройств)
Encryption: aes ccm
group key update: 20 sek
Passphrase: V3m@h-TR

## Общая конфигурация CAPsMAN
Настройка находится в CAPsMAN→Configurations
В этом разделе нужно добавить две отдельные конфигурации, но суть их в одном – указать ссылки на произведенные ранее настроить в разделах Channels, Datapaths, Security Cfg. Будет рассмотрена конфигурация для частоты 2,4ГГц, а 5ГГц, настраивается по абсолютной аналогии.

Если при подключении точки доступа к CAPsMAN выводится ошибка country does not match locked, параметр Country следует оставить пустым.

<!-- Заполняем
Вкладка Wireles. Лучше сделать несколько настроек 7, как и каналов-->
Имя точки: 2G-1CH-20Whz
Mode: ap
SSID: VZMAKH_2G
Country: russia4
installation: any
HT TX Chains: 0 1 2 3
HT RX Chains: 0 1 2 3
<!-- Вкладка Chanel -->
Выбираем тот канал, который ранее завели и на котором точка сейчас хорошо работает
<!-- Вкладка Datapaths -->
Выбираем нашу офисную сеть
Больше настроек не ставим!

<!-- Вкладка Security -->
Выбираем только наш пароль в Security

## Подключить точку доступа к CAPsMAN


Настройка находится в Wireless→WiFi Interfaces

Включаем CAP и прописываем настройки:

Выбираем оба интерфейса вай-фай. Для связи с контроллером достаточно указать его IP Adress


## Подключения точки к соответствующим настройкам
После подключения CapsMan контроллеру о вкладке СapInterface увидем появившеюся точку. Меняем ей Имя и присваиваем настройки ранее заведенные.

## CAPsMAN Provisioning, распространение конфигурации на точки доступа
Настройка находится в CAPsMAN→Provisioning
Можно сделать, чтоб конфигурация настраивалась автоматом при совпадении условий. 

Делаем им настройки
2G
Вводим мас адрес 2П или 5Г интерфейса
HW. Supported: gn или ac для 5G
Action: create dynamic enabled
Master configuration: 2G/5G
Name format: prefix identy
Name prefix: 2g /5G -->

<!-- Иногда надо во вкладке RADIO вручную применять Provision -->