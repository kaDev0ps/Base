# Современный VPNротокол с веб интерфейсом
# Источник https://pikabu.ru/story/nastroyka_luchshikh_vpn_protokolov_vless_s_xtlsreality_na_servere_xray_s_ustanovkoy_cherez_3xui_panel_10782662

## Установка только от root!
apt install curl
bash <(curl -Ls https://raw.githubusercontent.com/mhsanaei/3x-ui/master/install.sh)
<!-- Если ошибка, то -->
sudo bash < <(curl -Ls https://raw.githubusercontent.com/mhsanaei/3x-ui/master/install.sh)
<!-- Заходим в фаервол и меняем порт -->
sudo x-ui 
21
<!-- Блокировка хакеров ботов по IP -->
apt install fail2ban -y && 
systemctl start fail2ban &&
systemctl enable fail2ban

<!-- Теперь заблокируем вход пользователя root по SSH. Для этого нужно отредактировать файл /etc/ssh/sshd_config -->

sudo nano /etc/ssh/sshd_config

<!-- Затем, найдите (перемещайтесь с помощью стрелок на клавиатуре) директиву PermitRootLogin и при необходимости раскомментируйте (если она закомментирована) -->
## Подключение

Идем в Подключения и начинается магия, жмякаем Добавить подключение
и заполняем поля:
Примечание: вводим любое название
Протокол: выбираем VLESS
Порт IP: оставить пустым
Порт: 443
Общий расход(GB): оставить пустым нужно для ограничения трафика
Дата окончания: оставить пустым нужно для ограничения по дате

Развернуть вкладку Клиент
E-mail: почту сюда вводить не нужно введите любое название или оставить сгенерированный текст

Включаем переключатель Reality
uTLS: выбираем Chrome
Dest: сюда вводим адрес сайта, которым хотим притворяться, например www.ferzu.com
Server Names: тоже вводим сайт согласно шаблону

И, в конце нажимаем Get New Cert, для того, чтобы панель сгенерировала нам ключи шифрования. Потом Создать.