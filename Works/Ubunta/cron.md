<!-- Варианты запуска cron -->

## От пользователя

crontab -e

## Общий файл

nano /etc/crontab

# Предопределенные макросы

Существует несколько специальных макросов расписания Cron, используемых для определения общих интервалов. Вы можете использовать эти ярлыки вместо указания даты в пять столбцов.

@yearly (или @annually) - запускать задание один раз в год в полночь (12:00) 1 января. Эквивалент 0 0 1 1 _.
@monthly - запускать заданное задание один раз в месяц в полночь первого дня месяца. Эквивалент 0 0 1 _ _.
@weekly - запускать задание раз в неделю в полночь воскресенья. Эквивалент 0 0 _ _ 0.
@daily - запускать задание один раз в день в полночь. Эквивалент 0 0 _ \* _.
@hourly - запускать заданную задачу один раз в час в начале часа. Эквивалент 0 _ \* \* \*.
@reboot - Запустить указанное задание при запуске системы (время загрузки).

# Частые команды

<!-- Запуск каждые 5 минут

*/5 * * * * echo "Privet" >> /home/ka/logs/privet.log

Запуск в нужное время каждый день

50 12 * * * echo "Privet" >> /home/ka/logs/privet.log

Запуск в нужное время каждый день

50 12,17 * * * echo "Privet" >> /home/ka/logs/privet.log -->

# Команда Crontab

<!-- crontab -e - отредактировать файл crontab или создать его, если он еще не существует.
crontab -l - Показать содержимое файла crontab.
crontab -r - удалить текущий файл crontab.
crontab -i - Удалить текущий файл crontab с запросом перед удалением.
crontab -u - Изменить другой файл crontab. Требуются права системного администратора.
Команда crontab открывает файл crontab с помощью редактора, указанного в переменных окружения VISUAL или EDITOR. -->

# Логи по cron хранятся в файле

sudo cat /var/log/syslog | grep CRON

# Проверяем статус cron

    systemctl status cron

Запускаем если надо.
systemctl start/restart cron

Если есть ошибки, то планировщик работать не будет

# Проверяем время

    date

Если нужно то настраиваем на верное

# Заходим в планировщик заданий и указываем путь к скрипту

    vim /etc/crontab
    02 10 * * * bash /home/ka/script.sh
    0 0 1 * * /usr/local/bin/serve

- Посмотреть весь список заданий
  cd /var/spool/cron

  Узнать все запущенные задачи у всех пользователей в системе поможет скрипт

# Определим цвета вывода

red='\e[0;31m'
RED='\e[1;31m'
green='\e[0;32m'
GREEN='\e[1;32m'
NC='\e[0m'

# Определим нашу функцию вывода списка всех задач cron у всех пользователей

function allcrontab() {
for user in $(cut -d':' -f1 /etc/passwd); do
        usercrontab=$(crontab -l -u ${user} 2>/dev/null)
        if [ -n "${usercrontab}" ]; then
echo -e "${RED}====== Start crontab for user ${NC}${GREEN}${user}${NC} ${RED}======${NC}"
crontab -l -u ${user} | sed '/ *#/d; /^ *$/d'
echo -e "${RED}====== End crontab for user ${NC}${GREEN}${user}${NC} ${RED}========${NC}\n"
fi
done
for crond in $(ls -L1 /etc/cron.d); do
        crondtab=$(cat "/etc/cron.d/${crond}" 2>/dev/null | egrep -Ev "^\s*(;|#|$)")
if [ -n "${crondtab}" ]; then
echo -e "${RED}====== Start cron.d ${NC}${GREEN}/etc/cron.d/${crond}${NC} ${RED}======${NC}"
echo "${crondtab}"
            echo -e "${RED}====== End cron.d ${NC}${GREEN}/etc/cron.d${crond}${NC} ${RED}======${NC}\n"
fi
done
}
