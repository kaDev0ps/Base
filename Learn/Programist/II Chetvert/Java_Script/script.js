let a = 1;
if (a > 1) {
  let f = a + 2;
  console.log(f);
}

console.log("Text from script.js");

/* Создаем пееменную num со значением 123 и вывести в консоль
создать переменную а и присвоить ей значение 5
переопределить значение а на 7
создать переменную ь записать сумму чисел 1,2,3
вывести в консоль содержимое ь */
// создать комментарии в коде

const num = 123;
let s = 5;
s = 7;
let b = 1 + 2 + 3;

console.log("num = " + num);
console.log(`a = ${s}`);
console.log(`a = ${b}`);
// console.log(`is comment`);
/* console.log("Comment"); */

/* Создаем пееменную С со значением 10 и переменную d со значением 2
логичное название
переменная суммы С+D
Переменная разности С D
переменная произведения с d
переменная частного c d
Вывести в консоль переменные
Сложить все переменные */

let c = 10;
let d = 2;
let summcd = c + d;
let razncd = c - d;
let proizvedenircd = c * d;
let delcd = c / d;
let total = summcd + razncd + proizvedenircd + delcd;
console.log(summcd);
console.log(razncd);
console.log(proizvedenircd);
console.log(delcd);
console.log(total);

/* а = 1,5
ь = 0,75
найти сумму и вывести на экран
создать переменную и вывести ее на экран с противоположным знаком */

let a2 = 1.5;
let b2 = 0.75;
let summa2b2 = a2 + b2;
console.log(summa2b2);
let c2 = 75;
console.log(c2 * -1);

/* создать 2 переменный с текстом и в третьей переменной вывести сразу в одну строку это 2 слова */

let a3 = "Hello";
let b3 = "World";
let summa3b3 = a3 + b3;
console.log(summa3b3);
console.log(`${a3} ${b3}`);
console.log(a3 + " " + b3);

let userAnswer = prompt(`Вопрос`);
alert(userAnswer.toLowerCase() === "елка" ? "Good" : "BAD");

// let pass = prompt("Password");
// pass = Number(pass);
// pass =String(pass); // преобразование
// if (pass === 123) {
//   alert("Good");
// } else {
//   alert("Bad");
// }

let pass = Number(prompt("Password"));
alert(pass === 123 ? "Good" : "Bad");
