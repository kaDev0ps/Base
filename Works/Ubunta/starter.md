### root

sudo adduser ka
sudo usermod -aG sudo ka
su testuser
sudo whoami

## меняем имя ПК

## VMWARE

apt install open-vm-tools

sudo hostname server-mail
sudo nano /etc/hostname
sudo nano /etc/hosts
sudo nano /etc/netplan/tab-tab **копируем и изменяем шаблон ниже**

## Ubuntu 22.04

cat /usr/share/doc/netplan/examples/static.yaml

  <!-- 
  version: 2
  renderer: networkd
  ethernets:
    enp3s0:
      addresses:
        - 95.214.119.26/24
      nameservers:
        search: [mydomain, otherdomain]
        addresses: [8.8.8.8, 1.1.1.1]
      routes:
        - to: default
          via: 10.10.10.1 -->

**REBOOT!**

<!-- В системах Ubuntu и Debian откройте файл /etc/apache2/apache2.conf с правами root: -->

sudo nano /etc/apache2/apache2.conf

<!--
Добавьте в конец файла строку ServerName 127.0.0.1: -->

ServerName 127.0.0.1

<!-- 127.0.1.1 server-mail-->

# Узнаем IP

ip a

# Включаем интерфейс

sudo ip link set dev eth0 up
Настройки сети находятся в файле /etc/network/interfaces
sudo ip addr add 10.200.202.46/24 dev ens160

        # Прописываем шлюз

        sudo ip route add default via 172.21.0.9 dev ens8

        # Прописываем DNS

        > nameserver 8.8.8.8
        > nameserver 8.8.4.4
        > nano /etc/resolv.conf

        # Меняем имя машины

        sudo nano /etc/hosts
        sudo nano /etc/hostname

# Настройка сети

> nano /etc/netplan/(tab-tab)

Пример статического IP

<!-- # This is the network config written by 'subiquity'
network:
  ethernets:
    ens160:
      addresses:
      - 10.200.202.147/24
      gateway4: 10.200.202.1
      nameservers:
        addresses:
        - 8.8.8.8
        - 77.88.8.8
        search:
        - test
  version: 2 -->

Пример DHCP

<!--
network:
  ethernets:
    ens160:
      dhcp4: true
  version: 2 -->

>

# применяем изменения

sudo netplan apply

# root

sudo adduser ka
sudo usermod -aG sudo ka

# Устанавливаем SSH

sudo apt install openssh-server
sudo systemctl enable ssh

[POOL4] WebArx-storage (sedyh-2-0)/WebArx-storage (sedyh-2-0)\_1.vmdk
