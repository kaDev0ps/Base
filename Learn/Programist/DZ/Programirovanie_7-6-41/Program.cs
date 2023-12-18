/* // Пользователь вводит с клавиатуры M чисел. Посчитайте, сколько чисел больше 0 ввёл пользователь.

Console.Write("Введите числа через пробел: ");
int[] array = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
int result = 0;
 
for (int i = 0; i < array.Length; i++)
{
    if (array[i] > 0)
    {
        result++;
    }
}
 
Console.WriteLine($"Кол-во положительных чисел: {result}");
*/
/* Второе решение */
// сперва считаем размер массива по количеству запятых + 1
// проходим по каждому симбволу и записываем в отдельную строку (23), пока не наткнемся на запятую
// как только наткнемся на запятую, то полученные цифры конвертируем в число и записываем в массив
// потом затираем полученную строку и идем дальше записывать цифры
// получаем массив чисел из строки
// "" (тип string) применяются для строки
// '' (тип char) применяются для симбвола

Console.WriteLine("Введите числа через запятую: ");
string numberString = Console.ReadLine() ?? ""; // проверка на NULL , чтобы не ругалась консоль
int[] numbers = ParseArray(numberString, ','); // пользователи вводят числа через запятую
PrintArray(numbers);

int count = 0;
for(int i = 0; i < numbers.Length; i++)
{
    if (numbers[i] > 0)
        count++;
}
Console.WriteLine(count);

int[] ParseArray(string input, char separator)
{
    int[] numbers = new int[GetCountNumbersInString(input)]; // 1 посчитает по запятым количество чисел в строке (сколько запятых +1) и создадим массив
    string subString = String.Empty; // вводим пустую строчную переменную
    int numbersIndex = 0; // будем считать числа
    for(int i = 0; i < input.Length; i++) // проходим по массиву конвертируем все числа кроме запятых и последнего
    {
        if(input[i] == separator) // если элемент равен запятой или separator то записываем его в массив
        {
            numbers[numbersIndex++] = Convert.ToInt32(subString);
            subString = String.Empty;
        }
        else
        {
        subString += input[i];
        }
    }
    numbers[numbersIndex] = Convert.ToInt32(subString); // добавляем в массив последнее число после цикла
    return numbers; // возвращаем полученный массив
}

int GetCountNumbersInString(string numbers)
{
    int countNumbers = 1;
    for(int i = 0; i < numbers.Length; i++)
    {
        if(numberString[i] == ',')
            countNumbers++;
    }
    return countNumbers;
}

void PrintArray(int[] array) // метод заполняющий наш массив
{
     for(int i = 0; i < array.Length; i++) // проходимся по всему массиву
     {
          Console.Write(array[i] + " "); // вывод на терминал
     }
     Console.WriteLine();
}


