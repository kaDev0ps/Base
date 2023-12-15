// Напишите программу, которая принимает на вход три числа и выдаёт максимальное из этих чисел.
Console.Write("Write number A:");
int a = Convert.ToInt32(Console.ReadLine()); // число A
Console.Write("Write number B:");
int b = Convert.ToInt32(Console.ReadLine()); // число B
Console.Write("Write number C:");
int c = Convert.ToInt32(Console.ReadLine()); // число C
int max = a;
if (b > max)
{
              max = b;
}
if (c > max)
{
              max = c;
}
Console.WriteLine("Max number is " + max);