# Настройка автоматизации
1. Нужен Flask на Python
2. закидываем на GitHub
3. настраиваем GitHub Actions 
4. публикует на AWS

#---------------------------------------------------------------------
# GitHub Action Workflow Basics
#
# Version      Date        Info
# 1.0          2019        Initial Version
#
#---------------------------------------------------------------------
# name - название pipiline или задачи, шага
# env - глобальные переменные
# jobs - наши задачи
# on - условия запуска
# uses - использование скрипта
# actions/checkout@v1 - копирование репозитория
# APPLICATION_NAME - имя приложения
# DEPLOY_PACKAGE_NAME - имя пакета приложения
# zip -r {{ env.DEPLOY_PACKAGE_NAME }} ./ -x *.git* Создание архива с исключением
name: My-GitHubActions-Test2
env:
  APPLICATION_NAME    : "MyFlask"
  DEPLOY_PACKAGE_NAME : "flask-deploy-ver-${{ github.sha }}.zip"

on: 
  push:
    branches: 
      - master

jobs:
  my_testing:
    runs-on: ubuntu-latest

    steps:
    - name: Print Hello Message in Testing
      run : echo "Hello World from Testing job"
    - name: Git clone my repo
      uses: actions/checkout@v1 
    - name: Create ZIP
      run : zip -r {{ env.DEPLOY_PACKAGE_NAME }} ./ -x *.git*
    
    - name: Execure few commands
      run : |
        echo "Hello Message1"
        echo "Appication name: ${{ env.APPLICATION_NAME }}"