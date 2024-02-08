## Описание инструмена Terraform
Terraform - инструмент для создания инфраструктуры в коде
Работает с провайдерами представленнные на сайте Terraform

### Установка в Windows. 
Скачиваем архив и запускаем его. Можно прописать системные переменные.

### Установка в Linux. 
`wget -O- https://apt.releases.hashicorp.com/gpg | sudo gpg --dearmor -o /usr/share/keyrings/hashicorp-archive-keyring.gpg`
`echo "deb [signed-by=/usr/share/keyrings/hashicorp-archive-keyring.gpg] https://apt.releases.hashicorp.com $(lsb_release -cs) main" | sudo tee /etc/apt/sources.list.d/hashicorp.list`
`sudo apt update && sudo apt install terraform`

Если не получилось, то скачиваем amd64 и заливаем на сервак. Удаляем архив и перемещаем в bin для всех пользователей
`sudo apt install unzip`
`unzip terraf*`
`rm *.zip`
`mv terra* /bin/`
`terraform -v`
Для обновления просто перезаписываем данный файл новой версией.

## Ставим расширение Hashicorp под Terraform
Все файлы с расширением *.tf будут красивые
Для форматинга нужно зайти в 
`File -> Preference -> Settings`
`Formatting -> Format on Save`
 