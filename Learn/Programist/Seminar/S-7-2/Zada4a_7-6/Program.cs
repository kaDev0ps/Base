// Программа принимает 2 числа и проверяет является ли одно квадратом лругого 
// Если условие ИЛИ то || если И то &&
// решение автоматическое
// решенеие с водом чисел
Console.WriteLine(" Write First number is ...");
int a = Convert.ToInt32(Console.ReadLine());
Console.WriteLine(" Write Second number is ...");
int b = Convert.ToInt32(Console.ReadLine());
Console.WriteLine($"число {a}");
if (a * a == b || b * b == a)
{
             Console.WriteLine("Да, является"); 
}
else
{
              Console.WriteLine("Нет, не является");
}