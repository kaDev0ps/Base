
const formControl2 = document.querySelector('#txtNewPassword');
const formControl3 = document.querySelector('#txtConfirmPassword');
formControl3.addEventListener('mouseout', function (e) {
    e.preventDefault();
    if (formControl2.value != formControl3.value) {
        formControl2.style.borderColor = 'red';
        formControl3.style.borderColor = 'red';
        return false;
    } else {
        formControl2.style.borderColor = 'green';
        formControl3.style.borderColor = 'green';
        return false;
    }
});


const button = document.querySelector('.buttonPurple');
button.addEventListener('click', function (f) {
    f.preventDefault();
    const formControl2 = document.querySelector('#txtNewPassword');
    const formControl3 = document.querySelector('#txtConfirmPassword');

    if (formControl2.value != formControl3.value || formControl2.value == "") {
        formControl2.style.borderColor = 'red';
        formControl3.style.borderColor = 'red';
        return false;
    } else {
        formControl2.style.borderColor = 'green';
        formControl3.style.borderColor = 'green';
        alert('Спасибо за вашу заявку!')
    }
});
