# Автоматизация сборки образов с помощью Jenkins и Packer
На основе заданной конфигурации Packer создает образы дисков виртуальной машины в сервисе Yandex Compute Cloud. Jenkins позволяет построить процесс непрерывной интеграции (Continuous integration, CI).
Образы можно использовать при создании облачной инфраструктуры, например, с помощью Terraform
****************************************************************
# Для начала надо поставить Packer и ansible,
https://cloud.yandex.ru/docs/tutorials/infrastructure-management/packer-quickstart

https://www.packer.io/plugins/builders/yandex

https://docs.ansible.com/ansible/latest/installation_guide/intro_installation.html

https://www.digitalocean.com/community/tutorials/how-to-install-and-configure-ansible-on-ubuntu-22-04

# Далее установим Интерфейс командной строки Yandex Cloud (CLI)

https://cloud.yandex.ru/docs/cli/quickstart#install

# Потом создадим сервисный аккаунт

https://cloud.yandex.ru/docs/tutorials/infrastructure-management/jenkins

# Создать сервисный аккаунт через терминал

https://cloud.yandex.ru/docs/iam/quickstart-sa

# Также надо будет получить IAM токен для сервисного аккаунта. Это ssh ключ  RSA_2048 по которому мы будем подключаться.

https://cloud.yandex.ru/docs/iam/operations/iam-token/create-for-sa

Во время все этого ты будешь получать некие ID, их и нужно заводить в конфиги в файле vars.json и template.json

Потом через Packer собираешь образ.

# Теперь надо создать платежный аккаунт.

https://cloud.yandex.ru/docs/getting-started/quickstart-individuals

# Приступим к созданию нашего образа, для этого сначала установим локально Packer.

https://packer.io/downloads.html

# Документация работы с Packer

https://cloud.yandex.ru/docs/tutorials/infrastructure-management/packer-quickstart

# Terraform является клиентом и необходимо выполнить установку на компьютер, с которого планируется управление инфраструктурой.
Чтобы terraform мог корректно взаимодействовать с инфраструктурой провайдера, необходимо использовать набор инструкций, которые будет использовать наша утилита. В терминологии terraform это называется провайдер.
# Создадим ресурс

В каталоге создадим файл infrastructure.tf файл с метаданными meta.yml

# Создаем хранилище. 

# Строим план и применяем настройки


***************************************
ПОДРОБНОСТИ
***************************************







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

Отформатируйте файлы конфигураций в текущем каталоге и подкаталогах:

`terraform fmt`

вывод будет каким:
<!--

infrastructure.tf

main.tf
-->


# После проверки конфигурации выполните команду:

`terraform plan`

В терминале будет выведен список ресурсов с параметрами. Это проверочный этап: ресурсы не будут созданы. Если в конфигурации есть ошибки, Terraform на них укажет.

После того, как мы убедимся в корректности действий, применяем настройку:

`terraform apply`

Чтобы удалить ресурсы, созданные с помощью Terraform: Выполните команду:

`terraform destroy`
Утилита пройдет по всем файлам tf в директории, найдет ресурсы и выполнит их удаление.

# Теперь рассмотрим как всем этим работать.

## Файл tfstate

В нем содержатся все изменения, которые происходили с применением terraform. Без него последний не будет знать, что уже было сделано — на практике это приведет к дублированию инфраструктуры, то есть, если мы создали сервер, потеряли файл состояния и снова запустили terraform, он создаст еще один сервер. Для больших инфраструктур с большой историей все намного критичнее.

И так, файл состояния важен с точки зрения понимания инфраструктуры самим terraform. Также у него есть еще одна функция — блокировка параллельных выполнений. Это важно для работы в командах, где одновременные действия могут привести к неожиданным последствиям. Чтобы этого не произошло, terraform создает блокировку, которая запрещает выполнения, если уже идет процесс применения плана.

Из вышесказанного делаем выводы:

Файлы состояния нужно бэкапить.
Они должны находиться в надежном месте.
У всех инженеров, которые работают с инфраструктурой должен быть доступ к файлу состояния. Они не должны его копировать на свои компьютеры и использовать индивидуально.
Рассмотрим возможность хранения данного файла состояния на облачном хранилище Яндекс.

# Сначала мы должны создать хранилище. Это можно сделать через веб-интерфейс, но мы это сделаем в terraform.

Но сначала мы создадим файл:

<!-- 
nano variables.tf

variable "yandex_folder_id" {

type = string

default  = "

} 
-->

где yandex_folder_id — название для нашей переменной;  тот идентификатор, который мы указали в файле main под аргументом folder_id.

# Теперь откроем файл main:

`nano main.tf`

И отредактируем значение folder_id на:

`folder_id = var.yandex_folder_id`

в данном примере мы теперь задаем folder_id не явно, а через созданную переменную


# Теперь создадим файл:

`nano yandex_storage_bucket.tf`

<!-- resource "yandex_iam_service_account" "sa" {

folder_id = var.yandex_folder_id

name = "sa-debian"

}

