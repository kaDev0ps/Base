# Проверяем порты на открытость

sudo apt install nmap -y

# Проверяем сколько хостов в нашей сети

nmap 172.12.0.0/16

# Сканирование на открытие портов

nmap 172.12.10.12

# Добавление в бан

apt install fail2ban -y
systemctl start fail2ban
systemctl status fail2ban

<!-- Настройки в /etc/fail2ban -->

vi /etc/fail2ban/fail2ban.conf

<!-- Правила банов регулируются тут -->

vi /etc/fail2ban/jail.conf

ps ax

<!-- Обратите внимание, откуда были запущены процессы.
Посмотреть пользователей: -->

cat /etc/passwd
ls /home

<!-- пользователи с паролями -->

cat /etc/shadow

<!-- Посмотреть неудачные попытки аутентификации: -->

sudo less /var/log/auth.log

<!-- Посмотреть crontab: -->

sudo less /etc/crontab

<!-- Посмотреть локальный crontab: -->

crontab -l

<!-- Редактировать локальный crontab: -->

crontab -e

<!-- Посмотреть ssh-ключи: -->

less .ssh/authorized_keys
