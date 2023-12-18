# Мы можем запускать несколько реплик одного приложения

<!-- Создадим конфигурацию и запустим ее -->

kubectl apply -f replicaset.yaml

<!-- ---
apiVersion: apps/v1
kind: ReplicaSet
metadata:
  name: my-replicaset
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

<!-- Просмотр наших реплик -->

kubectl get replicaset

<!-- Удаление наших реплик -->

kubectl delete rs my-replicaset

<!-- Удаляя репликасеты мы удаляем поды -->

<!-- Можем контролировать количество реплик указав имя и количество нужное нам -->

kubectl scale rs my-replicaset --replicas=0
