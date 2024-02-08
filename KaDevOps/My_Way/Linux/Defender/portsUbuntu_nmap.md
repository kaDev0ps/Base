# Инструмент по управлению портами

sudo apt install nmap -y

# Узнать сколько хостов занято в сети

nmap -v -sn 192.168.0.0/16

# Сканирование портов в сети

nmap 172.21.1.195

# Защита от ботов

apt install fail2ban -y
systemctl start fail2ban
systemctl status fail2ban

Заходим в настройки
vi /etc/fail2ban/fail2ban.conf
