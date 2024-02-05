# Добавление хостов

https://prometheus.io/download/

`wget https://github.com/prometheus/node_exporter/releases/download/v1.7.0/node_exporter-1.7.0.linux-amd64.tar.gz`
Распаковываем 
`tar xvfz node_ex*`

Копируем содержимое директории: 
`cp node_exporter /usr/local/bin/`
Создаем пользователя для node exporter: 
`useradd --no-create-home --shell /bin/false nodeusr`
Задаем владельца для исполняемого файла: 
`chown -R nodeusr:nodeusr /usr/local/bin/node_exporter`
Создаем unit-файл для запуска node exporter в качестве демона: 
`vim /etc/systemd/system/node_exporter.service`
Добавим в файл следующее:
<!-- 
[Unit]
Description=NodeExporter

[Service]
TimeoutStartSec=0
User=nodeusr
ExecStart=/usr/local/bin/node_exporter \
--web.listen-address=:9100

[Install]
WantedBy=multi-user.target -->

Перечитываем конфигурацию 
`systemctl daemon-reload`
Настраиваем автозапуск: 
`systemctl enable node_exporter`
Запускаем node exporter: 
`systemctl start node_exporter `
Команда должна вернуть статус Active.
Теперь переходим в браузер и вводим в строку поиска запрос: IP-адрес сервера:9100/metrics. Загрузится страница с метриками

Редактируем файл на хосту Prometheus в такойже папке. Добавляем jobs в наш конфигурацию 
`nano prometheus.yml`
<!-- 
- job_name: 'ubuntu-test'
  static_configs:
    - targets: ['172.21.0.14:9100'] -->

Пеезагружаем конейнер
Проверяем видит ли его Prometheus
http://172.21.0.102:9090/targets?search=