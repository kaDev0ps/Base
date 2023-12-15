// Задайте двумерный массив m*n заполненный случайными числами. Найти эллементы у которыхоба индекса нечетные и замените эти элементы на их квадраты
/*
1472        1472
5923   =>   58129
8424        8424
*/
int m = InputInt("Введите количество строк: ");
int n = InputInt("Введите количество столюцов: ");
int[,] numbers = new int[m, n];


     for(int i = 0; i < numbers.GetLength(0); i++) // печатаем каждый симбвол
     {
          for(int j = 0; j < numbers.GetLength(1); j++)
          {
              numbers[i, j] = new Random().Next(0,10);
          }
          Console.WriteLine(); // переходим на новую строку, чотб была таблица
     }

Console.WriteLine();
PrintArray(numbers);

     for(int i = 1; i < numbers.GetLength(0); i+=2) // печатаем каждый симбвол
     {
          for(int j = 1; j < numbers.GetLength(1); j+=2)
          {
               numbers[i,j] *= numbers[i,j];
          }
          Console.WriteLine(); // переходим на новую строку, чотб была таблица
     }

PrintArray(numbers);

void PrintArray(int[,] array)
{
     for(int i = 0; i < array.GetLength(0); i++) // печатаем каждый симбвол
     {
          for(int j = 0; j < array.GetLength(1); j++)
          {
               Console.Write(array[i, j] + " ");
          }
          Console.WriteLine(); // переходим на новую строку, чотб была таблица
     }
}

int InputInt(string output)
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}