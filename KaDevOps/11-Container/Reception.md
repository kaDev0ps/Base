# Базовый родительский образ
 FROM
<!-- Описывает метаданные, сведения о создателе -->
LABEL
<!-- Установка переменных сред -->
ENV
<!-- Выполняет команду и создает слой образа -->
RUN
<!-- Копирование файлов и папок -->
COPY
<!-- Копирование и распаковка архивовъ -->
ADD
<!-- Выполнение команд с аргументами -->
ARG
<!-- Задает рабочую директорию для следующих инструкций -->
WORKDIR
<!-- Передачи переменных во время сборки образаъ -->
ARG
<!-- Точка входа? передает аргументы -->
ENTRYPOINT
<!-- Указывает на открытие порта -->
EXPOSE
<!-- Создает точку монтирования для работы с хранилищем -->
VOLUME

# Готовим Docker
FROM ubuntu:18.04
LABEL maintainer="karmash@ya.ru"
RUN apt-get update && apt-get install -y phyton3 && apt-get install -y phyton3-pip
WORKDIR /usr/local/bin/
COPY script.py .
ENTRYPOINT [/usr/bin/python3, script.py]