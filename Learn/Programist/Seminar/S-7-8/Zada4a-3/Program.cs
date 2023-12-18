// Создайте частонтный словарь элементов двумерного массива. Словарь содержит инфо сколько раз встречается элемент входных данных
/*
1,9,9,2,2 // 1 - встречается 1 раз, 2 встречается 2 раза
// если встречается 0 раз - то не надо ее выводить. Просто поместить в переменную
1- сперва отсортировать массив
555
432
111
2 - если нет повторений, значит новое число
3 - создать новый массив, пройтись по ассортированным и посчитать количество повторений каждого числа по отдельности
*/

// Двумерный мссив. Программа меняет строки на столбцы. Если это невозможно выводит сообщение

// Задайте двумерный массив. Напишите прогу, которая меняет местами первую и последнюю строку массива

int m = InputInt("Введите количество строк: ");
int n = InputInt("Введите количество столюцов: ");
int[,] numbers = new int[m, n];
FillMatrix(numbers);
PrintMatrix(numbers);
Console.WriteLine();

SortArray(numbers); // отсортированный массив
PrintMatrix(numbers);
int[,] dictionary = FrequencyDictionary(numbers); // передаем массив в метод, который делает всю магию
for (int i = 0; i < dictionary.GetLength(0); i++)
{
     Console.WriteLine($"{dictionary[i, 0]} встречается {dictionary[i, 1]}");
}
Console.WriteLine();

int[,] FrequencyDictionary(int[,] matrix)
{
     int size = CalculateCountNumbers(matrix); // вызываем метод отвечающий за расчет количества чисел
     int[,] dictionary = new int[size, 2]; // создаем новый массив словарь
     int dictionaryIndex = 0;
     dictionary[dictionaryIndex, 0] = matrix[0, 0];
     for(int i = 0; i < matrix.GetLength(0); i++) // проходим по всей матрице
     {
          for(int j = 0; j < matrix.GetLength(1); j++)
          {
               if  (dictionary[dictionaryIndex, 0] == matrix[i, j]) // если число в словаре = матрице мы прибавляем 1 иначе мы переходим к следующему индексу, добавляем число в нулевой индекс, а 1 записываем в 1.
                    dictionary[dictionaryIndex, 1]++;
               else
               {
                    dictionaryIndex++;
                    dictionary[dictionaryIndex, 0] = matrix[i, j];
                    dictionary[dictionaryIndex, 1] = 1;
               }
          }
     }
     return dictionary;
}


void SortArray(int[,] matrix) // сортировка матрицы 4 циклами (2 цигла одномерный массив + 2 для двумерного массива)
{
     for (int i = 0; i < matrix.GetLength(0); i++) // проходим по всей матрице
     {
          for (int j = 0; j < matrix.GetLength(1); j++) // проходим по всей матрице
          {
               for (int a = 0; a < matrix.GetLength(0); a++) // проходим по всей матрице
               {
                    for (int b = 0; b < matrix.GetLength(1); b++) // проходим по всей матрице
                    {
                         if(matrix[a,b] < matrix[i,j]) // получаем один массив в котором все в одном порядке отсортированны
                         {
                              int temp = matrix[i,j];
                              matrix[i, j] = matrix[a,b];
                              matrix[a, b] = temp;
                         }
                    }
               }
          }
     }
}

int CalculateCountNumbers(int[,] sortMatrix) // считаем количество чисел. Если новый эллемент не равен текущему, то количество новых эллементов в массиве увеличивается на 1. Изначально равен 1 так как начинаем с какого-то числа
{
     int countNumbers = 1;
     int tempNumbers = sortMatrix[0,0];
     for(int i = 0; i < sortMatrix.GetLength(0); i++)
     {
          for(int j = 0; j < sortMatrix.GetLength(1); j++)
          {
               if(tempNumbers != sortMatrix[i, j])
               {
                    tempNumbers = sortMatrix[i, j];
                    countNumbers++;
               }
          }
     }
     return countNumbers;
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