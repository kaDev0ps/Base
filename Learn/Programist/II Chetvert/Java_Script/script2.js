// функия вывода значений в консоль
function helloName(name) {
  console.log(name);
}
helloName(`Alex`); //используем функцию и выводим значение

//////////////////////---nfn---//////////////////////////////
let age = Number(prompt('Сколько лет?'));
const lvlUpAge = () => {
  return age + 5;
};
console.log(`Через 5 лет вам будет ${lvlUpAge()}`);

/////////////////////////сокращаем//////////////////////////////

let age2 = Number(prompt('Сколько лет2?'));

const lvlUpAge2 = () => age2 + 5;
console.log(`Через 5 лет вам будет ${lvlUpAge2()}`);

/////////////////////////вызов функции//////////////////////////////

function hello() {
  console.log('Hello');
}
hello();

/////////////////////////вызов функции суммы//////////////////////////////
const sum = (a, b) => {
  return a + b;
};
const result = sum(2, 5);
console.log(result);

/////////////////////////упрощаем вызов функции суммы//////////////////////////////

const sum2 = (a2, b2) => {
  return a2 + b2;
};
console.log(sum2(2, 5));

/////////////////////////если функция возвращает, то применяем стрелочную/////////////////////////
// money - то условная пееменная, вместо которой будет подставляться та, которая будет в параметрах
const salary = (money) => {
  money = money * 0.87; // сумма без налогов
  return money * 0.75;
};
const userMoney = Number(prompt('Сколько у тебя денег?'));

let moneyMonth = salary(userMoney);
console.log(`На расходы ${salary(userMoney)}`);
console.log(`На еду потрачу ${moneyMonth * 0.3}`);

///////////// кнопка ////////////
function buy() {
  alert('Поздравляем с покупкой');
  alert('Товар в корзине');
}

///////////// загадки ////////////
let userAnswer = prompt('Зима?');
if (userAnswer === 'да') {
  console.log('Верно');
} else {
  console.log('Ошибся');
}

function askQuestio(answer, question) {
  const userAnswer = prompt(question);
  if (userAnswer.toLowerCase() === answer) {
    console.log('Верно');
  } else {
    console.log('Ошибся');
  }
}

///////////// вызов по кнопке ////////////
function puzzle() {
  askQuestio('нет', 'Лето?');
  askQuestio('нет', 'Лето2?');
}
