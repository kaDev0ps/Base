// Программа принимает число X и выдает сумму цифр в числе
int X (string output) // метод для ввода числа
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}

//Решение
     int number = X("Введите число: ");
     int sum = 0;
     int i = number;
     while(i > 0)
     {
          sum += i % 10;
          i /= 10;
     }
Console.Write($"Сумма цифр в числе {number} = {sum}");