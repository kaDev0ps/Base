## Работа с Terraform
Возможности определены в документации провайдера на сайте тераформ
https://registry.terraform.io/providers/hashicorp/vsphere/latest/docs

## Скрываем наши пароли в переменные
vSphere уже имеет встроенную поддержку трех переменных сред, которые можеyj использовать для установки этих значений без какой-либо дополнительной настройки:

`export VSPHERE_USER=xxxx`
`export VSPHERE_PASSWORD=xxxx`
`export VSPHERE_SERVER=x.x.x.x`

Поэтому мы можем удалить эти данные с файлы main, variables, terraform.tfvars. И каждую сессию начинать с загрузки данных переменных в нашу сессию. В итоге получим такую строчку в коде

<!-- 
*****************
provider "vsphere" {
  allow_unverified_ssl = true
} 
*****************
-->





