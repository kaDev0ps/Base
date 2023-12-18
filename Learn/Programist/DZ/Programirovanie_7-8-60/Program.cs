// сформируйте трехмерный массив из неповторяющихся двухзначных чисел. Программа построчно выводит массив. Добавляя индексы каждого элемента
/*
2*2*1
12(0,0,0) 22(0,0,1)
45(1,0,0) 53(1,0,1)
*/
int x = InputNumbers("Задайте значение x: ");
int y = InputNumbers("Задайте значение y: ");
int z = InputNumbers("Задайте значение z: ");
Console.WriteLine();

int[,,] arrayThird = new int[x, y, z];
FillArray(arrayThird);
PrintArray(arrayThird);

int InputNumbers(string input)
{
  Console.Write(input);
  int output = Convert.ToInt32(Console.ReadLine());
  return output;
}

void PrintArray (int[,,] array)
{
  for (int i = 0; i < array.GetLength(0); i++)
  {
    for (int j = 0; j < array.GetLength(1); j++)
    {
      Console.Write($"x({i}) y({j}) ");
      for (int k = 0; k < array.GetLength(2); k++)
      {
        Console.Write( $"z({k})={array[i,j,k]}; ");
      }
      Console.WriteLine();
    }
    Console.WriteLine();
  }
}

void FillArray(int[,,] array)
{
  int[] temp = new int[array.GetLength(0) * array.GetLength(1) * array.GetLength(2)];
  int  number;
  for (int i = 0; i < temp.GetLength(0); i++)
  {
    temp[i] = new Random().Next(10, 100);
    number = temp[i];
    if (i >= 1)
    {
      for (int j = 0; j < i; j++)
      {
        while (temp[i] == temp[j])
        {
          temp[i] = new Random().Next(10, 100);
          j = 0;
          number = temp[i];
        }
          number = temp[i];
      }
    }
  }
  int count = 0; 
  for (int x = 0; x < array.GetLength(0); x++)
  {
    for (int y = 0; y < array.GetLength(1); y++)
    {
      for (int z = 0; z < array.GetLength(2); z++)
      {
        array[x, y, z] = temp[count];
        count++;
      }
    }
  }
}