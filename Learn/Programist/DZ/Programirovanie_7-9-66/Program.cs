// задайте значения m и n . Программа найдет сумму натуральных элементов в промежутке 
// м=4 n=8 будет 30

Console.Write("Введите число M: ");
int m = Convert.ToInt32(Console.ReadLine()); 
Console.Write("Введите число N: ");
int n = Convert.ToInt32(Console.ReadLine()); 

Console.WriteLine($"Сумма чисел от {m} до {n} = {Step(m, n) + m}");

int Step(int m, int n) 
{
     if (n == m)
     return 0;
     return Step(m, n - 1) + n; 
}