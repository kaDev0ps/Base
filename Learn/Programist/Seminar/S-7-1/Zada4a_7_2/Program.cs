// Программа на вход принимает 2 числа и проверяет является ли второе число квадратом первого.
Console.Write("Write First number:");
int a = Convert.ToInt32(Console.ReadLine());
Console.Write("Write Second number:");
int b = Convert.ToInt32(Console.ReadLine());
int c = a * a;
if (b == c) Console.WriteLine("Second number is a quatro first number");
else
Console.WriteLine("Second number is NOT a quatro first number");    