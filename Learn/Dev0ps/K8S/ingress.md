# Ingress

Подключение из внешней сети
На входе стоит ingress, за ним service

<!-- Сперва нужно поставить аддон ingress
https://msk.cloud.vk.com/app/mcs3217651806/services/containers/b235500b-ceb1-41eb-a774-58b4acad8517/addons -->

В ingress задается с какого хоста на какой backend пойдет трафик.
Дальше сервис распределяет трафик на поднявшиеся поды.

<!-- Основные балансировщики: Haproxy и NGNIX -->
<!-- ---
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: my-ingress-simple
  annotations:
    kubernetes.io/ingress.class: nginx
spec:
  rules:
  - http:
      paths:
      - path: "/"
        pathType: Prefix
        backend:
          service:
            name: my-service
            port:
              number: 8080

---
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: my-ingress-simple
  annotations:
    kubernetes.io/ingress.class: nginx
spec:
  rules:
  - host: my-host.local
    http:
      paths:
      - path: "/"
        pathType: Prefix
        backend:
          service:
            name: my-service
            port:
              number: 8080 -->

<!-- Проверяем создание ingressa -->

$k get ing
% Смотрим все службы
$k get svc -A

<!-- Проверяем доступность ресурса по внешнему порту -->

curl -k 212.233.74.166 -L -H "Host: my-host.local"
