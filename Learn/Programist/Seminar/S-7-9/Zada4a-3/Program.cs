// программа принимает на вход число и возвращает сумму его цифр
// 453 = 12
Console.Clear();
Console.Write("Введите число: ");
int a = Convert.ToInt32(Console.ReadLine()); // делаем конвертацию в число
Console.WriteLine("Сумма равна: " + Recurs(a));

/*
Recurs(a);

int Recurs(int a)
{
     if(a / 10 == 0)
     {
          return a;
     }
     else
     {
          return a % 10 + Recurs(a / 10);
     }
}
Console.WriteLine(Recurs(a));
 Второе решение */

int Recurs(int a);

int Recurs(int a)
{
     if(a > 0)
     return a % 10 + Recurs(a / 10); // прибавляем последнюю цифру, а потом еще раз прибавляем последнюю цифру и так пока число больше 0 (например 3 + sum, 2+ sum, 1 + sum, 0 + sum = 0 а дальше в обратном порядке 0+1+2+3=6)
     return 0;
}