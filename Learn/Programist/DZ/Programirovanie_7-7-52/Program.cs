// Двумерный массив из целых чисел. Найти среднее арифметическое элементов в каждом столбце


int m = InputInt("Введите количество строк: ");
int n = InputInt("Введите количество столюцов: ");
int[,] numbers = new int[m, n];
Random rand = new Random();
int[] summ = new int[n];

FillArray(numbers);
PrintArray(numbers);

void FillArray(int[,] array)
{
     for (int i = 0; i < array.GetLength(0); i++)
     {
          for (int j = 0; j < array.GetLength(0); j++)
          {
          numbers[i, j] = rand.Next(0, 10); //рандомные значения для елементов 
          
          }
     }
}

void PrintArray(int[,] array)
{
     for (int i = 0; i < array.GetLength(0); i++)
     {
          for (int j = 0; j < array.GetLength(0); j++)
          {
          
          Console.Write(numbers[i, j] + " "); //вывод массива
          summ[i] += numbers[j, i];      //подсчет суммы колонки
          } 
          Console.WriteLine();
     }
}
Console.WriteLine();
Console.WriteLine("Считаем арифмет. сумму каждой колонки с новой строки:");
foreach (double elem in summ)
{
     
     Console.WriteLine("{0,6:F2}", elem/n); //вывод среднего для колонки
}


int InputInt(string output)
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}