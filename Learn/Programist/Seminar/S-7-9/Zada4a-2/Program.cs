// Задайте значение M и N. Напишите программу, которая выведет натуральные числа в промежутке от M до N
/*
M=1; N=5 -> 1.2.3.4.5
M=5; N= -> 1.2.3.4.5
alt + shift + F
*/

Console.Write("Введите N: ");
int n = Convert.ToInt32(Console.ReadLine()); // делаем конвертацию в число
Console.Write("Введите M: ");
int m = Convert.ToInt32(Console.ReadLine()); // делаем конвертацию в число

Console.WriteLine(NaturalNumber(n, m)); // 1 - первый вызов возвращает число, которое мы набрали


int NaturalNumber(int n, int m)

{
     int max = m;
     int min = n;
     if (max < min)
     {
          max = n;
          min = m;
     }
     if (min == max)
     {
          return min;
     }
     else
     {
          Console.Write(NaturalNumber(max - 1, min) + ", "); // 2 - так как наше число не ровно 1 мы отнимает 1 и выводим в консоль
          return max;
     }
}