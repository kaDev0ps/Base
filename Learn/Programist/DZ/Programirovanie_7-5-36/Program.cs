// Массив заполненный случайными числами. Программа показывает сумму элементов стоящих на нечетных позициях

int size = Input("Введите размер массива: ");
int[] numbers = new int[size];
int result = 0;
int nechet = 2;
FillArray(numbers); // вызываем метод заполняющий массив
PrintArray(numbers); // выводим на экран массив


for (int i = 1; i < numbers.Length; i = i + nechet) 
{
     result += numbers[i]; 
}
Console.Write($"Сумма нечетных элементов массива равна {result}");


int Input (string output)
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}

void FillArray(int[] array) // метод заполняющий наш массив
{
     for(int i = 0; i < array.Length; i++) // проходимся по всему массиву
     {
          array[i] = new Random().Next(-10, 10); // заполняем каждый элемент случайной цифрой
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