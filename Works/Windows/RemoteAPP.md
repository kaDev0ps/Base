# Настройка на сервере

Устанавливаем RDP по сеансом

<!-- Идем в реестр и создаем раздел 1С -->

HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications\1c

<!-- Вносим записи -->

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications\1c]
"RequiredCommandLine"=""
"Name"="1c"
"SecurityDescriptor"=""
"CommandLineSettings"=dword:00000000
"IconIndex"=dword:00000000
"ShowInTSWA"=dword:00000001
"Path"="\"C:\\\\Program Files (x86)\\\\1cv8\\\\common\\\\1cestart.exe\""
"IconPath"="\"%SystemRoot%\\Installer\\{0DC036FC-A55E-A26E-073B-BF6792A7AB9C}\\ShortCut_ThinStarter.exe\""
"ShortPath"="\"C:\\\\PROGRA~2\\\\1cv8\\\\common\\\\1cestart.exe\""

<!-- Изменяем параметр -->

HKLM\Software\Microsoft\WindowsNT\CurrentVersion\TerminalServer\TSAppAllowList
fDisableAllowList значение 1

<!-- Идем на ПК с которого подключаемся и создаем RDP соединение. Настройки из блокнота редактируем. -->

redirectclipboard:i:1
redirectposdevices:i:0
redirectprinters:i:1
redirectcomports:i:1
redirectsmartcards:i:1
devicestoredirect:s:_
drivestoredirect:s:_
redirectdrives:i:1
session bpp:i:32
prompt for credentials on client:i:1
span monitors:i:1
use multimon:i:1
remoteapplicationmode:i:1
server port:i:3389
allow font smoothing:i:1
promptcredentialonce:i:1
authentication level:i:2
gatewayusagemethod:i:2
gatewayprofileusagemethod:i:0
gatewaycredentialssource:i:0
full address:s:10.200.202.8
alternate shell:s:||1c
remoteapplicationprogram:s:||1c
gatewayhostname:s:
remoteapplicationname:s:1cestart.exe
remoteapplicationcmdline:s:
screen mode id:i:2
winposstr:s:0,3,0,0,800,600
compression:i:1
keyboardhook:i:2
audiocapturemode:i:0
videoplaybackmode:i:1
connection type:i:7
networkautodetect:i:1
bandwidthautodetect:i:1
displayconnectionbar:i:1
enableworkspacereconnect:i:0
disable wallpaper:i:0
allow desktop composition:i:0
disable full window drag:i:1
disable menu anims:i:1
disable themes:i:0
disable cursor setting:i:0
bitmapcachepersistenable:i:1
audiomode:i:0
autoreconnection enabled:i:1
prompt for credentials:i:0
negotiate security layer:i:1
remoteapplicationicon:s:
shell working directory:s:
gatewaybrokeringtype:i:0
use redirection server name:i:0
rdgiskdcproxy:i:0
kdcproxyname:s:
