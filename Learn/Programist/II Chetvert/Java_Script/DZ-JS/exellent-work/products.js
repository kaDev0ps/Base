'use strict';

// Находим элемент, куда надо вставить элементы.
const featuredItemsEl = document.querySelector('.featuredItems');
// Вставляем все данные из массива, предварительно каждые данные превратив в
// html-разметки товаров, складывая все разметки в одну строку.
featuredItemsEl.innerHTML = getProductsList()
  .map(product => renderProduct(product)).join('');

/**
 * Фугнкция создает html товара.
 * @param {object} data - объект с информацией о товаре.
 * @returns разметку товара.
 */
function renderProduct(data) {
  return `
    <div class="featuredItem">
        <div class="featuredImgWrap">
            <img src="${data.img}" alt="${data.name}">
            <div class="featuredImgDark">
                <button class="addToCart" data-id="${data.id}">
                    <img src="images/cart.svg" alt="">
                    Add to Cart
                </button>
            </div>
        </div>
        <div class="featuredData">
            <div class="featuredName">
                ${data.name}
            </div>
            <div class="featuredText">
                ${data.description}
            </div>
            <div class="featuredPrice">
                ${data.price} руб.
            </div>
        </div>
    </div>
  `;
}
