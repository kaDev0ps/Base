// Массив вещественных чисел. Найти разницу между минимальным и максимальным значением массива

int size = GetSizeArray("Введите длинну массива: ");
double[] userArray = GetArray(size); // 1 устанавливаем размер двумерного массива

PrintArray(userArray);
double maxNumber = userArray[0],
       minNumber = userArray[0];

for (int index = 0; index < userArray.Length; index++)
{
    if (userArray[index] > maxNumber)
    {
        maxNumber = userArray[index];
    }
    else if (userArray[index] < minNumber)
    {
        minNumber = userArray[index];
    }
}
Console.WriteLine($"{maxNumber} - {minNumber} = {maxNumber - minNumber}");

int GetSizeArray(string question) // вводимо число задает размер
{
    Console.Write(question);
    return Convert.ToInt32(Console.ReadLine());
}

double[] GetArray(int size)
{
    double[] array = new double[size];
    int wholePart = 0,
        fractionalPart = 0;
    for (int i = 0; i < size; i++)
    {
        wholePart = new Random().Next(-100, 100);
        fractionalPart = new Random().Next(0, int.MaxValue);
        array[i] = Convert.ToDouble(wholePart + "," + fractionalPart);
        // Можно:
        // array[i] = Convert.ToDouble(wholePart)/Convert.ToDouble(fractionalPart);
        // или
        // array[i] = wholePart + new Random().NextDouble();
    }
    return array;
}

void PrintArray(double[] userArray)
{
    string result = "[";
    for (int i = 0; i < userArray.Length; i++)
    {
        if (i == userArray.Length - 1)
        {
            result += userArray[i] + "]";
        }
        else
        {
            result += userArray[i] + ", ";
        }
    }
    Console.WriteLine();
    Console.WriteLine(result);
    Console.WriteLine();
}

/*
int size = Input("Введите размер массива: ");
int[] numbers = new int[size];

int minNumber = 0;
int maxNumber = 0;
int result = 0;

FillArray(numbers); // вызываем метод заполняющий массив
PrintArray(numbers); // выводим на экран массив


for (int i = 0; i < numbers.Length; i++) // ищем минимальное и максимальные элементы массива
{
     if(numbers[i] > maxNumber)
     {
          maxNumber = numbers[i];
     }
     else if(numbers[i] < minNumber)
     {
          minNumber = numbers[i];
     }
}

result = maxNumber - minNumber;

Console.Write($"Разница между максимальным ({maxNumber}) и минимальным ({minNumber}) значением равна {result}");


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
*/