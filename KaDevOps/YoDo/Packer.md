# instal Paker

Packer позволяет создавать образы дисков виртуальных машин с заданными в конфигурационном файле параметрами. Сценарий описывает создание образа диска с помощью Packer.

`curl -fsSL https://apt.releases.hashicorp.com/gpg | sudo apt-key add - `
`sudo apt-add-repository "deb [arch=amd64] https://apt.releases.hashicorp.com $(lsb_release -cs) main"`
`sudo apt-get update && sudo apt-get install packer`
`sudo apt  install packer`

Начнем сначала, template (шаблон, сценарий) для packer пишется на json языке.

Там есть две основных для нас секции:

- builders

- provisioners 


<!-- Начнем с builders. -->

В этом блоке, мы объясняем packer'у, с помощью каких ресурсов мы будем собирать образ. Ведь для создания образа, ему нужно где-то запустить исходный, потом с ним что-то сделать, а потом запаковать обратно в образ версия 2, например. 
В нашем примере, мы используем мощности Yandex Cloud для этого. Packer создаст виртуальную машину (instance) и запустит ее из образа debian11.

<!-- Также есть provisioners. -->

Они нужны для того, чтобы сделать изменения на ИСХОДНОМ образе. И в итоге получить КОНЕЧНЫЙ образ (image).

Их тоже довольно много, мы будем использовать ansible. В него я погружаться не буду, об этом в другом задании.


## У тебя в папке, есть файл vars.json

В нем хранятся переменные, для твоего шаблона (template.json).

Это правило хорошего тона, написать один раз template (шаблон), а далее подставлять в него значения из переменных (vars.json)

Тебе нужно будет изменить несколько значений, а также создать сервисный аккаунт для Packer. 

Обязательные значения, которые нужно поменять product_images_project_id и cloud_subnet_id

<!-- 
Cоздаем каталог в котором будут создаваться наши виртуалки
Создаем сервисный аккаунт для управления виртуальными машинами
Создаем Yandex CLI для управления сервисом
https://cloud.yandex.ru/ru/docs/cli/quickstart 
-->

# После завершения регистрации на Яндексе. У нас появились файлы.
- template.json (сам сценарий)
- vars.json (переменные для него)
- key.json (файл, который ты сейчас скачал)
- main.yml (ansible задание для установки nginx, docker)
Обязательные значения, которые нужно поменять product_images_project_id и cloud_subnet_id в файле vars.json


# Дерево файлов

├── **packer-debian11**
│   ├── debian11-config.json (Устанавливаем debian)
│   ├── **http**
│   │   └── preseed.cfg
│   ├── **scripts**
│   │   └── docker_nginx.sh
│   ├── yandex
│   └── yandex.pub
├── **vagrant**
│   └── vagrant_2.2.9_x86_64.deb
└── **yandex**
    ├── account.json
    ├── key.json
    ├── main.yml
    ├── template.json
    └── vars.json


# Пробуем собрать образ
`packer plugins install github.com/hashicorp/yandex`
`packer validate -var-file=vars.json template.json`
`packer build -var-file=vars.json template.json`

Образ создан.
