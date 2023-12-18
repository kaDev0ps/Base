// Задайте двумерный массив m*n каждый элемент находится по формуле A=m+n
/* m=3 n=4
0123
1234
2345
*/

int m = InputInt("Введите количество строк: ");
int n = InputInt("Введите количество столюцов: ");
int[,] numbers = new int[m, n];

FillArray(numbers);
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

void FillArray(int[,] array)
{
     for(int i = 0; i < array.GetLength(0); i++) // получаем размер первого измерения (строк)
     {
          for(int j = 0; j < array.GetLength(1); j++) // получаем размер второго измерения (столбец)
          {
               numbers[i, j] = i + j;
          }
     }
}

int InputInt(string output)
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}
