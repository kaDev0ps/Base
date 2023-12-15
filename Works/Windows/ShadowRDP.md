# Разрешение на RDP

Переключитесь в режим редактирования политики и перейдите в секцию GPO Computer Configuration -> Administrative Templates -> Windows Components -> Remote Desktop Services -> Remote Desktop Session Host -> Connections;
Найдите и включите политику Allow Users to connect remotely by using Remote Desktop Services, установив ее в Enable;

Если на компьютерах включен Windows Defender Firewall, нужно в этой же GPO разрешить RDP-трафик для доменного профиля. Для этого нужно активировать правило Windows Firewall: Allow inbound Remote Desktop Exceptions (находится в разделе Computer Configuration -> Administrative Templates -> Network -> Network Connections -> Windows Firewall -> Domain Profile).

# Групповые политики управления теневыми подключениями к RDS сессиям в Windows

Параметры удаленного управлениями RDS сессиями пользователя настраиваются отдельным параметром групповых политик — Set rules for remote control of Remote Desktop Services user sessions (Установить правила удаленного управления для пользовательских сеансов служб удаленных рабочих столов). Данная настройка находится в разделе Policies -> Administrative Templates -> Windows components -> Remote Desktop Services -> Remote Session Host -> Connections (Административные шаблоны –> Компоненты Windows –> Службы удаленных рабочих столов – Узел сеансов удаленных рабочих столов –> Подключения) в пользовательской и компьютерной секциях GPO. Данной политике соответствует DWORD параметр реестра Shadow в ветке HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services (значения этого параметра, соответствующие параметрам политики указаны в скобках).

Этой политикой можно настроить следующие варианты теневого подключения RD Shadow:

No remote control allowed — удаленное управление не разрешено (значение параметра реестра Shadow = 0);
Full Control with users’s permission — полный контроль сессии с разрешения пользователя (1);
Full Control without users’s permission — полный контроль без разрешения пользователя (2);
View Session with users’s permission – наблюдение за сеансом с разрешением пользователя (3);
View Session without users’s permission – наблюдение за сеансом без разрешения пользователя (4).

# Добавить группу не админов

wmic /namespace:\\root\CIMV2\TerminalServices PATH Win32_TSPermissionsSetting WHERE TerminalName="RDP-Tcp" CALL AddAccount "RDP-SHADOW",2

# Посмотреть права

wmic /namespace:\\root\CIMV2\TerminalServices PATH Win32_TSAccount WHERE TerminalName="rdp-tcp" get AccountName,PermissionsAllowed
