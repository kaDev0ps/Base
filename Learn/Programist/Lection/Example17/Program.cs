// f(1) = 1
// f(2) = 1
// f(n) = f(n-1) + f(n-2)
// Рекурсия вызов самой себя
double Fibonacci(int n) // каждое следующее равно сумме предыдущих
{
     if(n == 1 || n==2) return 1;
     else return Fibonacci(n-1) + Fibonacci(n-2);
}

for (int i = 1; i < 40; i++)
{
     Console.WriteLine($"f({i}) = {Fibonacci(i)}");
}
