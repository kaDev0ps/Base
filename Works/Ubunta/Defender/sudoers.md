# Доступ Без использования команды sudo следующим образом:

sudo nano /etc/sudoers
<!-- В этом файле найдите строку, содержащую ALL=(ALL:ALL) ALL и добавьте следующую строку ниже ЭТО ВАЖНО!!!: -->

username ALL=(ALL) NOPASSWD:ALL