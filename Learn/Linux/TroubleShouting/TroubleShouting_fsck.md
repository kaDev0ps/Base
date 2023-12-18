# Сверяем метаданные с файловаой системой

1. Никогда нельзя использовать на примонтированном диске

# LVM

LVM (Logical Volume Manager) – подсистема операционных систем Linux, позволяющая использовать разные области физического жесткого диска или разных жестких дисков как один логический том.

<!-- Block Device (sda, sdb, sdc)
Physical Volume (50, 50, 50) -->

pvs

<!-- Volume Group (150G) -->

vgs

<!-- Logical Volume (75, 30 , 45) -->

lvs

<!-- FS (/var /test /home) -->

vgcfgrestore -l centos
