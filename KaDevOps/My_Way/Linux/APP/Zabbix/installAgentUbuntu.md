# Установка агента Zabbix на Ubuntu

sudo apt-get install zabbix-agent
sudo vi /etc/zabbix/zabbix_agentd.conf

Server= 127.0.0.1:10050
ServerActive=10.200.202.200
HostName=AVA-StaffCop

<!-- Перезагрузим службу командой: -->

sudo systemctl restart zabbix-agent

<!-- Посмотрим, запустился ли он. -->

sudo systemctl status zabbix-agent
