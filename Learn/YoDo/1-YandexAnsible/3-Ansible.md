# Ansible
Ansible. Эта система контроля версий имеет свои отличительные черты, из-за которых она и выбрана в качестве темы сегодняшнего урока.
Формат файлов конфигурации YAML. 

https://docs.ansible.com/ansible/latest/installation_guide/installation_distros.html#installing-ansible-on-ubuntu

`sudo apt-get install ansible sshpass`

Даже для первого теста потребуется создать пару файлов. Поэтому предлагаю создать сразу отдельную папку, в которой будем проводить все эксперименты.

`cd ~`

`mkdir ansible`

`cd ansible`

Первый файл, который тебе придется создать это inventory.

В этом файле перечисляются узлы, которые ansible знает. Эти узлы можно группировать, чтобы применять команды сразу к группе узлов.

Вообще ansible с удовольствием использует файл hosts для поиска ip-адреса узла, к которому хочет подключиться. И настройки ssh-клиента пользователя под которым запущена.
Но я считаю что основные настройки лучше держать в конфигурационных файлах ansible. В этом случае, если перенести их на другую машину, то будет гарантия, что все будет работать также.
Учетные данные хоста записываются в виде переменных. Файлов переменных может быть несколько. Как минимум это переменные для групп хостов и персональные переменные для каждого хоста.
Они располагаются в разных папках:  host_vars и group_vars соответственно.
Персональные переменные имеют приоритет перед групповыми.

Нам с тобой сейчас надо создать папку для персональных переменных и файл для vm1 где описать учетные данные.

`mkdir host_vars`

`touch host_vars/vm1`

Теперь надо открыть host_vars/vm1 в любом текстовом редакторе и написать по аналогии с тем, что у меня:

`ansible_ssh_host: 172.22.3.191`

`ansible_ssh_user: elve`

`ansible_ssh_pass: 123654`

`ansible_become_pass: 123654`

Ты можешь посмотреть что же там собирается. Для этого можно воспользоваться модулем setup.

Выполняй:

`ansible -i inventory -m setup vm1`

Информация собранная с удаленного хоста представляется для команд и сценариев в виде внутренних переменных. Все это можно использовать.

Синтаксис такой:

`ansible -i  -m   -a host/group_host`
inventory - это наш файл со списком известных хостов. Помнишь мы его создавали в начале?
module - как понятно из названия это модуль. Каждый модуль выполняет атомарную операцию. С некоторыми я тебя познакомлю в ходе урока.

А полный список можно узнать тут - https://docs.ansible.com/ansible/2.9/modules/list_of_all_modules.html

или командой 

`ansible-doc -l`

option - это параметры, которые можно передать модулю. До сих пор мы использовали модули без параметров. Но я исправлюсь и покажу тебе модуль с параметрами =).
Хост или группа хостов берется из файла inventory. Либо хост, либо группа. Перечисление не прокатит.

А теперь я познакомлю тебя с тремя модулями, где используются опции.
Все 3 модуля позволяют выполнять  команду на удаленном хосте, но каждый со своими нюансами.
raw - это модуль позволяет выполнять команды, даже если на удаленном хосте не установлен python. Но в то же время команда передается как есть - без проверок на корректность и т.д.
command - тут команда выполняется напрямую мимо установленного для пользователя shell. Есть у этого модуля особенность в связи с этим - раз нет shell, то и перенаправление потока вывода не работает и другие фишечки.
shell - тут команда выполняется с помощью shell прописанного в /etc/passwd для пользователя под которым заходит ansible. Обычно /bin/sh или /bin/bash.
По сути, если они используются с утилитой ansible то эти модули ничем не отличаются от ssh -e. Тоже выполнение команды.
Мы их с тобой сейчас испробуем, но основная польза от этих модулей будет в сценариях (плейбуках).

# Попробуй выполнить следующие команды:

<!-- 
ansible -i inventory -m raw -a 'ls /'  vm1

ansible -i inventory -m raw -a 'ls / | grep lib*'  vm1

