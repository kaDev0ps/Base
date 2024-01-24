# установка Terraform
`wget https://releases.hashicorp.com/terraform/1.2.8/terraform_1.2.8_linux_amd64.zip`
Распаковываем
`unzip terraform_*_linux_amd64.zip`
Перенесем распакованный бинарник в каталог:
`mv terraform /usr/local/bin/`
Проверим работу
`terraform -version`

Чтобы terraform мог корректно взаимодействовать с инфраструктурой провайдера, необходимо использовать набор инструкций, которые будет использовать наша утилита. 
В терминологии terraform это называется провайдер.

# Создаем инструкцию для работы с терраформ
`nano main.tf`
<!--
terraform {

  required_version = "= 1.2.8"



  required_providers {

    yandex = {

      source  = "yandex-cloud/yandex"

      version = "= 0.73"

    }

  }

}



provider "yandex" {

token  = ""

cloud_id = "

folder_id = "

zone = "

}
-->

source — глобальный адрес источника провайдера.

terraform required_version — версия клиента terraform, для которого должен запускаться сценарий. Параметр не обязательный, но является правилом хорошего тона и может помочь избежать некоторых ошибок, которые возникнут из-за несовместимости при работе в команде.

required_providers version — версия провайдера. Мы можем указать, как в данном примере, необходимость использовать конкретную версию, а можно с помощью знака >= или 

token  — OAuth-токен для доступа к Yandex Cloud.

cloud_id — идентификатор облака, в котором Terraform создаст ресурсы.

folder_id — идентификатор каталога, в котором по умолчанию будут создаваться ресурсы.

zone — зона доступности, в которой по умолчанию будут создаваться все облачные ресурсы.

Если вдруг забыли команду, чтоб выдала вам OAuth-token и id_folder вводим в консоли

`yc config list`
<!--
token: y0_AgAAAAARB_HiAATuwQAAAAD47wPMS5I3f66RT6OSHewKkbrhw_EohHk
cloud-id: b1g3cd8s33rf8m9v2ivn
folder-id: b1g6plj85napa30ts353
compute-default-zone: ru-central1-a
-->
выполняем команду:

`terraform init`

Система загрузит нужные файлы и установит провайдер

Большую часть информации по написанию сценариев можно найти на официальном сайте хостинга или самого terraform.

# Создадим ресурс

В каталоге создадим файл infrastructure.tf

`nano infrastructure.tf`
<!--
data "yandex_compute_image" "debian_image" {

family = "debian-11"

}

resource "yandex_compute_instance" "vm-debian" {

name = "debian"

resources {

cores = 2

memory = 2

}

boot_disk {

initialize_params {

image_id = 

}

}

network_interface {

subnet_id = yandex_vpc_subnet.subnet_terraform.id

nat  = true

}

metadata = {

user-data = "${file("./meta.yml")}"

}

}

resource "yandex_vpc_network" "network_terraform" {

name = "net_terraform"

}

resource "yandex_vpc_subnet" "subnet_terraform" {

name  = "sub_terraform"

zone  = "ru-central1-a"

network_id  = yandex_vpc_network.network_terraform.id

v4_cidr_blocks = ["192.168.15.0/24"]

}
-->
data — позволяет запрашивать данные. В данном примере мы обращаемся к ресурсу yandex_compute_image с целью поиска идентификатора образа, с которого должна загрузиться наша машина.yandex_compute_image — ресурс образа. Его название определено провайдером.
debian_image — произвольное название ресурса. Его мы определили сами и будем использовать в нашем сценарии.
debian-11 — в нашем примере мы запрашиваем данные ресурса, который ищем по family с названием debian-11. Данное название можно посмотреть в контроль панели хостинга — нужно выбрать образ и кликнуть по нему. Откроется страница с дополнительной информацией. В данном случае debian-11 соответствуем debian-11.

Все образы можно посмотреть командой

`yc compute image list --folder-id standard-images`

resource — позволяет создавать различные ресурсы.yandex_compute_instance — ресурс виртуальной машины. Его название определено провайдером.
vm-debian — наше название ресурса для виртуальной машины.

image_id - Идентификатор существующего образа для использования в качестве источника образа. Изменение этого идентификатора приводит к созданию нового ресурса. Здесь указываем id нашего образа, который мы создавали с помощью Packer.

По умолчанию, если нет образов вписываем:

`data.yandex_compute_image.ubuntu_image.id`


yandex_vpc_network — ресурс сети, определенный провайдером.
network_terraform — наше название для ресурса сети.
yandex_vpc_subnet — ресурс подсети, определенный провайдером.
subnet_terraform — наше название для ресурса подсети.
metadata — обратите особое внимание на передачу метеданных. В данном примере мы передаем содержимое файла, а сам файл рассмотрим дальше.

Создаем файл с метаданными meta.yml

nano meta.yml
<!--
#cloud-config

users:

- name: test

groups: sudo

shell: /bin/bash

sudo: ['ALL=(ALL) NOPASSWD:ALL']

ssh-authorized-keys:

- ssh-rsa AAAAB7dd6r..................R4tgDDDf5r/hRR4dd=

-->
#cloud-config — как выяснилось, этот комментарий обязательный.

name — имя пользователя, который будет создан на виртуальной машине.

groups — в какую группу добавить пользователя.

shell — оболочка shell по умолчанию.

sudo — правило повышения привилегий.

ssh-authorized-keys — список ключей, которые будут добавлены в authorized_keys.

# Создание пары ключей SSH

`ssh-keygen -t rsa -b 2048`

После прописываем публичный ключ

# Проверьте конфигурацию командой:

`terraform validate`

Если конфигурация является допустимой, появится сообщение:

Success! The configuration is valid.