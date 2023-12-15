# Задачи

Создает под для выполнения задачи
Пеезапускает поды
activeDeadlineSeconds - через сколько завершится задача
backofflimit - через сколько раз задача будет завершена

<!-- ---
apiVersion: batch/v1
kind: Job
metadata:
  name: hello
spec:
  backoffLimit: 2
  activeDeadlineSeconds: 60
  template:
    spec:
      containers:
      - name: hello
        image: busybox
        args:
        - /bin/sh
        - -c
        - while true; do sleep 1; date; echo Hello from the Kubernetes cluster; done
      restartPolicy: Never -->

# Проверяем логи

$k logs --tail=20 hello-pods
$k get job

# CronJob