ansible -i inventory -m command -a 'ls / | grep lib*'  vm1

ansible -i inventory -m shell -a 'ls / | grep lib*'  vm1 -->
Вот так вот работают одиночные команды. Иногда, когда доступа по ключам нет, а все учетные данные в ansible прописаны, то так удобнее, чем через ssh.

В остальных же случаях пишут т.н. playbook - сценарий с большим количеством команд и переменных.

Запускается плейбук с помощью утилиты ansible-playbook

Синтаксис похож на предыдущую утилиту.

`ansible -i inventory  playbook.yml`

Вместо группы хостов как цель мы указываем название плейбука. Список хостов находится в самом плейбуке.

# Давай рассмотрим подробнее его состав и структуру.

Схематично она выглядит так:

- список хостов и групп, к которым применяется плейбук

- набор переменных (опционально)

- набор действий, которые надо обработать в любом случае.

- набор действий, которые надо призвести по сигналу (опционально)

Три тирэ в начале файла это требования формата YAML. Хотя если его пропустить, то плейбук все равно выполнится.

Отступы у разных блоков это не для красоты. Это тоже требование формата. Вот тут отступления недопустимы, т.к. будут вызывать ошибку.

Часть записей начинаются с тире, а часть нет.

Почему так?

С тире обычно начинаются пункты перечисления. А пункты, которые беэ тире обозначают функциональные части плейбука:  список переменных, список обязательных действий, список действий по сигналу.

Если какой-то блок подразумевает перечисление (даже если ты напишешь только один пункт, то его имя заканчивается двоеточием).

Почему тогда блок hosts начинается с тирэ? Потому что в одном плейбуке можно обозначить несколько блоков hosts и в каждом будут свои разделы vars, tasks, handlers.

Если тебе покажется удобным делать в одном плейбуке несколько блоков hosts, то никто не запретит тебе это сделать.

# Выполним простую задачку:
<!-- 
- Установка ntp-клиента

- Прописывание правила в cron для корректировки времени раз в час. -->


Задачка может и простая, но чтобы пообвыкнуться с синтаксисом самое оно. Мало действий и мало шансов запутаться.

Для выполнения задачи мы будем использовать модули  cron и apt.


Можешь почитать о них в документации:

https://docs.ansible.com/ansible/latest/modules/apt_module.html

https://docs.ansible.com/ansible/latest/modules/cron_module.html

#

Я надеюсь ты еще в папкe которую мы создали? =). Если нет, то делай 

`cd ~/ansible`

и дальше создаешь файл ntp_vm1.yml и открываешь его в удобном текстовом редакторе.
Для начала можешь написать вот такое содержимое:
<!-- 
---

- hosts: vm1 
  -->

Этот плейбук не несет никакой смысловой нагрузки, но уже может быть выполнен

Запускают плейбук вот такой командой:

`ansible-playbook -i inventory ntp_vm1.yml`

Как видишь мы опять указываем файл inventory, т.к. в нем содержится список подконтрольных серверов.
Сначала собираются факты, а дальше, если есть что делать - выполняются задания из блока tasks.

Первое что надо сделать это установить ntp-client. Делать это будем с помощью модуля apt.

Открывай наш плейбук (ntp_vm1.yml) и в конце дописывай следующее:

<!-- 
---
- hosts: vm1
  tasks:

  - name: "Install NTP-client"
    apt:
      pkg: "ntpdate"
    become: yes

    -->

Не забывай про отступы. Блок tasks  должен начинаться на уровне слова hosts - т.е. отступ два пробела.

Далее под блоком tasks перечисление должно начинаться с тем же отступом в два пробела.

Имя используемого модуля пишется также с отступом еще в два пробела (итого 4).

# Опция -  become. Это довольно важная опция. Она определяет - надо поднимать привилегии через sudo для выполнения задачи или нет.

Для любого действия из блока tasks можно дописывать в конце опции, условия и перечисления. Позже я тебя с ними познакомлю.

Все эти дополнительные штуки пишутся на одном уровне с названием используемого модуля. 

