# Настройка уведомлений
Заходим в Alerting/Contact points
Добавляем канал Телеграм

В телеграме создаем бота в @BotFather
<!-- 
/newbot
ZB_Prometheus
ZB_Prometheus_bot
6679814931:AAHFKCEw6XZjw60bjm6Mdm4RmAePP682AzY -->

Создаем новый канал
Делаем бота админом канала

<!-- Бот создан, получаем токен "Use this token to access the HTTP API". Копируем его и вставляем в Zabbix в разделе Administration → Media types → Telegram → Parameters → Token. -->
<!-- Для получения group id в Telegram добавляю бота @myidbot в группу и отправляю команду: -->

/getgroupid@myidbot

**Настраиваем разрешение на отправку сообщений в группу**
Для этого:
Перейдите в BotFather.
Напишите команду /mybots.
Выберите нужного бота
Перейдите в Bot Settings → Group Privacy.
Выберите Turn off.
Должна появиться фраза Privacy mode is disabled for Bot.

Отправляем тестовое сообщение

# Переходим в Notification policies
Меняем Default policy на нашу и сохраняем.
