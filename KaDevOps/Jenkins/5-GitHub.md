# Deploy проекта с GitHub
<!-- Jenkins логинится к GitHub, клонирует репозиторий и публикует
Создаем пу стой репозиторий и слонируем его на машину в директорию  -->
git clone git@github.com:kaDev0ps/jenkins.git

<!-- Сперва делаем соединение с GIThub по SSH
Ставим плагин: Git Plugin -->
<!-- Идем в настройки Jenkins Credentionals и добавляем пользователя через которого логинимся на GitHub

С машины на которой скачиваются репозитории с GitHihub делаем ключи -->
ssh-keygen -t ed25519
jenkins
<!-- Публичный на GitHub закидываем, А приватный на сервер Jenkins -->
При создании задания выбираем Add. Username пользак GitHub.
