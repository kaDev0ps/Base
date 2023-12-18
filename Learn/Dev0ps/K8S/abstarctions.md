# Service

Балансировщик за которым стоит iptables
В манифесте мы указываем selector: он обращаеся к lables

Поды меняют свои IP постоянно
К сервисам нужно обращаться по имени

1 - Запускаем деплоймент
2 - Запускаем сервис для доступа в интернет по средствам создание endpoint

<!-- ---
apiVersion: apps/v1
kind: Deployment
metadata:
  name: my-app
spec:
  replicas: 1
  selector:
    matchLabels:
      app: my-app
  template:
    metadata:
      labels:
        app: my-app
    spec:
      containers:
      - image: nginx:1.20
        name: nginx -->

Манифест сервиса

<!-- ---
apiVersion: v1
kind: Service
metadata:
  name: my-service
spec:
  ports:
  - port: 8080
    targetPort: 80
  selector:
    app: my-app
  type: ClusterIP -->

$k apply -f service.yaml
% Смотрим наши сервисы
$k get service

<!-- Расширенные параметры -->

$k get po -owide
% Проверяем сервис
$k get svc

# Получаем список ENDPOIMENT (запись в БД на какие адреса маршрутизируем трафик, трафик идет по меткам)

$k get ep

% Просмотр меток %
$k get po --show-labels

<!-- Через exec curl дергаем сервис. Моно зайти во внутрь пода, а можно запустить команду -->

$k exec -it my-app-5c999bf6f8-m8n4h -- curl http://my-service:8080

% # Давайте создадим namespace monday

$k create ns monday

<!-- Создадим абстракцию Service в которой укажем namespace -->

$k apply -f monday.yaml
% Проверяем поды, сервисы, endpoints в namespace
$k get po -owide -n monday
$k get po -owide -n default

% Нужно создать:
Приложение
Сервис (Endpoint создается автоматически)
И трафик пойдет в наше приложение

# Теперь мы можем с обычного пода достучатся до пода в другом namespace

$k exec -it my-app2-78d6d7f5fb-xjd8z -n monday -- curl http://my-service.default:8080

# Показать текущий namespace

kubectx

# # Показать все namespace

kubens

# Переключение между namespace

kubens monday
