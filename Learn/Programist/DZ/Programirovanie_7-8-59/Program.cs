// задать двумерный массив из целых чисел. Программа удаляет строку и столбец на пересечении которых расположен наименьший эллемент массива.
/*
123
456
789

56
89
*/

int m = InputNumbers("Введите количество строк: ");
int n = InputNumbers("Введите количество столбцов: ");
int range = InputNumbers("Диапазон значений: от 1 до ");

int[,] array = new int[m, n];
FillArray(array);
PrintArray(array);

int[,] minElementInRow = new int[1, 2];
minElementInRow = FindMinElementInRow(array, minElementInRow);

int[,] WithoutRows = new int[array.GetLength(0) - 1, array.GetLength(1) - 1];
DelLines(array, minElementInRow, WithoutRows);
Console.WriteLine($"\nНовый массив:");
PrintArray(WithoutRows);

int[,] FindMinElementInRow(int[,] array, int[,] position)
{
  int temp = array[0, 0];
  for (int i = 0; i < array.GetLength(0); i++)
  {
    for (int j = 0; j < array.GetLength(1); j++)
    {
      if (array[i, j] <= temp)
      {
        position[0, 0] = i;
        position[0, 1] = j;
        temp = array[i, j];
      }
    }
  }
  Console.WriteLine($"\nMинимальный элемент в строках: {temp}");
  return position;
}

void DelLines(int[,] array, int[,] smallerElement, int[,] arrayNonRows)
{
  int k = 0, l = 0;
  for (int i = 0; i < array.GetLength(0); i++)
  {
    for (int j = 0; j < array.GetLength(1); j++)
    {
      if (smallerElement[0, 0] != i && smallerElement[0, 1] != j)
      {
        arrayNonRows[k, l] = array[i, j];
        l++;
      }
    }
    l = 0;
    if (smallerElement[0, 0] != i)
    {
      k++;
    }
  }
}

int InputNumbers(string input)
{
  Console.Write(input);
  int output = Convert.ToInt32(Console.ReadLine());
  return output;
}

void FillArray(int[,] array)
{
  for (int i = 0; i < array.GetLength(0); i++)
  {
    for (int j = 0; j < array.GetLength(1); j++)
    {
      array[i, j] = new Random().Next(range);
    }
  }
}

void PrintArray(int[,] array)
{
  for (int i = 0; i < array.GetLength(0); i++)
  {
    for (int j = 0; j < array.GetLength(1); j++)
    {
      Console.Write(array[i, j] + " ");
    }
    Console.WriteLine();
  }
}