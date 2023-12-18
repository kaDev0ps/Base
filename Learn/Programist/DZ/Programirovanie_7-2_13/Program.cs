// Напишите программу, которая выводит цифру заданного числа или сообщает, что третьей цифры нет.
Console.Write("Write number:");
int a = Convert.ToInt32(Console.ReadLine());

if (a < 100)
{
              Console.WriteLine("Третьей цифры нет"); 
              

}
else
{
              int b = a;
              while (a > 999)
              {
                            a = a / 10;
              }
              int thirdDigit = a % 10; // узнаем третью цифру через остаток
              Console.WriteLine($"Число {b}. Третья цифра {thirdDigit}"); 
}