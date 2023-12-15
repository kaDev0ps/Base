# В этом файле хранится конфигурация

## vim configmap.yaml

apiVersion: v1
kind: ConfigMap
metadata:
name: my-configmap
data:
default.conf: |
server {
listen 80 default*server;
server_name *;

        default_type text/plain;

        location / {
            return 200 '$hostname\n';
        }
    }

$k create -f configmap.yaml

$k get cm

# Проверить какие службы использует кибернетис и их краткое обозначение

$k api-resources
