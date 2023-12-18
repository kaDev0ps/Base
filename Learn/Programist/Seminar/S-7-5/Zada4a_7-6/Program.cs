// Массив заполненный случайными положительными числами. Программа показывает количество четных чисел в массиве
int size = Input("Введите размер массива: ");
int[] numbers = new int[size];
int result = 0;

FillArray(numbers); // вызываем метод заполняющий массив
PrintArray(numbers); // выводим на экран массив


for (int i = 0; i < numbers.Length; i++) // проходим весь заданный массив до середины
{
     if(numbers[i] % 2 == 0)
     {
     result += + 1 ; // если элемент четный то добавляем +1
     }
}
Console.Write($"Четных чисел {result}");


int Input (string output)
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}

void FillArray(int[] array) // метод заполняющий наш массив
{
     for(int i = 0; i < array.Length; i++) // проходимся по всему массиву
     {
          array[i] = new Random().Next(1, 10); // заполняем каждый элемент случайной цифрой
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