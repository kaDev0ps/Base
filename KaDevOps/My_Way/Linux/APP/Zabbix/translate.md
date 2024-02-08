# Перевод Zabbix

<!-- Смотрим какие локали установлены: -->

sudo locale -a

<!-- Смотрим какие локали доступны, а конкретно применительно к русскому: -->

cat /usr/share/i18n/SUPPORTED | grep ru\_

ru_RU.KOI8-R KOI8-R

ru_RU.UTF-8 UTF-8

ru_RU ISO-8859-5

ru_RU.CP1251 CP1251

sudo locale-gen ru_RU
sudo locale-gen ru_RU.UTF8

sudo dpkg-reconfigure locales

# Перезагружаем сервер

sudo service apache2 restart
