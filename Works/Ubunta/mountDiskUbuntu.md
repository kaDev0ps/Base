проверяем наши диски
lsblk
Монтируем папку
mkdir -p ~/zbbackup
Монтируем диск на эту папку
sudo mount /dev/sda1 ~/zbbackup

# Проверяем диски

sudo fdisk -l
lsblk

<!-- Пишем команду создания тома к новому диску -->

sudo cfdisk /dev/sdb

<!-- Выбираем разметку -->

Enter > Write

<!-- Форматируем -->

sudo mkfs.ntfs -f /dev/sdb1

<!-- создаем папку -->

mkdir hdd2

<!-- Монтируем папку -->

nano /etc/fstab

<!-- Вносим запись, чтоб монтировалось по умолчанию любой диск  -->

/dev/sdb1 /home/ka/hdd2 ntfs defaults 0 0

<!-- Монтируем папку -->

sudo mount /dev/sdb1 /mnt/storage
