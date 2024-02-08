# Шаг 1 — Установка Docker Compose

<!-- Чтобы получить самую последнюю стабильную версию Docker Compose, мы загрузим это программное обеспечение из официального репозитория Github.

Для начала проверьте, какая последняя версия доступна на странице релизов. На момент написания настоящего документа наиболее актуальной стабильной версией является версия 1.26.0.

Следующая команда загружает версию 1.26.0 и сохраняет исполняемый файл в каталоге /usr/local/bin/docker-compose, в результате чего данное программное обеспечение будет глобально доступно под именем docker-compose: -->
<!-- Проверяем актуальную версию -->

https://github.com/docker/compose/releases

sudo curl -L "https://github.com/docker/compose/releases/download/1.26.0/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose

<!-- Затем необходимо задать правильные разрешения, чтобы сделать команду docker-compose исполняемой: -->

sudo chmod +x /usr/local/bin/docker-compose

<!-- Чтобы проверить успешность установки, запустите следующую команду: -->

docker-compose --version

<!-- Установка Docker Compose успешно выполнена. В следующем разделе мы покажем, как настроить файл docker-compose.yml и запустить контейнерную среду с помощью этого инструмента. -->

# Шаг 2 — Настройка файла docker-compose.yml

<!-- Чтобы продемонстрировать настройку файла docker-compose.yml и его работу с Docker Compose, мы создадим среду веб-сервера, используя официальный образ Nginx из Docker Hub, публичного реестра Docker. Контейнерная среда будет обслуживать один статичный файл HTML.

Для начала создайте новый каталог в домашнем каталоге и перейдите в него: -->

mkdir ~/compose-demo
cd ~/compose-demo

<!-- Настройте в этом каталоге папку приложения, которая будет выступать в качестве корневого каталога документов для вашей среды Nginx: -->

mkdir app

<!-- Создайте в предпочитаемом текстовом редакторе новый файл index.html в папке app: -->

nano app/index.html

<!-- Вставьте в файл следующее содержимое: -->

<!doctype html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>Docker Compose Demo</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/gh/kognise/water.css@latest/dist/dark.min.css">
</head>
<body>

    <h1>This is a Docker Compose Demo Page.</h1>
    <p>This content is being served by an Nginx container.</p>

</body>
</html>
<!-- Сохраните и закройте файл после завершения. Если вы использовали nano, нажмите CTRL+X, а затем Y и ENTER для подтверждения. -->

<!-- Затем создайте файл docker-compose.yml: -->

nano docker-compose.yml

<!-- Вставьте в файл docker-compose.yml следующее содержимое: -->

version: '3.7'
services:
web:
image: nginx:alpine
ports: - "8000:80"
volumes: - ./app:/usr/share/nginx/html

<!-- Файл docker-compose.yml обычно начинается с определения версии. Оно показывает Docker Compose, какую версию конфигурации мы используем.

Далее идет блок services, где настраиваются службы, являющиеся частью этой среды. В нашем примере у нас имеется одна служба с именем web. Эта служба использует образ nginx:alpine и настраивает переадресацию портов с помощью директивы ports. Все запросы порта 8000 на компьютере host (система, где вы запускаете Docker Compose) будут перенаправляться в контейнер web на порту 80, где будет работать Nginx.

Директива volumes создаст общий том для хоста и контейнера. Контейнер будет предоставлен доступ к локальной папке app, а том будет располагаться в каталоге /usr/share/nginx/html внутри контейнера, который заменит корневой каталог документов Nginx по умолчанию.

Сохраните и закройте файл.

Мы настроили демонстрационную страницу и файл docker-compose.yml для создания контейнерной среды веб-сервера, которая будет обслуживать ее. На следующем шаге мы запустим эту среду с помощью Docker Compose. -->

# Шаг 3 — Запуск Docker Compose

<!-- Теперь у нас имеется файл docker-compose.yml, и мы можем использовать Docker Compose для запуска нашей среды. Следующая команда загрузит необходимые образы Docker, создаст контейнер для службы web и запустит контейнерную среду в фоновом режиме: -->

docker-compose up -d

<!-- Docker Compose будет вначале искать заданный образ в локальной системе, и если не найдет его, загрузит его из Docker Hub. Вывод будет выглядеть следующим образом: -->

Output
Creating network "compose-demo_default" with the default driver
Pulling web (nginx:alpine)...
alpine: Pulling from library/nginx
cbdbe7a5bc2a: Pull complete
10c113fb0c77: Pull complete
9ba64393807b: Pull complete
c829a9c40ab2: Pull complete
61d685417b2f: Pull complete
Digest: sha256:57254039c6313fe8c53f1acbf15657ec9616a813397b74b063e32443427c5502
Status: Downloaded newer image for nginx:alpine
Creating compose-demo_web_1 ... done

<!-- Теперь ваша среда запущена в фоновом режиме. Для проверки активности контейнера используйте следующую команду: -->

docker-compose ps

<!-- Эта команда покажет вам информацию о работающих контейнерах и их состоянии, а также о действующей переадресации портов: -->

Output
Name Command State Ports

---

compose-demo_web_1 /docker-entrypoint.sh ngin ... Up 0.0.0.0:8000->80/tcp

<!-- Для получения доступа к демонстрационному приложению введите в браузере адрес localhost:8000, если оно запущено на локальном компьютере, или your_server_domain_or_IP:8000, если оно запущено на удаленном сервере. -->
