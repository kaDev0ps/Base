# Описание job через pipeline
Надо установить плагин Pipeline
Все команды запихиваем в скрипт, а сам pipeline должен быть коротким.

pipeline {
    agent any
    environment {
        OWNER = "Neptun"
        PROJET = "Testik"
    }
    stages {
        stage('pipeline') {
            steps {
                echo 'My pipeline'
                echo '$OWNER'
            }
        }
        stage('pipeline2') {
            steps {
                echo 'My pipeline2'
                echo '$PROJECT'
            }
        }
    }
}
# Скрипт выполнения может быть написан и размещен на GitHub
Мы в нашем Pipeline указываем ссылку на наш Jenkinsfile со скриптом.