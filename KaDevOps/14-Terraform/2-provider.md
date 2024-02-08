## Подключение к провайдеру
Создаем директорию terraform и в ней файл для подключения к нашему провайдеру
`nano main.tf`
<!-- 
terraform {
  required_providers {
    vsphere = {
      source  = "local/hashicorp/vsphere"
      version = ">= 2.6.1"
    }
  }
  required_version = ">= 0.13"
}
provider "vsphere" {
  user           = "${var.vsphere_user}"
  password       = "${var.vsphere_password}"
  vsphere_server = "${var.vsphere_server}"
  allow_unverified_ssl = true
}
  -->

  В файле main.tf в самом начале мы описали переменные, которые используются для подключения к система vSphere. Далее идет декларирование реальных ресурсов, которые задействованы в системе vSpehre. В данном примере это название виртуального дата центра VMware, название датастора, на котором будут храниться данные наших виртуалок, а также имена кластера, виртуальной сети и шаблона ВМ, который будет задействован при создании новых машин.

  Теперь нам нужно создать отдельный файл variables.tf, в котором мы задекларируем переменные, используемые в нашем примере. Эти переменные – vsphere_user, vsphere_password, vsphere_server. Содержимое данного файла представлено ниже.
 ` nano variables.tf`

<!-- 
variable "vsphere_user" {}
variable "vsphere_password" {}
variable "vsphere_server" {} 
variable "vsphere_pool" {}
variable "vsphere_iso_pool" {}
variable "vsphere_directory" {}
variable "vsphere_vlan" {}
-->

Также создадим отдельный файл, который должен называться terraform.tfvars. В нем мы определяем уже конкретные значения переменных, которые были ранее задекларированы в файле variables.tf.
`nano terraform.tfvars`

<!-- 
vsphere_user= "administrator@vsphere.local"
vsphere_password= "Some@Password1234"
vsphere_server= "192.168.1.100"
vsphere_pool= "702-POOL5"
vsphere_iso_pool= "702-POOL4"
vsphere_directory= "NO PRODUCTION - KA"
vsphere_vlan= "VLAN265"
 -->

Проверим есть ли соединение с нашей vSphere
`terraform plan`

Если все хорошо, то переходим к дальнейшей настройке. Добавляем в main код следующие данные:

> Указываем хранилище данных на котором создасться сервер
> Указываем хранилище данных где хранится образ
> Указываем pool ресурсов. Директорию в которой создасться VM
> Выбираем сеть
> Выбираем образ из какого разворачиваем машину

<!-- 
data "vsphere_datacenter" "dc" {
  name = "702"
}
data "vsphere_datastore" "datastore" {
  name          = "${var.vsphere_pool}"
  datacenter_id = "${data.vsphere_datacenter.dc.id}"
}
data "vsphere_datastore" "iso_datastore" {
  name          = "${var.vsphere_iso_pool}"
  datacenter_id = "${data.vsphere_datacenter.dc.id}"
}
data "vsphere_resource_pool" "pool" {
  name          = "${var.vsphere_directory}"
  datacenter_id = "${data.vsphere_datacenter.dc.id}"
}
data "vsphere_network" "network" {
  name          = "${var.vsphere_vlan}"
  datacenter_id = "${data.vsphere_datacenter.dc.id}"
}
-->

Проверяем все ли верно, не ругается ли сервер
`terraform plan`

## Мы создаем машину c помощью ISO
Теперь подготовим файл vm.tf, в котором опишем две виртуальные машины, которые будут запущены в процессе работы terraform. При описании свойств виртуальных машин, мы будем использовать переменные инфраструктуры, которые до этого определили. Также в terraform есть возможность задать такие настройки виртуальной машины, как ip адрес, хостнейм и DNS. Ниже представлен полный файл с конфигурацией этих виртуальных машин.
`nano vm.tf`

Указываем имя и размещение
<!-- 
resource "vsphere_virtual_machine" "vm1" {
  name             = "test-terra-vm"
  resource_pool_id = "${data.vsphere_resource_pool.pool.id}"
  datastore_id     = "${data.vsphere_datastore.datastore.id}"
  -->

Характеристики сервера
Устанавливаем guest id в соответствии с документацией
https://docs.vmware.com/en/VMware-HCX/4.8/hcx-user-guide/GUID-D4FFCBD6-9FEC-44E5-9E26-1BD0A2A81389.html
<!--
  num_cpus = 2
  memory   = 4096
  guest_id = "ubuntu64Guest"
  scsi_type = "lsilogic-sas"
  wait_for_guest_net_timeout = 0
  -->

 Сеть, которую определили в шаблоне
 <!--
  network_interface {
    network_id   = "${data.vsphere_network.network.id}"
  }
  -->

 Указываем параметры диска
 <!--
  disk {
    label            = "test-disk"
    size             = 40
    thin_provisioned = true
  } -->

Указываем где лежит образ в pool, который указали ранее
<!--
  cdrom {
    datastore_id = "${data.vsphere_datastore.iso_datastore.id}"
    path         = "ubuntu-20.04.3-live-server-amd64.iso"
  } 
}
  -->

Когда все необходимые файлы созданы, пришло время для запуска terraform. Мы будем использовать 3 команды.
`terraform init`
`terraform plan`
`terraform apply`

Так с помощью команды terraform init произойдет инициализация terraform и загрузка провайдера vsphere для связи с системой VMware. Команда terraform plan проанализирует текущее состояние системы и то, каков будет результат и изменения в случае полноценного выполнения заданных инструкций. Ну а уже с помощью terraform apply мы передадим инструкции в vCenter, и произойдет создание запланированных нами виртуальных машин.

## Статистика
При работе с terraform создается файл **terraform.tfstate** в нем описано, что было создано и сделано. если файл удален, то ему не с чем сравнивать и он повторно создаст. Эти файлы хранятся на удаленном репозиториях.

# Уничтожение созданных VM
`terraform destroy`

**Заключение**
> В данной статье приводится пример реального использования terraform для управления виртуализированной инфраструктурой. Лично для меня такой подход стал уже нормой и необходимостью в повседневной работе. Он позволяет прилично экономить время на рутинных операциях. И что самое главное, инструмент унифицированный и позволяет работать, как с различными системами виртуализации, так и с облачной инфраструктурой.