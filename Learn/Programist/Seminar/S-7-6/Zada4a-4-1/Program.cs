
// Программа выводит первые N чисел Фибонначи
//  3 -> 011


// Решение
int Number = input("Введите число: ");
string result = string.Empty;
int first = 0;
int second = 1;

if(Number > 0) // задаем первые числа в массив
{
     result += first + " ";
} 
if(Number > 1) // задаем первые числа в массив
{
     result += second + " ";
} 

for(int i = 2; i < Number; i++) 
{
     int next = first + second;
     result += next + " ";
     first = second;
     second = next;
}
Console.Write(result);

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