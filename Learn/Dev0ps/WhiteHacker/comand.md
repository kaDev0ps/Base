# работа с zip

sudo apt-get install unzip
unzip file.zip -d destination_folder
unzip file.zip

# для запаковки

zip -r /path/to/files/\*

# Работа с CRON

Посмотреть список задач в Cron: cat /etc/crontab

# Работа с правами

chown -R zabbix. /usr/lib/zabbix/alertscripts

# Работа с ключами SSH

~/.ssh/known_hosts

# Сканируем домен на открытие портов TCP и UDP портов

nmap -sS nmap.org
nmap -sU nmap.org

# Сканирование открытых портов

nmap -Pn -n -F -sT --open 192.168.10.0/24

# Пример команды для обнаружения узлов с ОС Windows:

nmap -Pn -n -sT -p 88,135,137,389,445,1433,3389 -sV -sC --open -iL list-of-machines.txt

# Сканирование открытых портов на узле

nmap -Pn -n -F -sV –open 172.21.1.172

# Утилита выведет список всех установленных пакетов

dpkg -l

# Подключимся к Windows-машине по ssh:

ssh john@10.8.0.14

# Передать файл на удаленную машину

# Передадим файл на удаленную машину по SSH с помощью scp и запустим файл

scp winPEASx64.exe john@10.8.0.14:C:\\Users\\John\\
john@WIN10> winPEASx64.exe
**политику количества попыток ввода пароля**

# Список активных интерфейсов

ip -br -c a

# Проверить маршрут трафика

ip r get 8.8.8.8
