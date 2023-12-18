// Задайте двумерный массив m*n заполненный случайными числами. Найти эллементы у которыхоба индекса нечетные и замените эти элементы на их квадраты
/*
1472        1472
5923   =>   58129
8424        8424
*/

int m = InputInt("Введите количество строк: ");
int n = InputInt("Введите количество столюцов: ");
int[,] numbers = new int[m, n];

FirstArray(numbers);
Console.WriteLine();
SecondArray(numbers);

void FirstArray(int[,] array) // заполняем массив
{
     for(int i = 0; i < array.GetLength(0); i++) // получаем размер первого измерения (строк)
     {
          for(int j = 0; j < array.GetLength(1); j++) // получаем размер второго измерения (столбец)
          {
               numbers[i,j] = new Random().Next(0, 10);
               Console.Write(array[i, j] + " ");
          }
          Console.WriteLine();
     }
}

void SecondArray(int[,] array) // меняем элементы если индекс у них не четный 
{
     for(int i = 0; i < array.GetLength(0); i++) // получаем размер первого измерения (строк)
     {
          for(int j = 0; j < array.GetLength(1); j++) // получаем размер второго измерения (столбец)
          {
               if(i % 2 != 0 && j % 2 != 0)
               numbers[i, j] *= numbers[i, j];
               Console.Write(array[i, j] + " ");
          }
          Console.WriteLine();
     }
}


int InputInt(string output)
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}
