// Программа принимает число X и выдает сумму чисел от 1 до X
// 7 -> 28
// Методы
Console.Write("Введите число:");
int a = Convert.ToInt32(Console.ReadLine());
int sum = 0; // наш будущий ответ
int count = 1; // счет начинаем с 1

/* while(count <= a) // пока наше значение меньше чем число
{
     sum += count; // sum = sum + count;
     count ++;
}

     Console.Write(sum); */

for (int i = 1; i <= a; i++) // цикл for создаст переменную i и сравнит ее с переменной a. Если условия будет выполняться то цикл сработает
{
     sum += i;
}
     Console.Write(sum);