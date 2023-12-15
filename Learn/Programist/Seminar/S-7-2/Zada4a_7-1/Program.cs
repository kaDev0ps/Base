// Программа выводит случайное число из отрезка [10. 99] и показывает наибольшую цифру числа
// 78 это 8
//57 это 7
int randomNumber = new Random().Next(10, 100);
int firstDigit = randomNumber / 10; // узнаем первую цифру
int secondDigit = randomNumber % 10; // узнаем вторую через остаток

int max = firstDigit; // по умолчанию 1 число максимальное

if (secondDigit > max)
{
              max = secondDigit;
}
// Console.WriteLine("Число = " + randomNumber + ", максимальное из двух чисел это " + max);
// вывод через интерпалляцию
Console.WriteLine($"Число = {randomNumber}, максимальное из двух чисел это {max}");