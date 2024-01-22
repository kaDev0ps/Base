# Jenkins CLI
Можно управлять Jenkins через CLI.
Скачивается клиент с нашего сервера.
Настройки - Jenkins CLI
wget http://172.21.0.102:8081/jnlpJars/jenkins-cli.jar

# Создаем пользователя
Заходим под пользователем и делаем ему токен в настройках профиля
114fe7f5a63df951fe77bcc28ac5f7e142



# Запуск команды 
java -jar jenkins-cli.jar -auth jenkins:Zel0bit04k@_J -s http://172.21.0.102:8081 who-am-i
java -jar jenkins-cli.jar -auth jenkins:114fe7f5a63df951fe77bcc28ac5f7e142 -s http://172.21.0.102:8081 who-am-i




Скомпилируйте сервер zabbix с необходимыми опциями (--with-libxml2 и --with-libcurl)