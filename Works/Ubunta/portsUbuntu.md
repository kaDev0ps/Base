# Очищаем имеющиеся правила

iptables -F

# разрешаем все установленные и связанные соединения:

<!-- Если кратко, то здесь добавляется два правила в цепочки INPUT и OUTPUT, разрешающие отправку и прием данных из интерфейса lo. Еще одно интересное и важное правило, которое многие упускают. Нужно запрещать только новые соединения, а пакеты для уже открытых нужно разрешать. Иначе получится, что мы отправляем серверу запрос (цепочка OUTPUT открыта), соединение открывается, но сервер не может нам ответить, потому что все пакеты отбрасываются в INPUT. Поэтому нужно разрешить все пакеты с состоянием ESTABLISHED и RELATED. Для этого есть модуль state: -->

iptables -A INPUT -m state --state ESTABLISHED,RELATED -j ACCEPT

<!-- Теперь нам нужно добавить правила, которые разрешат обмен данными между любыми портами на локальном интерфейсе lo, это нужно чтобы не вызвать системных ошибок (PHP не сможет обратиться к MySQL и так далее): -->

sudo iptables -A INPUT -i lo -j ACCEPT
sudo iptables -A OUTPUT -o lo -j ACCEPT

# отбрасываем все соединения с значением "INVALID"

iptables -I INPUT -m state --state INVALID -j DROP

# разрешаем ICMP трафик (ping)

iptables -A INPUT -p icmp -j ACCEPT
iptables -A OUTPUT -p icmp -j ACCEPT

# Далее следует разрешить локальные соединения с динамических портов:

# cat /proc/sys/net/ipv4/ip_local_port_range

# 32768 61000

# iptables -A OUTPUT -p TCP --sport 32768:60999 -j ACCEPT

# iptables -A OUTPUT -p UDP --sport 32768:60999 -j ACCEPT

# разрешаем себе доступ к SSH на порту 22:

iptables -A INPUT -p tcp --dport 22 -j ACCEPT

# разрешаем себе доступ к FTP на порту 21:

iptables -A INPUT -p tcp --dport 21 -j ACCEPT

# открываем HTTP (80) порт tcp.

iptables -A INPUT -p tcp --dport 80 -j ACCEPT

# открываем HTTPS (443) порт tcp.

iptables -A INPUT -p tcp --dport 443 -j ACCEPT

### !!! действие по умолчанию !!!

# запрещаем FORWARD любых пакетов сервером

iptables -P FORWARD DROP

### !!! действие по умолчанию !!!

# запрещаем INPUT любых пакетов сервером

iptables -P INPUT DROP

# Сохраняем прравила

apt install iptables-save
iptables-save

# Нумеруем список правил

iptables -L --line-numbers

# Удалим ненужне правило

iptables -t nat -D INPUT 9

<!-- Каждый раз необходимо выполнять iptables-save чтобы проверить результат. -->

<!-- Настройка переадресации с одного порта на другой -->

sudo iptables -t nat -A PREROUTING -p tcp --dport 8080 -j REDIRECT --to-port 80

<!-- Открыть порт для определенного ip -->

sudo iptables -A INPUT -p tcp -s 95.214.116.10 --dport 22 -j ACCEPT

################# ШАБЛОН СКРИПТА ########################>

sudo iptables -F
sudo iptables -A INPUT -m state --state ESTABLISHED,RELATED -j ACCEPT
sudo iptables -A INPUT -i lo -j ACCEPT
sudo iptables -A OUTPUT -o lo -j ACCEPT
sudo iptables -I INPUT -m state --state INVALID -j DROP
sudo iptables -A INPUT -p icmp -j ACCEPT
sudo iptables -A OUTPUT -p icmp -j ACCEPT
sudo iptables -A INPUT -p tcp -s 95.214.116.10 --dport 22 -j ACCEPT
sudo iptables -A INPUT -p tcp -s 95.214.116.10 --dport 80 -j ACCEPT
sudo iptables -A INPUT -p tcp -s 95.214.116.10 --dport 443 -j ACCEPT
sudo iptables -P FORWARD DROP
sudo iptables -P INPUT DROP
sudo apt install iptables-save
sudo iptables-save

######## СБРОС ВСЕх ПРАВИЛ и ЦЕПОЧЕКъЮ ###############
sudo iptables -P INPUT ACCEPT
sudo iptables -P FORWARD ACCEPT
sudo iptables -P OUTPUT ACCEPT

sudo iptables -t nat -F
sudo iptables -t mangle -F
sudo iptables -F
sudo iptables -X

sudo iptables-save
