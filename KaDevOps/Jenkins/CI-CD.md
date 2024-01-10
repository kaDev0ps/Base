# Термины
CI - DevOps модель по которой запускаются автоматом тесты или компиляция кода, которые  разработчики закоммитили в репзиторий
<!-- Коммит - сборка - тест -> Deployment -->
CD - тоже самое, только мы получаем артефакт, который устанавливается на сервера
# Установка
# Руководство
https://www.dmosk.ru/miniinstruktions.php?mini=jenkins-ubuntu#install-web
https://www.jenkins.io/download/
docker pull jenkins/jenkins
<!-- Надо установить java той версии, которую поддерживает Jenkins-->
<!-- Нужен свободный порт 8080. Остановить вебсервер! -->
<!-- Запускаем сборку -->
docker build -t adev0ps/ka-jenkins .
# Проверить какие порты открыты
sudo ss -ltn
# Проверить какими процессами заняты порты
sudo ss -ltnp
# посмотреть установленный пакет
dpkg -l | grep openjdk
# Полностью удалить пакет
sudo apt purge oracle-java11*
