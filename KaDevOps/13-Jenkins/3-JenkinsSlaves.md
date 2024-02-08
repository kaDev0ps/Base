# Slaves/Nodes
Они снижают нагрузку с масера Nodes
Node - для java
Node2 - Ansible
Node3 - .NET

Мастер может быть простым, а ноды мощные на которым будут происходить сборки.

# Настройка
Устанавливаем java на slave
apt install java
<!-- для Centos -->
sudo yum install java-11-openjdk-devel
Устанавливаем плагины
SSH Agent - сохраняем ключи  в Credentions
SSH Slave - добавляем агенты

Настройки > Nodes 

<!-- Ставим метки разделенные пробелом -->
ubuntu ansible

# ВАЖНО!
<!-- Ставим доверие к серверу подключения -->
Host Key Verification Strategy

Manually trusted key

<!-- Для успешного подключения публичный ключ нжно скопировать на сервер Jenkins -->

<!-- Для запуска сборки на конкретном ноде выбираем Label. Нужно удалить пробел -->