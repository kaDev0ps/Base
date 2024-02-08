Nginx с помощью Let’s Encrypt:

- Откройте и отредактируйте файл шаблона SSL: sudo nano /etc/nginx/templates/ssl.tmpl.

Найдите следующие 2 строки:
ssl_certificate /etc/ssl/certs/iRedMail.crt;
ssl_certificate_key /etc/ssl/private/iRedMail.key;

Замените их на:
ssl_certificate /etc/letsencrypt/live/mail.ваш-домен.com/fullchain.pem;
ssl_certificate_key /etc/letsencrypt/live/mail.ваш-домен.com/privkey.pem;

Сохраните и закройте файл. Затем проверьте конфигурацию nginx и перезагрузите:
sudo nginx -t
sudo systemctl reload nginx

Вам также потребуется настроить Postfix и Dovecot для использования сертификата, выданного Let's Encrypt, чтобы почтовый клиент для настольных компьютеров не отображал предупреждение системы безопасности.

Отредактируйте основной файл конфигурации Postfix:
sudo nano /etc/postfix/main.cf

Найдите следующие 3 строки:
smtpd_tls_key_file = /etc/ssl/private/iRedMail.key
smtpd_tls_cert_file = /etc/ssl/certs/iRedMail.crt
smtpd_tls_CAfile = /etc/ssl/certs/iRedMail.crt

Замените их на:
smtpd_tls_key_file = /etc/letsencrypt/live/mail.your-domain.com/privkey.pem
smtpd_tls_cert_file = /etc/letsencrypt/live/mail.your-domain.com/cert.pem
smtpd_tls_CAfile = /etc/letsencrypt /live/mail.ваш-домен.com/chain.pem

Сохраните и закройте файл. Затем перезагрузите Postfix:
sudo postfix reload

Затем отредактируйте основной файл конфигурации Dovecot:
sudo nano /etc/dovecot/dovecot.conf

Найдите следующие 2 строки:
ssl_cert = </etc/ssl/certs/iRedMail.crt
ssl_key = </etc/ssl/private/iRedMail.key

Замените их на:
ssl_cert = </etc/letsencrypt/live/mail.your-domain.com/fullchain.pem
ssl_key = </etc/letsencrypt/live/mail.your-domain.com/privkey.pem

Сохраните и закройте файл. Затем перезагрузите голубятню:
sudo dovecot reload

Чтобы автоматически обновить сертификат, просто откройте файл crontab пользователя root:
sudo crontab -e

Затем добавьте следующую строку внизу файла:
@daily letsencrypt renew --quiet && /usr/sbin/postfix reload && /usr/sbin/dovecot reload && systemctl reload nginx
