# Установка Docker
# Обновление индексов
sudo apt-get update
<!-- Необходимые пакеты для работы -->
sudo apt-get install ca-certificates curl gnupg
sudo install -m 0755 -d /etc/apt/keyrings
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
sudo chmod a+r /etc/apt/keyrings/docker.gpg

# Add the repository to Apt sources:
echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu \
  $(. /etc/os-release && echo "$VERSION_CODENAME") stable" | \
  sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
sudo apt-get update
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



# Флаги
-it  Интерактивный сеанс работы
-d   Запуск в фоновом режиме
-e   Устанавливаем переменные окружения
--link Устанвка соединения с БД
pull run Скачать образ и сразу запустить контейнер
--name Удобное имя контейнера для обращения
