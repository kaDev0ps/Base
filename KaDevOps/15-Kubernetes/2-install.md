## Установка кластера
Minikube - ПО для создания кластера из 1 ноды оптимизированный для тестов, разработки и учебы.

## Установка на Windows
Скачиваем Virtual Box
Создаем на диске папку 

`C:\Windows\kubernetes`

Скачиваем файл https://dl.k8s.io/release/v1.29.1/bin/windows/amd64/kubectl.exe в нашу папку
Скачиваем https://github.com/kubernetes/minikube/releases/tag/v1.32.0 в папку и переименовываем в minikube.exe

Пропишем путь папки где эти файлы в переменные среды
Редактировать параметр PATH - Вставляем путь нашей папки

В командной строке вводим 
`minikube version`
`kubectl version --client`

мы увидим версию значит все работает

## Создание кластера

minikube start