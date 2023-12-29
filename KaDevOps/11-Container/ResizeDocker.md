# Уменьшение размера Docker

<!-- На одном уровне можно удаль различные дистрибутивы и лишние файлы в Dockerfile-->

RUN apt-get update \
    && apt-get install -y cowsay \
    && ln -s /usr/games/cowsay /usr/bin/cowsay \
    && rm -rf /var/lib/apt/lists/*

<!-- Можно выбрать образ с минимальным размером -->
