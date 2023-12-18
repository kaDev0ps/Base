# Secret нужен дляЖ

Пароли - токены приложений
Данные авторизации в Registry
TLS сертификаты для ingress

create secret --help

# Создаем пароль

$k create secret generic my-secret --from-literal=key1=supersecret

# Проверяем создание

$k get secret
$k get secret
$k get secret my-secret -oyaml

<!-- Получили зашифрованный пароль -->

Можем расшифровать пароль
echo "c3VwZXJzZWNyZXQ=" | base64 -d

# Проверка ошибок

<!-- Можно открыть под и в событиях мосмотреть, что ему не нравится -->

$k describe pod

# Пароли создается только на контейнер

# Можем войти в контейнер

$k exec -it my-deployment-66dd5575ff-5rpdz env
