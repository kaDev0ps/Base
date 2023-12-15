Используйте команду ls -ld , чтобы убедиться в правильности прав доступа к файлам в домашнем каталоге. Ниже приведен список правильных разрешений:

Домашний каталог Linux, /home , например, должен быть (0755/drwxr-xr-x) .
Например, домашний каталог пользователя /home/ec2-user/ должен быть (0700/drwx------) .
Разрешение на доступ к каталогу .ssh, например, /home/ec2-user/.ssh , должно быть (0700/drwx------) .
разрешение файла author_keys, например /home/ec2-user/.ssh/authorized_keys , должно быть (0600/-rw-------) .
Ниже приведен пример команды ls -ld и ее результатов. В этом примере ec2-user — это имя пользователя. Измените имя пользователя в соответствии с вашим конкретным AMI .

$ ls -ld /home/ec2-user/
drwx------ 3 ec2-user ec2-user 4096 Apr 1 08:31 /home/ec2-user/ 4. На локальном компьютере проверьте открытый ключ SSH .

5. Если подпись открытого ключа SSH отсутствует в выходных данных, обновите файл author_keys , чтобы разрешить использование вашего ключа SSH. В следующем примере замените ключ примера своим открытым ключом SSH.

$ echo 'ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABAQDVogCW5eZogRp+vF6Ut360b0bYyTmqgYaCXOyiW77I916AS5jFL3zsCtONbGn4hnG/UGGWXpLfUV85qpVJb38fskPZNuyZtjGjXM2W7qqbCZ1N9HBb6IPBaL97tmqBi+8rD7mSkoHc40sIV+KxkQSvD6AAFjQruCjxzfGIApnOvuj6IMsVEuFHBx4QhkbCzafxo02D9BZT4+dMy7tmyuC+UiNEQpgfFoszl+4VNFTIPlQQyn6CpUiV/rFXIadXsHqc+UOdVnfEXP+30YL75RHabze/1F5MY6t94AEcmcb05Dq4vwN9IjcxKmwgvxLOXzryytepvHQU+PobBEXAMPLE' >> /home/ec2-user/.ssh/authorized_keys 6. Чтобы исправить разрешения, выполните следующие команды на своем экземпляре EC2.

$ sudo chown root:root /home
$ sudo chmod 755 /home
$ sudo chown ec2-user:ec2-user /home/ec2-user -R
$ sudo chmod 700 /home/ec2-user /home/ec2-user/.ssh
$ sudo chmod 600 /home/ec2-user/.ssh/authorized_keys
