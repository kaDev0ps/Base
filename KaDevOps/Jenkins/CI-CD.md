# Термины
CI - DevOps модель по которой запускаются автоматом тесты или компиляция кода, которые  разработчики закоммитили в репзиторий
<!-- Коммит - сборка - тест -> Deployment -->
CD - тоже самое, только мы получаем артефакт, который устанавливается на сервера
# Установка
# Руководство
https://www.dmosk.ru/miniinstruktions.php?mini=jenkins-ubuntu#install-web

https://www.jenkins.io/download/
<!-- Скачиваем контейнер -->
docker pull jenkins/jenkins
<!-- Запускаем c образа контейнер -->
docker run -p 8081:8080 --name jenkins --hostname doc-j-4 jenkins/jenkins:latest
<!-- Подключение к работающему контейнеру (чтобы взять ключ) и при выходе он не останавливается -->
docker exec -it test bash
<!-- Нужно выйти из работающего контейнера и сделать образ-->
<!-- docker commit jenkins adev0ps/jenkins -->
<!-- После выполнения установки прерываем работу контейнера в интерактивном режиме комбинацией Ctrl + С и запускаем его в бэкграунде: -->
docker start jenkins
<!-- Больше инфо о контейнере можно узнать  -->
<!-- docker inspect jenkins-->
# Проверить какие порты открыты
sudo ss -ltn
# Проверить какими процессами заняты порты
sudo ss -ltnp
# посмотреть установленный пакет
dpkg -l | grep openjdk
# Полностью удалить пакет
sudo apt purge oracle-java11*
