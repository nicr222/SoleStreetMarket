

let toggle = document.querySelector('.toggle');
let navigation = document.querySelector('.navigation');
let main = document.querySelector('.main');

toggle.onclick = function () {
    navigation.classList.toggle('active');
    main.classList.toggle('active');
}
// add hovered class in selected list item
let list = document.querySelectorAll('.navigation li');
function activatelink() {
    list.forEach((item) =>
        item.classList.remove('hovered'));
    this.classList.add('hovered');
}
list.forEach((item) =>
    item.addEventListener('mouseover', activatelink));


$(function () {
    $('#order').click();

    $('#order').click(function () {
        $('.recentItem').show();
        $('.recentItem').not('.order').hide();
        return false;
    });

    $('#product').click(function () {
        $('.recentItem').show();
        $('.recentItem').not('.product').hide();
        return false;
    });

    $('#employee').click(function () {
        $('.recentItem').show();
        $('.recentItem').not('.employee').hide();
        return false;
    });

    $('#department').click(function () {
        $('.recentItem').show();
        $('.recentItem').not('.department').hide();
        return false;
    });

    $('#messages').click(function () {
        $('.recentItem').show();
        $('.recentItem').not('.messages').hide();
        return false;
    });

});
