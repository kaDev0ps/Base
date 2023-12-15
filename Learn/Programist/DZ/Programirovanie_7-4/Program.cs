// Напишите программу, которая на вход принимает число (N), а на выходе показывает все чётные числа от 1 до N.
Console.Write("Write number:");
int a = Convert.ToInt32(Console.ReadLine());
int result = 1;
while (result <= a)
              {
              Console.Write(result + " ");
              result++;
              }