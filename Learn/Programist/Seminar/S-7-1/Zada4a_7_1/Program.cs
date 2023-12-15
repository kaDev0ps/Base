// Программа, которая принимает число и выдает его квадрат
// ReadLine - ждет пока пользователь введет строку и сохранит ее в переменную
// int a = Convert.ToInt32 Console.ReadLine() - конвертирование из string в int
// 
// int a = a newRandom().Next(1, 10); // рандомное значение от 1 до 9
Console.Write("Write number:");
int a = Convert.ToInt32(Console.ReadLine());
int quadro = a * a;
Console.WriteLine("Квадрат числа " + a + " равен " + quadro);
