# instal Paker
Как же работает Packer?
Packer позволяет создавать образы дисков виртуальных машин с заданными в конфигурационном файле параметрами. Сценарий описывает создание образа диска с помощью Packer.
Если объяснить по-простому, packer берет за основу образ, например с debian 11, далее с помощью "provisioner", в роли которого может выступать, например ansible, или shell, делает любые инструкции в образе.

Далее образ с выполненными инструкциями он запаковывает в новый образ, мы его назовём debian11-for-user1-v1

В данной задаче мы будем использовать  image в Google Cloud Platform или Yandex Cloud, тк он будет самым простым путем для разработчика. Ему нужно будет лишь создать сколько угодно машин каждый раз используя наш с вами образ!
`curl -fsSL https://apt.releases.hashicorp.com/gpg | sudo apt-key add - `
`sudo apt-add-repository "deb [arch=amd64] https://apt.releases.hashicorp.com $(lsb_release -cs) main"`
`sudo apt-get update && sudo apt-get install packer`
`sudo apt  install packer`

Начнем сначала, template (шаблон, сценарий) для packer пишется на json языке.

# Чтобы сразу сильно не загружать голову тонкостями мы сделаем следующее:

Packer установит debian11, прикатит туда пользователя, добавит репозиторий с docker, установит nginx и docker. Далее запакует это в ovf формат.

Тебе не придется каждый раз брать iso, устанавливать debian, нужный софт. Это сделает packer за тебя.

Для начала создай директорию(папку), например с названием packer-debian11.

Внутри нее папку scripts.

И положи в нее следующие файлы по следующим ссылкам:

Файл с названием - docker_nginx.sh

Его содержимое -

`docker_nginx.sh`

Его положи в папку scripts.



Файл с названием - debian11-config.json

Его содержимое -

`debian11-config.json`

Этот файл пусть лежит в корне нашей packer-debian11


Дальше в папке packer-debian11 создадим еще одну папку - http.

Внутрь положи файл с названием - preseed.cfg

Его содержимое -

`preseed.cfg`

# Давай начнем с файла preseed.cfg

Это специальный файл, который можно подсунуть установщику при, собственной, установке системы.

Когда происходит установка системы, консоль тебя спрашивает, какого пользователя создать, какой часовой пояс выбрать. Но как это автоматизировать?

Ты записываешь в preseed заранее подготовленные ответы, а дальше они просто подставляются.

Подробнее можно почитать тут -

https://www.debian.org/releases/bullseye/mips/apbs02.en.html


Там есть две основных для нас секции:

- builders

- provisioners 


<!-- Начнем с builders. -->

В этом блоке, мы объясняем packer'у, с помощью каких ресурсов мы будем собирать образ. Ведь для создания образа, ему нужно где-то запустить исходный, потом с ним что-то сделать, а потом запаковать обратно в образ версия 2, например. 
В нашем примере, мы используем мощности Yandex Cloud для этого. Packer создаст виртуальную машину (instance) и запустит ее из образа debian11.

<!-- Также есть provisioners. -->

Они нужны для того, чтобы сделать изменения на ИСХОДНОМ образе. И в итоге получить КОНЕЧНЫЙ образ (image).

Их тоже довольно много, мы будем использовать ansible. В него я погружаться не буду, об этом в другом задании.


Давай-ка посмотрим на нашу секцию provisioners:
<!-- 
"provisioners": [

{

"type": "shell",

"execute_command": "echo 'vagrant' | {{.Vars}} sudo -S -E bash '{{.Path}}'",

"script": "docker_nginx.sh"

}

] -->

Если вкратце, provisioner, установив debian11, запустит из-под sudo пользователя, передав пароль с помощью echo, наш скрипт:

- `docker_nginx.sh`

В него мы можешь записать абсолютно все что угодно, что мы можем сделать используя обычный bash.

Взглянем на наш файл:

Итак, под цифрой 1:

Это служебные команды, не обращай внимания.

Под цифрой 2:

Мы видим добавление репозитория с докером, установку зависимостей для него, и его установку, команды для этого я взял отсюда -

https://docs.docker.com/install/linux/docker-ce/debian/#install-docker-engine---community


Введи

packer validate debian11-config.json


Ты должен увидеть:

`Template validated successfully.`

Ругается на iso_checksum_type
Ничего страшного. Паниковать не надо.

Программа packer активно дописывается и в твоей версии возможно поменяли формат конфига

Но специально, чтобы преодолевать подобные проблемы в самом packer-е есть встроенная функция для исправления конфигурационного файла.

Выполни:

`packer fix debian11-config.json > debian11-config.json.new`


`mv debian11-config.json.new debian11-config.json`

Параметр fix конвертирует конфиг из старого в актуальный формат.



Все. Можно продолжать.

Теперь запускай сборку:

`packer build -force -var 'version=1.2.0' debian11-config.json`

Сначала packer скачает исходый образ, ты можешь наблюдать прогресс в терминале.

После загрузки он начнет установку, используя preseed.cfg, ты можешь понаблюдать за этим!

Нажимай далее, когда будет что-то вроде:

Executing: export packer-debian-11-amd64 --output output-virtualbox-iso/packer-debian-11-amd64.ovf

Он создал папку output-virtualbox-iso

В нее он положил наш с вами готовый образ. Давай его запустим, и проверим.

Открывай virtualbox, и нажимай в нем import. Выбирай образ с названием packer-debian-11-amd64.ovf


Если что-то поменялось, меняешь скрипт установки, собираешь, и отдаешь новый образ

# ИТОГ: С помощью paker создался образ и установились в него пакеты.






























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