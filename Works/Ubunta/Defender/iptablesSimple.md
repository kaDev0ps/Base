# Для удаления правила из цепочки INPUT в iptables на Ubuntu
<!-- Проверьте текущие правила iptables в цепочке INPUT с помощью команды: -->

sudo iptables -L INPUT --line-numbers

<!-- Удалите правило, указав его номер строки, с помощью команды: -->
sudo iptables -D INPUT <номер_строки>

 <!-- Повторно проверьте текущие правила iptables в цепочке INPUT, чтобы убедиться, что правило было успешно удалено: -->

sudo iptables -L INPUT --line-numbers

<!-- Правило должно исчезнуть из списка. -->
<!-- Для сохранения используем -->
iptables-save


# Вывести только цепочки
sudo iptables -t filter -S



# Если мы хотим проверить статус жругой цепочки, то просто меняем ее имя

sudo iptables -L DOCKER --line-numbers
sudo iptables -D DOCKER <номер_строки>
<!-- Вернем на место -->
iptables -A DOCKER -d 172.17.0.3/32 ! -i docker0 -o docker0 -p udp -m udp --dport 51821 -j ACCEPT

