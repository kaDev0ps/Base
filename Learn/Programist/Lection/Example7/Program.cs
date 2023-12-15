Console.Clear();
//Console.SetCursorPosition (10, 4);
//Console.WriteLine("+");
int xa = 40, ya = 1,
    xb = 1, yb = 30,
    xc = 80, yc = 30;

Console.SetCursorPosition(xa, ya); // Определяем первую точку
Console.WriteLine("+");
Console.SetCursorPosition(xb, yb); // Определяем вторую точку
Console.WriteLine("+");
Console.SetCursorPosition(xc, yc); // Определяем третью точку
Console.WriteLine("+");

int x = xa, y = xb; // первая случайная точка
int count = 0; // количество нахождения и деления отрезков пополам
while(count < 2000) // ставим ограниение на количество выполнения скрипта
{
      int what = new Random().Next(0, 3); // [0;3) 0 1 2 выбираем лдну из 3 точек  
      if(what == 0) // проверка 0
      {
           x = (x + xa) / 2; // кладем х в середину отрезка
           y = (y + ya) / 2; // кладем у в середину отрезка
      }  
      if(what == 1) // проверка 0
      {
           x = (x + xb) / 2; // кладем х в середину отрезка
           y = (y + yb) / 2; // кладем у в середину отрезка 
      }  
       if(what == 2) // проверка 0
      {
           x = (x + xc) / 2; // кладем х в середину отрезка
           y = (y + yc) / 2; // кладем у в середину отрезка 
      }  
      Console.SetCursorPosition(x, y); // устанавливаем курсор в центр
      Console.WriteLine("+");      // ставим плюсик
      count = count + 1; // count++ или count+=1
}