Действие желтое если:
1. Если действие выполняется на хосте впервые (ansible сохраняет лог действий в скрытом файле в личной папке пользователя под которым заходит)

2. Если действие что-то изменило. Ansible проверяет файлы до и после выполнения действия. Если ничего не поменялось, то цвет зеленый. Если поменялось, то желтый.


# Циклы
Хочу познакомить тебя с выполнением команды в цикле.  Это поможет ставить программы пачками, а не по одной. 

Поэтому в дополнение к ntpdate мы поставим еще и пакет tzdata (данные по часовым поясам для linux).

Цикл делается с помощью параметра with_item, а имя пакета будет обозначаться служебным словом "{{ item }}"

К слову сказать. Все служебные переменные в плейбуке обрамляются в одинарные или двойные фигурные скобки (в зависимости от контекста).

Тебе нужно привести блок "Install NTP-client" к следующему виду:
<!-- 
  - name: "Install NTP-client"

    apt:

       pkg: "{{ item }}"

    become: yes

    with_items:

    - "ntpdate"

    - "tzdata" -->
Хе-хе... увидел предупреждение розовым цветом? Не очень удачный я выбрал пример для знакомства. Циклы скоро выпилят из модуля apt, хотя пока все работает.
А я исправлю плейбуке так, как просят в сообщении =). Там тоже в своем роде цикл получается.

<!-- 
---
- hosts: vm1
  tasks:

  - name: "Install NTP-client"
    apt:
      pkg: ['ntpdate', 'tzdata']
    become: yes -->
#
Займемся модулем cron. Это конечно не так интересно, но надо же выполнить поставленную задачу.



Сможешь справиться сам? Думаю что да, но я все равно опишу конфиг

Запускать надо команду 

`/usr/sbin/ntpdate pool.ntp.org`

Для изменения системного времени потребуются права суперпользователя (т.е. sudo)

Вот конфиг добавления записи в crontab:


<!-- 
  - name: "Add NTP-client to crontab"

    cron:

       name: "NTP-Client"

       hour: "*/1"

       job: "/usr/sbin/ntpdate pool.ntp.org"

       user: root

    become: yes
    -->

    Выполни плейбук, чтобы изменения применились.

Как думаешь, куда и как ansible записал задание в cron?

Не буду томить ожиданием. Он записал задачу в персональный crontab пользователя, с чьими привилегиями выполнялась задача.
Чтобы проверить можешь зайти на vm1 по ssh и выполнить вот эту команду:

`sudo cat /var/spool/cron/crontabs/root`

Все доступные параметры модуля описаны в документации. 
И теперь ты можешь не только ПО ставить, но еще и задания в планировщик писать =). Еще небольшой плюс к автоматизации и удобству.
Как минимум тебе не надо помнить все задачи на всех серверах, если они у тебя структурированы в  одном месте - в плейбуке.

#
До сих пор ты использовал безусловные действия. Они просто выполняются и все.

Пора познакомиться с условиями и с еще несколькими модулями: **file, stat, debug.**

Добавим в наш плейбук еще и выставление таймзоны Europe/Moscow.

Выставляется таймзона с помощью установки симлинка на один из файлов в /usr/share/zoneinfo

Однако часто после установки вместо симлинка там оказывается копия такого файла. Поэтому надо проверить файл это или симлинк и уже потом решать удалять ли файл, либо перезаписывать симлинк.

Модуль debug просто для демонстрации и ознакомления. Как понятно из названия - он применяется при отладке плейбуков. В итоговом варианте плейбука его не будет.

#
Модуль stat  предназначен для того, чтобы передавать состояние файлов. На вход ему дается имя файла по полному пути. На выходе он дает переменную со сложной структурой.

Со структурой можно ознакомиться в документации:

https://docs.ansible.com/ansible/2.3/stat_module.html

Модуль Debug отображает  при выполнении плейбука данные, которые ему приказали отобразить.

И ты познакомишься с параметром задачи - when. Этот параметр содержит условие, по которому действие будет выполнено.

