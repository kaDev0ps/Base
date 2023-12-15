// Показывать дни недели в зависимости от числа
Console.Write("Write number:");
int number = Convert.ToInt32(Console.ReadLine());
if (number == 1) Console.WriteLine("Monday");
if (number == 2) Console.WriteLine("Tuesday");
if (number == 3) Console.WriteLine("Thurthsday");
if (number == 4) Console.WriteLine("Wednesday");
if (number == 5) Console.WriteLine("Friday");
if (number == 6) Console.WriteLine("Saturday");
if (number == 7) Console.WriteLine("Sunday");
if (number > 7)Console.WriteLine("I don't know this day");   
