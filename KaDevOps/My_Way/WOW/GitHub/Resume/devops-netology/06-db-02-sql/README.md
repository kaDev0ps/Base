# [Домашнее задание](https://github.com/a-prokopyev-resume/virt-homeworks/tree/virt-11/06-db-02-sql) к [занятию 2. «SQL»](https://netology.ru/profile/program/bd-dev-27/lessons/275711/lesson_items/1477589)
++ docker-compose stop postgres
++ docker-compose up -d postgres
++ docker-compose up -d postgres
++ docker inspect PostgreSQL
++ docker exec -it PostgreSQL bash -lc '
++ docker-compose stop postgres
++ docker-compose stop postgres2
++ docker-compose up -d postgres2
++ docker inspect PostgreSQL2
++ docker exec -it PostgreSQL2 createdb -U admin test-db2
++ docker exec -it PostgreSQL2 bash -lc '
++ docker-compose stop postgres2
++ docker-compose stop postgres