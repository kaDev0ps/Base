// Программа принимает на вход координаты точки Х и У, которые не равны 0 и выдает номер четверти плоскости в которой они находятся
//
while(true) // повторяем пока программа не выполнится
{
              Console.Clear(); // очищаем значения после цикла
              int x = InputIn(" Write X: ");
              int y = InputIn(" Write Y: ");
              int InputIn (string output) 
                            {
                                 Console.Write(output);
                                 return Convert.ToInt32(Console.ReadLine());
                            }
              if (x == 0 || y == 0)
                            {
                                 Console.WriteLine("X and Y не доджны быть = 0");
                                 return; // выходим из функции
                            }

              else if (x > 0 && y > 0)
                            {
                                 Console.WriteLine("1 четверть");
                            }
              else if (x < 0 && y > 0)
                            {
                                 Console.WriteLine("2 четверть");
                            }
              else if (x < 0 && y < 0)
                            {
                                 Console.WriteLine("3 четверть");
                            }
              else if (x > 0 && y < 0)
                            {
                                 Console.WriteLine("4 четверть");
                            }           
              break; // прервать функцию
}
/*
int firstQuadro = Quadro(firstNumber);
int secondQuadro = Quadro(secondNumber);

// это мы использовали функцию
// метод возведния в квадрат числа

int Quadro(int firstNumber)
{
int Quadro = firstNumber * firstNumber;
return Quadro;
}
*/