int Max(int arg1, int arg2, int arg3) // делим на тройку числа для сравнения
{
int result = arg1; // хранится значение максимума
if(arg2 > result) result = arg2; // сравниваем 1ые числа со вторыми
if(arg3 > result) result = arg3; // сравниваем 3ые числа с максимальными
return result; // выводим максимум
}


/* Решение задачи через массив */
// индексы      0   1   2   3   4   5   6   7   8
int[] array = {15, 24, 33, 24, 551, 64, 73, 82, 93}; // создаем массив
 // array[0] = 12; // нулевому элементу массива присваиваем значение 12
// Console,WriteLine(array[4]); - вывод элемента с индексом 4
int result = Max(
    Max(array[0], array[1], array[2]), // вписываем по тройке индексы
    Max(array[3], array[4], array[5]),
    Max(array[6], array[7], array[8])
);

Console.WriteLine(result); // показываем максимум