// программа преобразовывает десятичное число в двоичное

// Методы
int input (string output)
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}
// Решение
int Number = input("Введите число: ");
string result = ""; 

while(Number > 0)
{
     result = Number % 2 + result;
     Number /= 2;
}
Console.Write(result);