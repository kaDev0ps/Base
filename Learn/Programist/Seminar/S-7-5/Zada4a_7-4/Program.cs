// Программа проверяет есть ли заданное число в массиве
// return - выходим из метода или из программы
// break - выходим с цикла
int size = InputInt("Введите размер массива: ");
int[] numbers = new int[size];

int number = InputInt("Введите число: ");
bool isTrue = false;

FillArray(numbers); // вызываем метод заполняющий массив
PrintArray(numbers); // выводим на экран массив

     for(int i = 0; i < numbers.Length; i++) // проходимся по всему массиву
     {
               if(numbers[i] == number)
               {
                    isTrue = true;
                    break; // выходим из цикла
               }
     }
if(isTrue)
Console.Write($"Число {number} ЕСТЬ в массиве");
else
Console.Write($"Числа {number} НЕТ в массиве");

int InputInt(string output)
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}

void FillArray(int[] array) // метод заполняющий наш массив
{
     for(int i = 0; i < array.Length; i++) // проходимся по всему массиву
     {
          array[i] = new Random().Next(1, 10); // заполняем каждый элемент случайной цифрой от -9 до 9
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