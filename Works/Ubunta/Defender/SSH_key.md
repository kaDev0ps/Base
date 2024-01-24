# Генерирем ключ и отправляем его на ту машин, к которой хотим подключаться

# Публичный ключ должен быть на сервере К КОТОРОМУ ПОДКЛЮЧАЕМСЯ! Иначе будет ошибка доступа


<!-- Генерим ключи -->

ssh-keygen
cp ~/.ssh/id_rsa.pub /tmp/projects/keys/authorized_keys
<!-- Отправляем на хост -->
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


 # Настройка нового типа шифрования
cd ~/.ssh/
ssh-keygen -t ed25519
chmod 600 ~/.ssh/id_ed25519


 <!-- Настраиваем ssh config -->
<!-- Чтобы ssh мог автоматически использовать правильные ключи при работе с удалёнными репозиториями, необходимо задать некоторые настройки. А именно - добавить в файл ~/.ssh/config следующие строки: -->
nano ~/.ssh/config

Host github.com
    HostName github.com
    User git
    IdentityFile ~/.ssh/personal_key
    IdentitiesOnly yes
где:

gihub.com - url сервиса, с которым будем работать (указываем одинаковым в Host и HostName).

~/.ssh/personal_key - путь до файла с приватным ключом, который необходимо использовать для подключения.