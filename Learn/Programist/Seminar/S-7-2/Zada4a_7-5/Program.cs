// Программа принимает число и проверяет кратно ли оно одновременно 7 и 23

// Если условие ИЛИ то || если И то &&
// решение автоматическое
int randomNumber1 = new Random().Next(7, 46);
Console.WriteLine($"число {randomNumber1}");
if (randomNumber1 % 7 == 0 && randomNumber1 % 23 == 0)
{
             Console.WriteLine($"Кратно"); 
}
else
{
              Console.WriteLine($"Не кратно");
}

// решенеие с водом чисел
Console.WriteLine(" Write First number is ...");
int a = Convert.ToInt32(Console.ReadLine());
Console.WriteLine($"число {a}");
if (a % 7 == 0 && a % 23 == 0)
{
             Console.WriteLine($"Кратно"); 
}
else
{
              Console.WriteLine($"Не кратно");
}