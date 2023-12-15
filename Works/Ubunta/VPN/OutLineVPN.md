# Установка Outline Server

sudo apt update && sudo apt upgrade

# Если открытый IP настраиваем Firewall

sudo ufw allow 22/tcp
sudo ufw allow from <ВашПостоянныйIP> to any port 22
sudo ufw enable

# Устанавливаем из репозитория под root

sudo wget -qO- https://raw.githubusercontent.com/Jigsaw-Code/outline-server/master/src/server_manager/install_scripts/install_server.sh | bash

<!-- Будет установлен Docker и службы самого Outline, а также все зависимости. При необходимости Вы можете установить Docker самостоятельно перед запуском скрипта. -->

<!-- Когда скрипт закончит, то выведет примерно такое содержимое.

{
  "apiUrl": "https://0.0.0.0:0000/XXXXXXXXXXXX",
  "certSha256": "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
} -->

# Открываем порты (В нашем примере)

sudo ufw allow 31111/tcp
sudo ufw allow 24311/tcp

<!-- Управление серверном VPN, в т.ч. раздача доступов, осуществляется с помощью Outline Manager, доступной для Windows, Max и Linux.
https://getoutline.org/ru/ -->
<!-- При запуске нужно добавить сервер и выбрать «Настроить Outline где угодно». Появится инструкция по установке с помощью скрипта, который мы ранее запускали. А после поле для ввода ключа и адреса, который Вы до этого сохранили.

После этого у Вас появится доступ к управлению сервером.
Пишем Имя клиента и жмем на значок поделиться-->
