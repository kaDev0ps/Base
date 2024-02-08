# Настройка автоматического создания почтовых ящиков

## Zimbra умеет в 3 варианта создания ящиков:

<!-- EAGER – полностью автоматический, который через определенные промежутки времени просматривает AD и создает ящики для новых пользователей.

LAZY – полуавтоматический, который создает почтовый ящик при первом входе пользователя на почтовый сервер под учетными данными домена.

MANUAL – ручной поиск и отбор учетных записей, для которых нужно создать почтовые ящики.

По понятным причинам режим MANUAL пригоден только для маленьких компаний с вялой текучкой кадров. LAZY-режим подходит для использования почты с web-интерфейсом, без подключения почтового клиента. Меня не устраивали оба варианта, так как стояла задача автоматизировать по-максимуму (автоматическая установка клиентского приложения Zimbra Desktop, чтобы пользователю нужно было просто ввести логин-пароль и получить доступ к почте). Поэтому только EAGER. Да он и удобнее, если честно.

Для удобства правки и применения параметров проще и удобнее создать файл. Пусть будет /tmp/prov

Наполнение файла следующее:

Содержимое файла -->

md zimbramail.home.local zimbraAutoProvAccountNameMap "samAccountName"
md zimbramail.home.local +zimbraAutoProvAttrMap description=description
md zimbramail.home.local +zimbraAutoProvAttrMap displayName=displayName
md zimbramail.home.local +zimbraAutoProvAttrMap givenName=givenName
md zimbramail.home.local +zimbraAutoProvAttrMap cn=cn
md zimbramail.home.local +zimbraAutoProvAttrMap sn=sn
md zimbramail.home.local zimbraAutoProvAuthMech LDAP
md zimbramail.home.local zimbraAutoProvBatchSize 300
md zimbramail.home.local zimbraAutoProvLdapAdminBindDn "CN=ZimbraLDAP,OU=HOME_Users,DC=home,DC=local"
md zimbramail.home.local zimbraAutoProvLdapAdminBindPassword qwe123
md zimbramail.home.local zimbraAutoProvLdapBindDn "admin@zimbramail.home.local"
md zimbramail.home.local zimbraAutoProvLdapSearchBase "CN=HOME_Users,dc=home,dc=local"
md zimbramail.home.local zimbraAutoProvLdapSearchFilter "(&(objectClass=user)(objectClass=person))"
md zimbramail.home.local zimbraAutoProvLdapURL "ldap://home.local:389"
md zimbramail.home.local zimbraAutoProvMode EAGER
md zimbramail.home.local zimbraAutoProvNotificationBody "Your account has been auto provisioned. Your email address is ${ACCOUNT_ADDRESS}."
md zimbramail.home.local zimbraAutoProvNotificationFromAddress prov-admin@zimbramail.home.local
md zimbramail.home.local zimbraAutoProvNotificationSubject "New account auto provisioned"
ms zimbramail.home.local zimbraAutoProvPollingInterval "1m"
ms zimbramail.home.local +zimbraAutoProvScheduledDomains "zimbramail.home.local"

<!-- Еще немного теории:

В данном файле содержатся команды для присвоения переменных. Так, например, параметр zimbraAutoProvAttrMap cn=cn означает, что Zimbra будет формировать свои ящики таким образом, что «выводимое имя (CN в AD) будет подставляться в поле «выводимое имя» в Zimbra.

Параметр zimbraAutoProvLdapAdminBindDn отвечает за учетную запись, которую будет использовать Zimbra для доступа к каталогу AD. В Данном случае «CN=ZimbraLDAP,OU=HOME_Users,DC=home,DC=local», что значит следующее: будет использоваться учетная запись с отображаемым именем ZimbraLDAP, хранящаяся в OU HOME_Users, который расположен в корне домена home.local

zimbraAutoProvLdapAdminBindPassword хранит пароль от учетной записи ZimbraLDAP

zimbraAutoProvLdapBindDn хранит в себе учетную запись администратора сервера Zimbra для домена zimbramail.home.local

zimbraAutoProvLdapSearchBase отвечает за OU, в котором Zimbra будет искать доменные учетные записи для создания почтовых ящиков. В моем случае это тот же контейнер, в котором лежит и пользователь ZimbraLDAP

zimbraAutoProvPollingInterval это период обращения к AD на предмет поиска появления новых учетных записей.

С остальными параметрами все и так понятно.

На сайте разработчика написано, что если вы используете версию Zimbra до 8.0.8, то для работы EAGER-режима нужно будет еще устанавливать параметр zimbraAutoProvLastPolledTimestamp в пустое значение «», иначе больше одного раза он не отработает.

Далее выполняем команду: -->

$ su – zimbra
$ zmprov < /tmp/prov

<!-- Для просмотра всех значений zmprov можно ввести команду: -->

$ su – zimbra
$ zmprov gd aimbramail.home.local

<!-- Править параметры можно с помощью той же утилиты zmprov, переписывая значения переменных (утилита — действие — домен — переменная — значние), может помочь для дебагинга: -->

$ su – zimbra
$ zmprov md zimbramail.home.local zimbraAutoProvBatchSize 200
