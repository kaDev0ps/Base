// Заменить пробелы -
// маленькие буквы к заменить к
// большие С заменить с

string text = "— Я думаю сказал князь, цлыбаясь, — что, "
            + "ежели бы ваС  поСлали в место нашего Винцегорца";

// string s = "qwerty"
//             012345
// s[3] // r

string Replace(string text , char oldValue, char newValue)
{
   string result = String.Empty;
   int length = text.Length;
   for (int i = 0; i < length; i++)
   {
      if(text[i] == oldValue) result = result + $"{newValue}";
      else result = result + $"{text[i]}";
   }
   return result;
}
string newtext = Replace(text, ' ', '|');
Console.WriteLine(newtext);
Console.WriteLine();

newtext = Replace(newtext, 'к', 'К');
Console.WriteLine(newtext);
Console.WriteLine();

newtext = Replace(newtext, 'С', 'с');
Console.WriteLine(newtext);