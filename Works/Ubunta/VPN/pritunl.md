# Pritunl
Это комбайн с веб мордой, есть OpenVPN и WireGuard. Для OpenVPN можно юзать любой клиент, а вот для WireGuard у них есть свой клиент, который работает с первым и вторым типом VPN. Качаем.

docker pull ghcr.io/jippi/docker-pritunl
<!-- Создаем необходимые папки и файлы, иначе VPN выдаст ошибку. -->

mkdir /var/lib/pritunl
mkdir /var/lib/mongodb
touch /var/lib/pritunl.conf

<!-- И запускаем. compose -->

version: '3'

services:
  pritunl:
    image: 'jippi/pritunl:latest'
    privileged: true
    ports:
      - '10080:80'
      - '10443:443'
      - '11194:1194/udp'
      - '11194:1194/tcp'
    volumes:
      - 'pritunl_data:/var/lib/pritunl'
      - 'pritunl_db:/var/lib/mongodb'

volumes:
  pritunl_data:
    driver: local
  pritunl_db:
    driver: local

sudo iptables -A INPUT -p tcp -s 95.214.116.10 --dport 10443 -j ACCEPT
sudo iptables -A INPUT -p udp --dport 11194 -j ACCEPT

<!-- на веб-сервере указываем порт 1194, а в настройках файлика клиентов редактируем на 11194. Это если у нас маршрутизация стоит -->


Запуск долгий, ждем пока в процессах появится pritunl-web и переходим по ссылке https://ip-вашего-сервера:10443. 

# Источник https://github.com/jippi/docker-pritunl/tree/master


docker exec ubuntu_pritunl_1 pritunl default-password

Получаем пароль и идем в админку и создаем сервера и привязываем организации, разберетесь, в гугле инфы много :)

# В помощь 


sudo usermod -aG docker $USER
sudo ln -s /usr/local/bin/docker-compose /usr/bin/docker-compose
sudo service docker restart