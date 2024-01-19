# Генерирем ключ и отправляем его на ту машин, к которой хотим подключаться

<!-- Если машина в Docker -->

ssh-keygen
cp ~/.ssh/id_rsa.pub /tmp/projects/keys/authorized_keys
ssh-copy-id username@remote_host

<!-- подключаемся к машине -->
cat ~/.ssh/id_rsa
ssh -i ~/.ssh/id_rsa -p 2022 pupy@127.0.0.1

<!-- Отключение SSH пароль -->
/etc/ssh/sshd_config

systemctl restart sshd

# Подключение по SSH key
<!-- Если у нас есть файл ssh_key ввида  -->
-----BEGIN OPENSSH PRIVATE KEY-----
b3BlbnNzaC1rZXktdjEAAAAABG5vbmUAAAAEbm9uZQAAAAAAAAABAAABFwAAAAdzc2gtcn
....................
sJT8x7XP7DZ0s4PhPQAAAC1yb290QHRlc3R2bS02OHIzOGU5ajYwdjMwZDFwLTZmNGZmN2
M1ZjktczZkZnQBAgME
-----END OPENSSH PRIVATE KEY-----
<!-- То можем подключиться ксерверу командой указать путь к ключу -->
ssh -i ~/.ssh/ssh_key user@192.168.0.1

<!-- Внимание! Разрешение файла ключа должно быть 600 -->

<!-- Разрешаем аутентификацию по ключам -->
 nano  /etc/ssh/sshd_config
 PubkeyAuthentication yes