Итак. Приступим. Для начала небольшая демонстрация с помощью Debug. А в итоговом варианте мы ее удалим.

Допиши в плейбук следующий текст:

<!-- 
  - stat:

       path: /etc/localtime

    register: localtime

  - debug:

       msg: "islnk isn't defined (path doesn't exist)"

    when: localtime.stat.islnk is not defined

  - debug:

       msg: "islnk is defined (path must exist)"

    when: localtime.stat.islnk is defined



  - debug:

       msg: "Path exists and is a symlink"

    when: localtime.stat.islnk is defined and localtime.stat.islnk



  - debug:

      msg: "Path exists and isn't a symlink"

    when: localtime.stat.islnk is defined and localtime.stat.islnk == False
     -->
Обрати внимание. Две из четырех задач debug подсвечены голубым. Это означает что условие, которое в них прописано выдало результатом "ложь". А раз по условию задача не подошла, то она не требует выполнения и пропускается.

На самом деле это просто демонстрация. Нас интересует только одно условие:

`localtime.stat.islnk is defined and localtime.stat.islnk == False`

По срабатыванию этого условия мы будем удалять файл /etc/localtime.

Если же файл будет симлинком, то будем его перезаписывать.

Если же файла не будет вовсе, то создадим симлинк.

Создавать файлы (разные. т.е. обычный файл, симлинк, директорию и т.д.), удалять файлы, менять права на файлы... все это может модуль file.

По задаче требуется удалять файл, если он не симлинк.

Удаляем все debug-и, а вместо них пишем вот такую конструкцию:
<!-- 
  - name: If /etc/localtime not symlink - delete this

    file:

       path: /etc/localtime

       state: absent

    when: localtime.stat.islnk is defined and localtime.stat.islnk == False

    become: yes -->


Но это мы только удаляем файл. Теперь надо создать правильный симлинк.

Симлинк создается вот так:


<!-- 
  - name: Set Timezone to Europe/Moscow

    file:

       dest: "/etc/localtime"

       src: "/usr/share/zoneinfo/Europe/Moscow"

       state: link

    become: yes -->

Тот же самый модуль file, но уже с состоянием link создает симлинк. А ведь только на прошлом шаге с помощью состояния absent он удалил файл =). Вот такой вот модуль многостаночник. По-секрету - он еще и директории может создавать ;).

Для эксперимента можешь на vm1 удалить файл /etc/localtime и скопировать на его  есто что угодно. Я например сделал так:

`sudo rm /etc/localtime`

`sudo cp /etc/crontab /etc/localtime`

а потом запути плейбук два раза подряд:

`ansible-playbook -i inventory ntp_vm1.yml `

Первый раз:
Второй раз:
Обратил внимание, что во второй раз все было зеленое? Т.е. система в нужном состоянии и не требует изменений.

# Пришло время освоить еще несколько модулей и фишечек =).

В главной роли будет выступать модуль template. Этот модуль позволяет генерировать файлы по шаблону. В нашем случае это будут конфигурационные файлы.

Модуль template дает просто колоссальные возможности для творчества. Обязательно нужно научиться им пользоваться.

#
Будем устанавливать и настраивать nginx.

Последовательность действий такова:

1. Ставим nginx

2. По списку создаем папки для сайтов

3. По списку создаем конфигурационные файлы для сайтов

4. Если по п.3 что-то менялось, то перезапускам nginx


Первое что надо сделать это шаблон конфигурации сайта.

Создавай файл website.j2 и открывай в удобном текстовом редакторе.

В нашем примере этот файл должен лежать в той же папке где и плейбук.

Текст будет такой:
<!-- 
server {

  

    server_name {{ item }};

    access_log /var/log/nginx/{{ item }}_access.log;

    error_log /var/log/nginx/{{ item }}_error.log;

    root /var/www/{{ item }};



    location / {

                index  index.html index.htm;



    }



} -->

Обрати внимание, что в ответственных местах я использую системную переменную {{ item }}. Во время выполнения плейбука модуль template как бы пробрасывает переменные внутрь себя. Таким образом их можно использовать при генерации любых файлов по шаблону.

