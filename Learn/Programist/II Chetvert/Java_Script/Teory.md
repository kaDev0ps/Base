# Урок 1. JS Язык программирования

## Область видимости:

let (внутри скобок)

var (для всего сайта) никогда не используем

const (не меняемое значение)

---

## Типы данных

- string - текстовый тип
- number
- boolean - Отвечает на вопрос ДА или НЕТ (0 и 1)
- undefined - создал переменную, но не присвоил значение
- object - для хранения набора данных (student.firstNane student.lastName)
  - array (массив)
  - function (функция)
- null - пришли данные не такие, которые мы хотели
- symbol - для создания уникальных ключей для свойств объектов.
- BigInt - в конце дбавляется n

## Массивы

Массив - упорядоченный список эллементов. У каждого элемента есть свой индекс. Индекс начинается с 0.

> let listOfBooks = [`Stiv Dzhon "DZ Zelobit"`, `Stiv Dzhon2 "DZ Zelobit2"`];
> console.log(listOfBooks[0]);
> console.log(listOfBooks.length); // длинна массива

## Функция

Объект имеющий свои свойства и методы. Сохраняется в переменную и передается как аргумент в другую функцию.

## Числовые значения

- Nan (ошибка вычисления)
- Infinity, -Infinity

## Команды

- console.log(typeof Click); - отправляем данные в консоль

  let string = "Hello";
  string = `Hello`;

  let result = 8;
  let literal = `Результат: ${result}`;
  let emptyString = "";

## Правила хорошего тона

- script добавляем перед закрывающим тегом body
- первая переменная с маленькой буквы и должен быть понятен смысл

# Урок 2. Основы JS

== не строгое сравнение
=== сравнение типов данных
!== не равно

инкремент - увеличение операнда на установленный шаг
декремент - обратное инкременту операция ф-- или ф-1
Конкатенация - сложение строк

let age = 20;
age++

alert - всплывающая подсказка.
prompt() - строка текста для ввода пользователя
default - строка содержащая текст по умолчанию
confirm(); - да нет.

alert(`Hello`);
prompt(`Сколько лет`, 30);
confirm();

## Операторы

if, if-else

if (Условие) {
//Действие
} else {
//Действие
}

Тернарный оператор - операция возвращающая либо второй либо третий операнд

<!--
    let userAnswer = prompt (`Вопрос`);
    if (userAnswer ==='') {
        console.log('Пусто');
    } else {
        if (userAnswer === 'елка') {
            alert('Good');
        } else {
            alert('BAD');
        }
    }
-->

    let userAnswer = prompt (`Вопрос`);
    if (userAnswer ==='') {
        console.log('Пусто');
    } else if (userAnswer === 'елка') {
            alert('Good');
        } else {
            alert('BAD');
        }

## Тернальный оператор

(условие) ? ДА : НЕТ
(userAnswer === 'елка') ? alert('Good') : alert('BAD')

alert((2>3)?'sale':'NO sale');

alert((userAnswer.toLowerCase() === 'елка') ? 'Good' : 'BAD')

<button onclick="alert('tovar');">Button</button>

1 - Когда выводите расчитываемые значения пользователю - округляйте. Попробуйте ввести 21, посмотрите на результат, мы получим число 69.80000000000001. JavaScript не всегда может "правильно" посчитать некоторые операции, все из-за особенностей рассчетов, которые используются в языке (такое во многих языках встречается). Чтобы округлить число, можно использовать метод Math.round, например:
Math.round(num) округлит до целого числа по правилам математики.
Если мы хотим чтобы округление было до определенного знака после запятой, например, хотим 2 знака после запятой иметь (если они не нули), то можем пойти на хитрость, сначала умножить на 100 число, округлить его и потом поделить на 100:

const PI = 3.14159265359;
console.log(Math.round(PI \* 100) / 100); // 3.14
При умножении на 100, мы получаем число 314.159265359, затем округляем по правилам математики, остается 314, после чего делим на 100 и получаем 3.14.
Либо, если мы выводим пользователю результат в строку, то можно превратить число в строку округлив до определенного количества знаков дробной части, например num.toFixed(2) округлит до 2-х чисел в дробной части числа значение переменной num.
Так как обе переменные не меняются в программе, лучше создать их как const.
Если вам надо собрать строку с переменными, то лучше использовать обратные кавычки, с ними собрать строку проще, чем приклеивать переменные через плюсы (в данном случае конечно не сложно но я на будущее).
2 - Переменные лучше создать с помощью const, т.к. они не меняются в программе.
Проще было бы сразу записать const admin = name;

# Урок 3. Функции JS

- Именнованные - по которым мы ее вызываем
  Декларативный подход
  function getMaximum(numbers){// контент}
- Анонимные
  function (result){// код обработки результата}

* Функциональные выражения.
  const getMaximum = function(numbers) {return mumber +1 ;//код}
  стиль ES6
  const getMaximum = (numbers) => { return mumber +1 ; //код}

  var f = function(number) {
  return number+1;
  }

  let f = (number) => {
  return number+1;
  }

  let f = number => {
  return number+1;
  }
  let f = () => {
  return number+1;
  }

  ## Параметры

const а = (parametr=5) =>{
console.log(pram);
}
f();//5
f(10); //10 - аргумент
}

const sum1 = function(a,b){
return a+b;
}

можем сократить
const f = (a,b) => a+b;

## Область видимости

> Глобальная
> Локальная (внутри функции)

# Урок 4. Массивы и циклы JS

> Инициализация переменных
> Условие вывода

- Бесконечное выполнение
- Считаем действия

### Бесконечность

До тех пор
while (condition) {
//Тело цикла
}

### Выполнить, потом проверить

do while - проверка операторов. Спрашивать до тех пор пока значение не будет удовлетворять нашим условиям

### Счетчик или последовательность

for (инициализация; проверка; инкремент)
{
инструкция
}

## Массивы

Упорядоченные списки элементов.

push - принимает один или несколько аргументов в массив

массивы создаем через константу
const arr =[];
arr.push(1);
console.log(arr);

const arr =[1,2,`hello`];

pop - метод извлечения последнего элемента массивы с его удалением из массива
shift - метод извлечения последнего элемента массивы с его удалением из массива
slice - метод копирования и отрезания массивов

## Урок 5. Объекты в JS

обращение к объектам.
console.log(car.model);
car.power=350;

## Урок 6. Введение в DOM (Объектная модель документа)

Древовидная структура созданная в браузере

## Урок 7. Работа в DOM

- Управление стилями

const divElement = document.createElement('div')
const paragraphElement = document.createElement('p')
divElement.appendChild(paragraphElement)
paragraphElement.style.color = 'white'
paragraphElement.style.backgroundColor = 'black'
paragraphElement.style.padding = '10px'
paragraphElement.style.width = '250px'
paragraphElement.style.textAlign = 'center'

## Урок 8. События в JavaScript

Список возможных событий в DOM очень длинен. Вот лишь некоторые примеры:
● click — нажатие кнопки мыши
● touch— касание
● load— загрузка
● drag— перетаскивание
● change— изменение
● input— ввод
● error — ошибка
● resize — изменение размера
● contextmenu — открытие меню
● submit— отправка формы

- Делегирование событий

При делегировании используется свойство event.target для доступа к целевому элементу
события. Свойство event.currentTarget будет указывать на тот элемент, на который мы
делегировали обработчик:
