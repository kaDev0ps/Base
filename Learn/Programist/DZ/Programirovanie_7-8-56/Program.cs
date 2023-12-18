// задать прямоугольный двумерный массив. Программа находит строку с наименьшей суммой элементов
/*
12
13
14
Наименьшая сумма элементов в 1 строке.
*/

int m = InputNumbers("Сколько строк?: ");
int n = InputNumbers("Сколько столбцов?: ");
int range = InputNumbers("Диапазон значений от 1 до ");

int[,] myArray = new int[m, n];
NewArray(myArray);
PublishArray(myArray);

int minSum = 0;
int sumRow = SumElements(myArray, 0);
for (int i = 1; i < myArray.GetLength(0); i++)
{
  int temp = SumElements(myArray, i);
  if (sumRow > temp)
  {
    sumRow = temp;
    minSum = i;
  }
}

Console.WriteLine($"\n{minSum+1} - строкa с наименьшей суммой ({sumRow}) элементов ");

int InputNumbers(string input)
{
  Console.Write(input);
  int output = Convert.ToInt32(Console.ReadLine());
  return output;
}

void NewArray(int[,] array)
{
  for (int i = 0; i < array.GetLength(0); i++)
  {
    for (int j = 0; j < array.GetLength(1); j++)
    {
      array[i, j] = new Random().Next(range);
    }
  }
}

void PublishArray (int[,] array)
{
  for (int i = 0; i < array.GetLength(0); i++)
  {
    for (int j = 0; j < array.GetLength(1); j++)
    {
      Console.Write(array[i,j] + " ");
    }
    Console.WriteLine();
  }
}

int SumElements(int[,] array, int i)
{
  int sumLine = array[i,0];
  for (int j = 1; j < array.GetLength(1); j++)
  {
    sumLine += array[i,j];
  }
  return sumLine;
}


