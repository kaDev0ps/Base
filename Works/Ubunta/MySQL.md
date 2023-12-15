# УДАЛЕНИЕ MYSQL
Остановите сервис базы данных:

> sudo systemctl stop mysqld

Чтобы удалить MySQL с сохранением настроек и файлов конфигурации, нужно использовать опцию remove:

> sudo apt remove mysql-server mysql-common mysql-server-core-* mysql-client-core-*

Однако, если вы хотите удалить базу данных полностью, вместе со всеми её конфигурационными файлами, нужно использовать purge:

> sudo apt purge mysql-server mysql-common mysql-server-core-* mysql-client-core-*

Также нужно зайти в каталог /var/lib/mysql и удалить оттуда файлы базы данных, если они вам больше не нужны:

> sudo rm -Rf /var/lib/mysql/

И удалите папку конфигурационных файлов, если она осталась:

> sudo rm -Rf /etc/mysql/

Не забудьте про логи:

> sudo rm -rf /var/log/mysql
Остатки
> sudo apt autoremove
Удалите пользователя и группу, созданные для MySQL:

> sudo deluser --remove-home mysql

> sudo delgroup mysql