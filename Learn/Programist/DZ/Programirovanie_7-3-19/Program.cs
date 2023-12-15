// Программа принимает пятизначное число и проверяет является ли оно полиндромом
// 14741 - да, 14714 - нет
// Методы
int InputIn (string output) // метод для ввода числа
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}
// Решение
int A = InputIn("Введите пятизначное число: ");

int N1 = A / 10000;
int N2 = (A - N1 * 10000) / 1000;
int N3 = (A - N1 * 10000 - N2 * 1000) / 100;
int N5 = A % 10;
int N4 = (A % 100) / 10;

// Ответ

if (N1 == N5 && N2 == N4)
{
     Console.Write("Число полиандром");
}
else
{
     Console.Write("Число не полиандром");
}
