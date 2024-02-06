# Защищаем сервер

cd /etc/apt
source list - список подключенных репозиториев в том числе по безопасности

## Настройка авторизации по ключам

<!-- Короткий пример -->

sudo adduser user3
sudo -su user3
ssh-keygen
ssh-copy-id admin@192.168.10.10
ssh 192.168.10.10

# Подробно

<!-- Под пользователем НЕ ROOT выполняем команду -->

ssh-keygen

<!-- Создается ключ в /home/ka/.ssh/id_rsa.pub -->
<!-- Можем вывести и скопировать или сразу установить на удаленную машину. -->

cat .ssh/id_rsa.pub
ssh-copy-id 172.21.1.188

<!-- Если ssh-copy-id не работает то можно выполнить команду
cat ~/.ssh/id_rsa.pub | ssh username@remote_host "mkdir -p ~/.ssh && cat >> ~/.ssh/authorized_keys" -->

На удаленной машине должен создаться файл .ssh/authorized_keys

<!-- Проверяем права 600 -->

chmod 600 .ssh/authorized_keys

<!-- После изменений перезагружаем cлужбу -->

systemctl reload sshd

<!-- Подключаемся с машины у которой key ключ к той у которой pub-->

<!-- Если все хорошо, то отключаем авторизацию по паролю на той машине у которой pub ключ  -->

cd /etc/ssh/sshd_config

# Include /etc/ssh/sshd_config.d/\*.conf

PermitRootLogin no
PasswordAuthentication no
ChallengeResponseAuthentication no
UsePAM no

<!-- После изменений перезагружаем cлужбу -->

systemctl reload sshd
