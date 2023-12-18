// Программа принимает одно целое число и выдает все целые числа от -N до N
/*
int a = 5;
int b = 2;
int c = a % b;
console.WriteLine(c); - Вывести остаток от деления */
Console.Write("Write number:");
int a = Convert.ToInt32(Console.ReadLine()); // стартовое число N
int b = -a; // Противоположное число -N

while (b <= a)
{
              Console.Write(b + " "); // c - то что есть в переменной на старте очередного прохода цикла
              b++;
}
/*
              // Второй вариант
              Console.Write("Write number:");
              int a = Convert.ToInt32(Console.ReadLine()); // стартовое число N

              int b = -a; // Противоположное число -N
              string c = ""; // переменная с - пустая строка туда будем писать числа
              while (b <= a)
              {
                            c = c + ", " + b++; // c - то что есть в переменной на старте очередного прохода цикла
              }
              Console.WriteLine(c); // Выводим все числа с -N до N
              
*/