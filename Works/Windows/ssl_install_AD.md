# Добавляем роли

Служба сертификации

# Выделяем галочками дальше

- Центр сертификации
- Веб служба регистрации сертификатов
- Веб служба политик
  <!-- Далее все по умолчанию. После установке жмем на желтый значок и выбираем настройки на AD -->
  Сперва выбираем службы роли:
- Центр Сертификации

# На следующем шаге

- ЦС предприятия

# Указываем тип ЦС

- Корневой ЦС

# Тип ключа

Создаем новый закрытый ключ

<!-- Шифрование по умолчанию -->

# Срок ключа

ставим 20 лет

<!-- Далее все по умолчанию. После установки выскочит окошко с предложением настроить доп роли. Настраиваем -->

Отмечаем галочками все кроме :

- Служба регистрации на сетевых устройствах
- Сетевой передатчик

# Указываем ЦС

- Имя ЦС

# Проверка подлинности

- Подлинность Windows

# Учетная запись службы CES

- Выбираем учетку, которая в группе IIS_USRS

# Тип проверки подлинности

- Встроеная проверка

# Включить обновление на основе ключей

- Ставим галочку
<!-- Заканчиваем установку -->

# Создание SSL сертификата с альтернативными доменными именами

<!-- SAN (Subject Alternative Name)

В этом нам поможет утилита certutil.

Запускаем в центре сертификации командную строку под администратором. Выполняем команду: -->

certutil -setreg policy\EditFlags +EDITF_ATTRIBUTESUBJECTALTNAME2

<!-- После выполнения команды нужно перезапустить службу Certification Authority. Делаем это сразу из командной строки: -->

net stop certsvc && net start certsvc

<!-- Для добавления IP и альтернативного доменного имени при генерации сертификата указываем дополнительные атрибуты. -->

<!-- san:dns=m.snabavangard.ru&dns=mail.snabavangard.local&ipaddress=10.200.202.147 -->

# Изменение даты окончания срока действия сертификатов, выданных центром сертификации

<!-- Чтобы изменить параметры срока действия для ЦС, выполните следующие действия. -->

<!-- Найдите и щелкните следующий раздел реестра: -->

HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\CertSvc\Configuration\<CAName>

В правой области дважды щелкните ValidityPeriod.

<!-- В поле данных "Значение " введите одно из следующих значений и нажмите кнопку "ОК": -->

ValidityPeriodUnits.

<!--
В поле данных " Значение" введите нужное числовое значение и нажмите кнопку " ОК". Например, введите 5. -->

net stop certsvc && net start certsvc

# Дальше все по умолчанию. Заходим в Шелл вводим

mmc
Нажимаем Ctrl+M

# Добавляем

Сертификаты и шаблоны сертификатов

- Через поиск переходим в диспетчер служб IIS
- Также через Консоль сервера открывает Центр сертификации

# Настройка центра сертификации

Шаблоны > Управление > Web Server

- Делаем копию и изменяем период
- Во вкладке безопасность ставим галочки Чтение и Заявка
  <!-- Сохраняем шаблон и идем в консоль, которую открыли через mmc -->
  Добавляем наш шаблов в список Шаблонов в ЦС

# настройка Console

<!-- Теперь делаем сертификат на этот же сервер, чтоб было красиво -->

Снртификаты > Личные > Сертификаты
Жмеж ПКМ > Запросить новый сертификат

<!-- Выбираем Свойство созданной копии и вносим правки во вкладках -->

Субъект

- Страна - RU
- Общее имя=NAMEAD

# Переходим в раздел IIS

Web Sites

- Edit Bindigs
  <!-- Добавляем https выбираем наш сертификат и в хосте указываем имя AD -->
  Переходим по адресу имяСервера/certsrv
