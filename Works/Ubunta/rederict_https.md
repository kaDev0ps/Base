# Установка перенаправления на https

vi /etc/apache2/sites-enabled/nextcloud.conf

<VirtualHost \*:80>
Define root_domain cloud.snabavangard.ru
ServerName ${root_domain}
Redirect permanent / https://${root_domain}
</VirtualHost>

<VirtualHost \*:443>
Define root_domain cloud.snabavangard.ru
Define root_path /var/www/nextcloud

    ServerName ${root_domain}
    DocumentRoot ${root_path}

    SSLEngine on
    SSLCertificateFile /etc/ssl/adcs/ip6.pem
    SSLCertificateKeyFile /etc/ssl/adcs/ip6.key

    <IfModule mod_headers.c>
        Header always set Strict-Transport-Security "max-age=15552000; includeSubDomains; preload"
    </IfModule>
    <Directory ${root_path}>
        AllowOverride All
        Require all granted
    </Directory>

</VirtualHost>

vi /etc/apache2/sites-enabled/000-default.conf

<VirtualHost \*:80> # The ServerName directive sets the request scheme, hostname and port that # the server uses to identify itself. This is used when creating # redirection URLs. In the context of virtual hosts, the ServerName # specifies what hostname must appear in the request's Host: header to # match this virtual host. For the default virtual host (this file) this # value is not decisive as it is used as a last resort host regardless. # However, you must set it for any further virtual host explicitly. # ServerName 10.200.202.45 # Redirect permanent / https://10.200.202.45/
ServerName 10.200.202.45
Redirect permanent / https://10.200.202.45

        ServerAdmin webmaster@localhost
        DocumentRoot /var/www/nextcloud/

        # Available loglevels: trace8, ..., trace1, debug, info, notice, warn,
        # error, crit, alert, emerg.
        # It is also possible to configure the loglevel for particular
        # modules, e.g.
        #LogLevel info ssl:warn

        ErrorLog ${APACHE_LOG_DIR}/error.log
        CustomLog ${APACHE_LOG_DIR}/access.log combined

        # For most configuration files from conf-available/, which are
        # enabled or disabled at a global level, it is possible to
        # include a line for only one particular virtual host. For example the
        # following line enables the CGI configuration for this host only
        # after it has been globally disabled with "a2disconf".
        #Include conf-available/serve-cgi-bin.conf

</VirtualHost>
