// Напишите программу, которая найдёт точку пересечения двух прямых, заданных уравнениями y = k1 * x + b1, y = k2 * x + b2; значения b1, k1, b2 и k2 задаются пользователем.

// b1 = 2, k1 = 5, b2 = 4, k2 = 9 -> (-0,5; -0,5)
double k1 = coordinats("Введите k1: ");
double b1 = coordinats("Введите b1: ");
double k2 = coordinats("Введите k2: ");
double b2 = coordinats("Введите b2: ");

if(k1 == k2)
{
    Console.WriteLine("Прямые парралельны");
    return; // выходим из программы
}
double x = (b2 - b1) / (k1 - k2); // Находим х с уровнения (k1*x+b1=k2*x+b2) => k1*x - k2*x=b2-b1 => x(k1-k2)=b2-b1 => x=(b2-b1)/(k1-k2)
double y = k1 * x + b1; 
 
Console.WriteLine($"Пересечение в точке: ({x};{y})");

double coordinats(string output)
{
    Console.Write(output);
    return Convert.ToDouble(Console.ReadLine());
}
