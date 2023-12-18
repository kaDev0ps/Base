# Установка Bitwarden в Docker

<!-- ВНИМАНИЕ!
Это индивидуальне хранилище. Для корпоративных целей надо покупать лицензию 5$ на пользователя в месяц -->

sudo apt update -y && sudo apt upgrade -y

<!-- Устанавливаем дополнительные инструменты -->

sudo apt-get install apt-transport-https ca-certificates curl gnupg-agent software-propertiescommon

<!-- Устанавливаем ключ для развертывания менеджера Docker -->

curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add -

<!-- Добавим репозиторий Docker -->

sudo add-apt-repository \
"deb [arch=amd64] https://download.docker.com/linux/ubuntu \
$(lsb_release -cs) \
stable" \
$

 <!-- Обновляем систему -->

sudo apt update

 <!-- Устанавливаем необходимые компоненты Docker -->

sudo apt install docker-ce docker-ce-cli containerd.io docker-compose -y

 <!-- Создадим пользователя и отдельный каталог для него -->

sudo mkdir /opt/bitwarden && sudo adduser bitwarden

 <!-- Разрешаем доступ к каталогу -->

sudo chmod -R 700 /opt/bitwarden && sudo chown -R bitwarden:bitwarden /opt/bitwarden

 <!-- Разрешаем запуск команды Docker -->

sudo usermod -aG docker bitwarden

 <!-- Переключаемся на нашешего пользователя -->

su bitwarden

 <!-- Переходим в каталог -->

cd /opt/bitwarden

 <!-- Загружаем скрипт установки -->

curl -Lso bitwarden.sh https://go.btwrdn.co/bw-sh && chmod 700 bitwarden.sh

 <!-- Запускаем скрипт и дальше по порядку выполняем действия -->

./bitwarden.sh install

 <!-- If you need to make additional configuration changes, you can modify
 the settings in `./bwdata/config.yml` and then run:
 `./bitwarden.sh rebuild` or `./bitwarden.sh update`

 Next steps, run:
 `./bitwarden.sh start` -->

# Настройка почтового сервера

<!-- Все настройки сервера тут ./bwdata/env/global.override.env -->

globalSettings**mail**replyToEmail=ka@zelobit.com
globalSettings**mail**smtp**host=email.zelobit.com
globalSettings**mail**smtp**port=587
globalSettings**mail**smtp**ssl=false
globalSettings**mail**smtp**username=bitwarden@zelobit.com
globalSettings**mail**smtp**password=mail_Bitwarden
globalSettings**disableUserRegistration=true
globalSettings**hibpApiKey=REPLACE
adminSettings**admins=ka@zelobit.com

<!-- Запускаем сервер -->

./bitwarden.sh start

<!-- Вход в панель управления -->

IP/admin

<!-- Остановка и удаление сервера -->

./bitwarden.sh stop
rm -r ~/bwdata

<!-- Повторная установкаы -->

./bitwarden.sh install

<!-- Для обновления -->

./bitwarden.sh updateself
./bitwarden.sh update

Для установки учетки администратора, нужно зайти IP/admin
Учетка для регистрации должна быть отличной от IP/admin
