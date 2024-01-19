# Работа с Jenkins
# Основные настройки
В настройкахЖ
Количество одновременных сборщиков
Настройка переменных сред
# Обновления Jenkins

Сперва скачиваем через веб интерфейс оновление
заходим в 
cd /usr/share/jenkins/
<!-- Переименовываем текущую версию
скачиваем новый jenkins файл -->
sudo wget http://updates.********/jenkins.war
sudo service jenkins restart

<!-- Соответственно, чтобы понизить версию, то скачать нужно старый файл, переименоватьь и рестарт службы. -->

# Работа с плагинами
После установки плагина лучше перезагрузить сервер jenkins
Плагины устанавливаются с зависимостями
Во вкладке Advanced
Указываем к дистрибуту путь и ставим нужную версию
https://updates.jenkins.io/download/plugins/

<!-- Все плагины сохраняются по пути -->
cd /var/lib/jenkins/plugins

# Пейпланы
Job - Сборка - Test - Deploy - Testoviy server
Job - Сборка - Test - Deploy - Prod server

# Где хранятся jobs
cd /var/lib/jenkins/jobs/

<!-- Чтобы не хранить мусор указываем в настройках job сколько мы хотим хранить версий. Например 5. -->
# Делаем билд на публикацию страницы
echo "Build Hello World"
echo "Number $BUILD_NUMBER"
cat <<EOF > index.html
<html>
<body>
<H1>My first Ci/CD build</H1>
</body>
</html>
EOF
echo "My name $BUILD_DISPLAY_NAME"
echo "Finished Build Hello World"

# Делаем еще один шаг в который пишем тест
<!-- Если в скрипте есть нужное слово, то тест успешен -->
echo "TEST Build Hello World"
result=`grep "first" index.html | wc -l`
echo $result
if [ "$result" = "1" ]
then
	echo "Test OK"
    exit 0
else
	echo "Test Failed"
    exit 1
fi
echo ""
        
echo "TEST Build Finished"

# После того, как наши тесты прошли мы начинаем стадию Deploy. Публикуем наши файлы на тестовый стенд.
Копируем через плагин. Он по SSH закинет файлы на веб сервер.
Устанавливаем плагин: Jenkins SSH
<!-- В настройках сборки указываем финальный шаг это публикация артефактов на сервер. -->
Source files - что за файлы
Remote directory - В какую директорию
Exec command - какую команду выполнить на веб сервере
Спера подключим наш сервер на который закидываем файлы. После установки модуля открылись настройки SSH.
Вставляем ключ для подключения к серверу.

<!-- Добавляем тестовый и продакшн сервера
путь указываем до папки www -->
/var/www/html
Указываем SSH ключ, который сгенерировали на сервере Jenkins и отправили на Тестовый и Прод

В Job указываем:
* - все файлы заменить
sudo service httpd restart - выполнить рестарт вебсервера на appach
sudo service nginx restart - выполнить рестарт вебсервера на nginx

<!-- Делаем права для пользака -->

sudo chown ka -R html

