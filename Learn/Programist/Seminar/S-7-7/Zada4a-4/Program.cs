// Задайте двумерный массив m*n заполненный случайными числами. Найти сумму эллементов находящихся на главной диагонали (с индексами 0;0 1;1 и.д.)
/*
1472        
5923   =>   сумма 1+5+2=8
8424        
*/

int m = InputInt("Введите количество строк: ");
int n = InputInt("Введите количество столюцов: ");
int[,] numbers = new int[m, n];
int result = 0;

FirstArray(numbers);
Console.WriteLine($"Сумма по главной диагонали = {result}");

void FirstArray(int[,] array)
{
     for(int i = 0; i < array.GetLength(0); i++) // получаем размер первого измерения (строк)
     {
          for(int j = 0; j < array.GetLength(1); j++) // получаем размер второго измерения (столбец)
          {
               numbers[i, j] = new Random().Next(0,10);
               Console.Write(numbers[i, j]);
               if (i == j)
               result += numbers[i, j];
          }
          Console.WriteLine();

     }
}
int InputInt(string output)
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}