У нас шаблон простой. Но в шаблоне возможно использовать и циклы и условные операторы.
#
А теперь плейбук. По сравнению с предыдущей задачей он выглядит достаточно лаконично.
<!-- 
---

  

- hosts:

  - vm1

  vars:

  - siteslist:

    - "www.test1.com"

    - "www.test2.com"

    - "www.test3.com"

  tasks:

  - name: Install nginx

    apt:

       pkg: nginx

    become: yes

  - name: Create dir for websites

    file:

       path: /var/www/{{ item }}

       state: directory

    become: yes

    with_items: "{{ siteslist }}"

  - name: Create nginx config files

    template:

       src: "website.j2"

       dest: "/etc/nginx/sites-enabled/{{ item }}.conf"

    with_items: "{{ siteslist }}"

    become: yes

    notify: nginxrestart

  handlers:

  - name: nginxrestart

    service:

       name: "nginx"

       state: "restarted"

    become: yes -->
Что нового мы тут видим?

Раздел vars.  
Тут можно задавать различные переменные. Я задал одномерный массив, но конструкция вида:
username: "albatros"
тоже вполне рабочая.

Раздел handlers.
Тут располагаются одиночные действия (вроде перезапуска сервиса), которые выполняются по сигналу. Они выполняются после всего плейбука.
У действия задано имя. Оно важно. Чтобы отдать сигнал на выполнение задания, надо упомянуть его имя в параметре notify.
Параметр notify также как и become относится к отдельному действию из раздела tasks. Команда на выполнение отдается только в том случае, если во время выполнения действия произошли какие-то изменения.

При запуске шаблона установится NGINX и заведутся сайты в него.

<!-- Если честно, изучив вот такой набор, я надолго остановился на плейбуках, расширяя свой кругозор по части модулей. -->

Представь что ты всю свою сеть описал с помощью плейбуков. И в них большущее количество одинакового кода.
И этот код можно собрать во что-то похожее на подпрограммы и вызывать их из плейбука. Эти подпрограммы называются Role - т.е. роли.

К примеру роль веб-сервер подразумевает установку nginx, php-fpm. Или например роль  почтовый сервер предполагает установку postfix, dovecot и остального сопутствующего ПО.

В плейбуке роль добавляется через отдельный блок role. Она применяется ко всем хостам в блоке hosts

Если мы с тобой поместили бы установку ntpdate в роль, то плейбук стал бы выглядеть вот так:


<!-- 
---

- hosts:

  - vm1

  role:

  - ntp-client -->

Если в плейбуке указана роль, то когда ты запускаешь его на выполнение, то ansible-playbook сначала ищет папку roles в той же папке где ты находишься (она может не совпадать с той где плейбук. будь внимательнее). Если не находит, то смотрит в свой конфигурационный файл /etc/ansible/ansible.conf. Если там не указан путь к ролям, то ты получишь ошибку при исполнении плейбука.

Итак. Я упомнял папку roles. Создай ее рядом с нашими плейбуками. В ней будут храниться роли.

Название папок внутри папки roles это название ролей. Имя роли больше нигде не указывается. Если переименовать папку, то переименуется вся роль целиком.

Изнутри роль также имеет структуру из папок. При создании роли тебе надо будет эту структуру внутри создавать.

Итак:
<!-- 
role

- defaults

- files

- handlers

- tasks

- templates -->

Как видишь там есть знакомые уже нам названия блоков: tasks, handlers

В отдельную папку выделены templates. Тут следует складывать шаблоны. Отсюда их будет брать модуль template

Папка files содержит файлы, которые надо просто скопировать на настраиваемый сервер. К примеру не все конфиги надо генерировать. Я вот  свой .vimrc однажды написал и теперь просто раскидываю по серверам.

