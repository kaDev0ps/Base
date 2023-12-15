// программа на вход принимает позиции элемента в двемерном массиве и возвращает значение того элемента или говорит, что этого элемента в массиве нет
// например массив 
/*
123
456
Ответ: 7 нет в массиве
*/

int m = InputInt("Введите количество строк: ");
int n = InputInt("Введите количество столюцов: ");
int x = InputInt("Какое число ищем в массиве?: ");
string result = "Нет такого числа";
int[,] numbers = new int[m, n];

FillArray(numbers);
PrintArray(numbers);
Console.WriteLine(result);

void PrintArray(int[,] array)
{
     for(int i = 0; i < array.GetLength(0); i++)
     {
          for(int j = 0; j < array.GetLength(1); j++)
          {
               Console.Write(array[i, j] + " ");
               if(numbers[i, j] == x)
               {
                    result = "Такое число в массиве ЕСТЬ!";
               }
          }
          Console.WriteLine(); // переходим на новую строку, чтоб была таблица
     }
}

void FillArray(int[,] array)
{
     for(int i = 0; i < array.GetLength(0); i++) // получаем размер первого измерения (строк)
     {
          for(int j = 0; j < array.GetLength(1); j++) // получаем размер второго измерения (столбец)
          {
               numbers[i, j] = new Random().Next(0,10);
          }
     }
}

int InputInt(string output)
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}