resource "yandex_resourcemanager_folder_iam_member" "sa-editor" {

folder_id = var.yandex_folder_id

role = "storage.editor"

member = "serviceAccount:${yandex_iam_service_account.sa.id}"

}

resource "yandex_iam_service_account_static_access_key" "sa-static-key" {

service_account_id = yandex_iam_service_account.sa.id

description = "Static access key for object storage"

}

resource "yandex_storage_bucket" "state" {

bucket  = "tf-state-bucket-debian"

access_key = yandex_iam_service_account_static_access_key.sa-static-key.access_key

secret_key = yandex_iam_service_account_static_access_key.sa-static-key.secret_key

} -->

yandex_iam_service_account — создание ресурса для учетной записи. В нашем примере она будет иметь название sa-debian и входить в системный каталог yandex_folder_id (переменная, которую мы создали ранее).
yandex_resourcemanager_folder_iam_member — создание роли. Она будет иметь доступ для редактирования хранилищ. В состав роли войдет созданная запись sa (с именем sa-debian).
yandex_iam_service_account_static_access_key — создаем ключ доступа для нашей сервисной учетной записи.
yandex_storage_bucket — создаем хранилище с названием tf-state-bucket-debian.

# Создадим еще один файл:

`nano outputs.tf`

<!-- output "access_key" {

value = yandex_iam_service_account_static_access_key.sa-static-key.access_key

sensitive = true

}

output "secret_key" {

value = yandex_iam_service_account_static_access_key.sa-static-key.secret_key

sensitive = true

} -->

это делается для получения значений аргументов access_key и secret_key и сохранения данных значений в файле состояния. Если access_key можно посмотреть в панели Яндекса, то secret_key мы увидеть не можем.

Строим план и применяем настройки:

`terraform plan`

`terraform apply`

Заходим в контроль панель Яндекса и видим, что у нас создалось хранилище. Его мы будем использовать для хранения файлов состояний terraform.

# Открываем наш файл main:

`nano main.tf`

Добавляем:

<!-- 
backend "s3" {

  endpoint   = "storage.yandexcloud.net"

  bucket     = "tf-state-bucket-debian-ka"

  region     = "ru-central1-a"

  key        = "terraform/infrastructure/terraform.tfstate"

  access_key = ""

  secret_key = ""

 

  skip_region_validation      = true

  skip_credentials_validation = true

  }

} 
-->

Если будет ругаться, то добавьте этот код в блок terraform


* в раздел terraform мы добавили инструкцию для хранения файла state. В нашем примере он будет размещен на бакете tf-state-bucket-debian-ka по пути terraform/infrastructure/terraform.tfstate.
** особое внимание обратите на access_key и secret_key. В разделе terraform мы не можем указать переменные, как это делали при создании хранилища. Сами значения можно найти в нашем локальном файле terraform.tfstate.

"secret_key": "YCNilkrAqw31T5ovWcSRLeo9JKXakY2pkF-L2AlK",
"access_key": "YCAJEUvzAAZNvDWwyseC5r6Du",

В итоге получим main.tf вида
<!-- 
terraform {

  required_version = "= 1.2.8"



  required_providers {

    yandex = {

      source = "yandex-cloud/yandex"

      version = "= 0.73"

    }

  }

backend "s3" {

  endpoint   = "storage.yandexcloud.net"

  bucket     = "tf-state-bucket-debian-ka"

  region     = "ru-central1-a"

  key        = "terraform/infrastructure/terraform.tfstate"

  access_key = "YCAJEUvzAAZNvDWwyseC5r6Du"

  secret_key = "YCNilkrAqw31T5ovWcSRLeo9JKXakY2pkF-L2AlK"



  skip_region_validation      = true

  skip_credentials_validation = true

  }

}

provider "yandex" {

  token = "y0_AgAAAAARB_HiAATuwQAAAAD47wPMS5I3f66RT6OSHewKkbrhw_EohHk"

  cloud_id = "b1g3cd8s33rf8m9v2ivn"

  folder_id = var.yandex_folder_id

  zone = "ru-central1-a"
} -->

# Используем команду:,

`terraform init`

Система снова инициализирует состояние текущего каталога и создаст файл state на удаленном хранилище. Чтобы убедиться, можно зайти в контроль панель, найти наше хранилище, а в нем — файл terraform/infrastructure/terraform.tfstate.



Не забудьте удалить все что создали, если не используете. 

Фух, кажется можно отдавать заказчику, давай проговорим все, что мы сделали:
Взяли образ debian 11, с помощью ansible установили в него nginx, docker, положили это все к нам в проект yc
Дальше ты немного больше разобрался, зачем же нужно описывать инфраструктуру именно как код. Ты описал один раз, а дальше сколько угодно раз просто повторяешь. Не нужно каждый раз делать руками.

Ты разобрался с terraform, научился хранить его state в удаленном хранилище (bucket), о котором не нужно беспокоиться, а не сломается ли в нем диск.

После этого ты описал виртуальную машину, сделал plan изменений, и применил их.