FROM nginx

RUN  mkdir /var/www
RUN rm /etc/nginx/conf.d/default.conf
COPY test.conf  /etc/nginx/conf.d/

VOLUME /var/www
