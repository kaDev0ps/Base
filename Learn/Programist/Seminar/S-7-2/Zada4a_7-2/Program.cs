// Если несколько условий то используются логические операторы
// Если условие ИЛИ то || если И то &&
int number = 70;
if (number > 9 && number < 100)
{
              Console.WriteLine(number);
}

// Программа выводит случайное трехзначное число и удаляет вторую цифру этого числа
int randomNumber = new Random().Next(99, 1000);
int firstDigit = randomNumber / 100; // узнаем первую цифру
int thirdDigit = randomNumber % 10; // узнаем третью через остаток
Console.WriteLine($"Число {randomNumber} первая и последняя цифра дают число {firstDigit}{thirdDigit}");