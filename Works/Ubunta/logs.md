# Использование logs

cd /var/log/

<!-- messages **текущие события**
secure  **безопасноость** -->

journalctl **логи текущей нагрузки**

<!-- Пример использования -->

journalctl -u sshd.service

# Уровень критичности

<!-- Значение Серьезность Ключевое слово
0 Emergency emerg
1 Alert alert
2 Critical crit
3 Error err
4 Warning warning
5 Notice notice
6 Informational info
7 Debug debug -->

## Смотрим Уровень ошибок с 0 до 3

journalctl -p 0..3

## Смотрим ошибки ядра

dmesg

## Справка

dmesg -help
