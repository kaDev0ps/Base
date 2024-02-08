# Подготовка

На новом и старом серверах создаем каталог, в который будут складываться файлы для импорта и экспорта:

mkdir -p /backup/zimbra

Выдадим разрешающие права на созданную папку для пользователя zimbra:

chown -R zimbra:zimbra /backup/zimbra

На старом сервере входим под пользователем zimbra:

su - zimbra

Переходим в созданный ранее каталог:

cd /backup/zimbra

Можно приступать к экспорту.

# Экспорт данных

Начнем с экспорта — выполняем команды на старом (текущем сервере).

1. Домены:

zmprov gad | tee -a domains.txt

Проверяем:

cat domains.txt

2. Учетные записи администраторов:

zmprov gaaa | tee -a admins.txt

3. Учетные записи пользователей:

zmprov -l gaa | tee -a users.txt

4. Настройки пользователей:

mkdir users_settings

for user in `cat users.txt`; do zmprov ga $user | grep -i Name: | tee -a users_settings/$user.txt ; done

- операция может занять некоторое время.

5. Пароли:

mkdir passwords

for user in `cat users.txt`; do zmprov -l ga $user userPassword | grep userPassword: | awk '{ print $2}' | tee -a passwords/$user.shadow; done

6. Группы рассылки:

zmprov gadl | tee -a distribution_lists.txt

И список адресов, которые входят в группы рассылки:

mkdir distribution_lists

for list in `cat distribution_lists.txt`; do zmprov gdlm $list > distribution_lists/$list.txt ; echo "$list"; done

7. Псевдонимы:

mkdir aliases

for user in `cat users.txt`; do zmprov ga $user | grep zimbraMailAlias | awk '{print $2}' | tee -a aliases/$user.txt ; echo $i ; done

Удалим пустые файлы с алиасами (для записей которых нет псевдонимов):

find aliases/ -type f -empty | xargs -n1 rm -v

Готово. Мы выполнили импорт всех нужных нам список. Мы должны получить следующий список файлов:

[zimbra@mail zimbra]$ ll
total 24
-rw-r----- 1 zimbra zimbra 52 Jun 12 09:31 admins.txt
drwxr-x--- 2 zimbra zimbra 151 Jun 12 10:46 aliases
drwxr-x--- 2 zimbra zimbra 264 Jun 12 10:35 distribution_lists
-rw-r----- 1 zimbra zimbra 170 Jun 12 10:22 distribution_lists.txt
-rw-r----- 1 zimbra zimbra 69 Jun 12 09:28 domains.txt
drwxr-x--- 2 zimbra zimbra 4096 Jun 12 09:51 passwords
-rw-r----- 1 zimbra zimbra 878 Jun 12 09:34 users.txt
drwxr-x--- 2 zimbra zimbra 4096 Jun 12 09:43 users_settings

Приступаем к переносу данных и импорту.

# Подготовка к импорту

Перенесем полученные файлы на новый сервер. Для этого есть несколько способов, например, с помощью WinSCP. Мы же просто перенесем данные командой rsync или scp — рассмотрим оба примера.

а) с помощью rsync:

rsync -a -e 'ssh -p <SSH-порт>' /backup/zimbra <учетная запись на новом сервере>@<IP-адрес нового сервера>:/tmp/

Например:

rsync -a -e 'ssh -p 22' /backup/zimbra sppcmmail@95.214.119.12:/tmp/

б) с помощью scp:

scp -r -P <SSH-порт> /backup/zimbra <учетная запись на новом сервере>@<IP-адрес нового сервера>:/tmp/

Например:

scp -r -P 22 /backup/zimbra root@192.168.1.20:/tmp/

После того, как мы перенесли файлы в каталог /tmp на новом сервере, подключаемся к последнему и переносим эти файлы в каталог /backup:

mv /tmp/zimbra /backup

Задаем нужные права на перенесенные файлы:

chown -R zimbra:zimbra /backup

Устанавливаем локаль с кириллицей в UTF-8:

localedef -i ru_RU -f UTF-8 ru_RU

Заходим под учетной записью zimbra:

su - zimbra

Переходим в каталог:

cd /backup

Экспортируем системную переменную:

export LC_ALL=ru_RU.UTF-8

- это необходимо для корректного отображения символов на кириллице.

Переходим, непосредственно, к импорту.

# Импорт данных

Выполним обратные операции на новом сервере, которые приведут к созданию необходимых нам данных и настроек. После каждой операции рекомендуется заходить на веб-интерфейс почтовой системы и проверять перенос.

1. Домены:

for domain in `cat domains.txt` ; do zmprov cd $domain zimbraAuthMech zimbra ; echo $domain ; done

2. Учетные записи, их настройки и пароли.

Процесс выполняется с помощью скрипта. Создадим его:

nano restore_accounts.sh

PASSWDS="passwords"
ACCOUNT_DETAILS="users_settings"

for i in `cat users.txt`
do
givenName=$(grep givenName: $ACCOUNT_DETAILS/$i.txt | cut -d ":" -f2)
displayName=$(grep displayName: $ACCOUNT_DETAILS/$i.txt | cut -d ":" -f2)
shadowpass=$(cat $PASSWDS/$i.shadow)
zmprov ca $i "TeMpPa55^()" cn "$givenName" displayName "$displayName" givenName "$givenName"
zmprov ma $i userPassword "$shadowpass"
done

И запускаем его на выполнение:

bash ./restore_accounts.sh

3. Списки рассылки:

for lists in `cat  distribution_lists.txt`; do zmprov cdl $lists ; echo "$lists -- done " ; done

А также их содержимое (создадим сначала скрипт):

nano restore_dist_lists.sh

for list in `cat distribution_lists.txt`
do
for mbmr in `grep -v '#' distribution_lists/$list.txt | grep '@'`
do
zmprov adlm $list $mbmr
echo " $mbmr has been added to $list"
done
done

Запускаем его на выполнение:

bash ./restore_dist_lists.sh

4. Псевдонимы.

Создаем скрипт:

nano restore_aliases.sh

echo "Processing User accounts"
for user in `cat users.txt`
do
echo $user
    if [ -f "aliases/$user.txt" ]; then
for alias in `grep '@' aliases/$user.txt`
do
zmprov aaa $user $alias
    echo "$user ALIAS $alias - Restored"
    done
    fi
done
echo "Processing Admin accounts"
for user in `cat admins.txt`
do
    echo $user
    if [ -f "aliases/$user.txt" ]; then
for alias in `grep '@' aliases/$user.txt`
do
zmprov aaa $user $alias
    echo "$user ALIAS $alias - Restored"
done
fi
done

Запускаем его:

bash restore_aliases.sh

Со структурой разобрались. Идем дальше.

zmprov gqu mail|sort -k 3 -n|column -t
