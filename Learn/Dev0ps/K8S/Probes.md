# Probes

Liveness Probe

- Контроль за состоянием приложения
- Исполняется постоянно

Readiness Probe

- Проверяет готово ли приложение принимать трафик

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
        name: nginx
        ports:
        - containerPort: 80
        readinessProbe:
          failureThreshold: 3 # количество попыток
          httpGet:
            path: /
            port: 80
          periodSeconds: 10 # каждые 10 секунд проверяем доступность хоста
          successThreshold: 1 # достаточно 1 удачной проверки
          timeoutSeconds: 60 # ждем 60 секунд
        livenessProbe:
          failureThreshold: 3
          httpGet:
            path: /
            port: 80
          periodSeconds: 10
          successThreshold: 1
          timeoutSeconds: 60
          initialDelaySeconds: 10 # через сколько прогонять -->

# Под нельзя перезагрузить только создать,изменить и удалить

Рестарт идет контейнера в поде
