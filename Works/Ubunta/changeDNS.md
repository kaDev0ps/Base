# Настройка DNS

<!-- Чтобы показать текущие DNS-серверы, которые используются для каждого интерфейса, используйте команду «resolvectl»: -->

resolvectl status

<!-- Для систем с Ubuntu 20.04 или новее используйте следующую команду: -->

systemd-resolve --status

Чтобы временно изменить DNS-сервер, отредактируйте /etc/resolv.confфайл.
nameserver 1.1.1.2
nameserver 1.0.0.2

# Измените DNS навсегда

<!-- Чтобы навсегда изменить свой DNS-сервер, установите resolvconfпакет с помощью следующей команды: -->

sudo apt-get install resolvconf

<!-- После того, как это будет установлено, отредактируйте /etc/resolvconf/resolv.conf.d/headфайл и добавьте в него те же nameserverстроки, что и так (при условии, что Cloudflare является поставщиком DNS): -->

nameserver 1.1.1.2
nameserver 1.0.0.2

<!-- Как только это будет сделано, запустите resolvconf.serviceследующую команду: -->

sudo systemctl enable --now resolvconf.service
