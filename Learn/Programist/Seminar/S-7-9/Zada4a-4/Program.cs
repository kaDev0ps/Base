// Программа принимает два числа А и В и возводит число А в целую степень И с помощью рекурсии
// А=3 И=5 -> 243
/*
Console.Clear();
Console.Write("Введите число: ");
int a = Convert.ToInt32(Console.ReadLine()); // делаем конвертацию в число
Console.Write("Введите степень: ");
int b = Convert.ToInt32(Console.ReadLine()); // делаем конвертацию в число

Console.WriteLine(Step(a, b));

int Step(int a, int b)
{
     if (b == 0)
     {
          return 1;
     }

     else
     {
          return a * Step(a, b - 1); // опускаемся до 0 степени, а потом выходим
     }
}
*/
// Второй способ

Console.Clear();
Console.Write("Введите число: ");
int a = int.Parse(Console.ReadLine()); // делаем конвертацию в число
Console.Write("Введите степень: ");
int b = int.Parse(Console.ReadLine()); // делаем конвертацию в число

Console.WriteLine($"Сумма чисел от {a} до {b} = {Step(a, b)}");

int Step(int a, int step) // step мы вычисляем! В рекурсии всегда надо знать конец и выход!
{
     if (step == 0)
     return 1;
     return Step(a, step - 1) * a; // опускаемся до 0 степени, а потом выходим
}