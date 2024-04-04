function removeFromCart(productId, price) {
    $.ajax({
        url: '/Cart/RemoveFromCart',
        type: 'POST',
        data: { id: productId, quantity: 1 },
        success: function (data) {
            console.log(data);
            if (data.Success) {
                updateQuantity(productId);
                updateTotalQuantity();
                updateTotalMoney(price);
                if (getCurrentQuantity(productId) === 0) {
                    hideProduct(productId);
                }
                if (getCurrentTotalQuantity() === 0) {
                    reloadPage();
                }
            } else {
                reloadPage();
            }
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}

function updateQuantity(productId) {
    var quantityElement = $('.quantity[data-product-id="' + productId + '"]');
    var currentQuantity = parseInt(quantityElement.text().replace('Số lượng: ', ''));
    quantityElement.text('Số lượng: ' + (currentQuantity - 1));
}

function updateTotalQuantity() {
    var totalQuantity = parseInt($('#totalQuantity').text());
    $('#totalQuantity').text(totalQuantity - 1);
}

function updateTotalMoney(price) {
    var money = parseInt($('#totalMoney').text().replace(/\D/g, ''));
    $('#totalMoney').text((money - parseInt(price)).toLocaleString('en-US'));
}

function getCurrentQuantity(productId) {
    return parseInt($('.quantity[data-product-id="' + productId + '"]').text().replace('Số lượng: ', ''));
}

function hideProduct(productId) {
    $("#product_" + productId).hide();
}

function getCurrentTotalQuantity() {
    return parseInt($('#totalQuantity').text());
}

function reloadPage() {
    location.reload();
}