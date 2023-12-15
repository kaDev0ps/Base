// Задайте двумерный массив. Напишите прогу, которая меняет местами первую и последнюю строку массива

int m = InputInt("Введите количество строк: ");
int n = InputInt("Введите количество столюцов: ");
int[,] numbers = new int[m, n];
FillMatrix(numbers);
PrintMatrix(numbers);
Console.WriteLine();

int firstRowIndex = 0; // создаем переменные со строками, с которыми нужно поменять местами
int lastRowIndex = numbers.GetLength(0) - 1; // создаем переменные со строками, с которыми нужно поменять местами (на одну меньше чем длинна строк)
ReplaceRowsInMatrix(firstRowIndex, lastRowIndex, numbers); // передаю все в метод
PrintMatrix(numbers);


void ReplaceRowsInMatrix(int firstRow, int secondRow, int[,] matrix) // первая и вторая строка, которые меняем местами и матрица в которой меняем строки
{
     for(int i = 0; i < matrix.GetLength(1); i++) // проходимся по столбцам и меняем значения через временную переменную
     {
          int temp = matrix[firstRowIndex, i];
          matrix[firstRowIndex, i] = numbers[secondRow, i];
          matrix[secondRow, i] = temp;
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