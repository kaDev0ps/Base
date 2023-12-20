# GitHub Actions
Позволяет автоматизировать Test,Build,Deploy из GitHub без поднятия сервера автоматизации типа Jenkins

# Обычная схема работы
Пушит код на Git Hub
Срабатывает тригер, который запускает Jenkins job
Jenkins делает Test,Build,Deploy на сервер типа Aws

# настройка GitHub Actions
Создать файлы .github/workflows/my-pipeline.yaml в своем репозитории
При определенном действии проходит анализ всех файлов и запускаются действия
каждый jobs создает свою виртуальную машину

#---------------------------------------------------------------------
# GitHub Action Workflow Basics
#
# Version      Date        Info
# 1.0          2019        Initial Version
#
# Made by Denis Astahov ADV-IT Copyleft (c) 2019
#---------------------------------------------------------------------
name: My-GitHubActions-Basics
env:
  APPLICATION_NAME    : "MyFlask"
  DEPLOY_PACKAGE_NAME : "flask-deploy-ver-${{ github.sha }}"

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
    
    - name: Execure few commands
      run : |
        echo "Hello Message1"
        echo "Hello Message2"
        echo "Appication name: ${{ env.APPLICATION_NAME }}"
    
    - name: List current folder
      run : ls -la
   
    - name: Git clone my repo
      uses: actions/checkout@v1   
  
    - name: List current folder
      run : ls -la
  
  my_deploy:
    runs-on: ubuntu-latest
    needs: [my_testing]
    env:
      VAR1 : "This is Job Level Variable1"
      VAR2 : "This is Job Level Variable2"
    
    steps:
    - name: Print Hello Message in Deploy
      run : echo "Hello World from Deploy job"
      
    - name: Print env vars
      run : |
        echo "Var1 = ${{ env.VAR1 }}"
        echo "Var2 = ${{ env.VAR2 }}"
        echo "Var3 = $LOCAL_VAR"
      env:
        LOCAL_VAR: "This is Superr local Environment variable"

    
    - name: Printing Deployment package
      run : echo "Deplyo pakcage name is ${{ env.DEPLOY_PACKAGE_NAME }}"
    
    - name: Lets test some packages if they are here 1
      run : aws --version

    - name: Lets test some packages if they are here 2
      run : zip --version
      
      