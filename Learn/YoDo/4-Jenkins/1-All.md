Перед любым разработчиком ПО рано или поздно встает вопрос автоматизации тестирования, сборки и выкладывания проекта.
Эти системы позволяют быстро и удобно организовать автоматическое тестирование и деплой разрабатываемого приложения. При этом каждый этап контролируется машиной, а также визуализируется для человека.

Перед началом обновите систему!

Для работы всех этих программ требуется установить интерпретатор Java 11-й версии. В Ubuntu с этим все достаточно просто.
`sudo apt install openjdk-11-jre`

Проверим, что установка прошла успешно:
`java -version`

После установки интерпретатора надо провести еще ряд манипуляций, чтобы все работало корректно (почему это не делается
инсталляционным пакетом не представляю). Цель манипуляций - установка в системе переменной среды JAVA_HOME.

А что же в нее писать? Сейчас узнаем с помощью следующей команды:
`sudo update-alternatives --config java`
В списке может быть и несколько пунктов, если ты установил несколько версий java, то и пунктов в списке будет несколько.
Нужно выбрать тот, что соответствует 11-й версии и скопировать путь в скобках.

Теперь открывай текстовым редактором файл /etc/environment и давляй туда следующую строку (не забывай про sudo ;) ):

`JAVA_HOME="тут_надо_вставить_путь_из_предыдущего_шага"`

Сохраняй файл. А теперь выполни вот эту команду:
`source /etc/environment`
Она применит внесенные нами изменения в текущем сеансе.
# Установка Jenkins
Инструкция:
https://www.jenkins.io/doc/book/installing/linux/
и тут
https://www.digitalocean.com/community/tutorials/how-to-install-jenkins-on-ubuntu-22-04


В официальном репозитории его нет. Зато разработчики поддерживают свой собственный репозиторий, который мы сейчас подключим.
<!-- 
wget -q -O - https://pkg.jenkins.io/debian-stable/jenkins.io.key |sudo gpg --dearmor -o /usr/share/keyrings/jenkins.gpg
sudo sh -c 'echo deb [signed-by=/usr/share/keyrings/jenkins.gpg] http://pkg.jenkins.io/debian-stable binary/ > /etc/apt/sources.list.d/jenkins.list'
sudo apt update
sudo apt install jenkins -->

Так что сразу его запускаем и прописываем в автозагрузку:
`sudo systemctl enable jenkins`
`sudo systemctl start jenkins`

Запущенный Jenikns будет слушать порт 8080 tcp.
Ну и проверка будет выглядеть вот так:
`sudo netstat -lpn | grep 8080`

По умолчанию в ubuntu 22.04 файрвол не настроен. Но если вдруг у тебя он настроен, то предлагаю сразу открыть порт 8080 хотя бы для себя, чтобы подключаться к веб-интерфейсу Jenkins.

Но перед тем как перейти в веб-интерфейс, тебе нужно узнать одну строку. Она зовется Admin Initial Password и ее надо будет ввести, чтобы в первый раз войти в веб-интерфейс.
`sudo cat /var/lib/jenkins/secrets/initialAdminPassword`

Пришло время подключиться к веб-интерфейсу.

Открывай в браузере адрес: http://ip_твоего_сервера:8080

Все. Jenkins установлен и можно переходить к самой сути урока.
#####################
##### LESSON ########
#####################
<!-- Допустим у тебя есть сайт, который ты разрабатываешь. Ты хранишь файлы в git-репозитории и хочешь, чтобы после коммита происходило тестирование свежих данных и автодеплой на веб-сервер. -->

1. Git-репозиторий, доступный из нашей учебно-виртуальной среды.
2. Отдельный сервер или виртуалка с nginx+php-fpm на борту. Это мелочь. На ней я останавливаться не буду. Тебе придется самостоятельно организовать такой сервер. Не забудьте создать на сервере пользователя jenkins .
3. Jenkins - он у нас есть и ждет настройки.

Итак. У тебя есть репозиторий.

Надо будет в него выложить простенький проект на php. Можно из одного файла. К примеру такой, который я сейчас тебе продемонстрирую.

<!-- 
<?php
  
$a = 3;
$b = 4;

$c = $a + $b;

echo "a + b =".$c."\n";

?> -->
Алгоритм будет такой:

1. Ты загружаешь файл в репозиторий.
2. Jenkins по расписанию раз в 5 минут проверяет репозиторий. Если там появились изменения, то:
2.1 Выполняется проверка файла с помощью php -l
2.2 Если синтаксис в порядке, то файл заливается на веб-сервер.

Но для начала создадим ssh ключ. Он потребуются для работы с git.

https://docs.github.com/en/authentication/connecting-to-github-with-ssh/generating-a-new-ssh-key-and-adding-it-to-the-ssh-agent
и создадим токен.

https://docs.github.com/en/enterprise-server@3.4/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token

Но для начала создадим ssh ключ. Он потребуются для работы с git.
https://docs.github.com/en/authentication/connecting-to-github-with-ssh/generating-a-new-ssh-key-and-adding-it-to-the-ssh-agent

и создадим токен.
https://docs.github.com/en/enterprise-server@3.4/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token

Потом на Github, зайди в Settings, раздел SSH ключи, как на скрине. И добавь туда содержимое своего публичного ключа.

Итак. Начнем с файла и репозитория.

# Создавай папку для проекта, заходи в нее, создавай файл, инициализируй проект и загружай его в git-репозиторий.

Теперь у тебя есть проект и репозиторий. Можно настраивать разворачивание проекта через Jenkins.

