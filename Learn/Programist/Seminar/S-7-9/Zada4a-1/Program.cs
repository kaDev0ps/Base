// Задайте значение N. Напишите программу, которая выведет натуральные числа в промежутке от 1 до N
/*
N = 5-> 1.2.3.4.5
*/
Console.Write("Введите N: ");
int number = Convert.ToInt32(Console.ReadLine()); // делаем конвертацию в число
Console.WriteLine(NaturalNumber(number)); // 1 - первый вызов возвращает число, которое мы набрали

int NaturalNumber(int number)
{
     if(number == 1)// условия выхода из рекурсии
          return 1; 
     else
          Console.Write(NaturalNumber(number - 1) + ", "); // 2 - так как наше число не ровно 1 мы отнимает 1 и выводим в консоль
     return number;
}