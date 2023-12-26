# Установка корневого сертификата ЦС в хранилище доверенных сертификатов

## Преобразование из DER-формы в PEM-форму

<!-- Преобразуйте сертификат в формате DER, вызываемый local-ca.derв форму PEM, следующим образом: -->

sudo openssl x509 -inform der -outform pem -in local-ca.der -out ZB-CA.crt
sudo openssl x509 -inform der -outform pem -in ZB-CA.cer -out ZB-CA.crt

<!-- Установка сертификата в форме PEM
Предполагая, что сертификат корневого ЦС в формате PEM находится в local-ca.crt, выполните следующие действия, чтобы установить его.

Примечание. Важно, чтобы .crt у файла было расширение, иначе он не будет обработан. -->

sudo apt-get install -y ca-certificates
sudo cp ZB-CA.crt /usr/local/share/ca-certificates
sudo update-ca-certificates

<!-- Местоположение хранилища доверенных сертификатов ЦС
Хранилище доверенных сертификатов ЦС, созданное с помощью, update-ca-certificatesдоступно в следующих местах: -->

В виде одного файла (пакет PEM) в /etc/ssl/certs/ca-certificates.crt
В качестве каталога сертификатов, совместимого с OpenSSL, в /etc/ssl/certs

# Для CentOS
Включаем динамическое обновление сертификатов:

$ sudo update-ca-trust enable
Потом копируем нужный сертификат в доверенные:

$ sudo cp ca.crt /etc/pki/ca-trust/source/anchors/
Обновляем доверенные сертификаты:

$ sudo update-ca-trust extract