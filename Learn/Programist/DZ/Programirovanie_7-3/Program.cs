// Напишите программу, которая на вход принимает число и выдаёт, является ли число чётным (делится ли оно на два без остатка).
Console.Write("Write number:");
int a = Convert.ToInt32(Console.ReadLine()); // число
int b = a % 2;
if (b == 0)
{
              Console.WriteLine("The number is even");
}
else
{
Console.WriteLine("The number is not even");
}
