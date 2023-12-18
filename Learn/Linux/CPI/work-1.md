# Создание несколько каталогов

mkdir -p cli-for-dev/pz-1-4/storage/{docs,photo,video}

# Создание 20 файлов

sudo touch cli-for-dev/pz-1-4/storage/docs/chapter-{01..20}.txt

# создаём функцию

pretty_ls() { echo вывожу красивый ls; ls -la; }

# Вывод нечетных

echo {1..5..2}
