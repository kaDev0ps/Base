# Основы
CI - непрерывная интеграция
CD - непрерывная доставка
Pipeline - последовательность выполнения действий.

    CI (автоматически)
- Сборка
- Качество кода
- Тест
В итоге получаем пакет

    СD (pipeline)
- Предпросмотр
- Развертывание в STAGE
- Развертывание в ПРОД (возможно с админом)

# Установка сервера Управления Gitlab

<!-- Add Docker's official GPG key: -->
sudo apt-get update
sudo apt-get install ca-certificates curl gnupg
sudo install -m 0755 -d /etc/apt/keyrings
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
sudo chmod a+r /etc/apt/keyrings/docker.gpg

<!-- Add the repository to Apt sources: -->
echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu \
  $(. /etc/os-release && echo "$VERSION_CODENAME") stable" | \
  sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
sudo apt-get update

# Подключаем runner
<!-- Создаем проект на gitlab
Переходим в Settings -> CI/CD
https://docs.gitlab.com/runner/install/docker.html -->

docker run -d --name gitlab-runner --restart always \
  -v /srv/gitlab-runner/config:/etc/gitlab-runner \
  -v /var/run/docker.sock:/var/run/docker.sock \
  gitlab/gitlab-runner:latest

<!-- Регистрация Runnera -->
sudo docker run --rm -it -v /srv/gitlab-runner/config:/etc/gitlab-runner gitlab/gitlab-runner register
<!-- Если возникает ошибка -->
<!-- tls: failed to verify certificate: x509: certificate signed by unknown authority -->
<!-- Добавляем корневой сертификат в доверенные! Меняем адрес нашего сервера gitlab.zelobit.local -->
# Download the binary for your system
sudo curl -L --output /usr/local/bin/gitlab-runner https://gitlab-runner-downloads.s3.amazonaws.com/latest/binaries/gitlab-runner-linux-amd64

# Give it permission to execute
sudo chmod +x /usr/local/bin/gitlab-runner

# Create a GitLab Runner user
sudo useradd --comment 'GitLab Runner' --create-home gitlab-runner --shell /bin/bash

# Install and run as a service
sudo gitlab-runner install --user=gitlab-runner --working-directory=/home/gitlab-runner
sudo gitlab-runner start



sudo nano /etc/gitlab/gitlab.rb
sudo nano /etc/hosts

SERVER=gitlab.zelobit.local
PORT=443
CERTIFICATE=/etc/gitlab-runner/certs/${SERVER}.crt
sudo mkdir -p $(dirname "$CERTIFICATE")
openssl s_client -connect ${SERVER}:{PORT} -showcerts </dev/null 2>/dev/null | sed -e '/-----BEGIN/,/-----END/!d' | sudo tee "$CERTIFICATE" >/dev/null

gitlab-ctl restart

<!-- Запускаем повторно Gitlab runner -->
gitlab-runner register --tls-ca-file="$CERTIFICATE"
https://gitlab.zelobit.local/
GR1348941G1BCSTzxPy69DHKhB5Db
gitlab-server-runner
gitlab-server-runner-tag
gitlab-server-runner-note

<!-- Завершаем настройку runner -->
<!-- Выбираем среду docker -->
ubuntu:20.04
# Создание pipeline
<!-- Идем на главную проекта pipeline -->
.gitlab-ci.yml
Вставляем скрипт

stages:
    - build

stage_build:
    stage: build
    image: ubuntu:20.04
    tags:
        - gitlab3
    script:
        - echo "Privet Gitlab ya? PLease? 1234" >> installWEB.sh
        - echo 'sudo apt install apache2' >> installWEB.sh
        - cat installWEB.sh

# Создание артефактов
<!-- Мы добавляем артефакты чтоб создавался наш пакет -->
stages:
    - build

stage_build:
    stage: build
    image: ubuntu:20.04
    tags:
        - gitlab3
    script:
        - mkdir build
        - echo "Privet Gitlab ya? PLease? 1234" >> installWEB.sh
        - echo 'sudo apt install apache2' >> installWEB.sh
        - cat installWEB.sh
        - cp installWEB.sh build/
        - cd build
    artifacts:
        paths:
            - build/
<!-- Мы пишем последовательность действий и сохраняем артефакты, которые пожно потом скачать -->
# Устанавливаем runner на другую машину. На ней должен стоять git!
<!-- Если будет в первом pipeline ошибка, то дальше проверка не пройдет, но мы можем игнорировать ошибки -->
stages:
    - build
    - test

stage_build:
    stage: build
    image: ubuntu:20.04
    tags:
        - gitlab3
    script:
        - mkdir build
        - echo "Privet Gitlab ya? PLease? 1234" >> installWEB.sh
        - echo 'sudo apt install apache2' >> installWEB.sh
        - cat installWEB.sh
        - cp installWEB.sh build/
        - cd build
    artifacts:
        paths:
            - build/
# игнорируем ошибки
    allow_failure: true
            
testing:
    stage: test
    tags:
        - test
    script:
        - echo "Privet"
        - touch file1
        - echo "Hello Runner!1" > file1

<!-- Если у нас ошибка доступа, то мы можем расширить права пользователя на хосте с ранером. -->
visudo
<!-- добавляем -->
gitlab-runner ALL=(ALL) NOPASSWD: ALL

<!-- Можем добавлять переменные в настройках или в самих pipeline -->
<!-- Перезагрузка службы -->
sudo gitlab-ctl restart

# Пример кода выполнения скрипта и развертывание apache сервера

stages:
    - build
    - test
    - staging

stage_build:
    stage: build
    image: ubuntu:20.04
    tags:
        - gitlab3
    script:
        - mkdir build
        - cp installWEB.sh build/
        - cd build
        - echo 'echo "Privet Gitlab!!"' >> installWEB.sh
        - echo 'sudo yum install httpd -y' >> installWEB.sh
        - cat installWEB.sh
        - echo 'echo "Privet Gitlab!"' >> index.html

    artifacts:
        paths:
            - build/
    allow_failure: true
            
testing:
    stage: test
    tags:
        - test
    script:
        - sudo bash build/installWEB.sh
        - sudo cp build/index.html /var/www/html/index.html
deploy_staging:
    stage: staging
    tags:
        - stage
    script:
        - sudo bash build/installWEB.sh
        - sudo cp build/index.html /var/www/html/index.html
    when: manual

<!-- В домашней папке пользователя под которым запущен gitlab-runner создается каталог duilds. В нем раннеры связанные с гитлабом.  -->