
// Программа меняет элементы массива на противоположные

int size = InputInt("Введите размер массива: ");
int[] numbers = new int[size];
int minus = -1;

FillArray(numbers); // вызываем метод заполняющий массив
PrintArray(numbers); // выводим на экран массив

     for(int i = 0; i < numbers.Length; i++) // проходимся по всему массиву
     {
               numbers[i] *= minus;
     }
PrintArray(numbers); // выводим на экран массив

int InputInt(string output)
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}

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