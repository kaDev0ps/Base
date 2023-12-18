// 2 - этап метод заполнения массив числами
// void - не возвращает значения
void FillArray(int[] collection)
{
              int length = collection.Length; // определяем длинну массива
              int index = 0;
              while (index < length)
              {
                 collection[index] = new Random().Next(1,10);           
                 index++;           
              }
}
// 3 - этап метод вывод массива
void PrintArray(int[] col)
{
              int count = col.Length; // обозначаем количество элементов
              int position = 0; // обозначаем начало массива не через index
              while (position < count)
              {
                            Console.WriteLine(col[position]);
                            position++;
              }
}


int IndexOf(int[] collection, int find) // приходит массив
{
              int count = collection.Length; // значение равно количесту элементов массива
              // int position = 0; по умолчанию
              int position = -1; // если элемента нет
              int index = 0;
              while (index < count)
              {
                            if(collection[index] == find) // если index совпал с find то сохраняем позицию в переменную position]
                            {
                                          position = index;
                                          break; // остановился после нахождения первого значения
                            }
                            index++;
              }
              return position; // возвращаем новое значение позиции элемента
}

int[] array = new int [10]; // 1- этап создаем массив из 10 элементов (по умолчанию там все 000)

FillArray(array); // 4 - этап передаем наименование массива
array[4] = 4; // принудительно в массив ставим 4 на 4 индекс
PrintArray(array); // выводим массив
Console.WriteLine(); // Вывод пустой строки, чтобы быть уверенным, что это не относится к массиву

int pos = IndexOf(array, 15); // будет хранится наш массив и ищем 4
Console.WriteLine(pos); // после отработки метода покажет ПОСЛЕДНЕЕ значение pos