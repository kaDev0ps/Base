Подключившись к кибернетису можем создать свой сценарий POD. И после его запуска будет рабоать приложение, как это было бы в докере.

<!-- ---
apiVersion: v1
kind: Pod
metadata:
  name: my-pod
spec:
  containers:
  - image: nginx:1.12
    name: nginx
    ports:
    - containerPort: 80

-->

<!-- Создать под из файла -->

kubectl create -f pod.yaml

<!-- Получить список подов -->

kubectl get pod

<!-- Проксируем порты -->

kubectl port-forward my-pod 8888:80
