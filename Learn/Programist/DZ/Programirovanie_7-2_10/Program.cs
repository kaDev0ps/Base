// Напишите программу, которая на вход принимает трехзначноечисло, а на выходе показывает вторую цифру этого числа.
int randomNumber = new Random().Next(100, 999);
int firstDigit = randomNumber / 100; // узнаем первую цифру
int thirdDigit = randomNumber % 10; // узнаем третью через остаток
int secondDigit = (randomNumber - firstDigit * 100 - thirdDigit) / 10;
Console.WriteLine($"Число {randomNumber} вторая цифра {secondDigit}");