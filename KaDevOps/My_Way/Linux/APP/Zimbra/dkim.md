# Генерируем ключ и извлекаем данные

    # DKIM выдается на домен, а не на субдомен! Поэтому генерируем подпись на sppcm.ru
    su zimbra
    $ /opt/zimbra/libexec/zmdkimkeyutil -a -d sppcm.ru
    $ /opt/zimbra/libexec/zmdkimkeyutil -q -d sppcm.ru

    # Для удаления используем
    $ /opt/zimbra/libexec/zmdkimkeyutil -r -d sppcm.ru

# Добавляем на DNS хостинг

    # Создаем запись. Значение без кавычек и пробелов
    E88B1788-F58E-11ED-9C31-E22AD6EA0AC6._domainkey
    TXT
    v=DKIM1; k=rsa; p=…………. (здесь публичная часть ключа разделенный кавычками)

# проверяем открытый и закрытый ключи

    https://dkimcore.org/c/keycheck

    # вставляем селектор
    1E243DBA-F61C-11ED-A5A5-9F74E3EA0AC6

    # основной домен
    sppcm.ru

    # Сервис проверит есть ли открытый ключ на хостинге
    # Если ошибок нет то переходим ко второму сервису
    https://www.mail-tester.com/

    # При получении письма он проверяет закрытый ключ с открыты. Все должно быть хорошо.

# SPF запись

<!-- Задает для домена следующих разрешенных отправителей электронной почты:

Сервер mail.solarmora.com.
Сервер с IP-адресом 192.72.10.10.
Google Workspace -->

v=spf1 a:m.snabavangard.ru ip4:10.200.202.147 ~all

OLD DKIM

v=DKIM1; k=rsa; t=s; p=MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCxEWfHuUDYZSv/mZ+eWAENWLN0xzHAxmY66SoOR4Qy4eKVoh85LgXoPjmmaucMNrHpBgLs5KzdlEjS1s5/btytgteygbA+iuYxyIga0dj9q76EzJm+Vgjw6t3ZgkL8FMPAYn03jTw+t3nH7I+CV4QViH6o1qjbq44QWcroSTdAkQIDAQAB
