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

