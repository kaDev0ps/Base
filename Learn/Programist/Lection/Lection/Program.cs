/* Методы
Функции бывают 2ух видов:
* void - ничего не принимают и ничего не возвращают
* void - те который принимаютт и ничего не возвращают
* ничего не принимают и что=то возвращают
* что то принимают и что-то возвращают

//Пример 1 вида
void Method1 ()
{
     Console.Write("Автор ****");
}

// Вызываются такие методы таким образом
Method1();

//Пример 2 вида
void method2 (string msg)
{
     Console.Write(msg);
}
method2(msg: "Text methoda2");
*/
//Пример 2 вида 
/*
void method21 (string msg, int count)
{
     int i = 0;
     while(i < count)
     {
          Console.Write(msg + ", ");
          i++;
     }
}
// method21("Text", 4);
method21(msg: "Text", count: 6);
*/
// Пример 3 метода
// возвращает и не принимает
/*
int Method3()
{
   return DateTime.Now.Year;
}

int year = Method3();
Console.Write(year);

// Пример 4 вида
string Method4(int count, string text) // используем строковой метод. Берем параметр с и компануем count раз
{
   int i = 0;
   string result = ""; // сюда записываем результат

   while (i < count)
   {
      result = result + text;
      i++;
   }
   return result;
}
string res = Method4(10, "Z");
Console.Write(res);
*/
// Пример 4 вида
string Method4(int count, string text) // используем строковой метод. Берем параметр с и компануем count раз
{
   string result = "";
   for (int i = 0; i < count; i++)
   {
      result = result + text;
   }
   return result;
}
string res = Method4(10, "Z");
Console.Write(res);
