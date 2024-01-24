# Zabbix server: Utilization of icmp pinger processes over 85%
<!-- Заходим в  -->
sudo nano /etc/zabbix/zabbix_server.conf
<!-- Меняем параметр -->
StartPollers=5
<!-- Перезапускаем службу -->
systemctl restart zabbix-server.service
