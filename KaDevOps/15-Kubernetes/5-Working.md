# Сроздание и управление PODS
Проверяем наличие нод
`kubectl get nodes`

Проверяем запущеные поды
`kubectl get pods`

Запускаем под с именем hello указываем наш образ и открываем порт
`kubectl run hello --image=adev0ps/php --port=80`

Удаляем pod
`kubectl delete pods hello`

Вся инфо об объекте
`kubectl describe pods hello`