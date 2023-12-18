// 
int a1 = 12;
int b1 = 22;
int c1 = 15;
int a2 = 12;
int b2 = 22;
int c2 = 15;
int a3 = 18;
int b3 = 122;
int c3 = 124;

/* Решение стандарт

int max = a1; // задаем максимум аргументу
if (b1 > max) max = b1;
if (c1 > max) max = c1;
if (a2 > max) max = a1;
if (b2 > max) max = b2;
if (c2 > max) max = c2;
if (a3 > max) max = a3;
if (b3 > max) max = b3;
if (c3 > max) max = c3;
Console.WriteLine(max);

*/
// Решение через функцию 

int Max(int arg1, int arg2, int arg3) // делим на тройку числа для сравнения
{
int result = arg1; // хранится значение максимума
if(arg2 > result) result = arg2; // сравниваем 1ые числа со вторыми
if(arg3 > result) result = arg3; // сравниваем 3ые числа с максимальными
return result; // выводим максимум
}

/* Решение задачи широкое 

int max1 = Max(a1, b1, c1); // ищем максимальное значение из 1 массива
int max2 = Max(a2, b2, c2); // ищем максимальное значение из 2 массива
int max3 = Max(a3, b3, c3); // ищем максимальное значение из 3 массива
int max = Max(max1, max2, max3); // ищем максимальное значение из 3 массива
*/
// Решение компактное
int max = Max(
              Max(a1, b1, c1), 
              Max(a2, b2, c2), 
              Max(a3, b3, c3)); // ищем максимальное значение из 3 массива

Console.WriteLine(max);