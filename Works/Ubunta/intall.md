# Настраиваем сеть

- Узнаем, какой порт используется

  > ip link show
  > или
  > ip a

- Прописываем нужный ip адрес
  > sudo ip addr add 172.20.2.11/24 dev ens8
- Включаем интерфейс

  > ip link set dev ens192 up
  > ip link set dev enp0s25 down (выключить)

- Прописываем шлюз

  > sudo ip route add default via 172.21.1.1

## Прописываем DNS

> nameserver 8.8.8.8
> nameserver 8.8.4.4
> nano /etc/resolv.conf

- Удалить всю IP-конфигурацию из интерфейса, вы можете использовать команду ip с параметром flush, как показано ниже. После удаления нужно перезагрузиться.

> ip addr flush eth0

ВАЖНО!!! Нужно установить статику по умолчанию DHCP

## Меняем имя машины

sudo vi /etc/hosts

> 127.0.1.1 ubuntu newname
> или
> sudo hostnamectl set-hostname новое_имя

## Устанавливаем на интерфейсе IP и включаем его

ip a (узнаем адрес)
sudo ip addr add 192.168.1.14/24 dev eth0
sudo ip link set dev eth0 up

===

- применяем изменения
  sudo netplan try ИЛИ sudo reboot

## Устанавливаем net-tools

sudo apt install net-tools

## Обновляемся

sudo apt update
sudo apt upgrade

## Устанавливаем SSH

sudo apt install openssh-server

## Устанавливаем DOTNET

sudo apt-get install -y dotnet6

## Архивный диск

sudo apt list installed | grep macroscop

## Управление дисками

fdisk -l
fdisk /dev/sdb
n для разметки
w для записи

- форматируем новый раздел
  mkfs.ext4 /dev/sdb1

> sudo systemctl restart systemd-resolved.service

Проверяем статус

> sudo systemctl status systemd-resolved.service

- Установка .NET 6

> sudo apt-get update && \
> sudo apt-get install -y dotnet6
> sudo reboot

- В настройках выбираем последнюю версию MySql и сервера
- Создаем каталок с доменом
- Заходим в MySQL и создаем новую БД с "Сравнение"

## Команды ubunta

Перезагрузка

> sudo shutdown -r now
