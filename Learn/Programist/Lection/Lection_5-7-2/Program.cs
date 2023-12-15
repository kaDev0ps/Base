// работа с файлами
/*
string path = "C:\1";
DirectoryInfo di = new DirectoryInfo(path);
System.Console.WriteLine(di.CreationTime);
FileInfo[] fi = di.GetFiles();

for (int i = 0; i < fi.Length; i++)
{
     System.Console.WriteLine(fi[i].Name); // Пробегаем по всем файлам в папке и выводим их имена
}
*/
// Код, который ходит по папка и сканирует их содержимое

void CatalogInfo(string path, string indent = "") // метод записывает путь и делает отступы
{
     DirectoryInfo catalog = new DirectoryInfo(path); // получаем инфо о директории в которую зашли
     DirectoryInfo[] catalogs = catalog.GetDirectories(); // получаем массив всех файлов в папке
     for (int i = 0; i < catalogs.Length; i++)
     {
          Console.WriteLine($"{indent}{catalogs[i].Name}"); // пробегаем по каталогу и показываем файлы
          CatalogInfo(catalogs[i].FullName, indent + "  ");
     }
     FileInfo[] files = catalog.GetFiles(); // получаем весь список файлов в текущей директории
     for (int i = 0; i < files.Length; i++)
     {
          Console.WriteLine($"{indent}{files[i].Name}"); // показываем файлы
     }
}
string path = @"C:\1";
CatalogInfo(path);