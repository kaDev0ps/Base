# Добавляем несколько пользователей

<!-- Залогинимся на сервер по SSH и создадим наш файлик. В этом примере мы создадим трех новых юзеров. Шаблон будет таким: -->

createAccount имя@domain.com HardPassw0rD# displayName 'Имя Фамилия' givenName Имя sn Фамилия

## Создадим файл

nano /tmp/account-create.zmp

<!-- Вставим код с нашими аккаунтами -->

createAccount hlebushkin@less-it.ru HardPassw0rD#1 displayName 'Денис Хлебушкин' givenName Денис sn Хлебушкин
createAccount suslikov@less-it.ru HardPassw0rD#2 displayName 'Андрей Сусликов' givenName Андрей sn Сусликов
createAccount bulkin@less-it.ru HardPassw0rD#3 displayName 'Евгений Булкин' givenName Евгений sn Булкин

<!-- Теперь запустим создание аккаунтов из под root -->

sudo su - zimbra -c "zmprov -f /tmp/account-create.zmp"

<!-- или от пользователя zimbra -->

zmprov -f /tmp/account-create.zmp
