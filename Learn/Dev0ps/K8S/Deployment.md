# Deployment - управляет состоянием развертывания подов

<!-- replicaSet создает поды, а deployment создает Repicaset -->

Манифест такой же, как у ReplicaSet. Кроме kind

<!-- ---
apiVersion: apps/v1
kind: Deployment
metadata:
  name: my-deployment
spec:
  replicas: 2
  selector:
    matchLabels:
      app: my-app
  template:
    metadata:
      labels:
        app: my-app
    spec:
      containers:
      - image: nginx:1.12
        name: nginx
        ports:
        - containerPort: 80 -->

<!-- Развернем наш deployment -->

kubectl apply -f my-deployment
kubectl get deploy

<!-- Удаление наших реплик -->

kubectl delete rs my-replicaset

<!-- Проверим настройки deploy -->

kubectl describe deploy my-deployment

<!-- Изменение настроек -->

kubectl edit deploy my-deployment

<!-- Можем изменить на лету версию -->

<!-- Проверка истории -->

kubectl rollout history my-deployment

curl -k 212.233.74.166 -L -H "Host: my-host.local"
