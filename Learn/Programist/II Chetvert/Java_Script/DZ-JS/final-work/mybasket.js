'use strict';

const basket = {};


const products = getProductsObject();


const basketEl = document.querySelector('.mybasket');
const basketListEl = document.querySelector('.mybasketList');
const basketCounterEl = document.querySelector('.cartIconWrap span');

const basketTotalValueEl = document.querySelector('.basketTotalValue');

document.querySelector('.cartIconWrap').addEventListener('click', () => {
    basketEl.classList.toggle('hidden');
});

document.querySelector('.featuredItems').addEventListener('click', event => {

    const addToCartEl = event.target.closest('.addToCart');

    if (!addToCartEl) {
        return;
    }

    addToCart(addToCartEl.dataset.id);
});


basketEl.addEventListener('click', event => {

    if (!event.target.classList.contains('productRemove')) {
        return;
    }

    removeFromCart(event.target.closest('.mybasketRow').dataset.id);
});


function addToCart(id) {

    if (!(id in basket)) {

        basket[id] = {
            id: id,
            name: products[id].name,
            price: products[id].price,
            count: 0
        };
    }

    basket[id].count++;

    renderBasketContent();
}


function removeFromCart(id) {

    if (basket[id].count <= 1) {
        delete basket[id];
    } else {
        basket[id].count--;
    }

    renderBasketContent();
}

function renderBasketContent() {
    basketListEl.innerHTML = Object.values(basket).reduce(
        (acc, product) => acc + getBasketProductMarkup(product),
        "",
    );
    basketCounterEl.textContent = getTotalBasketCount().toString();
    basketTotalValueEl.textContent = getTotalBasketPrice().toFixed(2);
}

function getTotalBasketCount() {
    return Object.values(basket).reduce((acc, product) => acc + product.count, 0);
}

function getTotalBasketPrice() {
    return Object
        .values(basket)
        .reduce((acc, product) => acc + product.price * product.count, 0);
}

function getBasketProductMarkup(product) {
    return `
    <div class="mybasketRow" data-id="${product.id}">
      <div>${product.name}</div>
      <div>
        <span class="productCount">${product.count}</span> шт.
      </div>
      <div>${product.price} ₽</div>
      <div>
        <span class="productTotalRow">
          ${(product.price * product.count).toFixed(2)} ₽
        </span>
      </div>
      <div><button class="productRemove">-</button></div>
    </div>
  `;
}
