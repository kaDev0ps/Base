# Работа с кластером в Яндекс Cloud
Проверяем версию kubectl
`kubectl version --client`
Подключаемся к кластеру
`kubectl cluster-info --kubeconfig /home/ka/.kube/config`
Проверяем состояние 
`kubectl get componentstatuses`
`kubectl cluster-info`
Из каких нодов состоит кластер
`kubectl get nodes`

Показать roles
`kubectl get nodes --show-labels`
Сменить roles
`kubectl label node cl1ungdmeefn49h9jv9p-yqob node-role.kubernetes.io/worker=`
`kubectl label node cl1ungdmeefn49h9jv9p-yted node-role.kubernetes.io/worker2=`
`kubectl label node cl1ungdmeefn49h9jv9p-yvag node-role.kubernetes.io/worker3=`
## Бесплатный Тренажер для работы с кластером
https://labs.play-with-k8s.com/
