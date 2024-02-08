# Устанавливаем корректный часовой пояс:

timedatectl set-timezone Europe/Moscow

# Zimbra чувствительна к hostname. Первое, что нужно сделать перед установкой – привести файл /etc/hosts к виду:

127.0.0.1 localhost.localdomain localhost
10.40.0.80 zimbramail.home.local zimbramail

<!-- Имя должно быть такое же как /etc/hostname -->

# Теперь установим утилиту для синхронизации времени и запустим ее.

apt-get install chrony -y

systemctl enable chrony --now

# Брэндмауэр

<!-- Для нормальной работы Zimbra нужно открыть много портов:

25 — основной порт для обмена почтой по протоколу SMTP.
80 — веб-интерфейс для чтения почты (http).
110 — POP3 для загрузки почты.
143 — IMAP для работы с почтовым ящиком с помощью клиента.
443 — SSL веб-интерфейс для чтения почты (https).
465 — безопасный SMTP для отправки почты с почтового клиента.
587 — SMTP для отправки почты с почтового клиента (submission).
993 — SSL IMAP для работы с почтовым ящиком с помощью клиента.
995 — SSL POP3 для загрузки почты.
5222 — для подключения к Zimbra по протоколу XMPP.
5223 — для защищенного подключения к Zimbra по протоколу XMPP.
7071 — для защищенного доступа к администраторской консоли.
8443 — SSL веб-интерфейс для чтения почты (https).
7143 — IMAP для работы с почтовым ящиком с помощью клиента.
7993 — SSL IMAP для работы с почтовым ящиком с помощью клиента.
7110 — POP3 для загрузки почты.
7995 — SSL POP3 для загрузки почты.
9071 — для защищенного подключения к администраторской консоли.
В зависимости от утилиты управления фаерволом, команды будут следующие. -->

<!-- Порты для веб: -->

iptables -I INPUT -p tcp --match multiport --dports 80,443 -j ACCEPT

<!-- Порты для почты: -->

iptables -I INPUT -p tcp --match multiport --dports 25,110,143,465,587,993,995 -j ACCEPT

<!-- Порты для Zimbra: -->

iptables -I INPUT -p tcp --match multiport --dports 5222,5223,9071,7071,8443,7143,7993,7110,7995 -j ACCEPT

<!-- запрещаем INPUT любых пакетов сервером -->

iptables -P INPUT DROP

## Сохраняем правила:

apt-get install iptables-persistent
netfilter-persistent save

# Pадаем FQDN-имя для сервера:

myhost=mail.local
hostnamectl set-hostname $myhost

# Теперь открываем отредактируем файл:

vi /etc/hosts

... и добавляем:

10.200.202.147 mail.snab-avangard.ru mail

- где 10.200.202.147 — IP-адрес нашего сервера; mail — имя сервера; snab-avangard.ru — наш домен.

<!-- Не совсем очевидная проблема, но если в системе не будет пакета hostname, при попытке запустить установку зимбры, мы будем получать ошибку определения IP-адреса по имени. Устанавливаем пакет. -->

apt-get install hostname

# Скачиваем ссылку на дистрибутив

<!-- https://www.zimbra.com/downloads/zimbra-collaboration-open-source/ -->

wget https://files.zimbra.com/downloads/8.8.15_GA/zcs-8.8.15_GA_4179.UBUNTU20_64.20211118033954.tgz

# Распаковываем скачанный архив:

tar -xzvf zcs-\*.tgz

## Переходим в распакованный каталог:

cd zcs-\*/

## Запускаем установку почтового сервера соглашаясь с лицензией Y и выбираем нужные модули:

./install.sh

## Отказываемся от смены домена и настраиваем zimbra

<!-- последовательно/// Соглашаемся с путем сохранения и с изменением конфигурации -->

No -7 -4 r a Yes

<!-- Дожидаемся окончания установки, на запрос отправки уведомления можно ответить отказом:

Notify Zimbra of your installation? [Yes] n -->

# Zimbra DNSCache

<!-- Для корректной настройки службы dnscache необходимо сначала посмотреть Master DNS в настройках Zimbra: -->

su - zimbra -c "zmprov getServer $myhost | grep DNSMasterIP"

<!-- * где $myhostname — имя сервера, на котором установлена Zimbra (в данной конфигурации, mail.snab-avangard.ru). -->

<!-- Если видим: -->

zimbraDNSMasterIP: 127.0.0.53

<!-- Удалить данную запись: -->

su - zimbra -c "zmprov ms '$myhost' -zimbraDNSMasterIP 127.0.0.53"

<!-- И добавить свои рабочие серверы DNS, например: -->

su - zimbra -c "zmprov ms '$myhost' +zimbraDNSMasterIP 10.200.202.1"

su - zimbra -c "zmprov ms '$myhost' +zimbraDNSMasterIP 8.8.8.8"

su - zimbra -c "zmprov ms '$myhost' +zimbraDNSMasterIP 77.88.8.8"

# zimbraMtaLmtpHostLookup

<!-- Если наш сервер находится за NAT и разрешение IP происходит не во внутренний адрес, а внешний (можно проверить командой nslookup <имя сервера>), после настройки наш сервер не сможет принимать почту, а в логах мы можем увидеть ошибку delivery temporarily suspended: connect to 7025: Connection refused). Это происходит из-за попытки Zimbra передать письмо в очереди по внутреннему порту локальной почты 7025 (LMTP) на внешний адрес, который недоступен из NAT. Для решения проблемы можно использовать внутренний DNS с другими А-записями (split dns) или собственный поиск IP-адресов для lmtp, а не для DNS. Рассмотрим второй вариант — вводим две команды: -->

su - zimbra -c "zmprov ms $myhost zimbraMtaLmtpHostLookup native"

su - zimbra -c "zmprov mcf zimbraMtaLmtpHostLookup native"

<!-- * где $myhostname — имя нашего почтового сервера.

После перезапускаем службы зимбры: -->

su - zimbra -c "zmmtactl restart"

<!-- Настройка редерикта с http на https -->

su zimbra
zmprov ms mail.sppcm.ru zimbraReverseProxyMailMode redirect
zmproxyctl restart
netstat -lntp | grep 80

# Переходим в веб-панель строго по https://10.200.202.147:7071/zimbraAdmin/
