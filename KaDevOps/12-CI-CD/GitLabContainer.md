# Гитлаб можно использовать для хранения образов, но нужен letsencrypt серификат
<!-- Проверяем настройки Gitlab -->

nano /etc/gitlab/gitlab.rb
<!-- Если локальный сертификат то -->
registry_external_url 'https://registry.example.com'
gitlab_rails['registry_enabled'] = true
registry['enable'] = true
registry_nginx['enable'] = true
registry_nginx['proxy_set_headers'] = {
  "Host" => "$http_host",
  "X-Real-IP" => "$remote_addr",
  "X-Forwarded-For" => "$proxy_add_x_forwarded_for",
  "X-Forwarded-Proto" => "https",
  "X-Forwarded-Ssl" => "on"
}
registry_nginx['listen_port'] = 5050

<!-- Если Letsscript то -->
registry_external_url 'https://registry.example.com'
gitlab_rails['registry_enabled'] = true
registry['enable'] = true
registry_nginx['enable'] = true
registry_nginx['proxy_set_headers'] = {
  "Host" => "$http_host",
  "X-Real-IP" => "$remote_addr",
  "X-Forwarded-For" => "$proxy_add_x_forwarded_for",
  "X-Forwarded-Proto" => "https",
  "X-Forwarded-Ssl" => "on"
}
registry_nginx['listen_port'] = 8090
registry_nginx['listen_https'] = false

## Источник https://habr.com/ru/companies/timeweb/articles/589675/

<!-- Запускаем перенастройку GitLab: -->

gitlab-ctl reconfigure

<!-- Создание токенов -->
<!-- Проект - Settings - Access token -->
<!-- Пользователь -Edit profile - Access token -->
glpat-yzNyXtvrH-AYbkyv5yjP
# Должен быть установлен докер на машине с которой будем заливать пакеты
<!-- Выполняем подключение -->
docker login gitlab.zelobit.local
git
glpat-SDh2kZf97gY-zQj7x5YN

<!-- Создаем образ контейнера, например можем скачать -->
docker pull nginx
<!-- Переименовываем -->
docker tag lscr.io/linuxserver/wireguard:latest gitlab.zelobit.local/ka/demo/test-wirenboard:v1
<!-- Удаляем images -->
docker rmi hello-world:latest
Отправляем в репозиторий
docker push gitlab.zelobit.local/ka/demo/test-wirenboard:v1
<!-- Надо зайти в Проект - Deploy Пакеты и Репозитории -->