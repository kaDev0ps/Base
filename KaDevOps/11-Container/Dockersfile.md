# Что такое
<!-- DockerFile - файл c инструкцией по созданию образа -->
# Начало
<!-- Создаем папку и в ней файл DockerFile -->
# Готовим контейнеры
<!-- Выбираем образ с которым работаем -->
FROM ubuntu
<!-- Пишем, чей образ -->
MAINTAINER KaDevOps <faq@kadevops.ru>
<!-- Пишем, что мы хотим с этим образом сделать -->
RUN apt-get update && apt-get install -y cowsay && ln -s /usr/games/cowsay /usr/bin/cowsay
<!-- Делаем точку входа. Чтобы автоматом запускалось нужная оболочка. В нашем случае только аргумент передать, что говорит корова -->
ENTRYPOINT ["cowsay"]docker images

<!-- Выполняем команду создания образа в том каталоге, где dokerfile -->
docker build -t adev0ps/cowsayDockerfile .