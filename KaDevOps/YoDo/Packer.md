# instal Ubuntu

<!-- Packer позволяет создавать образы дисков виртуальных машин с заданными в конфигурационном файле параметрами. Сценарий описывает создание образа диска с помощью Packer. -->

curl -fsSL https://apt.releases.hashicorp.com/gpg | sudo apt-key add -
sudo apt-add-repository "deb [arch=amd64] https://apt.releases.hashicorp.com $(lsb_release -cs) main"
sudo apt-get update && sudo apt-get install packer
sudo apt  install packer

<!-- Packer установит debian11, прикатит туда пользователя, добавит репозиторий с docker, установит nginx и docker. Далее запакует это в ovf формат. -->
<!-- 
Instal VirtualBox Vargant
sudo apt install virtualbox
curl -O https://releases.hashicorp.com/vagrant/2.2.9/vagrant_2.2.9_x86_64.deb
sudo apt install ./vagrant_2.2.9_x86_64.deb
vagrant --version
packer plugins install github.com/hashicorp/virtualbox
Запуск сборки
packer build -force -var 'version=1.2.0' debian11-config.json -->

# Связь машин с яндексом
По идентификатору и зоне