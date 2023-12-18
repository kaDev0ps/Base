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

# Пример Работаы с GitHub

<!-- Скачиваем все без пароля -->

git clone https://github.com/AlexZelobit/ZB.git

<!-- После изменения публикуем -->

git push -u origin master

<!-- Сперва создаем ключ -->

Create Personal Access Token on GitHub
From your GitHub account, go to Settings => Developer Settings => Personal Access Token => Generate New Token (Give your password) => Fillup the form => click Generate token => Copy the generated Token, it will be something like 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

# Работа с Git в компании

<!-- Делаем копию с мастереа -->

git clone https://github.com/kaDev0ps/Base.git

<!-- Делаем свою ветку, чтобы ничего не испортить -->

git checkout -b ka_branch_task_01

<!-- проверяем в какой ветке сейчас -->

git branch

<!-- Вносим все изменения и коммитим -->

git add .
git commit -m "Add test"

<!-- Отправляем в github -->

git push origin

<!-- Выдаст сообщения что данной ветки нет и предложит команду чтоб ее создать -->
<!-- После заливки кода нужно отправить проверяющему запрос на добавления ветки в мастер -->
<!-- после этого можно удалить свою ветку -->

git checkout master
git branch -d test_branche_task_1

<!-- git branch -d test_task
удаляем удаленно из github -->

git push origin --delete test_branch
