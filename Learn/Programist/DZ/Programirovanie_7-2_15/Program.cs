// Напишите программу, которая на вход принимает число (N), а на выходе показывает все чётные числа от 1 до N.
Console.Write("Write number:");
int a = Convert.ToInt32(Console.ReadLine());
if (a == 6 || a == 7)
{
        Console.WriteLine($"День {a} выходной");      
}
else
{
        Console.WriteLine($"День {a} не выходной");  
}