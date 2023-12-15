# Создаем Dockerfile

<!-- Обычный файл в котором прписаны операции для создания образа. -->

FROM python:3.9-alpine
RUN pip install Django==3.2 -y
ADD . /app
EXPOSE 8000
ENTRYPOINT python /app/manage.ru runserver 0.0.0.0:8000

# В этом же каталоге запускаем команду

<!-- Создаем образ в текущем каталоге по сценарию dockerfile -->

docker build . -t app-python:v1
docker images

<!-- Проверяем то, что мы создали образ по той конфигурации, которая указана в Dockerfile. -->
<!-- Запускаем контейнер из образа в фоновом режиме с портом указаным в конфигурации-->

docker run -d -P app-python:v1

# Все изменения лучше сохранять не в образе, а в томах volume

docker run --name py-app -d -p 8000:8000 -v $PWD:/app app-python:v1
