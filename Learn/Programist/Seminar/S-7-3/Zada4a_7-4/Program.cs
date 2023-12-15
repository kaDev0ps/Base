// Программа принимает число X и выдает квадраты числа от 1 до X
// 5 -> 1 4 9 16 25
// Методы
int InputIn (string output) // метод для ввода числа
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}
int Quadro (int index) // метод для получения квадрата числа
{
     int Quadro = index * index;
     return Quadro;
}
//Решение
     int number = InputIn("Введите число: ");
     int index = 1;

     while(index <= number)
     {
          int numberQuadro = Quadro(index);
          Console.Write(numberQuadro + ", ");
          index++;
     }
