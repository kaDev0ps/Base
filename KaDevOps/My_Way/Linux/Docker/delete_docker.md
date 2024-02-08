# Чтобы полностью удалить Docker:

<!-- Шаг 1 -->

dpkg -l | grep -i docker

<!-- Чтобы определить, какой у вас установлен пакет: -->

<!-- Шаг 2 -->

sudo apt-get purge -y docker-engine docker docker.io docker-ce docker-ce-cli docker-compose-plugin
sudo apt-get autoremove -y --purge docker-engine docker docker.io docker-ce docker-compose-plugin

<!-- Приведенные выше команды не удалят образы, контейнеры, тома или созданные пользователем файлы конфигурации на вашем хосте. Если вы хотите удалить все образы, контейнеры и тома, выполните следующие команды: -->

sudo rm -rf /var/lib/docker /etc/docker
sudo rm /etc/apparmor.d/docker
sudo groupdel docker
sudo rm -rf /var/run/docker.sock

<!-- Вы полностью удалили Docker из системы. -->

docker run hello-world

## Остановка и удаление контейнера

<!-- Чтобы просмотреть все контейнеры в системе, введите: -->

docker ps -a

<!-- Чтобы удалить контейнеры, передайте их ID командам docker stop и docker rm с помощью флага –q: -->

docker stop $(docker ps -a -q) && docker rm $(docker ps -a -q)

<!-- Чтобы удалить все остановленные контейнеры и неиспользуемые образы (а не только образы, не связанные с контейнерами), добавьте в эту команду флаг -a: -->

docker system prune -a
