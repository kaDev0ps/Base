// Программа принимает координаты двух точек и показывает расстояние между ними
// применить теорему пифагора = гипотенуза = корень из суммы квадратов катетов
/* А (5,3) и В(4,2) Длинна будет = V(5-4)^2+(3-2)^2*/
// Методы
int InputIn (string output) // метод для ввода числа
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}
int Quadro (int number) // метод для получения квадрата числа
{
     int Quadro = number * number;
     return Quadro;
}
// Решение
int Ax = InputIn("Введите координату Х для первого числа: ");
int Ay = InputIn("Введите координату Y для первого числа: ");
int Bx = InputIn("Введите координату Х для второго числа: ");
int By = InputIn("Введите координату Y для второго числа: ");

int AxBx = Ax - Bx;
int AyBy = Ay - By;

int QuadroAxBx = Quadro(AxBx);
int QuadroAyBy = Quadro(AyBy);

int SumQuadroAxBxAyBy = QuadroAxBx + QuadroAyBy;
//Функция
double sqrt = Math.Sqrt(SumQuadroAxBxAyBy); // метод получения корня из числа
// Ответ
Console.WriteLine($"Расстояние между точками {sqrt}");
