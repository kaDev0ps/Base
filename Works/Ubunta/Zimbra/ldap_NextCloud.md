# Настройка интеграции Nextcloud с LDAP

<!-- необходимо убедиться в наличие модуля php-ldap -->

php -m | grep ldap

<!-- Если мы получим в ответ пустую строку, то необходимо установить пакет php-ldap и перезапустить службу, обрабатывающую скрипты php. -->

apt install php-ldap
apt install php8.2-ldap
systemctl restart php8.2-fpm
systemctl restart apache2

<!-- У нас должна быть учетная запись на сервере LDAP с правами чтения (DN user). От данной учетной записи будет выполняться подключение к серверу каталогов.

В моем примере будет создана запись с именем nextcloud. -->

# Для включения приложения интеграции LDAP вводим команду...

sudo -u www-data php /var/www/nextcloud/occ app:enable user_ldap

<!-- * где  /var/www/nextcloud — путь, в котором установлен nextcloud. -->

# Настройка интеграции

<!-- Для дальнейшего удобства, определим переменную с пользователем, от которого работает веб-сервер. -->

webuser=www-data

<!-- Теперь создаем конфигурацию: -->

sudo -u ${webuser} php /var/www/nextcloud/occ ldap:create-empty-config

<!-- Мы должны увидеть в ответ что-то на подобие: -->

Created new configuration with configID s01

<!-- * в данном примере создана конфигурация с идентификатором s01 — последующие команды будут вводиться с данным ID.

Посмотреть информацию о созданной конфигурации можно командой: -->

sudo -u ${webuser} php /var/www/nextcloud/occ ldap:show-config

<!-- Теперь задаем настройки. -->

<!-- Указываем ldap-сервер для подключения и порт: -->

sudo -u ${webuser} php /var/www/nextcloud/occ ldap:set-config s01 ldapHost "10.200.202.10"

sudo -u ${webuser} php /var/www/nextcloud/occ ldap:set-config s01 ldapPort "389"

<!-- * в данном примере мы сервер dmosk.local и порт 389 (не зашифрованные запросы к ldap). -->

<!-- Задаем пользователя, от которого будем выполнять подключения к каталогу: -->

sudo -u ${webuser} php /var/www/nextcloud/occ ldap:set-config s01 ldapAgentName "nextcloud"

sudo -u ${webuser} php /var/www/nextcloud/occ ldap:set-config s01 ldapAgentPassword "AVA_cloud"; history -d $((HISTCMD-1))

<!-- * мы указали, что подключение будет выполняться от пользователя bind с паролем bind. Дополнительная команда history -d $((HISTCMD-1)) удалить из истории строку с паролем. -->

<!-- Задаем область поиска учетных записей: -->

sudo -u ${webuser} php /var/www/nextcloud/occ ldap:set-config s01 ldapBase "ou=Office Users,dc=snab,dc=local"

<!-- Задаем фильтры пользователя, поля для логина и класса объекта: -->

sudo -u ${webuser} php /var/www/nextcloud/occ ldap:set-config s01 ldapLoginFilter "(&(|(objectclass=person))(uid=%uid))"

sudo -u ${webuser} php /var/www/nextcloud/occ ldap:set-config s01 ldapUserFilter "(|(objectclass=person))"

sudo -u ${webuser} php /var/www/nextcloud/occ ldap:set-config s01 ldapUserFilterObjectclass "person"

<!-- Указываем поле для атрибута электронной почты: -->

sudo -u ${webuser} php /var/www/nextcloud/occ ldap:set-config s01 ldapEmailAttribute "mail"

<!-- Проверяем конфигурацию: -->

sudo -u ${webuser} php /var/www/nextcloud/occ ldap:test-config s01

<!-- Мы должны увидеть: -->

The configuration is valid and the connection could be established!

<!-- Если же мы увидим ошибку, смотрим лог и устраняем проблемы: -->

tail /var/www/nextcloud/data/nextcloud.log

<!-- Активируем конфигурацию: -->

sudo -u ${webuser} php /var/www/nextcloud/occ ldap:set-config s01 ldapConfigurationActive "1"

<!-- Пользователи будут загружаться из каталога ldap, но их идентификаторы будут отображаться в виде UID — это произвольные набор цифр и букв и его использовать не удобно. Чтобы изменить атрибут для имени nextcloud, вводим: -->

sudo -u ${webuser} php /var/www/nextcloud/occ ldap:set-config s01 ldapExpertUsernameAttr "sAMAccountName"