Папка defaults. Тут содержатся значения переменных по умолчанию. В плейбуке мы с тобой уже использовали переменные (блок vars).   В ролях они тоже используются. И с их помощью можно управлять тем, какой результат выдаст роль. Однако если не передать переменные в роль, то ее выполнение завершится с ошибкой. Поэтому нужны значения по-умочанию.
#
В папках defaults, handlers и tasks ansible ищет файл с названием main.yml. С него начинается выполнение этих блоков.

В блоке tasks все же можно хранить файлы с другим названием. И подключать их через команду include по условию.

Как это могло бы выглядеть схематично в древовидном листинге:

<!-- 
role

- defaults

   - main.yml

- files

   - file1

   - file2

- handlers

    - main.yml

- tasks

    - main.yml

    - pl1.yml

    - pl2.yml

- tempaltes

    - template1.j2

    - template2.j2

    - template3.j2
     -->

# Преобразования playbook по установке nginx  в роль

Предлагаю плейбук по установке и настройке nginx преобразовать в роль.

Роль будет называться **nginx-test**

Создадим под нее нужный набор папок:


`mkdir roles`

`mkdir roles/nginx-test`

`mkdir roles/nginx-test/{defaults,files,handlers,tasks,templates}`

Чтобы не забыть - первой перенесем переменную **siteslist**

Если она не будет указана, то будет добавлен сайт www.example.com

Для этого нужно создать файл roles/nginx-test/defaults/main.yml  следующего содержания:

<!-- 
---

siteslist:

- "www.example.com" -->

Если честно, то можно было бы с помощью when ничего не делать, если переменной не существует. Однако тогда мы бы не охватили блок defaults в этом уроке.

Теперь копируем наш шаблон в папку с шаблонами:

`cp website.j2 roles/nginx-test/templates/`

Следующим шагом создаем файл roles/nginx-test/handlers/main.yml следующего содержания:
`nano roles/nginx-test/handlers/main.yml`


<!-- 
---

 - name: nginxrestart

   service:

      name: "nginx"

      state: "restarted"

   become: yes -->


И вот когда все вспомогательные блоки готовы, можно переходить к tasks

Название файла, как ты уже понял будет вот таким - roles/nginx-test/tasks/main.yml

А содержимое это все что было в блоке tasks в плейбуке:


<!-- 
---

  

  - name: Install nginx

    apt:

       pkg: nginx

    become: yes

  - name: Create dir for websites

    file:

       path: /var/www/{{ item }}

       state: directory

    become: yes

    with_items: "{{ siteslist }}"

  - name: Create nginx config files

    template:

       src: "website.j2"

       dest: "/etc/nginx/sites-enabled/{{ item }}.conf"

    with_items: "{{ siteslist }}"

    become: yes

    notify: nginxrestart
     -->

А теперь давай создадим новый плейбук с использованием нашей свежей роли - testroles_vm1.yml  

Содержимое у него будет таким:


<!-- 
---

- hosts:

  - vm1

  roles:

  - nginx-test -->

И запустим

`ansible-playbook -i inventory testroles_vm1.yml`

Желтые строки в отчете. Подозрительно...
Ах да! Это же наша переменная по умолчанию. Мы же забыли наш список сайтов указать =).

В зависимости от наших целей мы можем дописать переменную либо в плейбук - и тогда все серверы будут иметь идентичную конфигурацию. Либо можно прописать переменную в персональный файл виртуалки host_vars/vm1  и т.д. и наборы сайтов на виртуалках будут персональными.

Открывай host_vars/vm1 и добавляй туда следующий текст:

siteslist:

- "www.test1.com"

- "www.test2.com"

- "www.test3.com"

А теперь запусти плейбук =).

#

И вот теперь мы с тобой вплотную подошли к использованию наших 4-х vm. Ты их создал уже?
Если нет, я подожду =).

Напоминаю:

vm1 - ubuntu 22.04
vm2 - debian 11
vm3 - centos 7
vm4 - centos 8

На каждой из них нужно настроить доступ по ssh. Доступ надо либо под пользователем root, либо под обычным, но тогда надо еще поставить sudo и включить его в нужную группу.

Узнаешь список сайтов?