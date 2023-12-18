// Программа проверяет есть ли заданное число в массиве
// return - выходим из метода или из программы
// break - выходим с цикла
int[] numbers = new int[10];

Console.Write("Введите число:");
int number = Convert.ToInt32(Console.ReadLine());

FillArray(numbers); // вызываем метод заполняющий массив
PrintArray(numbers); // выводим на экран массив

     for(int i = 0; i < numbers.Length; i++) // проходимся по всему массиву
     {
               if(numbers[i] == number)
               {
                    Console.Write($"Число {number} присутствует в массиве");
                    return; // выходим из программы, чтобы не добрались до следующих условий
               }
     }
Console.Write($"Числа {number} НЕТ в массиве");

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