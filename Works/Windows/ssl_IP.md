# Создание SSL сертификата с альтернативными доменными именами

<!-- SAN (Subject Alternative Name)

В этом нам поможет утилита certutil.

Запускаем в центре сертификации командную строку под администратором. Выполняем команду: -->

certutil -setreg policy\EditFlags +EDITF_ATTRIBUTESUBJECTALTNAME2

<!-- После выполнения команды нужно перезапустить службу Certification Authority. Делаем это сразу из командной строки: -->

net stop certsvc && net start certsvc

<!-- Для добавления IP и альтернативного доменного имени при генерации сертификата указываем дополнительные атрибуты. -->

san:dns=spbslsv-vdi-hv1-ipmi&ipaddress=10.15.22.41
