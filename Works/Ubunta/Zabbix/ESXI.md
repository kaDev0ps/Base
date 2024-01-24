# Установка шаблона ESXI

<!-- Создайте на хосте ESXi отдельного пользователя zabbix с правами Read-Only.
В Zabbix доступно несколько параметров для VMware ESXi: -->

cat /etc/zabbix/zabbix_server.conf | grep VMware
nano /etc/zabbix/zabbix_server.conf
<!-- StartVMwareCollectors =3
VMwareFrequency = 60
VMwarePerfFrequency = 60
VMwareCacheSize = 32M
VMwareTimeout=120
Включите эти параметры и перезапустите Zabbix: -->

systemctl restart zabbix-server.service

<!-- Проверьте, что в Zabbix включена поддержка мониторинга параметров хостов VMware: -->

cat /var/log/zabbix/zabbix_server.log | grep vmware

VMware monitoring: YES
<!-- Получите UUID вашего хоста ESXI. Для этого в расширенных настройках хоста (System - Advaned settings) ESXi включите опцию  -->
Config.HostAgent.plugins.solo.enableMob = True (Enables or disables the Debug Managed Object Browser for the ESXi host).
<!-- Перейдите на веб страницу  -->
https://172.25.70.11/mob/?moid=ha-host&doPath=hardware.systemInfo

<!-- Скопируйте значение UUID. и в шаблоне VMWARE в макросе вставить -->
Через Агент интерфейс подключаемся
Имя узла dc8a2910-20ff-0a11-8afc-002590080560
Видимое имя ESXI  

<!-- В параметрах -->

{$URL} https://172.25.70.12/sdk/
{$VMWARE.PASSWORD} M0nit0ring!
{$PASSWORD} M0nit0ring!
{$VMWARE.URL} https://172.25.70.12/sdk/
{$VMWARE.USERNAME} zabbix
{$USERNAME} zabbix
{$VMWARE.VM.UUID} dc8a2910-20ff-0a11-8afc-002590080560
{$UUID} dc8a2910-20ff-0a11-8afc-002590080560
