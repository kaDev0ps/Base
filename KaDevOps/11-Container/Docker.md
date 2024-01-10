# Установка Docker
# Обновление индексов
sudo apt update
<!-- Далее добавим в систему GPG-ключ для работы с официальным репозиторием Docker: -->
sudo apt install apt-transport-https ca-certificates curl software-properties-common -y
<!-- Теперь добавим репозиторий Docker в локальный список репозиториев: -->
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add -
<!-- Повторно обновим данные о пакетах операционной системы: -->
sudo apt update
<!-- Приступаем к установке пакета Docker. -->
sudo apt install docker-ce -y
<!-- После завершения установки запустим демон Docker и добавим его в автозагрузку: -->
sudo systemctl start docker
sudo systemctl enable docker
# Test
sudo docker run hello-world

# Операции с Docker
<!-- Зайти в контейнер -->
docker run -it ubuntu bash
<!-- Выход с контейнера -->
exit
<!-- Добавить пользователя в sudo группу -->
sudo usermod -aG docker $USER
<!-- Перезапуск службы docker -->
sudo service docker restart
<!-- Просмотр всех остановленных контейнеров -->
docker ps -a
<!-- перезапуск контейнеров -->
docker stop/start/restart
<!-- Запуск контейнера с инициализацией имени хоста. Теперь вместо ID будет наше имя -->
docker run -h karmash -it ubuntu bash
<!-- Больше инфо о контейнере можно узнать  -->
docker inspect interesting_dubinsky | grep IPAddress
<!-- Задаем имя контейнеру -->
docker run --name Ubuntu-doc -it ubuntu bash
<!-- Посмотреть изменения в контейнере -->
docker diff Ubuntu-doc
<!-- Все события в контейнере -->
docker logs Ubuntu-doc
<!-- Удалить контейнер -->
docker rm NameContainer
<!-- Удалить все остановленные контейнеры с помощью подстановки команды -->
docker rm -v $(docker ps -aq -f status=exited)
<!-- Удалить все образы -->
docker rmi $(docker images -q) --force
<!-- Запуск контейнера в фоновом режиме Apache -->
docker run -d bitnami/apache
<!-- Чтобы достучаться до контейнера через http надо проблросить порты -->
docker run -d -p 8000:8080 bitnami/apache
<!-- Запуск контейнера с хостом и именем -->
docker run -it --name myapp --hostname myapp ubuntu bash
<!-- Создаем симбвольную ссылку, чтобы при запуске не писать путь /usr/games/cowsay достаточно cowsay "test" -->
ln -s /usr/games/cowsay /usr/bin/cowsay
cowsay "test"
<!-- Сохранить изменения в контейнере создав из него образ. Указав логин на гитхабе/имяНовогоОбраза -->
<!-- Нужно выйти из работающего контейнера -->
docker commit myapp adev0ps/cowsay
docker commit -m "xx" -a "test" container-id test/image:tag
<!-- Можно запустить команду на докере вместо bash -->
docker run adev0ps/cowsay cowsay "HI"
<!-- Отправляем образ на Docker Hub -->
docker push adev0ps/cowsay
<!-- Удалияем образы -->
docker rmi gitlab/gitlab-runner:v2
<!-- Скачать образ и запустить команду -->
docker run adev0ps/cowsay cowsay "YESSS"
<!-- Узнать ip docker -->
docker-machine ip default

# Дополнение
<!-- Поиск образа с dockerHub -->
docker search ubuntu
<!-- Скачать образ -->
docker pull
<!-- Создать именнованый образ с имеющегося -->
docker create -it --name testt ubuntu:18.04
<!-- Изменить название контейнера -->
docker rename testt test

<!-- Подключение к запущенному контейнеру -->
docker attach test
<!-- Подключение к работающему контейнеру и при выходе он не останавливается -->
docker exec -it test bash
<!-- Запуск контейнера nginx, подключение через bash, прокидываем наш файл и удаление конейнера после выхода, проброс порта, -->
docker run -it --rm -p 8080:80 \
-v ~/www:/var/www/html \
-v ~/conf.d:etc/nginx/conf.d \
nginx bash

# Флаги
-it  Интерактивный сеанс работы
-d   Запуск в фоновом режиме
-e   Устанавливаем переменные окружения
--link Устанвка соединения с БД
pull run Скачать образ и сразу запустить контейнер
--name Удобное имя контейнера для обращения
