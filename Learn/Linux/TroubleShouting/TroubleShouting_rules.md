# Стандарнтая система безопасности - Мандатное управление доступом

Mandatory Access Control - работает, как белый список взаимодействия между процессами,файлами, пользователями ипортами

# DAC (discretionary access control) - Избирательное управление доступом

getfacl ABC **Проверяем права на файл**
setfacl -m u:lisa:rw ABC **Дали права на запись только Лизе**

## Атрибуты

vi /etc/resolv/conf
systemctl restart network

проверяем атрибуты файла

lsattr /etc/resolv.conf
chattr +i /etc/resolv.conf

<!-- Теперь никто ничего с файлом не сможет сделать. Отменить изменение трибуа можно -->

chattr -i /etc/resolv.conf

## SELINUX

<!-- вкл или выкл / После перезагрузки
Если включен то есть 2 режимаЖ
1. Enforcing
2. Permissive (Временное отклюение политики) -->

getenforce
setenforce

<!-- vi /etc/selinux/config Тут можно полностью выключить -->

ls -laZ Можно посмотреть юникс контекст какие метки у файлов

Устанавливаем apache
yum install httpd
systemctl start httpd
systemctl status httpd
systemctl stop firewalld **Отключаем firewall на CentOS**

<!-- Узнаем откуда загрузка сайта -->

vi /etc/httpd/conf/httpd.conf

<!--
После изменения файла открываем страницу и смотрим вкладку Network. Ответ должен быть 200. Код 304 это ответ с хэша.
Создаем новую директорию сайта -->

touch /var/www/test

## Добавляем в белый список директорию

grep AVC /var/log

<!-- Видим, что у нас отрабатывает отказ -->
<!-- Проверяем юникс контекст рабочей папки и сравниваем его с юникс контентом новой -->

ls -laZ /var/www/html/
ls -laZ /web/

<!-- Устанавливаем политику -->

yum provides semanage
semanage fcontext -a -t httpd_sys_content_t "/web(/.\*)?"

<!-- Проверяем приминение политики -->

restorecon -Rv .
getenforce **Проверяем**

<!-- Утилита для чтения логов -->

yum provides sealert

<!-- После ошибок перейти на  -->

/etc/var/log/message

<!-- Найти sealert -->

sealert -l 13d93fc6-1e8e-413a-a6cf-6ccc458f87a0

<!-- Применить рекомендацию -->

semanage port -a -t http_port_t -p tcp 180
restorecon -Rv .
systemctl restart httpd
