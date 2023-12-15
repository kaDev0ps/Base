# Генерирем ключ и отправляем его на ту машин, к которой хотим подключаться

<!-- Если машина в Docker -->

ssh-keygen
cp ~/.ssh/id_rsa.pub /tmp/projects/keys/authorized_keys
ssh-copy-id username@remote_host

<!-- подключаемся к машине -->

ssh -i ~/.ssh/id_rsa -p 2022 pupy@127.0.0.1
