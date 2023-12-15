// Вычислит функцию Аккермана с помощью рекурсии.
// m=2, n=3 будет А(m,n) = 9
// m=3, n=2 будет А(m,n) = 29
/*
А(m,n) = 
n + 1 если m = 0
А(m-1,1) если m>0, n=0
A(m-1, A(m,n-1)) m>0, n>0
*/
int m = 3;
int n = 2;
Console.Clear();
Console.Write($"При m = {m}, n = {n} А(m,n) = {recursion(m, n)}"); // вызов рекурсивной функции
int recursion(int m, int n)
{
     // Базовый случай
     if (m == 0)
     {
          return n + 1;
     } // Шаг рекурсии / рекурсивное условие
     else if (n == 0 && m > 0)
     {
          return recursion(m - 1, 1);
     } // Шаг рекурсии / рекурсивное условие
     else
     {
          return recursion(m - 1, recursion(m, n - 1));
     }
}






