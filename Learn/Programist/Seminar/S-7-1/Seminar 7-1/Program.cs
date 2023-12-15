// 4 типа переменных
// int number = 0;
// double
// string - пример string name = "denis"; Console.WriteLine("Denis");
// bool - любой результат сравнения
// count = +1  Это то же самое что count +=1; count++;
// if (result == result2) Условия сравнения
// != не равно
// while () {} действия выполняется до определенного значнения
// Console.WriteLine() - одна строка

int firstFriendSpead = 2,
secondFriendSpead = 3,
dogSpeed = 5,
count = 0;

double distance = 10000, time = 0;
int directionDog = 1; // 1 - от первого ко второму, 2 - от второго к первоу
while (distance > 2)
{
              if (directionDog == 1)
              {
                            time = distance / (secondFriendSpead + dogSpeed);
                            directionDog = 2;
              }
              else
              {
                            time = distance / (secondFriendSpead + dogSpeed);
                            directionDog = 1;
              }
              distance = distance - (firstFriendSpead + secondFriendSpead) * time;
              count++;
}
Console.WriteLine("Собака пробежала между друзьями " + count + " раз");