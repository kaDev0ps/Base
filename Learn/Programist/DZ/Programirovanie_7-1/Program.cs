// программа,которая на вход принимает два числа и выдаёт, какое число большее, а какое меньшее. 
Console.Write("Write number A:");
int a = Convert.ToInt32(Console.ReadLine()); // число A
Console.Write("Write number B:");
int b = Convert.ToInt32(Console.ReadLine()); // число B
if (a > b)
              {
                   Console.WriteLine("Number A more than number B");         
              }
else          
              {
                    Console.WriteLine("Number B more than number A");          
              }