// Рекурсия - функция, которая вызывает сама себя
// Самое важное написать условия выхода из рекурсии
// Задача собрать строку  с числами от А до Б, a<=b
Console.WriteLine("Задача собрать строку  с числами от А до Б, a<=b");

string Number(int a, int b)
{
     string result = String.Empty;
     for (int i = a; i <= b; i++)
     {
          result += $"{i} ";
     }
     return result;
}

string NumberRec(int a, int b)
{
     if (a <= b) return $"{a} " + NumberRec(a + 1, b);
     else return String.Empty;
}
// доп задача вызов на 1 меньше
string NumberRec3(int a, int b)
{
     if (a < b) return $"{a} " + NumberRec3(a + 1, b);
     else return $"{a - 1} ";
}

// Задача собрать строку  с числами от А до Б, a>=b

string Number2(int a, int b)
{
     string result = String.Empty;
     for (int i = b; i >= a; i--)
     {
          result += $"{i} ";
     }
     return result;
}

string NumberRec2(int a, int b)
{
     if (a <= b) return NumberRec2(a + 1, b) + $"{a} ";
     else return String.Empty;
}
Console.WriteLine(Number(1, 10));
Console.WriteLine(NumberRec(1, 10));
Console.WriteLine("Задача собрать строку  с числами от А до Б, a<=b на -1");
Console.WriteLine(NumberRec3(1, 10));
Console.WriteLine("Задача собрать строку  с числами от А до Б, a>=b");
Console.WriteLine(Number2(1, 10));
Console.WriteLine(NumberRec2(1, 10));

// Сумма чисел от 1 до n
Console.WriteLine("Сумма чисел от 1 до n");
int SumFor(int n)
{
     int result = 0;
     for (int i = 1; i <= n; i++) result += i;
     return result;
}
int SumRec(int n)
{
     if (n == 0) return 0; // условия выхода
     else return n + SumRec(n - 1); // рекурсивный вызов функции на 1 меньше
}

Console.WriteLine(SumFor(10)); // 55
Console.WriteLine(SumRec(10)); // 55

// Факториал числа
Console.WriteLine("Факториал числа");

int Factorial(int n)
{
     int result = 1;
     for (int i = 1; i <= n; i++) result *= i;
     return result;
}
int FactorialRec(int n)
{
     if (n == 1) return 1; // условия выхода
     else return n * FactorialRec(n - 1);
}
Console.WriteLine(Factorial(10)); //3628800
Console.WriteLine(FactorialRec(10)); //3628800

// Возведение числа а в степень n
Console.WriteLine("Возведение числа а в степень n");

int Power(int a, int n)
{
     int result = 1;
     for (int i = 1; i <= n; i++) result *= a; // перебор счетчика от 1 до n с записыванием результата
     return result;
}

int PowerRec(int a, int n)
{
     // если n = 0 то возвращаем 1
     if (n == 0) return 1; // условия выхода
     else return PowerRec(a, n - 1) * a; // выражение а умножается на функцию, где a - это число, n - степень числа
     // запись в 1 строку
     // return n == 0 ? 1 : PowerRec(a, n - 1) * a;
}
int PowerRecMath(int a, int n)
{
     if (n == 0) return 1; // условия выхода
     else if (n % 2 == 0) return PowerRecMath(a * a, n / 2);
     else return PowerRecMath(a, n - 1) * a;
}
Console.WriteLine(Power(2, 10)); //1024
Console.WriteLine(PowerRec(2, 10)); //1024
Console.WriteLine(PowerRecMath(2, 10)); //1024

// Есть 4 биквы. Надо пказать все слова состоящие из t букв
Console.WriteLine("Есть 4 биквы. Надо пказать все слова состоящие из t букв");
char[] s = {'а','с','и','в'}; // алфавит в массиве s
int count = s.Length; // запоминаем количество эллементов
int n = 1;
for (int i = 0; i < count; i++) 
{
     for (int j = 0; j < count; j++) 
     {
          for (int k = 0; k < count; k++) 
          {
               for (int l = 0; l < count; l++) 
               {
                    Console.WriteLine($"{n++, -5}{s[i]}{s[j]}{s[k]}{s[l]}"); // цикл, который перебирает однобуквенные слова. Для 2 буквенных слов нужен цикл в цикле и т.д (-5 это отступ)

               }
          }
     }
}
// Есть 4 биквы. Надо пказать все слова состоящие из t букв с помощью рекурсии
Console.WriteLine("Есть 4 биквы. Надо пказать все слова состоящие из t букв с помощью рекурсии");

void FindWords(string alphabet, char[] word, int length = 0) // метод состоящий из строкового параметра, массив из букв, которое будет составлять новое слово, и длинна нашего слова
{
     if (length == word.Length) // условия выхода = длинна слова совпала с текущей длинной
     {
          Console.WriteLine($"{n++} {new String(word)}"); return; // мы просто показываем это слово
     }
     for (int i = 0; i < alphabet.Length; i++) // в противном случае запускаем цикл по всем эллементам нашего массива, чтобы собрать слово
     {
          word[length] = alphabet[i];
          FindWords(alphabet, word, length + 1);
     }
}
FindWords("асив", new char[4]); // ожидаем получить все двухбуквенные слова
