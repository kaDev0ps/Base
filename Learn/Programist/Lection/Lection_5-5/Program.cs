// получаем массив строк

using System.Linq;

string text = "(1,2) (3,4) (5,6) (7,8)"
     .Replace("(", "") // меняем скобу на пустое место
     .Replace(")", "") // меняем скобу на пустое место
     ;
var data = text.Split(" ") // пробел будет служить разделителем
     .Select(item => item.Split(',')) // выбираем элемент с запятой
     .Select(e => (x: int.Parse(e[0]), y: int.Parse(e[1]))) // делаем разбор строки первая координата это первое число, а вторая это второе
     .Where(e => e.x % 2 == 0) // первая координата четная
     .Select(point => (point.x * 10, point.y)) // разделяем числа на координаты
     .ToArray(); // получим что каждый элемент это массив
     for(int i=0; i < data.Length; i++)
     {
          Console.WriteLine(data[i]);
          /*for (int k=0; k < data[i].Length; k++)
          {
               Console.WriteLine(data[i][k]);
          }*/
     }
