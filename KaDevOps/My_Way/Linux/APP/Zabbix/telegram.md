# Настройка Телеграм

<!-- Создаем бота в @BotFather -->

/start
/newbot
Zabbix internet-lab.ru
zabbix_internet_lab_ru_bot

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

# Добавляем в группу бота LeadConverterToolkitBot

узнаем ID группы
/get_chat_id
