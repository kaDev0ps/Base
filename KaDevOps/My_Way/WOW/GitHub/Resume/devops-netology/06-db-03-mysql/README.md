# [Домашнее задание](https://github.com/a-prokopyev-resume/virt-homeworks/tree/virt-11/06-db-03-mysql) к [занятию 3. «MySQL»](https://netology.ru/profile/program/bd-dev-27/lessons/275712/lesson_items/1477599)
++ source lib.sh
+++ ComposeEnv=' --env-file=env/docker-compose.env --env-file=env/super.env --env-file=env/common.env '
++ Action=start
++ MoreArgs='my1 gui iac'
+++ date
++ echo -e '\n\n\n===> Debug at Fri 27 Oct 2023 06:40:26 PM UTC:'
++ case $Action in
++ Services='my1 gui iac'
++ Options=' -d'
++ set -x
++ docker-compose --env-file=env/docker-compose.env --env-file=env/super.env --env-file=env/common.env -f /download/netology/docker-compose.yml up -d my1 gui iac
++ docker ps -a