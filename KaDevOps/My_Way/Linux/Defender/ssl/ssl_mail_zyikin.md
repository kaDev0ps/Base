# Обновление

letsencrypt renew --quiet && postfix reload && dovecot reload && systemctl reload nginx
