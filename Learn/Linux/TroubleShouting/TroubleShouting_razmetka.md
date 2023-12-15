# Делаем размер диска GPT

Смотрим состояние
lsblk
yum provides gdisk
yum install gdisk

<!-- Выбираем диск для разметки -->

gdisk /dev/sdb
n **создаем разделы, сколько надо и созраняем**
w

<!-- Ломаем разметку диска -->

dd if=/dev/zero of=/dev/sdb bs=1024 count=2

<!-- Восстанавливаем -->

gdisk /dev/sdb
1
w
Y

<!-- Фрагментируем диск в формат ext4 -->

mkfs.ext4 /dev/sdb1

<!-- Сохраняем параметрыъ -->

vi /etc/fstab
/dev/sdb1 /sdb ext4 defaults 0 0
