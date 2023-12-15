// Программа принимает координаты двух точек и показывает расстояние между ними в 3D
// применить теорему пифагора = гипотенуза = корень из суммы квадратов катетов
/* А (3,6,8) и В(2,1,-7) Длинна будет = 15,84 */
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
int Az = InputIn("Введите координату Z для первого числа: ");
int Bx = InputIn("Введите координату Х для второго числа: ");
int By = InputIn("Введите координату Y для второго числа: ");
int Bz = InputIn("Введите координату Z для второго числа: ");

int AxBx = Ax - Bx;
int AyBy = Ay - By;
int AzBz = Az - Bz;

int QuadroAxBx = Quadro(AxBx);
int QuadroAyBy = Quadro(AyBy);
int QuadroAzBz = Quadro(AzBz);

int SumQuadroAxBxAyByAzBz = QuadroAxBx + QuadroAyBy + QuadroAzBz;
//Функция
double sqrt = Math.Sqrt(SumQuadroAxBxAyByAzBz); // метод получения корня из числа
// Ответ
Console.WriteLine($"Расстояние между точками {sqrt}");