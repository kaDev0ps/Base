// Программа показывает доступный диапазон при вводе номера четверти
//
while(true) // повторяем пока программа не выполнится
{
              Console.Clear(); // очищаем значения после цикла
              int number = InputIn("Введите номер четверти: ");
              int InputIn (string output) // метод для ввода числа
                            {
                                 Console.Write(output);
                                 return Convert.ToInt32(Console.ReadLine());
                            }
              if (number > 4)
                            {
                                 Console.WriteLine("Четверть должны быть < 4");
                                 return; // выходим из функции
                            }
           if (number > 0)         
           {        
              if (number == 1)
                            {
                                 Console.WriteLine("x и у должны быть > 0");
                            }
              else if (number == 2)
                            {
                                 Console.WriteLine("x < 0 && y > 0");
                            }
              else if (number == 3)
                            {
                                 Console.WriteLine("x < 0 && y < 0");
                            }
              else if (number == 4)
                            {
                                 Console.WriteLine("x > 0 && y < 0");
                            }     
           }
           else
                            {
                                 Console.WriteLine("Четверть не должна быть отрицательной");
                            }   

              break; // прервать функцию
}
/*
              int firstQuadro = Quadro(firstNumber);
              int secondQuadro = Quadro(secondNumber);
// это мы использовали функцию
// давайте используем метод возведния в квадрат числа
int Quadro(int firstNumber)
{
              int Quadro = firstNumber * firstNumber;
              return Quadro;
}
Console.WriteLine($"Квадрат первого числа {firstNumber} второго {secondNumber}");*/