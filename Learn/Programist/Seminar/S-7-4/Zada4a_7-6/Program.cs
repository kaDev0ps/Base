// Программа принимает число N и выдает количество цифр в числе
// 732 -> 3
// Методы
int input (string output)
{
     Console.Write(output);
     return Convert.ToInt32(Console.ReadLine());
}
// Решение
int N = input("Введите число N:");
int i = 0; // глобальный счетчик
/*
 while(N > 0) // пока наше значение меньше чем число
{
     N /= 10; // result = result / 10;
     i++;
}

     Console.Write(i); // программа считает сколько раз число можно разделить на 10 пока оно не станет < 0
*/
for (; N > 0; i++) // цикл for выполняется пока наше число больше 0
{
     N /= 10;
}
     Console.Write(i);