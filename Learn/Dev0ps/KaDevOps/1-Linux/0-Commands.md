# Посмотреть перечень процессов:

<!-- Посмотреть время и сколько включен ПК -->

uptime

<!-- Посмотреть систему -->

uname -a

<!-- Очистить терминал -->

clear

<!-- Ищет команды в пути -->

echo $PATH

<!-- Если не знаешь команду то помошник -->

man -k time

<!-- Узнать где лежит програмка -->

whereis time
locate time

<!-- Закрыть процесс -->

ctrl + c

<!-- Выйти но процесс будет идти -->

ctrl + z

<!-- Отобразить процесс, который идет на фоне -->

fg

<!-- Посмотреть запущенный процессы от пользователя -->

ps

<!-- показыть скрытые файлы -->

ls -la

<!-- показыть в какой я директории -->

pwd

<!-- навигация по директориям  Домашняя, Корень, Родительская-->

cd
cd ~/Documents/
cd /
cd ..

<!-- Вывод содержимого файла на экран -->

cat file.txt
more file.txt
less file.txt

<!-- создание файлов, копирование ? любой симбвол, -v показать процесс-->

touch file.txt
cp file?.txt -v
cp -R Dir1 Dir2 -v
rm file.txt

<!-- создание папок начиная с родительской, удаление рекурсивное-->

mkdir -p Dir2/Dir1
rmdir Dir2 (пустые папки)
rm -R Dir\*

<!-- Создание ссылок символических и постоянных -->

ln -s /home/ka/Dir4 MyLinkDir4
ln file.txt fileDuble.txt

<!-- Поиск файлов / строчки слова символы -->

find /home -name "\*.txt"
wc file.txt
wс -l # строчки
wс -w # слова

<!-- Сортировка строчек по алфавиту -->

sort names.txt
sort -n numbers.txt

<!-- Разделение текска по делиметру и использование пайпа -->

cut -d ">" -f 3 files.txt | sort

<!-- Поиск слова в строчках в файле, команда grep
Что ищется и где
-i Игнорируем капслок -->

grep -i linux ./_
grep -i ka /etc/passwd
grep -E "[A-Za-z\._-]@[A-Za-z]\*" mail.txt | wc
grep -E "[A-Za-z\.]|[A-Za-z]" mail.txt | wc

<!-- Перенаправление вывода и ввода | отправить в файл и добавить -->

sort names.txt > nameSorted.txt
sort -n numbers.txt >> nameSorted.txt
grep ka /etc/_ > good.txt 2> errors.txt
grep ka /etc/_ &> all.txt

<!-- архивирование (c - создание, t - просмотр, ч - распаковка, v - чтобы видеть процесс, f - всегда в конце с какаким архивом работаем)-->

tar cf mytar.tar mydir
tar tf mytar.tar
tar xvf mytar2.tar

<!-- компрессия gzip bzip2 xz (c - создание, t - просмотр, ч - распаковка, v - чтобы видеть процесс, f - всегда в конце с какаким архивом работаем)-->

gzip mytar.tar  
gunzip mytar.tar.gz
bzip2 mytar.tar  
bunzip2 mytar.tar.bz2
xz mytar.tar  
unxz mytar.tar.bz2

<!-- Все одной командой / Распаковка одинакова-->

tar cvf MyTAR.tar Dir2/
tar cvzf MyGZIP.gz Dir2/
tar cjf MyBZIP2.bz2 Dir2/
tar cJf MyXZ.xz Dir2/
tar xvf MyXZ.xz

<!--ZIP для windows-->

zip -r myZIP.zip Dir2
unzip myZIP.zip

<!-- Администрировнаие пользователей -->

cat /etc/passwd
cat /etc/shadow
cat /etc/group
su user2
whoami
id kolya
last
who
w (кто в онлайн и чем занят)
ls -l /home (у пользователей создаются домашние каталоги)

<!-- Администрировнаие пользователей -->

sudo adduser -m kolya (домашняя директория home)
sudo userdel -r kolya
sudo deluser vasya sudo (удалить из группы)

<!-- Директория с шаблонами для пользователей -->

cd /etc/skel/
sudo groupadd testers
sudo groupdel testers
cat /etc/group
sudo usermod -aG sudo vasya

<!-- Права доступа на файлы и папки -->

sudo chown petya zzz/ ## Изменить владельца папки
sudo chgrp moderators file.txt/ ## Изменить группу
sudo chmod o+t test.txt ## Удалять файлы может только владелец
sudo chmod g+w,г+x test.txt
sudo chmod ugo=r test.txt
sudo chmod a=r test.txt

<!-- Изменение прав цифрами -->

r=4
w=2
x=1
sudo chmod 760 read.txt
sudo chmod 1777 read.txt ## Stikibit на директорию
sudo chmod 0777 read.txt ## Stikibit

<!-- Сетевые настройки -->

ifconfig
route
ip route show
traceroute
host ya.ru
dig ya.ru
netstat
ssh mylinux

<!-- Cкачивание вайлов -->

wget http://ya.ru/img.jpg
apt-get install chromium-bsu
whereis chromium-bsu
apt-get remove chromium-bsu
cat /etc/apt/sources.list ## репозиторий
sudo dpkg -i name.deb ## Установка пакетовы
sudo dpkg -r name.deb ## Удаление пакетовы

## Для CentOS

yum install inscape
yum remove inscape
wget http://inscape.rpm
rpm -i inscape.rpm
rpm -e inscape.rpm

<!-- Поиск файлов -->

locate file.txt

<!-- Справка по ключам -->

man -k copy
echo $PATH ## Показать директории в которых идет поиск программ

<!-- SSH -->
<!-- Проверяем статус -->

sudo apt-get install openssh-server
service ssh status
service ssh start
ssh petya@8.8.8.8
uname -a

<!-- Внешний IP -->

wget -qO- eth0.me

<!-- Узнать размер папки -->

du -sh

<!-- Посмотреть все сетевые интерфейсы -->

ip -br -c a

# Установка vmware tools

sudo apt install open-vm-tools

# Проверка версии OS

hostnamectl

# Утилита статистики

sudo apt-get glances

# Посмотреть все службы

systemctl list-unit-files --type service

# Поиск службы по имени

systemctl list-unit-files 'mysql\*'

# Перезапуск

sudo systemctl restart nginx

# Меняем цвет и вормат приглашения

export PS1="\e[1;32m\A \u@\h:\w\$ \e[m"

<!-- Нужно создать если нет -->

source /etc/skel/.bashrc
