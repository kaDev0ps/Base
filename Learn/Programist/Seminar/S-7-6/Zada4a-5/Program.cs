// Программа создает копию массива с помощью элементного копирования

// Программа переворачивает первый и последний элемент массива, второй и предпоследний и т.д.

int size = Input("Введите размер массива: ");
int[] numbers = new int[size];

FillArray(numbers); // вызываем метод заполняющий массив
int[] newNumbers = copyArray(numbers);

// numbers[1] = 123;
// newNumbers[2] = 654;
PrintArray(numbers); // выводим на экран массив
PrintArray(newNumbers); // выводим на экран массив
int[] copyArray(int[] array) 
{
     int[] copyArray = new int[array.Length];
     for(int i = 0; i < copyArray.Length; i++)
     {
          copyArray[i] = array[i];
     }
     return copyArray;
}


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