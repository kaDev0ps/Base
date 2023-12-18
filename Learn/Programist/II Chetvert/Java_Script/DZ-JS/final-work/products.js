'use strict';

const featuredItemsEl = document.querySelector('.featuredItems');

featuredItemsEl.innerHTML = getProductsList()
    .map(product => renderProduct(product)).join('');

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