Выполнять работы мы будем с помощью bash-скриптов.
Т.к. у Jenkins нет агентов, то подключение к веб-серверу будет происходить по ssh.
А т.к. требуется автоматическое выполнение всех команд, то нужно настроить безпарольный вход без ключа.

При установке Jenkins в системе был создан одноименный пользователь jenkins. От имени этого пользователя и будут работать скрипты (его можно и в sudoers добавлять, если надо).

Предлагаю зайти на сервер и принудительно залогиниться под jenkins
`sudo su`
`su jenkins`
Итак. Ты пользователь jenkins в его домашней папке (/var/lib/jenkins)

Теперь надо скопировать наши ssh-ключи, которые мы делали для git в /var/lib/jenkins/.ssh
командой pwd можно поверить в какой директории находитесь
`pwd`
Создаем директорию:
`mkdir .ssh`
Теперь, чтобы нам перенести два наших ключа мы их копируем командой:
`cp /home/YOUR_USER/.ssh/id_ed25519* /var/lib/jenkins/.ssh/`
если не удается скопировать , тогда перейдите в пользователя (или вернитесь) в root пользователя и повторите попытку.

В результате у тебя появится два файла:
<!-- 
id_ed25519;
id_ed25519.pub; -->

id_ed25519 ты никуда не девай. Он должен быть только тут.
id_ed25519.pub нужно раскладывать по серверам, куда у Jenkins должен быть доступ.
эти файлы должны иметь права пользователя и группу jenkins


В принципе ты можешь подключаться из скриптов Jenkins любым пользователем. Но для красоты, создай на веб-сервере пользователя jenkins с зубодробительным (или вообще отключенным) паролем и NOPASSWD в sudoers.
Вот так:
`jenkins ALL=(ALL) NOPASSWD:ALL`

От имени этого пользователя jenkins на веб-сервере надо будет создать папку .ssh и закинуть туда ключ:
<!-- 
mkdir ~/.ssh
chmod 0700 ~/.ssh -->

Публичный ключ это текстовый файл. Поэтому ты можешь его скопировать по scp, а можешь с помощью текстового редактора создать новый файл и скопировать текст.
`scp root@172.22.3.203:/var/lib/jenkins/.ssh/id_ed25519.pub ~/.ssh/authorized_keys`
chmod 0600 ~/.ssh/authorized_keys

или

Отрываем на сервере authorized_keys и копируем туда содержимое id_ed25519.pub
Обязательно убедись, что авторизация проходит. Чтобы дальше не отвлекаться на ненужные проблемы.

И вот пришло время вернуться в веб-интерейс Jenkins.

# Жми кнопку "Создать Item"

Создаем имя ( name ) и выбираем Создать задачу со свободной конфигурацией ( Freestyle project )
Появится форма создания задачи для дженкинса.

Тут можно просто прокручивать страницу вниз, а можно жать кнопки справа и перемещаться по кускам конфига.
На этапе подключения репозитория надо будет указать учетные данные, с которыми ты в него входишь.

Жмешь Add, выбираешь пункт Jenkins (это внутренняя база jenkins для учетных данных) и заполняешь по аналогии со скриншотом ниже:
ключ вписываем от id_ed25519
Теперь можно выбрать эти учетные данные из выпадающего списка
Теперь определим условия, по которым проект будет автособираться.

Там был еще пункт "Запускать периодически". Но по нему проект будет без вопросов собираться каждый раз.
А SCM будет по расписанию опрашивать репозиторий. Но собирать проект только если есть изменения.

И вот мы подошли к самому главному - сборке проекта.

Для начала надо выбрать метод сборки:

И вот в это окно можно уже писать bash-скрипт.

Вот мой скрипт:
<!-- 
. /etc/profile
WEBSERV="159.69.149.247"
/usr/bin/php -l index.php
scp index.php $WEBSERV:/tmp
ssh $WEBSERV "sudo cp /tmp/index.php /var/www/test/index.php" -->


Сохраняешь проект.

У нас есть еще предупреждение, которое светилось красным. Сейчас исправим.. Идем в консоль
`git ls-remote -h -- git@github.com:Crypto-Secure/onefirst.git HEAD`

и введем еще одну команду
`ssh jenkins@159.69.149.247`
Это мы подключились к нашему серверу с nginx+php-fpm.

это для того, чтоб прошла аутентификация в файле know hosts.
Он перейдет в консоль сервера jenkins. (смотрите скрин)
Наберите exit, чтоб вернутся в нашу консоль

Там есть кнопочка "Собрать проект". Жми ее, чтобы собрать проект принудительно - тут ты решаешь, а не SCM.
Как ты заметил на предыдущем шаге - значок сборки красного цвета. Это означает одно - сборка завершилась с ошибкой.

Чтобы узнать по какой причине сборка не завершилась - жми прямо на этот красный кружок.
И теперь жми "Вывод консоли".

Возможно когда-то тебе потребуется весь вывод. Но сейчас листай в самый низ, чтобы найти сообщение об ошибке, на которой падает сборка.

Хм... а и правда. Jenkins то мы поставили, а вот php на сервере нет. Ошибка вполне логична.

Доустанови пакет php на сервер с Jenkins и попробуй собрать проект снова.

Ну вот. Совсем другое дело. Зеленый кружок означает, что сборка прошла успешно.
В твое отстуствие всем этим будет заведовать SCM =).
Кстати, там же в настройках проекта, можно настроить отсылку уведомлений о неудачных сборках - это для того, чтобы быть в курсе без захода в панель Jenkins.
А теперь давай поменяем значения переменных в index.php, загрузим его в репозиторий и отвлечемся на чашку чая Итак. Пришло время обновить страницу. И циферка изменилась - т.е. jenkins увидел изменения, протестил файл на орфографические ошибки и залил на веб-сервер.