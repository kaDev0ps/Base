# GIT

## Существуют 3 самых популярных GIT репозитория

GitHub
BitBucket
GitLab

## Установка на Ubuntu

sudo apt install git
git --version

## инициируем себя, чтоб в коммитах было наше имя

git config --global user.name "Alex"
git config --global user.email "ka@zelobit.com"

<!-- проверяем настройки  в домашней директории .gitconfig-->

git config -l

## работа с локальными директориями

создаем рабочую папку
mkdir my-git

<!-- создаем базу данныхъ, чтоб гит следил за изменениями в этой папке -->

git init .

<!-- Проверяем свой статус -->

git status

<!-- Добавляем свои изменения -->

git add \*

<!-- Добавляем коммит -->

git commit -m "test commit"

<!-- Проверка коммитов -->

git log

<!-- Проверка что добавлено в последний коммит -->

git log -1 -p

<!-- есл мы е отправили файлы в stage то их можно восстановить -->

git restore --staged <file>
git restore <file>

<!-- посмотреть, какие изменения будут внесены в коммит -->

git diff --staged

<!-- Как заставить игнорировать файлы и директории -->
<!-- Создаем файл .gitignore -->

\*.log
testik/

# Работа с GitHub

git clone https://github.com/AlexZelobit/ZB.git
