# Автоматическую сборку можно запускать через
1. Спомощью url ссылки. При переходе по адресу запуск job.
2. После успешно выполненного предыдущего job
3. Периодически. Каждую минуту или по определенным дням
4. Проверять каждую минуту есть ли новые изменения на GIT и запускать сборку. SCM.

# Установить плагин от GitHub Плагин
GitHub отправляет Webhock
<!-- Указывааем url GitHub project -->
git@github.com:kaDev0ps/jenkins.git
<!-- Выбираем тригер слежение за коммитом -->
<!-- На GitHub настраиваем Webhoock -->
<!-- Settings - webhoock -->
http://172.21.0.102:8081/github-webhook/
application/json

<!-- Нужно внешний адрес! -->
# Работа с параметрами
При  создания JOb можно поставить параметры 
String paramaetr - переменная
Choice parametr - выбор при сборке
File parametr - куда загрузить

$FOLDERNAME