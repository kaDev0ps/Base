// задайте 2 матрицы. Напишите программу, которая будет искать произведение двух матриц.

int a = InputInt("Введите количество строк 1 матрицы: ");
int b = InputInt("Введите количество столюцов 1 матрицы: ");
int[,] numbers = new int[a, b];

int c = InputInt("Введите количество строк 2 матрицы: ");
int d = InputInt("Введите количество столюцов 2 матрицы: ");
int[,] numbers2 = new int[c, d];


FillMatrix(numbers);
PrintMatrix(numbers);
Console.WriteLine();

FillMatrix2(numbers2);
PrintMatrix(numbers2);
Console.WriteLine();

MatrixResult(numbers);
PrintMatrix(numbers);

void MatrixResult(int[,] array)
{
     for(int i = 0; i < array.GetLength(0); i++)
     {
          for(int j = 0; j < array.GetLength(1); j++)
          {
              numbers[i, j] *= numbers2[i, j]; 
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

void FillMatrix2(int[,] matrix)
{
     for(int i = 0; i < matrix.GetLength(0); i++) // получаем размер первого измерения (строк)
     {
          for(int j = 0; j < matrix.GetLength(1); j++) // получаем размер второго измерения (столбец)
          {
               numbers2[i, j] = new Random().Next(0,10); // вставляем рандомные числа
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