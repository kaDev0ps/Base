# Устанавливаем скрипт отправки сообщений c графиками Zabbix

<!-- Скачиваем скрипт с гитлаба -->

cd ~
git clone https://github.com/ableev/Zabbix-in-Telegram

<!-- Устанавливаем Python -->

sudo apt install python3-pip
cd Zabbix-in-Telegram/
pip3 install -r requirements.txt

<!-- Копируем в директорию /usr/lib/zabbix/alertscripts файлы zbxtg.py и zbxtg_settings.example.py из скачанного с github проекта. Последний переименовываем в zbxtg_settings.py. Назначаем пользователя zabbix владельцем этих файлов. -->

sudo mv zbxtg_settings.example.py zbxtg_settings.py
sudo cp zbxt\*.py /usr/lib/zabbix/alertscripts
chown -R zabbix. /usr/lib/zabbix/alertscripts

<!-- Приводим содержимое zbxtg_settings.py примерно к такому виду. -->

tg_key = "1393668911:AAHtEDfgHJHYDVDwxKfvH1WR6-vdNw" # telegram bot api key
zbx_server = "http://127.0.0.1/zabbix/" # zabbix server full url
zbx_api_user = "Admin"
zbx_api_pass = "zabbix"
zbx_server_version = 4 # for Zabbix 4.x version, default will be changed in the future with this
zbx_db_host = "localhost"
zbx_db_database = "zabbix"
zbx_db_user = "root"
zbx_db_password = "123"

<!-- На 797 строчке должен быть код -->

    if tg.type == "group":
        uid = zbx_to
    if tg.type == "private":
        zbx_to = zbx_to.replace("@", "")

<!-- Проверим из консоли работу скрипта. -->

/usr/lib/zabbix/alertscripts/zbxtg.py "-1001590367971" "тест" "тестовое сообщение" --debug --group
sudo -u zabbix ./zbxtg.py "-1001590367971" test "$(echo -e 'zbxtg;graphs: \nzbxtg;graphs_period=3600\nzbxtg;itemid:3082\nzbxtg;title:ololo')" --debug --group

<!-- то откройте скрипт и в самом начале вместо python напишите python3: -->

#!/usr/bin/env python3

<!-- Так же нам нужно настроить шаблоны оповещений. Я предлагаю следующий вариант. Тип сообщений Проблема. -->

# Шаблон Проблема

{{WARNING}} {HOST.NAME} - {TRIGGER.STATUS}

zbxtg;graphs
zbxtg;graphs_width=700
zbxtg;graphs_height=300
zbxtg;graphs_period=1800
zbxtg;itemid:{ITEM.ID1}
zbxtg;title:{HOST.HOST} - {TRIGGER.NAME}
{{{TRIGGER.SEVERITY}}} {TRIGGER.NAME}
{{bomb}} {HOSTNAME} ({HOST.IP})
{{DISASTER}} {ITEM.VALUE1} ({TIME})

# Шаблон Восстановление

{{OK}} {HOST.NAME} - {TRIGGER.STATUS}

zbxtg;graphs
zbxtg;graphs_width=700
zbxtg;graphs_height=300
zbxtg;graphs_period=1800
zbxtg;itemid:{ITEM.ID1}
zbxtg;title:{HOST.HOST} - {TRIGGER.NAME}
{{{TRIGGER.SEVERITY}}} {TRIGGER.NAME}
{{Information}} {HOSTNAME} ({HOST.IP})
{{time}} {DATE} {TIME}

## Пояснения

{{WARNING}} - макрос для выставления иконки emoji с восклицательным знаком
zbxtg;graphs - указывает, что будем отправлять график
zbxtg;itemid:{ITEM.ID1} - параметр автоматически определяет itemid для графика на основании информации об итеме в триггере, можно указать нужный itemid вручную
zbxtg;title - задает имя для заголовка графика
Все остальное понятно по смыслу, так как относится к стандартным макросам zabbix. Вот полный список параметров, которые поддерживает скрипт:

zbxtg;graphs включает отправку графиков
zbxtg;graphs_period=10800 период за который строится график
zbxtg;graphs_width=700 ширина графика
zbxtg;graphs_height=300 высота графика
zbxtg;itemid:{ITEM.ID1} выбор itemid для графика на основе триггера
zbxtg;title:{HOST.HOST} - {TRIGGER.NAME} заголовок графика
zbxtg;debug включает режим отладки, некоторая дополнительная информация сохраняется в tmp_dir
zbxtg;channel включает возможность отправки оповещения в telegram channel
zbxtg;to:username1,username2,username3 можно сразу в шаблоне указать, кому будут отправляться оповещения
zbxtg;to_group:Group то же самое, что выше, только для групп

zbxtg.py

{ALERT.SENDTO}
{ALERT.SUBJECT}
{ALERT.MESSAGE}

emoji_map = {
"Disaster": "🔥",
"High": "🛑",
"Average": "❗",
"Warning": "⚠️",
"Information": "ℹ️",
"Not classified": "🔘",
"OK": "✅",
"PROBLEM": "❗",
"info": "ℹ️",
"WARNING": "⚠️",
"DISASTER": "❌",
"bomb": "💣",
"fire": "🔥",
"time": "⏰",
"hankey": "💩",
}

Данные для Ввода 1,2,3, по которым ведется мониторинг и настроены уведомления с учетом поправки на нужный коэффициент по Вводу 1,2.

Ежедневные отчеты

P_L1 Активная однофазная мощность
P_L2 Активная однофазная мощность
P_L3 Активная однофазная мощность

S_L1 Полная однофазная мощность
S_L2 Полная однофазная мощность
S_L3 Полная однофазная мощность

AP_energy_L1 Активная фазная энергия, (показания за час)
AP_energy_L2 Активная фазная энергия, (показания за час)
AP_energy_L3 Активная фазная энергия, (показания за час)

Оповещения при аварии с уведомлением при завершении аварии

AP_energy_L1_Dangerous Триггер по всплеску электричества в 2 раза от максимального значения
AP_energy_L2_Dangerous Триггер по всплеску электричества в 2 раза от максимального значения
AP_energy_L3_Dangerous Триггер по всплеску электричества в 2 раза от максимального значения

Urms L1 Триггер оповещения отсутствует электричество на вводе
Urms L2 Триггер оповещения отсутствует электричество на вводе
Urms L3 Триггер оповещения отсутствует электричество на вводе
