# Установка SSL на vCenter7

Скачиваем корневой сертификат в DER и конвертируем в PEM
https://www.getssl.in/ssltools/ssl-converter.php

Делаем снапшот виртуальной машины.
Заходим через консоль и включаем SHELL и SSH
Подключаемся к ней по SSH

<!-- Заходим на vCenter по SSH под пользователем administrator@vsphere.local.
Переходим в shell: -->

shell
vcenter

<!-- Переключаемся в sudo: -->

sudo -i
shell
vcenter

<!-- Вот мы и под рутом. Выполняем команду: -->

/usr/lib/vmware-vmca/bin/certificate-manager

<!-- Запускается скрипт. -->

**_ Welcome to the vSphere 6.7 Certificate Manager _**

<!-- vcenter

Если вы пытаетесь заменить "протухший сертификат", то сначала воспользуйтесь пунктом 8 (Reset all Certificates). Скрипт сгенерирует для всех сервисов самоподписанные сертификаты. Перезагрузите vCenter. После этого продолжите по инструкции ниже.

Выбираем первый пункт. -->
<!--
Replace Machine SSL certificate with Custom Certificate -->

Вводим "1". Логинимся под administrator@vsphere.local.

<!-- Для начала генерируем CSR (Certificate Signing Request), нажимаем "1".

И тут нас начинают спрашивать: -->

**Output directory path:** /tmp
**Country:** RU
**Name:** FQDN адрес vcenter
**Organization:** YourCompany (название вашей компании)
**OrgUnit:** IT
**State:** Moscow
**Locality:** Moscow
**IPAddress:** IP адрес vCenter.
**Email:** admin@yourcompany.ru (почта)
**Hostname:** FQDN виртуалки vcenter, может не совпадать с Name
**VMCA Name:** FQDN адрес vcenter, обычно совпадает с Name

<!-- Отвечаем, получаем в итоге два файла: -->

/tmp/vmca_issued_csr.csr
/tmp/vmca_issued_key.key

<!-- Для выхода нажимаем "2". -->

<!-- Получаем содержимое vmca_issued_csr.csr: -->

cat /tmp/vmca_issued_csr.csr

<!-- Копируем содержимое CSR в буфер.

Открываем центр сертификации.

Request a certificate — запрашиваем сертификат.

Advanced certificate request. Заполняем поля.

Certificate Template: vSphere 7. Это наш шаблон в центре сертификации.
Attributes: дополнительные атрибуты в формате:
san:ipaddress=10.11.12.13&dns=vcenter.domain1.local&dns=vcenter.domain2.local&dns=vcenter&dns=vcenter-machine -->

<!-- Скачиваем DER 64 и конвертируем в PEM https://www.getssl.in/ssltools/ssl-converter.php -->

<!-- Снова идём в shell. -->

<!-- Создаём файл vmca_issued_csr.cer: -->

vim /tmp/vmca_issued_csr.cer

<!-- Вставляем внутрь содержимое полученного сертификата, сохраняем. -->

<!-- Создаём файл E-CA.cer: -->

vim /tmp/E-CA.cer

<!-- Вставляем внутрь содержимое корневого сертификата или цепочки (помним что переводили её в формат PEM), сохраняем. -->

<!-- Запускаем certificate-manager. -->

/usr/lib/vmware-vmca/bin/certificate-manager

<!-- Выбираем первый пункт. Replace Machine SSL certificate with Custom Certificate. Вводим "1". Логинимся под administrator@vsphere.local. Потом выбираем второй пункт. Import custom certificate(s) and key(s) to replace existing Machine SSL certificate. -->

<!-- Нас спрашивают: "Please provide valid custom certificate for Machine SSL", указываем -->

/tmp/vmca_issued_csr.cer.

<!-- Нас спрашивают: "Please provide valid custom key for Machine SSL", указываем -->

/tmp/vmca_issued_key.key.

<!-- Нас спрашивают: "Please provide the signing certificate of the Machine SSL certificate", указываем -->

/tmp/E-CA.cer.

Y.

<!-- Полетело обновление сертификата, можно идти пить кофе. -->

<!-- В конце увидите: -->

All tasks completed successfully

<!-- Можно перезагрузить vCenter 7. -->
