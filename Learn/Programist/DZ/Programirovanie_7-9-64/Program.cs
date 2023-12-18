// задайте значения m и n . Программа выведит все натуральные числа в промежутке 
// м=2 n=5 будет 2 3 4 5

Console.Write("Введите число M: ");
int m = Convert.ToInt32(Console.ReadLine()); // делаем конвертацию в число
Console.Write("Введите число N: ");
int n = Convert.ToInt32(Console.ReadLine()); // делаем конвертацию в число

Console.WriteLine(NaturalNumber(m, n)); // 1 - первый вызов возвращает число, которое мы набрали


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