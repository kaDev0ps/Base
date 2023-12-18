// Задайте значение M и N. Напишите программу, которая выведет натуральные числа в промежутке от M до N
/*
M=1; N=5 -> 1.2.3.4.5
M=5; N= -> 1.2.3.4.5
alt + shift + F
*/
Console.Clear();
Console.Write("Введите N: ");
int m = Convert.ToInt32(Console.ReadLine()); // делаем конвертацию в число
Console.Write("Введите M: ");
int n = Convert.ToInt32(Console.ReadLine()); // делаем конвертацию в число


NaturalNumber(m, n);

void NaturalNumber(int m, int n)
{

     if (m < n)
     {
          Console.Write($"{m}, ");
          NaturalNumber(m + 1, n);
     }
     if (m > n)
     {
          NaturalNumber(m - 1, n); // меняем местами порядок
          Console.Write($"{m}, "); // это навверх
     }
     if (m == n)
     {
          Console.Write($"{m}, ");
     }
}