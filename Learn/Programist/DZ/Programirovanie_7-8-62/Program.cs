// Заполните спирально массив 4*4
/*
1 2 3 4
12 13 14 5
11 16 15 6
10 9 8 7
*/
int size = 4;
int[,] array = new int[size, size];

int value = 1;
int i = 0;
int j = 0;
while(value <= size * size) // идем циклом по массиву пока не дойдем до конечного элемента
{
     array[i, j] = value; // вставляем первое значение
     if(i <=j + 1 && i + j < size - 1)
     ++j;
     else if (i < j && i + j >= size - 1)
     ++i;
     else if (i >= j && i + j > size - 1)
     --j;
     else --i;
     ++value;
}
for(int k = 0; k < array.GetLength(0); k++)
{
     for (int l = 0; l < array.GetLength(1); l++)
     {
          Console.Write(array[k,l] + " ");
     }
     Console.WriteLine();
}