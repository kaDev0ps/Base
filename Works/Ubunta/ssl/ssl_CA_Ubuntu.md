# Установка корневого сертификата ЦС в хранилище доверенных сертификатов

## Преобразование из DER-формы в PEM-форму

<!-- Преобразуйте сертификат в формате DER, вызываемый local-ca.derв форму PEM, следующим образом: -->

$ sudo openssl x509 -inform der -outform pem -in local-ca.der -out AVA-CA.crt

% Установка сертификата в форме PEM
% Предполагая, что сертификат корневого ЦС в формате PEM находится в local-ca.crt, выполните следующие действия, чтобы установить его.

% Примечание. Важно, чтобы .crt у файла было расширение, иначе он не будет обработан.

$ sudo apt-get install -y ca-certificates
$ sudo cp AVA-CA.crt /usr/local/share/ca-certificates
$ sudo update-ca-certificates

<!-- Местоположение хранилища доверенных сертификатов ЦС
Хранилище доверенных сертификатов ЦС, созданное с помощью, update-ca-certificatesдоступно в следующих местах: -->

В виде одного файла (пакет PEM) в /etc/ssl/certs/ca-certificates.crt
В качестве каталога сертификатов, совместимого с OpenSSL, в /etc/ssl/certs
