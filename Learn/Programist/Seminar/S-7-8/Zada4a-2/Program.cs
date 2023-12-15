// Двумерный мссив. Программа меняет строки на столбцы. Если это невозможно выводит сообщение

// Задайте двумерный массив. Напишите прогу, которая меняет местами первую и последнюю строку массива

int m = InputInt("Введите количество строк: ");
int n = InputInt("Введите количество столюцов: ");
int[,] numbers = new int[m, n];
FillMatrix(numbers);
int[,] invertMatrix = ReplacePowsAdnColumns(numbers); // создаем матрицу полученную с метода

PrintMatrix(numbers);

Console.WriteLine();
PrintMatrix(invertMatrix);

int[,] ReplacePowsAdnColumns(int[,] matrix) // метод, который обрабатывает матрицу и возвращает матрицу
{
     int[,] result = new int[matrix.GetLength(1), matrix.GetLength(0)]; // размер новой инвертируемой матрицыматрицы
     for (int i = 0; i < result.GetLength(0); i++) // проходим по всей матрице
     {
          for (int j = 0; j < result.GetLength(1); j++) // проходим по всей матрице
          {
          result[i, j] = matrix[j, i]; // меняем местами индексы в матрице
          }
     }
     return result;
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