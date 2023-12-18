// Двумерный массив заполненный случайными вещественными числами

int m = InputInt("Введите количество строк: ");
int n = InputInt("Введите количество столюцов: ");
double[,] a = new double[m, n];

Random random = new Random();
PrintArray(a);
 
void PrintArray(double[,] array)
{
     for (int i = 0; i < array.GetLength(0); i++)
     {
          for (int j = 0; j < array.GetLength(1); j++)
          {
               a[i, j] = random.NextDouble() * 100; // NextDouble() дает случайное вещественное число в диапазоне от 0 до 1
               Console.Write("{0,6:F2}", a[i, j]);
          }
          Console.WriteLine();
     }
}

int InputInt(string output)
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}
