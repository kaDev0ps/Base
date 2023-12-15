// Программа выводит первые N чисел Фибонначи
//  3 -> 011


// Решение
int Number = input("Введите число: ");
int[] fiboNumbers = new int[Number];
if(fiboNumbers.Length > 0) // задаем первые числа в массив
{
     fiboNumbers[0] = 0;
} 
if(fiboNumbers.Length > 1)
{
     fiboNumbers[1] = 1;
}

for(int i = 2; i < fiboNumbers.Length; i++) 
{
     fiboNumbers[i] = fiboNumbers[i - 1] + fiboNumbers[i - 2];
}
PrintArray(fiboNumbers);

int input (string output)
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