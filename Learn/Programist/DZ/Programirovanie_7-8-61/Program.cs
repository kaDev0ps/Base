// Вывести первые N строк треугольника Паскаля. Сделать ввыдов ввиде равнобедренного треугольника.

int n = InputNumbers("Введите количество строк: ");

double[,] pascal = new double[n + 1, 2 * n + 1];

CreatPascal(pascal);

Console.WriteLine();
WriteArray(pascal);

Transformation(pascal);

Console.WriteLine("\nПереворачиваем треугольник \n");
WriteArray(pascal);

void Transformation(double[,] array)
{
  for (int i = 0; i < array.GetLength(0); i++)
  {
    int count = 0;
    for (int j = array.GetLength(1) - 1; j >= 0; j--)
    {
      if (array[i, j] != 0)
      {
        array[i, array.GetLength(1) / 2 + j - count] = array[i, j];
        array[i, j] = 0;
        count++;
      }
    }
  }
  array[array.GetLength(0) - 1, 0] = 1;
}

void CreatPascal(double[,] Triangle)
{
  for (int k = 0; k < Triangle.GetLength(0); k++)
  {
    Triangle[k, 0] = 1;
  }
  for (int i = 1; i < Triangle.GetLength(0); i++)
  {
    for (int j = 1; j < i + 1; j++)
    {
      Triangle[i, j] = Triangle[i - 1, j] + Triangle[i - 1, j - 1];
    }
  }
}

void WriteArray(double[,] array)
{
  for (int i = 0; i < array.GetLength(0); i++)
  {
    for (int j = 0; j < array.GetLength(1); j++)
    {
      if (array[i, j] != 0)
      {
          Console.Write($"{array[i, j]} ");
      }
      else Console.Write("  ");
    }
    Console.WriteLine();
  }
}

int InputNumbers(string input)
{
  Console.Write(input);
  int output = Convert.ToInt32(Console.ReadLine());
  return output;
}