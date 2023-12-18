// программа упорядочит по убыванию элементы каждой строки двумерного массива.
/*
143
354
будет
134
345
*/

int m = InputInt("Введите количество строк: ");
int n = InputInt("Введите количество столюцов: ");
int[,] numbers = new int[m, n];
FillMatrix(numbers);
PrintMatrix(numbers);
Console.WriteLine();

SortArray(numbers); // отсортированный массив
PrintMatrix(numbers);
Console.WriteLine();



void SortArray(int[,] matrix) // сортировка матрицы 4 циклами (2 цигла одномерный массив + 2 для двумерного массива)
{
     for (int i = 0; i < matrix.GetLength(0); i++) // проходим по всей матрице
     {
          for (int j = 0; j < matrix.GetLength(1); j++) // проходим по всей матрице
          {
               for (int a = 0; a < matrix.GetLength(0); a++) // проходим по всей матрице
               {
                    for (int b = 0; b < matrix.GetLength(1); b++) // проходим по всей матрице
                    {
                         if(matrix[a,b] > matrix[i,j]) // получаем один массив в котором все в одном порядке отсортированны
                         {
                              int temp = matrix[i,j];
                              matrix[i, j] = matrix[a,b];
                              matrix[a, b] = temp;
                         }
                    }
               }
          }
     }
}


void FillMatrix(int[,] matrix)
{
     for(int i = 0; i < matrix.GetLength(0); i++) // получаем размер первого измерения (строк)
     {
          for(int j = 0; j < matrix.GetLength(1); j++) // получаем размер второго измерения (столбец)
          {
               numbers[i, j] = new Random().Next(0,10); // вставляем рандомные числа
          }

     }
}

void PrintMatrix(int[,] matrix)
{
     for(int i = 0; i < matrix.GetLength(0); i++) // печатаем каждый симбвол
     {
          for(int j = 0; j < matrix.GetLength(1); j++)
          {
               Console.Write(matrix[i, j] + " ");
          }
          Console.WriteLine(); // переходим на новую строку, чотб была таблица
     }
}


int InputInt(string output)
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}