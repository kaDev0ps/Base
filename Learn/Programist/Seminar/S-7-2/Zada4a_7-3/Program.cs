// Программа принимает 2 числа и выводит ответ является ли первое кратным второму. Если оно не кратно то выводит остаток от деления.
// 31, 5 -> не кратно, остаток 4
// 16, 4 -> кратно
// Если условие ИЛИ то || если И то &&
// решение автоматическое
int randomNumber1 = new Random().Next(16, 30);
int randomNumber2 = new Random().Next(2, 4);
Console.WriteLine($"Первое число {randomNumber1}");
Console.WriteLine($"Второе число {randomNumber2}");
if (randomNumber1 % randomNumber2 == 0)
{
             Console.WriteLine($"Кратно"); 
}
else
{
              Console.WriteLine($"Не кратно остаток {randomNumber1 % randomNumber2}");
}

// решенеие с водом чисел
Console.WriteLine(" Write First number is ...");
int a = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Write Second number is ...");
int b = Convert.ToInt32(Console.ReadLine());
Console.WriteLine($"Первое число {a}");
Console.WriteLine($"Второе число {b}");
if (a % b == 0)
{
             Console.WriteLine($"Кратно"); 
}
else
{
              Console.WriteLine($"Не кратно остаток {a % b}");
}