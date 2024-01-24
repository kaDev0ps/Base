# Deploy проекта с GitHub
<!-- Jenkins логинится к GitHub, клонирует репозиторий и публикует
Создаем пу стой репозиторий и слонируем его на машину в директорию  -->
git clone git@github.com:kaDev0ps/jenkins.git

<!-- Сперва делаем соединение с GIThub по SSH
Ставим плагин: Git Plugin -->
<!-- Идем в настройки Jenkins Credentionals и добавляем пользователя через которого логинимся на GitHub

С сервера Jenkins в докер если надо на который скачиваются репозитории с GitHihub делаем ключи -->
ssh-keygen -t ed25519
jenkins 
<!-- Публичный на GitHub закидываем, А приватный на сервер Jenkins -->
При создании задания выбираем Add. Username пользак GitHub.

 # Настройка нового типа шифрования
cd ~/.ssh/
ssh-keygen -t ed25519
chmod 600 ~/.ssh/id_ed25519


 <!-- Настраиваем ssh config -->
<!-- Чтобы ssh мог автоматически использовать правильные ключи при работе с удалёнными репозиториями, необходимо задать некоторые настройки. А именно - добавить в файл ~/.ssh/config следующие строки: -->
nano ~/.ssh/config

Host github.com
    HostName github.com
    User git
    IdentityFile ~/.ssh/personal_key
    IdentitiesOnly yes

# Запуск сборки
<!-- Если соединение с сервером есть, то проверяем ветку. По умолчанию */master, о возможно надо изменить на main -->