// индексы      0   1   2   3   4   5   6   7   8
int[] array = {15, 24, 33, 82, 51, 64, 73, 82, 93}; // создаем массив
int n = array.Length; // длинна массива = количеству элементов
int index = 0; // начальный индекс
int find = 82; //ищем нужный индекс
while (index < n)
{
              if(array[index] == find)
              {
                            Console.WriteLine(index);
                            // если несколько одинаковых элементов, то чтоб показались не все а только первый
                            break;
              }
              //index = index + 1;
              index++;
}