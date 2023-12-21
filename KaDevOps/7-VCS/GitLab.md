# Установка
Источник https://docs.gitlab.com/ee/topics/offline/quick_start_guide.html

<!-- Добавляем репозиторий -->
sudo apt-get update
sudo apt-get install -y curl openssh-server ca-certificates tzdata perl

<!-- Установка бесплатной версии -->
curl https://packages.gitlab.com/install/repositories/gitlab/gitlab-ce/script.deb.sh | sudo bash

<!-- Пишем домен -->
sudo EXTERNAL_URL="https://gitlab.zelobit.local" apt-get install gitlab-ce

<!-- Добавляем на контроллер в ДНС запись нашего сервера -->
https://gitlab.zelobit.local

<!-- Узнаем пароль -->
sudo cat /etc/gitlab/initial_root_password
<!-- Авторизируемся -->
root
KW2wMCdRpTRjEvLYyfTL3R/Vl13b93atfOXhBvDwAzE=

<!-- Создаем пользователя -->
Add Users- Users

# На клиентской машине

<!-- Создаем директорию и файлы -->
mkdir project_gitlab
cd project_gitlab 
touch file1 file2

<!-- Создаем конфигурацию и отправляем файлы в проект -->

git config --global user.name "Alex"
git config --global user.email "ka@zelobit.com"

<!-- Добавляем SSL -->
sudo mkdir -p /etc/gitlab/ssl
sudo chmod 755 /etc/gitlab/ssl
sudo openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout /etc/gitlab/ssl/my-host.internal.key -out /etc/gitlab/ssl/my-host.internal.crt
# Инструкция по генерации ssl сертификата
<!-- Заменяем сертификаты на наши c AD /etc/gitlab/ssl -->
 sudo mv gitlab.zelobit.local.pem gitlab.zelobit.local.crt
 sudo mv gitlab.zelobit.local.key gitlab.zelobit.local.key

# Создание SSH key
<!-- Перенастраиваем конфигурацию -->
sudo gitlab-ctl reconfigure

<!-- Меняем подключение по ключу -->

git remote set-url origin  git@gitlab.zelobit.local:ka/project_test.git


<!-- Добавим в hosts наш домен -->
sudo nano /etc/hosts
172.21.1.150 gitlab.zelobit.local gitlab

cd existing_folder
git init --initial-branch=main
git remote add origin git@gitlab.zelobit.local:ka/project_test.git
git add .
git commit -m "Initial commit"

<!-- Если нет ssl
Git config --global http.sslverify false -->

git push -u origin main

# Скачивание репозиториев
git clone git@gitlab.zelobit.local:ka/project_test.git