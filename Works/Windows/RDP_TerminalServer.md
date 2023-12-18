# Активация терминальника WS 2019

Соглашение Enterprise agreement - 4965437б, 6565792, 5296992, 3325596 или любой другой, найденный в сети.

Windows Server 2019
Разворачиваем необходимые роли и при активации сервера выбираем Соглашение Enterprise Аgreement, номер соглашения легко найти в интернете.

В ветке HKLM\SYSTEM\CurrentControlSet\Control\Terminal Server\RCM\Licensing Core нужно изменить значение параметра DWORD с именем LicensingMode

2 — если используется лицензирование на устройства (Per Device)
4 — при использовании RDS лицензирования на пользователей (Per User)

Далее открываем редактор локальных/групповых политик (в данном примере рассматриваются локальные) gpedit.msc.
Переходим в раздел Конфигурация компьютера Административные шаблоны -> Компоненты Windows -> Службы удаленных рабочих столов -> Узел сеансов удаленных рабочих столов -> Лицензирование
или
Computer Configuration -> Administrative Templates -> Windows Components -> Remote Desktop Services -> Remote Desktop Session Host -> Licensing.

Нас интересуют две политики:

Использовать указанные серверы лицензирования удаленных рабочих столов (Use the specified Remote Desktop license servers) - Включаем и указываем значение 127.0.0.1
Set the Remote Desktop licensing mode - выбираем режим лицензирования Per Device.
После перезагрузки сервера открываем средство диагностики лицензирования RDS и проверяем, что всё ок.
