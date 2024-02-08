# Шаг 1. Ставим Docker

<!-- Заходим пользователем на машину, где будет стоять OnlyOffice.

По инструкции с сайта ставим пакеты. Тут всё просто.
https://docs.docker.com/engine/install/ubuntu/
-->

sudo apt-get update
sudo apt-get install \
 apt-transport-https \
 ca-certificates \
 curl \
 gnupg-agent \
 software-properties-common -y

curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add -

sudo add-apt-repository \
"deb [arch=amd64] https://download.docker.com/linux/ubuntu \
$(lsb_release -cs) \
stable"

sudo apt-get update

sudo apt-get install docker-ce docker-ce-cli containerd.io -y
% Если вы хотите запускать docker не от рута, то добавьте пользователя в группу docker

sudo usermod -aG docker $USER

<!-- После этого надо завершить текущий сеанс и выполнить вход повторно. Затем выполните проверку работы Docker -->

docker run hello-world

<!-- В ответ должно выйти приветственное сообщение об успешном выполнении. -->

<!-- Запуск службы -->

systemctl start docker

<!-- Добавляем себя в пользователи, где не нужно вводить sudo -->

gpasswd -a $USER docker
