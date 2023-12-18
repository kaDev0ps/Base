// Задайте одномерный массив из 123 случайных чисел. Найти количество элементов массива в отрезке от 10 до 99
int[] array = new int[123];
int min = 10;
int max = 99;
int sum = 0;

FillArray(array); // вызываем метод заполняющий массив
PrintArray(array); // выводим на экран массив

for(int i = 0; i < array.Length; i++) // проходимся по всему массиву
{
     if(array[i] >= min && array[i] <= max)
     {
          sum += 1;
     }
}

Console.Write($"Количество элементов от 10 до 99 в массиве {sum}");

void FillArray(int[] array) // метод заполняющий наш массив
{
     for(int i = 0; i < array.Length; i++) // проходимся по всему массиву
     {
          array[i] = new Random().Next(1, 500); // заполняем каждый элемент случайной цифрой
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