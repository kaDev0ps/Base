Предварительные требования
Для данного обучающего руководства вам потребуется следующее:

Один сервер Ubuntu 20.04, настроенный в соответствии с обучающим модулем Начальная настройка сервера Ubuntu 20.04, включая пользователя без прав root с привилегиями sudo и брандмауэр.

Зарегистрированное доменное имя. В этом обучающем руководстве мы будем использовать example.com. Вы можете купить доменное имя на Namecheap, получить его бесплатно на Freenom или воспользоваться услугами любого предпочитаемого регистратора доменных имен.

На вашем сервере должны быть настроены обе нижеследующие записи DNS. Если вы используете DigitalOcean, ознакомьтесь с нашей документацией по DNS для получения подробной информации по их добавлению.

Запись A, где example.com указывает на публичный IP-адрес вашего сервера.
Запись A, где www.example.com указывает на публичный IP-адрес вашего сервера.
Nginx, установленный в соответствии с указаниями руководства Установка Nginx в Ubuntu 20.04. Убедитесь, что для вашего домена существует серверный блок. В качестве примера мы используем в этом обучающем модуле /etc/nginx/sites-available/example.com.

# Шаг 1 — Установка Certbot

<!-- sudo apt install certbot python3-certbot-nginx -->

# Шаг 2 — Настройка конфигурации Nginx

серверный блок для вашего домена должен располагаться по адресу /etc/nginx/sites-available/example.com с уже настроенной директивой server_name

<!-- server_name example.com www.example.com; -->

Если нет, проведите обновление. Затем сохраните файл, закройте редактор и проверьте синтаксис внесенных правок конфигурации:

<!-- sudo nginx -t -->

Если вы получите сообщение об ошибке, откройте файл серверного блока заново и проверьте его на наличие опечаток или отсутствующих символов. Когда синтаксис файла конфигурации будет правильным, перезагрузите Nginx для загрузки новой конфигурации:

<!-- sudo systemctl reload nginx -->

Теперь Certbot сможет найти правильный серверный блок и автоматически обновлять его.

Теперь изменим настройки брандмауэра, чтобы он разрешал трафик HTTPS.

# Шаг 3 — Доступ к HTTPS через брандмауэр

Если вы включили брандмауэр ufw в соответствии с предварительными требованиями, вам нужно будет настроить его так, чтобы разрешить трафик HTTPS. К счастью, при установке Nginx регистрирует в ufw несколько профилей.

Вы можете просмотреть текущие настройки с помощью следующей команды:

<!-- sudo ufw status -->

Возможно профиль будет выглядеть так, т. е. на веб-сервере будет разрешен только трафик HTTP:

Output
Status: active

To Action From

---

OpenSSH ALLOW Anywhere  
Nginx HTTP ALLOW Anywhere  
OpenSSH (v6) ALLOW Anywhere (v6)  
Nginx HTTP (v6) ALLOW Anywhere (v6)
Чтобы разрешить трафик HTTPS, активируйте профиль Nginx Full и удалите лишний профиль Nginx HTTP:

sudo ufw allow 'Nginx Full'
sudo ufw delete allow 'Nginx HTTP'
sudo ufw allow 'OpenSSH'
Теперь ваш статус должен выглядеть следующим образом:

sudo ufw status
Output
Status: active

To Action From

---

OpenSSH ALLOW Anywhere
Nginx Full ALLOW Anywhere
OpenSSH (v6) ALLOW Anywhere (v6)
Nginx Full (v6) ALLOW Anywhere (v6)
Запустим Certbot и доставим наши сертификаты.

# Шаг 4 — Получение сертификата SSL

Certbot предоставляет широкий выбор способов получения сертификатов SSL с помощью плагинов: Плагин Nginx изменит конфигурацию Nginx и перезагрузит ее, когда это потребуется. Для использования этого плагина введите следующую команду:

<!-- sudo certbot --nginx -d example.com -d www.example.com -->

Эта команда запускает certbot с плагином --nginx, используя опцию -d для указания доменных имен, для которых мы хотим использовать сертификат.

Если это первый запуск certbot, вам будет предложено указать адрес эл. почты и принять условия обслуживания. После этого certbot свяжется с сервером Let’s Encrypt и отправит запрос с целью подтвердить, что вы контролируете домен, для которого запрашиваете сертификат.

Если это будет подтверждено, certbot запросит у вас предпочитаемый вариант настройки HTTPS:

Output
Please choose whether or not to redirect HTTP traffic to HTTPS, removing HTTP access.

---

1: No redirect - Make no further changes to the webserver configuration.
2: Redirect - Make all requests redirect to secure HTTPS access. Choose this for
new sites, or if you're confident your site works on HTTPS. You can undo this
change by editing your web server's configuration.

---

Select the appropriate number [1-2] then [enter] (press 'c' to cancel):
Выберите желаемый вариант, после чего нажмите ENTER. Конфигурация будет обновлена, а Nginx перезагрузится для получения новых настроек. Затем certbot завершит работу и выведет сообщение, подтверждающее завершение процесса и указывающее место хранения ваших сертификатов:

Output
IMPORTANT NOTES:

- Congratulations! Your certificate and chain have been saved at:
  /etc/letsencrypt/live/example.com/fullchain.pem
  Your key file has been saved at:
  /etc/letsencrypt/live/example.com/privkey.pem
  Your cert will expire on 2020-08-18. To obtain a new or tweaked
  version of this certificate in the future, simply run certbot again
  with the "certonly" option. To non-interactively renew _all_ of
  your certificates, run "certbot renew"
- If you like Certbot, please consider supporting our work by:

  Donating to ISRG / Let's Encrypt: https://letsencrypt.org/donate
  Donating to EFF: https://eff.org/donate-le
  Ваши сертификаты загружены, установлены и активированы. Попробуйте перезагрузить веб-сайт с помощью https:// и посмотрите на индикатор безопасности в браузере. Теперь в браузере должен отображаться символ замка, означающий, что сайт защищен надлежащим образом. Если вы протестируете свой сервер с помощью теста SSL Labs Server Test, он получит оценку A.

В заключение протестируем процесс обновления.

# Шаг 5 — Проверка автоматического обновления Certbot

Сертификаты Let’s Encrypt действительны только в течение 90 дней. Это сделано для стимулирования пользователей к автоматизации процесса обновления сертификатов. Установленный нами пакет certbot выполняет это автоматически, добавляя таймер systemd, который будет запускаться два раза в день и автоматически продлевать все сертификаты, истекающиее менее, чем через 30 дней.

Вы можете запросить статус таймера с помощью команды systemctl:

<!-- sudo systemctl status certbot.timer -->

Output
● certbot.timer - Run certbot twice daily
Loaded: loaded (/lib/systemd/system/certbot.timer; enabled; vendor preset: enabled)
Active: active (waiting) since Mon 2020-05-04 20:04:36 UTC; 2 weeks 1 days ago
Trigger: Thu 2020-05-21 05:22:32 UTC; 9h left
Triggers: ● certbot.service
Чтобы протестировать процесс обновления, можно сделать запуск «вхолостую» с помощью certbot:

<!-- sudo certbot renew --dry-run -->

Если ошибок нет, все нормально. Certbot будет продлевать ваши сертификаты, когда это потребуется, и перезагружать Nginx для активации изменений. Если процесс автоматического обновления когда-нибудь не выполнится, то Let’s Encrypt отправит сообщение на указанный вами адрес электронной почты с предупреждением о том, что срок действия сертификата подходит к концу.
