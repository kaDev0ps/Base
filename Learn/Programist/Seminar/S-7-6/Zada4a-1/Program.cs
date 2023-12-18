// Программа переворачивает первый и последний элемент массива, второй и предпоследний и т.д.

int size = Input("Введите размер массива: ");
int[] numbers = new int[size];

FillArray(numbers); // вызываем метод заполняющий массив
PrintArray(numbers); // выводим на экран массив

for (int i = 0; i < numbers.Length / 2; i++) // циклом до середины массива и меняем первый с последним
{
     int temp = numbers[i];
     numbers[i] = numbers[numbers.Length -1 - i];
     numbers[numbers.Length -1 - i] = temp;
}
PrintArray(numbers); // выводим на экран массив


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