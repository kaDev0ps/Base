// Программа меняет эллементы массива на противоположные

int[] numbers = new int[12];
int sumPositiv = 0;
int sumNegativ = 0;

FillArray(numbers); // вызываем метод заполняющий массив
PrintArray(numbers); // выводим на экран массив

     for(int i = 0; i < numbers.Length; i++) // проходимся по всему массиву
     {
          if(numbers[i] > 0) // если положительный элемент, то мы складываем его в переменную +
          {
               sumPositiv += numbers[i];
          }
          else
          {
               sumNegativ += numbers[i];
          }
     }
Console.WriteLine($"Сумма положительных равна {sumPositiv}, сумма отрицательных равна {sumNegativ}");


void FillArray(int[] array) // метод заполняющий наш массив
{
     for(int i = 0; i < array.Length; i++) // проходимся по всему массиву
     {
          array[i] = new Random().Next(-9, 10); // заполняем каждый элемент случайной цифрой от -9 до 9
     }
}

// метод, который будет выводить массив

void PrintArray(int[] array) // метод заполняющий наш массив
{
     for(int i = 0; i < array.Length; i++) // проходимся по всему массиву
     {
          Console.Write(array[i] + " "); // вывод на терминал
     }
     Console.WriteLine();
}
