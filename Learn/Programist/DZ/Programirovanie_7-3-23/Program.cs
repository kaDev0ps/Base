// Программа принимает число X и выдает таблицу кубов числа от 1 до X
// 5 -> 1 8 27 64 125
// Методы
int InputIn (string output) // метод для ввода числа
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}
int Trio (int index) // метод для получения квадрата числа
{
     int Trio = index * index * index;
     return Trio;
}
//Решение
     int number = InputIn("Введите число: ");
     int index = 1;

     while(index <= number)
     {
          int numberTrio = Trio(index);
          Console.Write(numberTrio + ", ");
          index++;
     }
