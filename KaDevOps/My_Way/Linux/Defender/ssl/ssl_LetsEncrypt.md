# Создание сертификата с помощью Let’s Encrypt

<!-- Сначала установим Certbot: -->

cd /usr/local/sbin
sudo wget https://dl.eff.org/certbot-auto
sudo chmod a+x /usr/local/sbin/certbot-auto

<!-- Получим сертификат: -->

sudo certbot-auto certonly --webroot --agree-tos --email myemail@domen.ru -w
/var/www/ -d domen.ru -d www.domen.ru
sudo certbot-auto --apache

<!-- ● --webroot — специальный ключ, повышающий надежность работы Certbot под Nginx;
● --agree-tos — автоматическое согласие с Условиями предоставления услуг (Terms of Services);
● --email myemail@domen.ru — ваш email. Будьте внимательны: его нельзя изменить. Он
потребуется для восстановления доступа к домену и для его продления;
● -w /var/www — указываем корневую директорию сайта;
● -d domen.ru — через ключ -d указываем, для каких доменов запрашиваем сертификат.
Начинать надо c домена второго уровня domen.ru и через такой же ключ указывать
поддомены: например, -d www.domen.ru -d opt.domen.ru.
Команда sudo certbot-auto --apache внесет необходимые изменения в конфигурационные файлы
Apache.

Когда установка SSL-сертификата Apache Ubuntu будет завершена, вы найдете созданные файлы
сертификатов в папке /etc/letsencrypt/live/domen.ru/. В этой папке будет четыре файла:
● cert.pem — ваш сертификат домена;
● chain.pem — сертификат цепочки Let's Encrypt;
● fullchain.pem — cert.pem и chain.pem вместе;
● privkey.pem — секретный ключ вашего сертификата.-->

<!-- Чтобы запустить процесс обновления для всех настроенных доменов, выполните: -->

sudo certbot-auto renew

<!-- Если сертификат был выдан недавно, команда проверит его дату истечения и выдаст сообщение, что
продление пока не требуется. Если вы создали сертификат для нескольких доменов, в выводе будет
показан только основной. Но обновление будет актуально для всех. -->

<!-- Самый простой способ автоматизировать этот процесс — добавить вызов утилиты в планировщик
cron. Для этого выполним команду: -->

crontab -e

<!-- И добавим строку: -->

30 2 \* \* 1 /usr/local/sbin/certbot-auto renew

<!-- Команда обновления будет выполняться каждый понедельник в 2:30. Информация про результат
выполнения будет сохраняться в файл /var/log/le-renewal.log. -->