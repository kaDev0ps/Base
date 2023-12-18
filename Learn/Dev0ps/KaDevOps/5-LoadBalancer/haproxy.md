# Установка балансировщика HAProxy

## Источник https://www.dmosk.ru/miniinstruktions.php?mini=haproxy-centos7&ysclid=lq3ih7cznf919131169

<!-- HAProxy присутствует в репозиториях Linux. Команды для установки немного различаются и зависят от типа операционной системы.

а) RPM (Rocky Linux, CentOS, Red Hat, Fedora): -->

yum install haproxy

<!-- б) DEB (Debian, Ubuntu): -->

apt update

apt install haproxy

<!-- После устрановки запустим сервис и разрешим его автозапуск.

Это можно сделать командами: -->

systemctl enable haproxy

systemctl start haproxy

<!-- На этом установка и запуск закончены. -->

<!-- Конфигурирование HAProxy выполняется в файле /etc/haproxy/haproxy.cfg. Все основные настройки находятся в 4-х секциях:

global. Глобальные настройки, распространяемые на все публикации.
defaults. Настройки, применяемые по умолчанию, если они не указаны явно в публикации.
frontend. Правила обработки запросов, приходящих на сервер и передачи этих запросов серверам backend. Может быть несколько.
backend. Настройка конечных серверов, которые обрабатывают запросы и возвращают результаты. Может быть несколько. -->

<!-- Рассмотрим часто встречаемые варианты использования HAProxy.

После выполнения настройки не забываем перезапустить сервис командой: -->

systemctl reload haproxy

<!-- 1. HTTPS запросы
Frontend: -->

frontend https-frontend
bind \*:443 ssl crt /etc/ssl/domaincert.pem
reqadd X-Forwarded-Proto:\ https
default_backend https-backend

<!-- * где https-frontend — название фронтэнда; bind *:443 — сервер слушает на всех IP-адресах на порту 443 (https); /etc/ssl/domaincert.pem — файл, в котором хранятся алгоритмы для закрытого и открытого ключей; https-backend — бэкэнд, на который будут отправляться запросы. -->

Backend:

backend https-backend
redirect scheme https if !{ ssl_fc }
server s1 192.168.0.20:80 check
server s2 192.168.0.30:80 check

<!-- * где https-backend — название бэкэнда; s1 и s2 — название серверов, на которые переправляем запросы; 192.168.0.20 и 192.168.0.30 — IP-адреса серверов, на которые переправляем запросы. -->
