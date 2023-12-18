// Отсортировать массив от минимального значения к максимальному путем переставления наименьших значений массива
// ДЗ от максимального к минимальному
int[] arr = {1, 5, 4, 3, 2, 6, 7, 1, 1 };

void PrintArray(int[] array)
{
   int count = array.Length;
   for (int i = 0; i < count; i++)
   {
      Console.Write($"{array[i]} ");
   }
   Console.WriteLine();
}

void SelectionSort(int[] array) // метод упорядочивания массива
{
   for (int i = 0; i < array.Length - 1; i++)
   {
      int minPosition = i; // запоминаем позицию рабочего элемента

      for (int j = i + 1; j < array.Length; j++) // ищем максимальный элемент
      {
         if(array[j] < array[minPosition]) minPosition = j;
      }

      int temporary = array[i]; // простой обмен двух переменных местами
      array[i] = array[minPosition];
      array[minPosition] = temporary;
   }
}
PrintArray(arr);
SelectionSort(arr);
PrintArray(arr);