# Настраиваем VPN сервер L2TP и IPsec на Mikrotik RouterOS

<!-- Переходим в IP -> Pool мы создадим пул на 20 IP адресов. Назовем его l2tp_pool чтобы было понятно за что он отвечает. -->
<!-- Создаем профиль нашего L2TP сервера -->

переходим в PPP -> Profiles

<!-- В настройках профиля указываем: -->

Имя профиля: L2TP-server ( чтобы было понятно за что он отвечает)
Создаем pool адресов
Local adrdress: 192.168.10.1 начало диапазона IP адресов
Remote Address: l2tp_pool указываем название пула адресов который мы создали ранее
Change TCP MSS: yes

<!-- Переходим в вкладку Protocols -->

Use MPLS: yes
Use Compressiaon: no
Use VJ Compressiaon: no
Use Encription: yes

<!--
Переходим во вкладку Limits
Там все оставляем по умолчанию и в строке Only one оставляем default -->
VPN_nts_magnus

Создаем пользователя, переходим в PPP -> Secrets
Где указываем имя пользователя, пароль, указываем сервис к которому этот пользователь будет применен L2TP, а также профиль с которым будет работать наш L2TP сервер, мы его создали ранее (L2TP-server)

нам остается включить L2TP сервер, переходим в PPP -> Interface нажимаем кнопку L2TP server

Включаем сам сервер L2TP (ставим галку)
Включаем профиль, который мы создали ранее L2TP-server
Убираем все протоколы, оставляем mschap2 (остальные протоколы уже давно и успешно взломаны!)
Ставим галку Use IPsec
Придумываем IPsec Secret: по сути это парольная фраза, которая едина для всех.
Указываем MTU и MRU 1400, чтоб не было тормозов.

Нам с остается создать правила для фаерволла, чтобы мы могли достучаться до нашего L2TP сервера.
Переходим в IP -> Firewall -> Filter Rules
необходимо создать разрешающее правило INPUT для следующих портов и протоколов:

Протокол: 17 UDP
Разрешаем Dst.порты: 1701,500,4500
В качестве In.Interface указываем тот что подключен к интернет

Протокол: 50 ipsec-esp
В качестве In.Interface указываем тот что подключен к интернет

Также добавляем правило разрешающее ipcec
протокол: ipsec-esp
В качестве In.Interface указываем тот что подключен к интернет

Почти все готово, но если мы подключимся к нашему L2TP серверу, то не сможем выйти в интернет т.к. не создано разрешающее правило, переходим во вкладку NAT
Создаем новое правило
Chain: srcnat (т.к. NAT это у нас источник пакетов)
Src. Address: 192.168.10.0/24 (указываем подсеть которая у нас используется для выдачи IP адресов клиентам L2TP сервера)
Out.Inerface указываем интерфейс который у нас подключен к интернет.

переходим во вкладку Action и в строке Action указываем маскарадинг

# настройка RADIUS для LDAP аудентификации
Авторизовывать пользователей мы будем через Windows Active Directory, для этого нам потребуется Radius, переходим

IP->PPP->Secrets и как указано на рисунке включаем Radius аутентификацию
PPP&Autentification&Accouting - Ставим галочку Use Radius

Заходим в главном меню RADIUS и создаем новый.

Далее нам потребуется настроить работу Radius, радиус будет использоваться только для PPP соединений,

- Ставим ppp
- Указываем Адрес контроллера домена
- Указываем новый Secret

Source IP address of the packets sent to RADIUS server оставляем 0.0.0.0/0

# Настройка сервера AD
Управление - Добавить роли и компоненты
Выбираем - Служба полиики сети и доступа

После устаноки заходим:
Средства - Сервер политики сети

В верхнем меню 4 иконка в виде квадрата. ЭТО ВАЖНО!
Зарегистрировать сервер AD

Щелкаем ПКМ на AD(локально) - Свойства

Если работаем с Mikrotik, то достаточно оставить только порты 1812 и 1813

<!-- Создаем радиус клиента -->
# Источник https://habr.com/ru/articles/672738/
RADIUS-клиенты
ПКМ - Новый Документ
Заполняем Имя, любое, IP Микротика, наш секрет для связи с Микротиком NTS_m@gnu$

создаём группу доступа к VPN, добавляем в неё пользователей, кто будет иметь доступ к подключению.

Вернёмся в NPS.

Идём в "Политики->Сетевые политики", создаём новую
Указываем в политиках, что пускать будем конкретную группу.

Доходим до момента типы EAP
Оставляем только первый. Галочка на MS-CHAP-v2

Политики - Политики запросов - Тип сервера доступа к сети ставим Сервер удаленного доступа.
Политики - Сетевые политики - Ставим галочку на Игнорировать свойства удаленного доступа
# Настройка ограничений
Выбираем тип порта NAS и ставим галочку на Виртуальная VPN

В настройках параметров выбираем пункт Шифрование. Далее указываем только Сильное и Стойкое шифрование.

# Создаем политику запросов
Например Понятное имя клиента укажем Mikrotik

# Идем на Микротик

Cjplftv ghfdbk








# Настройка клиентского пакета через CMAK
Управление - Средства - Пакет администрирования пакета подключений
Листаем до указания имени и придумываем файл

Листаем до поддержки VPN подключений

Ставим галочку на Телефонной книге и указываем IP микротика

На следующем шаге ПКМ на созданной записи и указываем во вкладке Общие только IPv4. Заходим на вкладку Безопасность - Настройки и ставим галочку на использование общего ключа LT2P. Во вкладке IPv4 ставим галочку использовать общий шлюз клиента

Общий ключ будет тот, который дадим клиенту. NTS_vpn_2024!
Создаем пин-код 2024

Жмем Далее. Соглашаемся с Автоматическим скачиванием телефонной кники