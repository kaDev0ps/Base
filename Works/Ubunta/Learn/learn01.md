## Обновляем инфо об обновлениях в репозиториях

sudo apt update

## Ставим обновления по умолчанию соглашаемс со всеми вопросами

sudo apt upgrade -y

## Устанавливаем SSH

sudo apt install openssh-server -y

## Устанавливаем пакет если не знаем полного названия

sudo apt search

### Проверяем статус службы

systemctl status sshd
#№ Настраиваем сеть
ip a ### Можно вызвать помощь
ip -- help
Подробный разбор опций
man ip

## Популярные команды

kill -s 9 — убить процесс (KILL)
kill -s 15 — корректно завершить процесс (TERM)

## Логи

Где хранятся:
○ /var/log/syslog (Ubuntu 16 и др.)
○ /var/log/messages (Centos 7 и др.)
● Как посмотреть:
○ tail -f /var/log/syslog
○ остановить вывод Ctrl-C

## Программы для управления окнами:

● enlightement
● fvwm
● openbox
