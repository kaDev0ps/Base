// Программа принимает 2 числа и возводит число А в натуральную степень В
// 3, 5 -> 243
// Методы
int InputIn (string output) // метод для ввода числа
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}
//Решение
int A = InputIn("Введите число A: ");
int B = InputIn("Введите число B: ");
int stepen = Convert.ToInt32(Math.Pow(A, B));

Console.Write($"{A} в степени {B} = {stepen}");
