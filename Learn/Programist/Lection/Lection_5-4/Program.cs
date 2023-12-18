/*
String[,] table = new string[2, 5]; //двумерный массив строками и столбцами
// table[0, 0] table[0,1] table[0,2]
//table[1,0] table[1,1] table[1,2] 
table[1, 2] = "слово";
for (int rows = 0; rows < 2; rows++)
{
     for (int columns = 0; columns < 5; columns++) // столбцы
     {
          Console.WriteLine($"-{table[rows, columns]}-");
     }
}
//////////////////////////////
int[,] matrix = new int[3, 4];
for (int i = 0; i < matrix.GetLength(0); i++) // matrix.GetLength(0) - количество строк
{
     for (int j = 0; j < matrix.GetLength(1); j++) // matrix.GetLength(1) - количество столбцов
     {
     Console.Write($"{matrix[i, j]} ");
     }
Console.WriteLine();
}
///////////////////////////////
*/
// int[,] matrix = new int[3, 4];

void PrintArray(int[,] matr) // принимает двумерную таблицу чисел и передает на экран
{
     for (int i = 0; i < matr.GetLength(0); i++) // matrix.GetLength(0) - количество строк
     {
          for (int j = 0; j < matr.GetLength(1); j++) // matrix.GetLength(1) - количество столбцов
          {
               Console.Write($"{matr[i, j]} ");
          }
     Console.WriteLine();
     }
}
void FillArray(int[,] matr) // заполняем матрицу
{
     for (int i = 0; i < matr.GetLength(0); i++) // matrix.GetLength(0) - количество строк
     {
          for (int j = 0; j < matr.GetLength(1); j++) // matrix.GetLength(1) - количество столбцов
          {
               matr[i, j] = new Random().Next(1,10);
          }

     }
}

int[,] matrix = new int[3, 4];

PrintArray(matrix);
FillArray(matrix);
Console.WriteLine();
PrintArray(matrix);