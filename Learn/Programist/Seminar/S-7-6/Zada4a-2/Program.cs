// Программа принимает 3 числа и проверяет, может ли существовать треугольник со сторонами такой длинны
//Теорема: каждая сторона треугольника меньше суммы двух других сторон

// Методы
int input (string output)
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}
// Решение
int firstNumber = input("Введите первое число: ");
int secondNumber = input("Введите второе число: ");
int thirdNumber = input("Введите третье число: ");

if(firstNumber < secondNumber + thirdNumber
&& secondNumber < firstNumber + thirdNumber
&& thirdNumber < firstNumber + secondNumber)
{
     Console.Write("Треугольник возможен");
}
else
{
     Console.Write("Треугольник НЕ возможен");
}
