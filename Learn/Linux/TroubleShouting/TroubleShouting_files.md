# Хранение данных в Данных

0. Приложение
1. Виртуальная файловая система VFS
2. DEVICE MAPPER
3. SDA,SDB
4. MultiPart
5. SCSI-MIB
6. SCSI-Lock
7. Жесткий диск

# Как выглядит диск

MBR таблица разделов (SDA1[ext4],SDA2[xfs],SDA3[fat])

# Как восстановить диск если удалена разметка диска

1. Создаем диск SDA1 на все пространство
2. Из 100ГБ будет занято 20ГБ под [ext4]
3. Удаляем SDA1 и создаем заного под наши 20ГБ
4. Проделываем эту операцию под все разделы

# Запись файлов на диск

Сперва идет 512 КБ раздел MBR,который говорит о разметке диска
Потом 1 МБ драйверов GRUB2
Дальше файловая система, на которую пишутся файлы

<!-- Мы можем посмотреть информация записанную в 512 Б раздела MBR -->

xxd -l 512 /dev/sda

<!-- Запишем 0 в раздел sda/ Пропускаем 446 байт и пишем по 1 разу 64 раза 0 lj 510 байт-->

dd if=/dev/zero of=/dev/sda bs=1 seek=446 count=64

<!-- Мы сломали таблицу MBR, но данные сохранены!! Загружаемся с LIVI CD и создаем новый диск -->

sudo fdisk /dev/sda
p смотрим какие есть диски
n создаем разделы
w записать
sudo mount /dev/sda1 /mnt монтируем и проверяем реальный размер
df -Th

<!-- Если таблицы созданы верно, то появится тип разметки LVM -->

